using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WAConverter
{
    public partial class XamlConverter
    {
        Dictionary<string, string> namespaceMapping;

        Dictionary<string, object> objectMappings;
        string rootNamespace;
        string rootTypeName;

        bool silent = true;

        Type[] stopList = new Type[] { typeof(UserControl) };
        Dictionary<Type, string> typesMapping;
        Dictionary<Type, string> editorValueSuffixMapping;

        public string BaseType { get; internal set; }

        public XamlConverter()
        {
            this.objectMappings = ConvertToDictionary(propertiesMapping);
        }

        #region Start conversion

        public void StartConvert(Control target)
        {
            try
            {
                if (target.IsDesignerHosted())
                    return;

                var doc = new XmlDocument();
                rootTypeName = target.IsDesignerHosted() ? target.Name : target.GetType().Name;
                var fullName = target.GetType().FullName;
                rootNamespace = fullName.Substring(0, fullName.LastIndexOf('.'));
                CreateAppFromTemplate(rootNamespace);
                var rootClassName = target.IsDesignerHosted() ? target.Name : target.GetType().FullName;
                InitTypeMapping();
                InitNamespaceMapping();
                var isUserControl = target is UserControl;
                var currentNode = CreateElement(doc, isUserControl ? "UserControl" : "Window");
                foreach (var key in namespaceMapping.Keys)
                    currentNode.SetAttribute(
                        "xmlns" + (key.Length > 0 ? ":" + key : string.Empty),
                        namespaceMapping[key]);
                SetAttribute(currentNode, "x:Class", rootClassName);
                if (!isUserControl)
                {
                    SetAttribute(currentNode, "Width", target.Width.ToString());
                    SetAttribute(currentNode, "Height", target.Height.ToString());
                    SetLocalizableProperty(currentNode, "Title", target.GetType().GetProperty("Text"), "Text", target);
                }
                doc.AppendChild(currentNode);

                AvaloniaFieldInfo fi;
                XmlElement scrollViewerNode;
                CreateNewNode(new RootScrollViewer(), -1, -1, 1, currentNode, doc, out fi, out scrollViewerNode);

                XmlElement newCurrentNode;
                CreateNewNode(target, -1, -1, 1, scrollViewerNode, doc, out fi, out newCurrentNode);

                radioButtonContainers.Clear();
                ConvertControl(target, newCurrentNode, doc);
                var dir = Directory.GetCurrentDirectory() + "\\Converted";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                var fileFullName = dir + "\\" + rootTypeName + ".axaml";
                doc.Save(fileFullName);
                var txt = File.ReadAllText(fileFullName);
                txt = txt.Replace("x:=", "\n\tx:=").Replace("xmlns", "\n\txmlns");
                File.WriteAllText(fileFullName, txt);
                GenerateCodeFile(target, rootTypeName, rootNamespace);
                GenerateViewModelFile(target, rootTypeName, rootNamespace);
                AddResXFiles(rootTypeName);
                ProcessImages(fileFullName, target);
            }
            catch (Exception e)
            {
                ShowError("Error converting: ", e);
            }
        }

        #endregion

        #region Generate markup

        public void ConvertControl(Control parent, XmlNode convertedParent, XmlDocument doc)
        {
            var parentNode = convertedParent;
            if (parent is GroupBox)
            {
                AvaloniaFieldInfo fi;
                XmlElement stackPanelNode;
                CreateNewNode(new UserControl(), -1, -1, 1, convertedParent, doc, out fi, out stackPanelNode);
                parentNode = stackPanelNode;
            }

            var row = -1;
            var column = -1;

            var controls = parent.Controls.Cast<Control>();

            if(parent is DataGridView dataGridView)
            {
                AddDataGridColumns(dataGridView.Columns, parentNode, doc);
                return;
            }
            if (parent is TableLayoutPanel panel)
                controls = controls.OrderBy(c => panel.GetColumn(c));

            foreach (var control in controls)
            {
                if (parent is TableLayoutPanel tablePanel)
                {
                    row = tablePanel.GetRow(control);
                    column = tablePanel.GetColumn(control);
                }
                if(control is DataGridView)
                {
                    var panelNode = CreateElement(doc, "Panel");
                    AssignProperiesCore(panelNode, control);
                    parentNode.AppendChild(panelNode);
                    ConvertControlCore(control, row, column, 1, panelNode, doc);
                } else
                    ConvertControlCore(control, row, column, 1, parentNode, doc);
            }
        }

        private bool ConvertControlCore(Control control, int row, int column, int colSpan, XmlNode convertedParent, XmlDocument doc)
        {
            AvaloniaFieldInfo fi;
            XmlElement currentNode;
            if (ignoredControls.Contains(control.GetType().Name))
                return false;
            CreateNewNode(control, row, column, colSpan, convertedParent, doc, out fi, out currentNode);
            AssignProperiesCore(currentNode, control);
            if (!string.IsNullOrWhiteSpace(control.Name))
                SetAttribute(currentNode, "x:Name", control.Name);
            else if (control is Label)
                SetAttribute(currentNode, "Classes", "LayoutItem");

            if (ShouldAddEditorValue(control))
                SetAttribute(currentNode, "EditorValue", $"{{Binding {AddSuffixToEditorValuePropertyName(control, fi.PropertyName)}}}");

            if (ShouldAddFocusedItem(control))
                SetAttribute(currentNode, "FocusedItem", $"{{Binding {AddSuffixToEditorValuePropertyName(control, fi.PropertyName)}}}");

            if (ShouldAddItemsSource(control))
                SetAttribute(currentNode, "ItemsSource", $"{{Binding {fi.PropertyName + "ItemsSource"}}}");

            if (ShouldAddValue(control))
                SetAttribute(currentNode, "Value", $"{{Binding {fi.PropertyName + "Value"}}}");

            if (control is GroupBox)
                SetAttribute(currentNode, "Margin", "8");

            if (control is RadioButton radioButton)
                AddRadioButtonAttributes(currentNode, radioButton, fi);
            if (control is TableLayoutPanel tableLayoutPanel)
                AddTableLayoutPanelAttributes(currentNode, tableLayoutPanel, fi);

            SetMinWidth(currentNode, control);

            if (ShouldAddCommand(control))
                SetAttribute(currentNode, "Command", $"{{Binding {fi.CommandName}}}");
            if (control.Controls.Count > 0 && !stopList.Any(item => control.GetType().IsSubclassOf(item)))
                ConvertControl(control, currentNode, doc);
            return true;
        }

        private void AddRadioButtonAttributes(XmlElement currentNode, RadioButton radioButton, AvaloniaFieldInfo fi)
        {
            SetAttribute(currentNode, "IsChecked", $"{{Binding {AddSuffixToEditorValuePropertyName(radioButton, fi.PropertyName)}}}");
            var parent = radioButton.Parent;
            var radioButtonName = radioButton.Name;
            if (!radioButtonContainers.ContainsKey(parent) && !string.IsNullOrEmpty(radioButtonName))
                radioButtonContainers.Add(parent, radioButtonName + "Group");
            if (radioButtonContainers.ContainsKey(parent))
                SetAttribute(currentNode, "GroupName", radioButtonContainers[parent]);
        }

        private void SetMinWidth(XmlElement currentNode, Control control)
        {
            if (ShouldAddMinWidthAttribute(control))
                SetAttribute(currentNode, "MinWidth", control.Size.Width.ToString());
        }

        private void AddTableLayoutPanelAttributes(XmlElement currentNode, TableLayoutPanel layoutPanel, AvaloniaFieldInfo fi)
        {
            if (layoutPanel.ColumnCount == 0 || layoutPanel.RowCount == 0)
                return;
            var columnsString = "";
            var rowsString = "";
            foreach (var column in layoutPanel.ColumnStyles)
            {
                if (column is ColumnStyle columnStyle)
                {
                    switch (columnStyle.SizeType)
                    {
                        case SizeType.Absolute:
                            columnsString += $"{columnStyle.Width.ToString(CultureInfo.InvariantCulture)} ";
                            break;
                        case SizeType.Percent:
                            columnsString += $"{(columnStyle.Width / 100.0).ToString(CultureInfo.InvariantCulture)}* ";
                            break;
                        default:
                            columnsString += "Auto ";
                            break;
                    }
                }
            }
            foreach (var row in layoutPanel.RowStyles)
            {
                if (row is RowStyle rowStyle)
                {
                    switch (rowStyle.SizeType)
                    {
                        case SizeType.Absolute:
                            rowsString += $"{rowStyle.Height.ToString(CultureInfo.InvariantCulture)} ";
                            break;
                        case SizeType.Percent:
                            rowsString += $"{(rowStyle.Height / 100.0).ToString(CultureInfo.InvariantCulture)}* ";
                            break;
                        default:
                            rowsString += "Auto ";
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(columnsString))
                SetAttribute(currentNode, "ColumnDefinitions", columnsString.Remove(columnsString.Length - 1, 1));
            if (!string.IsNullOrEmpty(rowsString))
                SetAttribute(currentNode, "RowDefinitions", rowsString.Remove(rowsString.Length - 1, 1));
            SetAttribute(currentNode, "Margin", "8");
        }

        private void AddDataGridColumns(DataGridViewColumnCollection columns, XmlNode parent, XmlDocument doc)
        {
            foreach (DataGridViewColumn column in columns)
            {
                var node = CreateElement(doc, "mxdg:GridColumn");
                AssignProperiesCore(node, column);
                parent.AppendChild(node);
            }
        }

        #endregion

        #region Generate ViewModel

        private string GenerateFields(Control target)
        {
            var builder = new StringBuilder();
            foreach (var fi in Fields)
            {
                if (ShouldAddEditorValue(fi.Control) || ShouldAddFocusedItem(fi.Control))
                    builder.AppendLine($"\t[ObservableProperty] {GetEditorValuePropertyType(fi.Control)} {AddSuffixToEditorValuePropertyName(fi.Control, fi.FieldName)};");
                if (ShouldAddItemsSource(fi.Control))
                    builder.AppendLine($"\t[ObservableProperty] ObservableCollection<object> {fi.FieldName + "ItemsSource"};");
                if (ShouldAddValue(fi.Control))
                    builder.AppendLine($"\t[ObservableProperty] int {fi.FieldName + "Value"};");
                if (fi.Control is RadioButton)
                    builder.AppendLine($"\t[ObservableProperty] bool {fi.FieldName + "Checked"}{(((RadioButton)fi.Control).Checked ? " = true" : "")};");
            }
            if (target is Form)
                builder.AppendLine("\tpublic event Action RequestClose;");
            return builder.ToString();
        }

        private string GenerateMethods()
        {
            var builder = new StringBuilder();
            foreach (var fi in Fields)
            {
                if (ShouldAddCommand(fi.Control))
                {
                    builder.AppendLine($"\t[RelayCommand(CanExecute=nameof(CanExecute{fi.Name}))]");
                    builder.AppendLine($"\tvoid {fi.Name}(object parameter)");
                    builder.AppendLine("\t{");
                    if (fi.CommandName.StartsWith("ok") ||
                        fi.CommandName.StartsWith("cancel") ||
                        fi.CommandName.StartsWith("close"))
                        builder.AppendLine("\t\tRequestClose?.Invoke();");
                    builder.AppendLine("\t}");
                    builder.AppendLine($"\tbool CanExecute{fi.Name}(object parameter)\n\t{{");
                    builder.AppendLine("\t\treturn true;");
                    builder.AppendLine("\t}");
                }
            }
            return builder.ToString();
        }
        #endregion

        #region Resx file management

        private void AddResXFiles(string rootTypeName)
        {
            var dir = Directory.GetCurrentDirectory();
            while (!Directory.GetFiles(dir, "*.csproj", SearchOption.TopDirectoryOnly).Any() && dir != null)
                dir = Path.GetDirectoryName(dir);
            if (dir == null)
                return;
            var dirConv = Directory.GetCurrentDirectory() + "\\Converted";

            string[] files = Directory.GetFiles(dir, rootTypeName + "*.resx", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var copiedFile = dirConv + "\\" + Path.GetFileName(file);
                if (file.Equals(copiedFile))
                    continue;
                try
                {
                    if (File.Exists(copiedFile))
                    {
                        File.SetAttributes(copiedFile, FileAttributes.Normal);
                        File.Delete(copiedFile);
                    }
                    File.Copy(file, copiedFile, true);
                }
                catch
                {
                }
                ResXCleaner.CleanupFile(copiedFile);
            }
        }

        bool AssignValuePropertyFromResource(XmlElement node, string propertyName, object src, PropertyInfo pi)
        {
            var propertyOwnerName = (src as Control)?.Name;
            if ((pi.Name == "Text" || pi.Name == "Caption") && propertyOwnerName != null)
            {
                SetLocalizableProperty(node, propertyName, pi, propertyOwnerName, src);
                return true;
            }
            return false;
        }

        bool ShouldSetLocalizableProperty()
        {
            return false; //TODO detect is resx contains localizable value for a property
        }

        private void SetLocalizableProperty(XmlElement node, string propertyName, PropertyInfo pi, string propertyOwnerName, object instance)
        {
            string value = "";
            if (ShouldSetLocalizableProperty())
                value = $"{{{$"x:Static p:{rootTypeName}.{propertyOwnerName}_{pi.Name}"}}}";
            else
                value = (string)pi.GetValue(instance);
            SetAttribute(node, propertyName, value);
        }
        #endregion

        #region Utils

        private void ApplyRowAndColumn(XmlElement currentNode, int currentNodeRow, int currentNodeColumn, int colSpan)
        {
            if(currentNodeRow > 0)
                SetAttribute(currentNode, "Grid.Row", currentNodeRow.ToString());
            if(currentNodeColumn > 0)
                SetAttribute(currentNode, "Grid.Column", currentNodeColumn.ToString());
            if(colSpan > 1)
                SetAttribute(currentNode, "Grid.ColumnSpan", colSpan.ToString());
        }

        private void AssignCollectionProperties(XmlElement parentNode, string propTo, IEnumerable enumerable)
        {
            var collNode = CreateElement(parentNode.OwnerDocument, parentNode.Name + "." + propTo);
            parentNode.AppendChild(collNode);
            foreach(var item in enumerable)
            {
                var xmlItem = CreateObjectNode(collNode.OwnerDocument, item);
                if(xmlItem == null)
                    continue;
                AssignProperiesCore(xmlItem, item);
                collNode.AppendChild(xmlItem);
            }
        }

        private void AssignNestedObjectProperties(XmlElement parentNode, string propTo, object nestedObject)
        {
            var nestedNode = CreateElement(parentNode.OwnerDocument, parentNode.Name + "." + propTo);
            parentNode.AppendChild(nestedNode);

            var xmlItem = CreateObjectNode(nestedNode.OwnerDocument, nestedObject);
            if(xmlItem == null)
                return;
            nestedNode.AppendChild(xmlItem);

            AssignProperiesCore(xmlItem, nestedObject);
        }

        private void AssignProperiesCore(XmlElement node, object src)
        {
            Dictionary<string, string> mappings = GetPropertiesMappings(node.Name);
            if(mappings == null)
                return;
            foreach(var map in mappings)
            {
                var propFrom = map.Key;
                var propTo = map.Value;

                var propertyOwner = src;
                var pi = GetPropertyInfo(propFrom, ref propertyOwner);
                if(pi == null)
                    continue;
                var attr = pi.GetCustomAttribute<DesignerSerializationVisibilityAttribute>();

                if(pi.PropertyType.IsValueType || pi.PropertyType == typeof(string))
                {
                    if(!AssignValuePropertyFromResource(node, propTo, propertyOwner, pi))
                        AssignValueProperty(node, propTo, propertyOwner, pi);
                } 
                else if(pi.GetValue(propertyOwner) is IEnumerable)
                    AssignCollectionProperties(node, propTo, (IEnumerable)pi.GetValue(propertyOwner));
                else if(pi.GetValue(propertyOwner) is object)
                    AssignNestedObjectProperties(node, propTo, pi.GetValue(propertyOwner));
            }
        }

        private void AssignValueProperty(XmlElement node, string propertyName, object source, PropertyInfo pi)
        {
            DefaultValueAttribute att = pi.GetCustomAttribute<DefaultValueAttribute>();
            var value = pi.GetValue(source);
            if(att != null && object.Equals(att.Value, value))
                return;
            SetAttribute(node, propertyName, Convert.ToString(value));
        }

        private XmlElement CreateElement(XmlDocument doc, string nameWithPrefix)
        {
            string prefix, name;
            SplitName(nameWithPrefix, out prefix, out name);

            var currentNode = doc.CreateElement(nameWithPrefix, namespaceMapping[prefix]);
            return currentNode;
        }

        private XmlElement CreateObjectNode(XmlDocument doc, object item)
        {
            if(item == null)
                return null;
            var name = GetMappedType(item.GetType());
            if(name == null)
            {
                ShowError("Cannot map object: " + item.GetType().Name, null);
                return null;
            }
            return CreateElement(doc, name);
        }

        private string ExtractTypeName(string type)
        {
            string[] items = type.Split(':');
            return items.Length == 2 ? items[1] : items[0];
        }

        string GetMappedType(Type type)
        {
            var typeAndInterfaces = Enumerable.Concat(new[] { type, type.BaseType, type.BaseType.BaseType }, type.GetInterfaces());
            var mappedTypes = typeAndInterfaces.Select(
                item =>
                {
                    string result = null;
                    typesMapping.TryGetValue(item, out result);
                    return result;
                });
            return mappedTypes.FirstOrDefault(item => item != null);
        }

        private Dictionary<string, string> GetPropertiesMappings(string typeName)
        {
            this.objectMappings.TryGetValue(typeName, out object mappings);
            if(mappings != null)
                return (Dictionary<string, string>)mappings;
            return null;
        }

        private PropertyInfo GetPropertyInfo(string propFrom, ref object src)
        {
            string[] items = propFrom.Split('.');
            PropertyInfo pi = null;
            var nextSrc = src;
            for(int i = 0; i < items.Length; i++)
            {
                src = nextSrc;
                try
                {
                    pi = src.GetType().GetProperty(items[i], BindingFlags.Instance | BindingFlags.Public);
                } catch(Exception)
                {
                    pi = src.GetType()
                        .GetProperty(items[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                }
                if(pi == null)
                    return null;
                nextSrc = pi.GetValue(src);
            }
            return pi;
        }

        private static void ProcessImages(string fName, Control target)
        {
        }

        private static void SaveImageCollection(Control target, IEnumerable<Image> images, string directory, int imageCollectionCounter)
        {
            int counter = 0;
            foreach(Image image in images)
            {
                try
                {
                    image.Save(
                        Path.Combine(
                            directory,
                            target.GetType().Name +
                                "_imageCollection" +
                                imageCollectionCounter.ToString() +
                                "_" +
                                counter.ToString() +
                                ".png"));
                    counter++;
                } catch
                {
                }
            }
        }

        private void SetAttribute(XmlElement element, string nameWithPrefix, string value)
        {
            string prefix, name;
            SplitName(nameWithPrefix, out prefix, out name);
            if(string.IsNullOrWhiteSpace(prefix))
                element.SetAttribute(name, value);
            else
                element.SetAttribute(name, namespaceMapping[prefix], value);
        }

        private static void SplitName(string nameWithPrefix, out string prefix, out string name)
        {
            var parts = nameWithPrefix.Split(':');
            if(parts.Length == 2)
            {
                prefix = parts[0];
                name = parts[1];
            } else
            {
                prefix = string.Empty;
                name = parts[0];
            }
        }

        List<AvaloniaFieldInfo> Fields { get; } = new List<AvaloniaFieldInfo>();

        private string AddSuffixToEditorValuePropertyName(Control control, string name)
        {
            string suffix = null;
            var hasElement = editorValueSuffixMapping.TryGetValue(control.GetType(), out suffix);
            if(hasElement)
                return name + suffix;
            return name;
        }

        private void CreateNewNode(Control control, int row, int column, int colSpan, XmlNode convertedParent, XmlDocument doc, out AvaloniaFieldInfo fi, out XmlElement currentNode)
        {
           
            string currentNodeType;
            currentNodeType = GetMappedType(control.GetType());

            if (currentNodeType == null)
            {
                currentNodeType = control.GetType().Name;
            }
            fi = new AvaloniaFieldInfo()
            {
                Type = ExtractTypeName(currentNodeType),
                Name = control.Name,
                Control = control
            };
            Fields.Add(fi);
            currentNode = CreateElement(doc, currentNodeType);
            ApplyRowAndColumn(currentNode, row, column, colSpan);

            convertedParent.AppendChild(currentNode);
        }

        public void ShowError(string message, Exception e)
        {
            string composedMessage = message + e?.ToString();
            if(silent)
                Debug.WriteLine(composedMessage);
            else
                MessageBox.Show(composedMessage);
        }

        private void AppendParameter(StringBuilder b, string param)
        {
            if (b.Length > 0)
                b.Append(", ");
            b.Append(param);
        }

        #endregion

        #region AvaloniaFieldInfo
        internal class AvaloniaFieldInfo
        {
            string name;

            private string RemovePrefix(string value)
            {
                if(string.IsNullOrEmpty(value))
                    return value;
                if(value.StartsWith("btn") || value.StartsWith("lbl"))
                    return value.Substring(3);
                int prefixCount = 0;
                if (char.IsUpper(value[0]))
                    return value;
                for(int i = 0; i < value.Length; i++)
                {
                    if(char.IsUpper(value[i]) && i > 0)
                    {
                        prefixCount = i + 1;
                        break;
                    }
                }
                if(prefixCount <= 3 && value.Length > 5)
                    return value.Substring(prefixCount);
                return value;
            }

            public string CommandFieldName
            {
                get
                {
                    if(char.IsLower(CommandName[0]))
                        return CommandName;
                    return char.ToLower(CommandName[0]) + CommandName.Substring(1);
                }
            }

            public string CommandName { get { return Name + "Command"; } }

            public Control Control { get; set; }

            public string FieldName
            {
                get
                {
                    if(char.IsLower(Name[0]))
                        return Name;
                    return char.ToLower(Name[0]) + Name.Substring(1);
                }
            }

            public string FocusedItemName { get { return FieldName + "FocusedItem"; } }

            public string Name
            {
                get { return name; }
                set
                {
                    if(Name == value)
                        return;
                    name = RemovePrefix(value);
                }
            }

            public string NameFocusedItem { get { return PropertyName + "FocusedItem"; } }

            public string NameSelectedItems { get { return PropertyName + "SelectedItems"; } }

            public string PropertyName
            {
                get
                {
                    if(char.IsUpper(Name[0]))
                        return Name;
                    return char.ToUpper(Name[0]) + Name.Substring(1);
                }
            }

            public string SelectedItemsName { get { return FieldName + "SelectedItems"; } }

            public string Type { get; set; }
        }
        #endregion

        Dictionary<Control, string> radioButtonContainers = new Dictionary<Control, string>();
        internal class RootScrollViewer : Control { }
    }
}

