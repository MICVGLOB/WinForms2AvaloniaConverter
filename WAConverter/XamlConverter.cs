

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WAConverter
{
    public class XamlConverter
    {
        const string avaloniaVersion = "11.0.8";

        const string controlsVersion = "0.0.49-demo";

        string[] ignoredControls = new string[]
        {
            "FakeFocusContainer",
            "BarDockControl",
            "TextBoxMaskBox",
            "DPILabel",
            "VTLScrollBar",
            "HTLScrollBar",
            "HCrkScrollBar",
            "VCrkScrollBar",
            "VGridVertScrollBar",
            "VGridHorzScrollBar",
            "FindControl",
            "HScrollBar",
            "VScrollBar"
        };
        Dictionary<string, string> namespaceMapping;

        Dictionary<string, object> objectMappings;
        string rootNamespace;
        string rootTypeName;

        bool silent = true;

        Type[] stopList = new Type[] { typeof(UserControl) };
        Dictionary<Type, string> typesMapping;

        public XamlConverter()
        {
            object[,] mappings = new object[,]
            {
            //!!! DO not add x:Name here. It is generated without mappings

            {
                "mxtl:TreeListControl",
                new string[,] { { "Columns", "Columns" } }
            },

            {
                "mxtl:TreeListColumn",
                new string[,]
                {
                {
                    "Name",
                    "x:Name"
                },
                {
                    "Caption",
                    "Header"
                },
                {
                    "Visible",
                    "IsVisible"
                },
                {
                    "VisibleIndex",
                    "VisibleIndex"
                },
                {
                    "ColumnEdit",
                    "EditorProperties"
                }
                }
            },

            {
                "mx:MxTabItem",
                new string[,] { { "Text", "Header" }, }
            },

            {
                "Button",
                new string[,] { { "Text", "Content" }, }
            },

            {
                "Label",
                new string[,] { { "Text", "Content" }, }
            },

            {
                "mxe:SplitContainerControl",
                new string[,] { { "Horizontal", "Orientation" }, }
            },
            {
                "mxe:SplitGroupPanel",
                new string[,] { }
            },

            {
                "mxe:GroupBox",
                new string [,]
                { { "ShowCaption", "ShowCaption" }, { "CaptionLocation", "CaptionLocation" }, { "Text", "Header" } }
            },
            {
                "mxe:TextEditor",
                new string[,] { { "Text", "EditorValue" }, { "Properties.ReadOnly", "ReadOnly" } }
            },
            {
                "TextBox",
                new string[,] { { "Properties.ReadOnly", "IsReadOnly" } }
            },
            {
                "mxe:ButtonEditor",
                new string[,] { { "Text", "EditorValue" }, { "Properties.ReadOnly", "ReadOnly" } }
            },
            {
                "mxe:SpinEditor",
                new string[,] { { "Text", "EditorValue" }, { "Properties.ReadOnly", "ReadOnly" } }
            },
            {
                "mxe:ComboBoxEditor",
                new string[,] { { "Text", "EditorValue" }, { "Properties.ReadOnly", "ReadOnly" } }
            },
            {
                "mxe:TextEditorProperties",
                new string[] { }
            },

            {
                "mxe:ButtonEditorProperties",
                new string[] { }
            },

            {
                "mxe:SpinEditorProperties",
                new string[] { }
            },

            {
                "mxe:CheckEditorProperties",
                new string[] { }
            },

            {
                "mxe:PopupEditorProperties",
                new string[] { }
            },

            {
                "mxe:ComboBoxEditorProperties",
                new string[] { }
            },
            };
            this.objectMappings = ConvertToDictionary(mappings);
        }

        private void AddResXFiles(string rootTypeName)
        {
            string dir = Directory.GetCurrentDirectory();
            while(!Directory.GetFiles(dir, "*.csproj", SearchOption.TopDirectoryOnly).Any() && dir != null)
                dir = Path.GetDirectoryName(dir);
            if(dir == null)
                return;
            string dirConv = Directory.GetCurrentDirectory() + "\\Converted";

            string[] files = Directory.GetFiles(dir, rootTypeName + "*.resx", SearchOption.AllDirectories);
            foreach(string file in files)
            {
                string copiedFile = dirConv + "\\" + Path.GetFileName(file);
                if(file.Equals(copiedFile))
                    continue;
                try
                {
                    if(File.Exists(copiedFile))
                    {
                        File.SetAttributes(copiedFile, FileAttributes.Normal);
                        File.Delete(copiedFile);
                    }
                    File.Copy(file, copiedFile, true);
                } catch
                {
                }
                ResXCleaner.CleanupFile(copiedFile);
            }
        }

        private void AppendParameter(StringBuilder b, string param)
        {
            if(b.Length > 0)
                b.Append(", ");
            b.Append(param);
        }

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
                XmlElement xmlItem = CreateObjectNode(collNode.OwnerDocument, item);
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

            XmlElement xmlItem = CreateObjectNode(nestedNode.OwnerDocument, nestedObject);
            if(xmlItem == null)
                return;
            nestedNode.AppendChild(xmlItem);

            AssignProperiesCore(xmlItem, nestedObject);
        }

        private void AssignProperiesCore(XmlElement node, object src)
        {
            Dictionary<string, string> mappings = GetPropertiesMappings(node.Name);
            if(mappings == null)
            {
                return;
            }

            foreach(var map in mappings)
            {
                string propFrom = map.Key;
                string propTo = map.Value;

                object propertyOwner = src;
                PropertyInfo pi = GetPropertyInfo(propFrom, ref propertyOwner);
                if(pi == null)
                    continue;
                var attr = pi.GetCustomAttribute<DesignerSerializationVisibilityAttribute>();

                if(pi.PropertyType.IsValueType || pi.PropertyType == typeof(string))
                {
                    if(!AssignValuePropertyFromResource(node, propTo, propertyOwner, pi))
                        AssignValueProperty(node, propTo, propertyOwner, pi);
                } else if(pi.GetValue(propertyOwner) is IEnumerable)
                {
                    AssignCollectionProperties(node, propTo, (IEnumerable)pi.GetValue(propertyOwner));
                } else if(pi.GetValue(propertyOwner) is object)
                {
                    AssignNestedObjectProperties(node, propTo, pi.GetValue(propertyOwner));
                }
            }
        }

        private void AssignValueProperty(XmlElement node, string propertyName, object source, PropertyInfo pi)
        {
            DefaultValueAttribute att = pi.GetCustomAttribute<DefaultValueAttribute>();
            object value = pi.GetValue(source);
            if(att != null && object.Equals(att.Value, value))
                return;
            SetAttribute(node, propertyName, Convert.ToString(value));
        }

        bool AssignValuePropertyFromResource(XmlElement node, string propertyName, object src, PropertyInfo pi)
        {
            string propertyOwnerName = (src as Control)?.Name;
            if((pi.Name == "Text" || pi.Name == "Caption") && propertyOwnerName != null)
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

        private void SetLocalizableProperty(
            XmlElement node,
            string propertyName,
            PropertyInfo pi,
            string propertyOwnerName,
            object instance)
        {
            string value = "";
            if(ShouldSetLocalizableProperty())
            {
                value = $"{{{$"x:Static p:{rootTypeName}.{propertyOwnerName}_{pi.Name}"}}}";
            } else
            {
                value = (string)pi.GetValue(instance);
            }
            SetAttribute(node, propertyName, value);
        }

        private Dictionary<string, object> ConvertToDictionary(object[,] mappings)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            for(int i = 0; i < mappings.GetLength(0); i++)
            {
                object value = mappings[i, 1];
                if(value is string)
                {
                    result.Add((string)mappings[i, 0], mappings[i, 1]);
                } else if(value is string[,])
                {
                    Dictionary<string, string> props = ConvertToStringDictionary((string[,])value);
                    try
                    {
                        result.Add((string)mappings[i, 0], props);
                    } catch(Exception e)
                    {
                        MessageBox.Show("Key is already exists: " + (string)mappings[i, 0]);
                        throw e;
                    }
                }
            }
            return result;
        }

        private Dictionary<string, string> ConvertToStringDictionary(string[,] mappings)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            for(int i = 0; i < mappings.GetLength(0); i++)
            {
                object value = mappings[i, 1];
                if(value is string)
                {
                    try
                    {
                        result.Add(mappings[i, 0], mappings[i, 1]);
                    } catch(Exception e)
                    {
                        MessageBox.Show("Key already exists: " + mappings[i, 0]);
                        throw e;
                    }
                }
            }
            return result;
        }

        void CreateAppFromTemplate(string rootNamespace)
        {
            CreateAppFromTemplateCore(rootNamespace, "App.axaml", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "App.axaml.cs", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "ConvertedAvalonia.csproj", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "MainWindow.axaml", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "MainWindow.axaml.cs", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "nuget.config", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "Program.cs", controlsVersion, avaloniaVersion);

            var packageName = $"Eremex.Avalonia.Controls.{controlsVersion}.nupkg";
            try
            {
                var templateName = Assembly.GetAssembly(typeof(WAForm))
                    .GetManifestResourceNames()
                    .Single(x => x.EndsWith(packageName));
                Stream resource = Assembly.GetAssembly(typeof(WAForm)).GetManifestResourceStream(templateName);
                if(resource != null)
                {
                    string dir = Directory.GetCurrentDirectory() + "\\Converted\\packages\\";
                    if(!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    using(FileStream fs = new FileStream(dir + packageName, FileMode.Create))
                        resource.CopyTo(fs);
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void CreateAppFromTemplateCore(
            string namespaceName,
            string templateName,
            string controlsVersion,
            string avaloniaVersion)
        {
            try
            {
                string template = LoadTemplate(templateName);
                template = template.Replace("@NamespaceName@", namespaceName);
                template = template.Replace("@AvaloniaVersion@", avaloniaVersion);
                template = template.Replace("@ControlsVersion@", controlsVersion);

                string dir = Directory.GetCurrentDirectory() + "\\Converted";
                if(!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string fileFullName = dir + "\\" + templateName;
                File.WriteAllText(fileFullName, template);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            string name = GetMappedType(item.GetType());
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

        private void GenerateCodeFile(Control target, string rootTypeName, string namespaceName)
        {
            string vmClass = rootTypeName + "ViewModel";

            try
            {
                string baseType = target is Form ? "Window" : "UserControl";
                string templateName = "CodeFileTemplate.cs";
                string template = LoadTemplate(templateName);
                template = template.Replace("@BaseClass@", baseType);
                template = template.Replace("@TypeClass@", rootTypeName);
                template = template.Replace("@NamespaceName@", namespaceName);
                template = template.Replace("@ViewModelClass@", vmClass);

                string dir = Directory.GetCurrentDirectory() + "\\Converted";
                string fileFullName = dir + "\\" + rootTypeName + ".axaml.cs";
                File.WriteAllText(fileFullName, template);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string GenerateFields(Control target)
        {
            StringBuilder b = new StringBuilder();
            foreach(var fi in Fields)
            {
                if(fi.Control is ComboBox || fi.Control is CheckBox || fi.Control is NumericUpDown)
                {
                    b.AppendLine($"\t[ObservableProperty] {GetPropertyType(fi.Control)} {fi.Name};");
                }
            }

            if(target is Form)
            {
                b.AppendLine("\tpublic event Action RequestClose;");
            }
            return b.ToString();
        }

        private string GenerateFieldsInitialization(string viewModelClassName)
        {
            StringBuilder b = new StringBuilder();
            foreach(var fi in Fields)
            {
            }
            return b.ToString();
        }

        private string GenerateMethods()
        {
            StringBuilder b = new StringBuilder();
            foreach(var fi in Fields)
            {
                if(IsCommandControl(fi.Control))
                {
                    b.AppendLine($"\t[RelayCommand(CanExecute=nameof(CanExecute{fi.CommandName}))]");
                    b.AppendLine($"\tvoid On{fi.CommandName}(object parameter)");
                    b.AppendLine("\t{");
                    if(fi.CommandName.StartsWith("ok") ||
                        fi.CommandName.StartsWith("cancel") ||
                        fi.CommandName.StartsWith("close"))
                        b.AppendLine("\t\tRequestClose?.Invoke();");
                    b.AppendLine("\t}");
                    b.AppendLine($"\tbool CanExecute{fi.CommandName}(object parameter)\n\t{{");
                    b.AppendLine("\t\treturn true;");
                    b.AppendLine("\t}");
                }
            }
            return b.ToString();
        }

        private void GenerateViewModelFile(Control target, string rootTypeName, string namespaceName)
        {
            string vmClass = rootTypeName + "ViewModel";

            try
            {
                string baseType = target is Form ? "Window" : "UserControl";
                string templateName = "ViewModelClassTemplate.cs";
                string template = LoadTemplate(templateName);
                template = template.Replace("@BaseClass@", baseType);
                template = template.Replace("@TypeClass@", rootTypeName);
                template = template.Replace("@NamespaceName@", namespaceName);
                template = template.Replace("@ViewModelClass@", vmClass);
                template = template.Replace("@Fields@", GenerateFields(target));
                template = template.Replace("@FieldsInitialization@", GenerateFieldsInitialization(vmClass));
                template = template.Replace("@Methods@", GenerateMethods());

                string dir = Directory.GetCurrentDirectory() + "\\Converted";
                string fileFullName = dir + "\\" + vmClass + ".cs";
                File.WriteAllText(fileFullName, template);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            object nextSrc = src;
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

        private string GetPropertyType(Control control)
        {
            if(control is NumericUpDown)
                return "Decimal";
            if(control is CheckBox)
                return "bool";
            if(control is TextBox)
                return "string";
            return "object";
        }

        void InitNamespaceMapping()
        {
            namespaceMapping = new Dictionary<string, string>
            {
                { string.Empty, "https://github.com/avaloniaui" },
                { "x", "http://schemas.microsoft.com/winfx/2006/xaml" },
                { "d", "http://schemas.microsoft.com/expression/blend/2008" },
                { "mc", "http://schemas.openxmlformats.org/markup-compatibility/2006" },
                { "mx", "clr-namespace:Eremex.AvaloniaUI.Controls;assembly=Eremex.Avalonia.Controls" },
                { "mxtl", "clr-namespace:Eremex.AvaloniaUI.Controls.TreeList;assembly=Eremex.Avalonia.Controls" },
                { "mxe", "clr-namespace:Eremex.AvaloniaUI.Controls.Editors;assembly=Eremex.Avalonia.Controls" },
                { "mxpg", "clr-namespace:Eremex.AvaloniaUI.Controls.PropertyGrid;assembly=Eremex.Avalonia.Controls" },
                { "mxb", "clr-namespace:Eremex.AvaloniaUI.Controls.Bars;assembly=Eremex.Avalonia.Controls" },
                { "mxu", "clr-namespace:Eremex.AvaloniaUI.Controls.Utils;assembly=Eremex.Avalonia.Controls" },
                { "p", $"clr-namespace:{rootNamespace}.{rootTypeName}" },
            };
        }

        void InitTypeMapping()
        {
            typesMapping = new Dictionary<Type, string>
            {
                { typeof(GroupBox), "Grid" },
                { typeof(NumericUpDown), "mxe:SpinEditor" },
                { typeof(ComboBox), "mxe:ComboBoxEditor" },
                { typeof(CheckBox), "mxe:CheckEditor" },
                { typeof(DataGridView), "mxtl:TreeListControl" },

                { typeof(Form), "StackPanel" },
                { typeof(UserControl), "StackPanel" },
            };
        }

        string LoadTemplate(string resourceTemplateName)
        {
            var templateName = Assembly.GetAssembly(typeof(WAForm))
                .GetManifestResourceNames()
                .Single(x => x.EndsWith(resourceTemplateName));

            Stream resource = Assembly.GetAssembly(typeof(WAForm)).GetManifestResourceStream(templateName);
            if(resource == null)
            {
                return null;
            }
            using(StreamReader reader = new StreamReader(resource))
            {
                return reader.ReadToEnd();
            }
        }

        private static void ProcessImages(string fName, Control target)
        {
        }

        private static void SaveImageCollection(
            Control target,
            IEnumerable<Image> images,
            string directory,
            int imageCollectionCounter)
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

        protected bool IsCommandControl(Control control) { return control is Button; }

        public void ConvertControl(Control parent, XmlNode convertedParent, XmlDocument doc)
        {
            foreach(Control control in parent.Controls)
                ConvertControlCore(control, -1, -1, 1, convertedParent, doc);
        }


        public bool ConvertControlCore(
            Control control,
            int row,
            int column,
            int colSpan,
            XmlNode convertedParent,
            XmlDocument doc)
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

            if (control is ComboBox || control is CheckBox || control is NumericUpDown)
                SetAttribute(currentNode, "EditorValue", $"{{Binding {fi.PropertyName}}}");

            if (IsCommandControl(control))
            {
                SetAttribute(currentNode, "Command", $"{{Binding {fi.CommandName}}}");
            }
            if (control.Controls.Count > 0 && !stopList.Any(item => control.GetType().IsSubclassOf(item)))
                ConvertControl(control, currentNode, doc);
            return true;
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

        public void StartConvert(Control target)
        {
            try
            {
                if(target.IsDesignerHosted())
                    return;

                XmlDocument doc = new XmlDocument();
                rootTypeName = target.IsDesignerHosted() ? target.Name : target.GetType().Name;
                var fullName = target.GetType().FullName;
                rootNamespace = fullName.Substring(0, fullName.LastIndexOf('.'));
                CreateAppFromTemplate(rootNamespace);
                string rootClassName = target.IsDesignerHosted() ? target.Name : target.GetType().FullName;
                InitTypeMapping();
                InitNamespaceMapping();
                bool isUserControl = target is UserControl;
                var currentNode = CreateElement(doc, isUserControl ? "UserControl" : "Window");
                foreach(var key in namespaceMapping.Keys)
                    currentNode.SetAttribute(
                        "xmlns" + (key.Length > 0 ? ":" + key : string.Empty),
                        namespaceMapping[key]);
                SetAttribute(currentNode, "x:Class", rootClassName);
                if(!isUserControl)
                {
                    SetAttribute(currentNode, "Width", target.Width.ToString());
                    SetAttribute(currentNode, "Height", target.Height.ToString());
                    SetLocalizableProperty(currentNode, "Title", target.GetType().GetProperty("Text"), "Text", target);
                }
                doc.AppendChild(currentNode);

                AvaloniaFieldInfo fi;
                XmlElement newCurrentNode;
                CreateNewNode(target, -1, -1, 1, currentNode, doc, out fi, out newCurrentNode);

                ConvertControl(target, newCurrentNode, doc);
                string dir = Directory.GetCurrentDirectory() + "\\Converted";
                if(!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string fileFullName = dir + "\\" + rootTypeName + ".axaml";
                doc.Save(fileFullName);
                var txt = File.ReadAllText(fileFullName);
                txt = txt.Replace("x:=", "\n\tx:=").Replace("xmlns", "\n\txmlns");
                File.WriteAllText(fileFullName, txt);
                GenerateCodeFile(target, rootTypeName, rootNamespace);
                GenerateViewModelFile(target, rootTypeName, rootNamespace);
                AddResXFiles(rootTypeName);
                ProcessImages(fileFullName, target);
            } catch(Exception e)
            {
                ShowError("Error converting: ", e);
            }
        }

        public string BaseType { get; internal set; }

        internal class AvaloniaFieldInfo
        {
            string name;

            private string RemovePrefix(string value)
            {
                if(value == null)
                    return value;
                if(value.StartsWith("btn") || value.StartsWith("lbl"))
                    return value.Substring(3);
                int prefixCount = 0;
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
    }
}

