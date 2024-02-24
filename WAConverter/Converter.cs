

/*using DevExpress.Utils;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraVerticalGrid;

using Prosoft.ECAD.API.Graphics;
using Prosoft.ECAD.UI.Base;
*/
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

        #region DevExpress LayoutControl to Avalonia Grid conversion
        /*
                 void ConvertLayoutControl(LayoutControl layoutControl, XmlNode convertedParent, XmlDocument doc)
                    {
                    ConvertLayoutControlItem(layoutControl.Root, convertedParent, doc, -1, -1, 1);
                    }
                void ConvertLayoutControlItem(BaseLayoutItem currentLayoutItem, XmlNode convertedParent, XmlDocument doc, int layoutRow, int layoutColumn, int colSpan)
                {
                    string currentNodeType = GetMappedType(currentLayoutItem.GetType());
                    XmlElement currentNode = null;
                    if (currentNodeType != null)
                    {
                        if (currentLayoutItem is LayoutGroup lg && (!lg.GroupBordersVisible || !lg.TextVisible))
                            currentNodeType = typesMapping[typeof(PanelControl)];
                        currentNode = CreateElement(doc, currentNodeType);
                        InitName(currentLayoutItem, currentNode);
                        ApplyRowAndColumn(currentNode, layoutRow, layoutColumn, colSpan);
                        convertedParent.AppendChild(currentNode);
                    }
                    if (currentLayoutItem is LayoutGroup layoutGroup)
                    {
                        var childNode = CreateElement(doc, "Grid");
                        currentNode.AppendChild(childNode);
                        currentNode = childNode;
                        var items = new LayoutControlWalker(layoutGroup).ArrangeElements(new OptionsFocus(null) { MoveFocusDirection = MoveFocusDirection.AcrossThenDown });
                        List<BaseLayoutItem> li = new List<BaseLayoutItem>();
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                LayoutItemContainer gr = item as LayoutItemContainer;
                                if (gr != null)
                                {
                                    li.Add(gr);
                                    continue;
                                }
                                LayoutControlItem litem = item as LayoutControlItem;
                                if (litem.TextVisible)
                                {
                                    LayoutControlItem textItem = new LayoutControlItem();
                                    textItem.Control = new Label() { Text = litem.Text, Name = litem.Name };

                                    if (litem.TextLocation == DevExpress.Utils.Locations.Left)
                                    {
                                        textItem.Location = litem.Location;
                                        textItem.Size = new Size(litem.Control.Location.X - litem.Location.X, litem.Size.Height);

                                        LayoutControlItem newItem = new LayoutControlItem();
                                        newItem.TextVisible = false;
                                        newItem.Size = new Size(litem.Bounds.Right - litem.Control.Location.X, litem.Size.Height);
                                        newItem.Location = new Point(litem.Control.Location.X, litem.Location.Y);
                                        newItem.Control = litem.Control;
                                        litem = newItem;
                                    }
                                    else if (litem.TextLocation == DevExpress.Utils.Locations.Top)
                                    {
                                        textItem.Location = litem.Location;
                                        textItem.Size = new Size(litem.Size.Width, litem.Control.Location.Y - litem.Location.Y);

                                        LayoutControlItem newItem = new LayoutControlItem();
                                        newItem.TextVisible = false;
                                        newItem.Size = new Size(litem.Size.Width, litem.Bounds.Bottom - litem.Control.Location.Y);
                                        newItem.Location = new Point(litem.Location.X, litem.Control.Location.Y);
                                        newItem.Control = litem.Control;
                                        litem = newItem;
                                    }

                                    li.Add(textItem);
                                }
                                li.Add(litem);
                            }
                        }
                        try
                        {
                            var rows = li.GroupBy(i => i.Location.Y).OrderBy(g => g.Key).ToList();
                            var itemsLayoutedInColumns = GetItemsLayoutedInColumns(rows);
                            var columns = itemsLayoutedInColumns.GroupBy(i => GetLayoutItemLocationX(i)).OrderBy(g => g.Key).ToList();

                            string rowDefinitions = GetRowDefinitions(rows);
                            if (!string.IsNullOrEmpty(rowDefinitions))
                                SetAttribute(currentNode, "RowDefinitions", rowDefinitions);
                            string colDefinitions = GetColumnDefinitions(columns);
                            if (!string.IsNullOrEmpty(colDefinitions))
                                SetAttribute(currentNode, "ColumnDefinitions", colDefinitions);

                            for (int i = 0; i < rows.Count; i++)
                            {
                                var row = rows[i];
                                XmlElement grid = currentNode;
                                if (IsNestedHorizontalLayoutGrid(row))
                                {
                                    grid = InsertGridRow(currentNode, row, i, columns.Count);
                                    LayoutItemsInRow(grid, row);
                                    continue;
                                }

                                foreach (var item in row)
                                {
                                    int locX = GetLayoutItemLocationX(item);
                                    int itemColSpan = CalcColumnSpan(item, columns);
                                    int rowIndex = rows.IndexOf(r => r.Key == item.Location.Y);
                                    int colIndex = columns.IndexOf(c => c.Key == locX);
                                    ConvertLayoutControlItem(item, currentNode, doc, rowIndex, colIndex, itemColSpan);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            ShowError("", e);
                        }
                    }
                    if (currentLayoutItem is TabbedControlGroup tabbedControlGroup)
                    {
                        foreach (var item in tabbedControlGroup.TabPages)
                            ConvertLayoutControlItem((BaseLayoutItem)item, currentNode, doc, -1, -1, 1);
                    }
                    if (currentLayoutItem is LayoutControlItem layoutControlItem)
                    {
                        if (currentLayoutItem is EmptySpaceItem) return;
                        if (layoutControlItem.Control != null)
                            ConvertControlCore(layoutControlItem.Control, layoutRow, layoutColumn, colSpan, convertedParent, doc);
                    }
                }

                private string GetRowDefinitions(List<IGrouping<int, BaseLayoutItem>> rows)
                {
                    StringBuilder b = new StringBuilder();
                    foreach (var row in rows)
                    {
                        bool isAutoHeight = IsAutoHeightRow(row);
                        AppendParameter(b, isAutoHeight ? "Auto" : "*");
                    }
                    return b.ToString();
                }

                private string GetColumnDefinitions(List<IGrouping<int, BaseLayoutItem>> columns)
                {
                    StringBuilder b = new StringBuilder();
                    foreach (var col in columns)
                    {
                        int columnWidth = GetCustomColumnWidth(col);
                        if (columnWidth > -1)
                        {
                            AppendParameter(b, columnWidth.ToString());
                            continue;
                        }
                        AppendParameter(b, IsAutoWidthColumn(col) ? "Auto" : "*");
                    }
                    return b.ToString();
                }

                private List<BaseLayoutItem> GetItemsLayoutedInColumns(List<IGrouping<int, BaseLayoutItem>> rows)
                {
                    List<BaseLayoutItem> res = new List<BaseLayoutItem>();
                    foreach (var row in rows)
                    {
                        if (IsNestedHorizontalLayoutGrid(row))
                            continue;
                        foreach (var item in row)
                        {
                            if (!res.Contains(item))
                                res.Append(item);
                        }
                    }
                    return res;
                }

                private void LayoutItemsInRow(XmlElement grid, IGrouping<int, BaseLayoutItem> row)
                {
                    var items = row.ToList().OrderBy(i => i.Bounds.X).ToList();
                    for (int i = 0; i < items.Count; i++)
                        ConvertLayoutControlItem(items[i], grid, grid.OwnerDocument, 0, i, 1);
                }

                private XmlElement InsertGridRow(XmlElement parent, IGrouping<int, BaseLayoutItem> row, int rowIndex, int columnSpan)
                {
                    XmlElement grid = CreateElement(parent.OwnerDocument, "Grid");
                    ApplyRowAndColumn(grid, rowIndex, 0, columnSpan);
                    parent.AppendChild(grid);

                    string columnDefinition = CalcColumnDefinitions(row);
                    if (!string.IsNullOrEmpty(columnDefinition))
                        SetAttribute(grid, "ColumnDefinitions", columnDefinition);

                    return grid;
                }
                private bool IsAutoHeightRow(IGrouping<int, BaseLayoutItem> row)
        {
            if (IsNestedHorizontalLayoutGrid(row))
                return true;
            foreach (var litem in row)
            {
                IXtraResizableControl resizeable = GetResizeableControl(litem);
                if (IsAutoHeightItem(litem))
                    return true;
            }
            return false;
        }
        private bool IsAutoWidthColumn(IGrouping<int, BaseLayoutItem> col)
        {
            foreach (var litem in col)
            {
                if (IsAutoWidthItem(litem))
                    return true;
            }
            return false;
        }
        private int GetCustomColumnWidth(BaseLayoutItem item)
        {
            LayoutControlItem lc = item as LayoutControlItem;
            if (lc != null && lc.SizeConstraintsType == SizeConstraintsType.Custom)
                return lc.Size.Width;
            return -1;
        }
        private int GetCustomColumnWidth(IGrouping<int, BaseLayoutItem> col)
        {
            int maxWidth = -1;
            foreach (var item in col)
            {
                LayoutControlItem lc = item as LayoutControlItem;
                if (lc != null && lc.SizeConstraintsType == SizeConstraintsType.Custom)
                    maxWidth = Math.Max(maxWidth, lc.Size.Width);
            }
            return maxWidth;
        }
        private bool IsAutoWidthItem(BaseLayoutItem item)
        {
            if (item is EmptySpaceItem)
                return false;
            LayoutControlItem lci = item as LayoutControlItem;
            if (lci == null)
                return false;

            LabelControl lc = lci.Control as LabelControl;
            if (lc != null && lc.AutoSizeInLayoutControl)
                return true;

            SimpleButton sb = lci.Control as SimpleButton;
            return sb != null && sb.AutoWidthInLayoutControl;
        }
        private bool IsAutoHeightItem(BaseLayoutItem item)
        {
            if (IsAutoWidthItem(item))
                return true;
            LayoutControlItem lci = item as LayoutControlItem;
            if (lci != null)
            {
                if (lci.Control != null && lci.Control is Label || lci.Control is LabelControl)
                    return true;
                if (lci.Control is BaseEdit be && be.Properties.AutoHeight)
                    return true;
            }
            var resizeable = GetResizeableControl(item);
            return resizeable != null && resizeable.MinSize.Height > 0;
        }
        private string CalcColumnDefinitions(IEnumerable<BaseLayoutItem> items)
        {
            StringBuilder b = new StringBuilder();
            foreach (var item in items)
            {
                int width = GetCustomColumnWidth(item);
                if (width != -1)
                {
                    AppendParameter(b, width.ToString());
                    continue;
                }
                AppendParameter(b, IsAutoWidthItem(item) ? "Auto" : "*");
            }

            return b.ToString();
        }

        private bool IsNestedHorizontalLayoutGrid(IGrouping<int, BaseLayoutItem> row)
        {
            int buttonCount = 0;
            foreach (var item in row)
            {
                if (item is EmptySpaceItem)
                    continue;
                LayoutControlItem lc = item as LayoutControlItem;
                if (lc != null && lc.Control is SimpleButton)
                {
                    buttonCount++;
                    continue;
                }
                return false;
            }
            return buttonCount > 0;
        }

        private IXtraResizableControl GetResizeableControl(BaseLayoutItem baseItem)
        {
            LayoutControlItem litem = baseItem as LayoutControlItem;
            if (litem == null)
                return null;
            return litem.Container as IXtraResizableControl;
        }

        private int GetLayoutItemLocationX(BaseLayoutItem i)
        {
            return i.Location.X;
        }
                private int CalcColumnSpan(BaseLayoutItem item, List<IGrouping<int, BaseLayoutItem>> columns)
        {
            int count = 0;
            foreach (var column in columns)
            {
                if (column.Key >= item.Bounds.X && column.Key < item.Bounds.Right)
                    count++;
            }
            return count;
        }
                private void InitName(BaseLayoutItem currentLayoutItem, XmlElement currentNode)
        {
            if (!string.IsNullOrWhiteSpace(currentLayoutItem.Name))
                SetAttribute(currentNode, "x:Name", currentLayoutItem.Name);
        }
         */

        #endregion
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
            "FindControl"
        };
        Dictionary<string, string> namespaceMapping;

        Dictionary<string, object> objectMappings;
        string rootNamespace;
        string rootTypeName;

        bool silent = true; //Please do not commit this option set to a false

        Type[] stopList = new Type[] { typeof(UserControl) };
        Dictionary<Type, string> typesMapping;

        public XamlConverter()
        {
            object[,] mappings = new object[,] {
				// Properties Mappings
				// AvaloniaControl - > Pairs of Properties WinFormProperty -> AvaloniaProperty
				// TreeListControl
				//!!! DO not add x:Name here. It is generated without mappings
				{ "mxtl:TreeListControl", new string[,] {
                        { "Columns", "Columns" }
                    }
                },

				// TreeListColumn
				{ "mxtl:TreeListColumn", new string[,] {
                        { "Name", "x:Name" },
                        { "Caption", "Header" },
                        { "Visible", "IsVisible" },
                        { "VisibleIndex", "VisibleIndex" },
                        { "ColumnEdit", "EditorProperties" }
                    }
                },

				// MxTabItem
				{ "mx:MxTabItem", new string[,] {
                        { "Text", "Header" },
                    }
                },
				
				// Button
				{ "Button", new string[,] {
                        { "Text", "Content" },
                    }
                },

				// Label
				{ "Label", new string[,] {
                        { "Text", "Content" },
                    }
                },

				// SplitContainerControl
				{ "mxe:SplitContainerControl", new string[,] {
                        { "Horizontal", "Orientation" },
                    }
                },
				// SplitContainerControl
				{ "mxe:SplitGroupPanel", new string[,] {
                    }
                },

				// GroupBox
				{ "mxe:GroupBox", new string [,] {
                        { "ShowCaption", "ShowCaption" },
                        { "CaptionLocation", "CaptionLocation" },
                        { "Text", "Header" }
                    }
                },
                { "mxe:TextEditor", new string[,] {
                        { "Text", "EditorValue" },
                        { "Properties.ReadOnly", "ReadOnly"}
                    }
                },
                { "TextBox", new string[,] {
                        { "Properties.ReadOnly", "IsReadOnly"}
                    }
                },
                { "mxe:ButtonEditor", new string[,] {
                        { "Text", "EditorValue" },
                        { "Properties.ReadOnly", "ReadOnly"}
                    }
                },
                { "mxe:SpinEditor", new string[,] {
                        { "Text", "EditorValue" },
                        { "Properties.ReadOnly", "ReadOnly"}
                    }
                },
                { "mxe:ComboBoxEditor", new string[,] {
                        { "Text", "EditorValue" },
                        { "Properties.ReadOnly", "ReadOnly"}
                    }
                },
				// TextEditorProperties
				{ "mxe:TextEditorProperties", new string[]{ } },

				// ButtonEditorProperties
				{ "mxe:ButtonEditorProperties", new string[]{ } },

				// SpinEditorProperties
				{ "mxe:SpinEditorProperties", new string[]{ } },

				// CheckEditorProperties
				{ "mxe:CheckEditorProperties", new string[]{ } },

				// PopupEditorProperties
				{ "mxe:PopupEditorProperties", new string[]{ } },

				// PopupEditorProperties
				{ "mxe:ComboBoxEditorProperties", new string[]{ } },
            };
            this.objectMappings = ConvertToDictionary(mappings);
        }

        private void AddResXFiles(string rootTypeName)
        {
            string dir = Directory.GetCurrentDirectory();
            while (!Directory.GetFiles(dir, "*.csproj", SearchOption.TopDirectoryOnly).Any() && dir != null)
                dir = Path.GetDirectoryName(dir);
            if (dir == null) return;
            string dirConv = Directory.GetCurrentDirectory() + "\\Converted";

            string[] files = Directory.GetFiles(dir, rootTypeName + "*.resx", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string copiedFile = dirConv + "\\" + Path.GetFileName(file);
                if (file.Equals(copiedFile)) continue;
                try
                {
                    if (File.Exists(copiedFile))
                    {
                        File.SetAttributes(copiedFile, FileAttributes.Normal);
                        File.Delete(copiedFile);
                    }
                    File.Copy(file, copiedFile, true);
                }
                catch { }
                ResXCleaner.CleanupFile(copiedFile);
            }
        }

        private void AppendParameter(StringBuilder b, string param)
        {
            if (b.Length > 0)
                b.Append(", ");
            b.Append(param);
        }

        private void ApplyRowAndColumn(XmlElement currentNode, int currentNodeRow, int currentNodeColumn, int colSpan)
        {
            if (currentNodeRow > 0)
                SetAttribute(currentNode, "Grid.Row", currentNodeRow.ToString());
            if (currentNodeColumn > 0)
                SetAttribute(currentNode, "Grid.Column", currentNodeColumn.ToString());
            if (colSpan > 1)
                SetAttribute(currentNode, "Grid.ColumnSpan", colSpan.ToString());
        }

        private void AssignCollectionProperties(XmlElement parentNode, string propTo, IEnumerable enumerable)
        {
            var collNode = CreateElement(parentNode.OwnerDocument, parentNode.Name + "." + propTo);
            parentNode.AppendChild(collNode);
            foreach (var item in enumerable)
            {
                XmlElement xmlItem = CreateObjectNode(collNode.OwnerDocument, item);
                if (xmlItem == null)
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
            if (xmlItem == null)
                return;
            nestedNode.AppendChild(xmlItem);

            AssignProperiesCore(xmlItem, nestedObject);
        }
        private void AssignProperiesCore(XmlElement node, object src)
        {
            Dictionary<string, string> mappings = GetPropertiesMappings(node.Name);
            if (mappings == null)
            {
                //ShowError("There is no mappings for " + src.GetType().Name, null);
                return;
            }

            foreach (var map in mappings)
            {
                string propFrom = map.Key;
                string propTo = map.Value;

                object propertyOwner = src;
                PropertyInfo pi = GetPropertyInfo(propFrom, ref propertyOwner);
                if (pi == null)
                    continue;
                var attr = pi.GetCustomAttribute<DesignerSerializationVisibilityAttribute>();

                if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string))
                {
                    if (!AssignValuePropertyFromResource(node, propTo, propertyOwner, pi))
                        AssignValueProperty(node, propTo, propertyOwner, pi);
                }
                else if (pi.GetValue(propertyOwner) is IEnumerable)
                {
                    AssignCollectionProperties(node, propTo, (IEnumerable)pi.GetValue(propertyOwner));
                }
                else if (pi.GetValue(propertyOwner) is object)
                {
                    AssignNestedObjectProperties(node, propTo, pi.GetValue(propertyOwner));
                }
            }
        }

        private void AssignValueProperty(XmlElement node, string propertyName, object source, PropertyInfo pi)
        {
            DefaultValueAttribute att = pi.GetCustomAttribute<DefaultValueAttribute>();
            object value = pi.GetValue(source);
            if (att != null && object.Equals(att.Value, value))
                return;
            SetAttribute(node, propertyName, Convert.ToString(value));
        }

        bool AssignValuePropertyFromResource(XmlElement node, string propertyName, object src, PropertyInfo pi)
        {
            string propertyOwnerName = (src as Control)?.Name;
            //if (propertyOwnerName == null) propertyOwnerName = (src as BaseLayoutItem)?.Name; // Uncomment for DevExpress controls conversion
            if ((pi.Name == "Text" || pi.Name == "Caption") && propertyOwnerName != null) //Localizeable properties here
            {
                string value = $"{{{$"x:Static p:{rootTypeName}.{propertyOwnerName}_{pi.Name}"}}}";
                SetAttribute(node, propertyName, value);
                return true;
            }
            return false;
        }

        private Dictionary<string, object> ConvertToDictionary(object[,] mappings)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            for (int i = 0; i < mappings.GetLength(0); i++)
            {
                object value = mappings[i, 1];
                if (value is string)
                {
                    result.Add((string)mappings[i, 0], mappings[i, 1]);
                }
                else if (value is string[,])
                {
                    Dictionary<string, string> props = ConvertToStringDictionary((string[,])value);
                    try
                    {
                        result.Add((string)mappings[i, 0], props);
                    }
                    catch (Exception e)
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
            for (int i = 0; i < mappings.GetLength(0); i++)
            {
                object value = mappings[i, 1];
                if (value is string)
                {
                    try
                    {
                        result.Add(mappings[i, 0], mappings[i, 1]);
                    }
                    catch (Exception e)
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
                var templateName = Assembly.GetAssembly(typeof(WAForm)).GetManifestResourceNames().Single(x => x.EndsWith(packageName));
                Stream resource = Assembly.GetAssembly(typeof(WAForm)).GetManifestResourceStream(templateName);
                if (resource != null)
                {
                    string dir = Directory.GetCurrentDirectory() + "\\Converted\\packages\\";
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    using (FileStream fs = new FileStream(dir + packageName, FileMode.Create))
                        resource.CopyTo(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void CreateAppFromTemplateCore(string namespaceName, string templateName, string controlsVersion, string avaloniaVersion)
        {
            try
            {
                string template = LoadTemplate(templateName);
                template = template.Replace("@NamespaceName@", namespaceName);
                template = template.Replace("@AvaloniaVersion@", avaloniaVersion);
                template = template.Replace("@ControlsVersion@", controlsVersion);

                string dir = Directory.GetCurrentDirectory() + "\\Converted";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                string fileFullName = dir + "\\" + templateName;
                File.WriteAllText(fileFullName, template);
            }
            catch (Exception ex)
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
            if (item == null)
                return null;
            string name = GetMappedType(item.GetType());
            if (name == null)
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
                //string templateName = "WAConverter.WAConverter.Templates.CodeFileTemplate.cs";
                string templateName = "CodeFileTemplate.cs";
                string template = LoadTemplate(templateName);
                template = template.Replace("@BaseClass@", baseType);
                template = template.Replace("@TypeClass@", rootTypeName);
                template = template.Replace("@NamespaceName@", namespaceName);
                template = template.Replace("@ViewModelClass@", vmClass);

                string dir = Directory.GetCurrentDirectory() + "\\Converted";
                string fileFullName = dir + "\\" + rootTypeName + ".axaml.cs";
                File.WriteAllText(fileFullName, template);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string GenerateFields(Control target)
        {
            StringBuilder b = new StringBuilder();
            foreach (var fi in Fields) // Uncomment for DevExpress controls conversion
            {
                if (fi.Control is ComboBox || fi.Control is CheckBox || fi.Control is NumericUpDown)
                {
                    b.AppendLine($"\t[ObservableProperty] {GetPropertyType(fi.Control)} {fi.Name};");
                }
            }

            /*            foreach (var fi in Fields) // Uncomment for DevExpress controls conversion
                        {
                            if (fi.Control is BaseEdit)
                            {
                                b.AppendLine($"\t[ObservableProperty] {GetPropertyType(fi.Control)} {fi.Name};");
                            }
                            else if (fi.Control is TreeList)
                            {
                                b.AppendLine($"\t[ObservableProperty] object {fi.FocusedItemName};");
                                b.AppendLine($"\t[ObservableProperty] ObservableCollection<object> {fi.SelectedItemsName};");
                            }
                        }*/
            if (target is Form)
            {
                b.AppendLine("\tpublic event Action RequestClose;");
            }
            return b.ToString();
        }

        private string GenerateFieldsInitialization(string viewModelClassName)
        {
            StringBuilder b = new StringBuilder();
            foreach (var fi in Fields)
            {
                //if (fi.Control is TreeList) // Uncomment for DevExpress controls conversion
                //{
                //    b.AppendLine($"\t\tthis.{fi.SelectedItemsName} = new ObservableCollection<object>();");
                //}
            }
            return b.ToString();
        }

        private string GenerateMethods()
        {
            StringBuilder b = new StringBuilder();
            foreach (var fi in Fields)
            {
                if (IsCommandControl(fi.Control))
                {
                    b.AppendLine($"\t[RelayCommand(CanExecute=nameof(CanExecute{fi.CommandName}))]");
                    b.AppendLine($"\tvoid On{fi.CommandName}(object parameter)");
                    b.AppendLine("\t{");
                    if (fi.CommandName.StartsWith("ok") || fi.CommandName.StartsWith("cancel") || fi.CommandName.StartsWith("close"))
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
                //string templateName = "WAConverter.WAConverter.Templates.ViewModelClassTemplate.cs";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        string GetMappedType(Type type)
        {
            var typeAndInterfaces = Enumerable.Concat(new[] { type }, type.GetInterfaces()); //lookup mapped type by interfaces
            var mappedTypes = typeAndInterfaces.Select(item =>
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
            if (mappings != null)
                return (Dictionary<string, string>)mappings;
            return null;
        }

        private PropertyInfo GetPropertyInfo(string propFrom, ref object src)
        {
            string[] items = propFrom.Split('.');
            PropertyInfo pi = null;
            object nextSrc = src;
            for (int i = 0; i < items.Length; i++)
            {
                src = nextSrc;
                try
                {
                    pi = src.GetType().GetProperty(items[i], BindingFlags.Instance | BindingFlags.Public);
                }
                catch (Exception)
                {
                    pi = src.GetType().GetProperty(items[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                }
                if (pi == null)
                    return null;
                nextSrc = pi.GetValue(src);
            }
            return pi;
        }

        private string GetPropertyType(Control control)
        {
            //if (control is SpinEdit) // Uncomment for DevExpress controls conversion
            //    return "Decimal";
            //if (control is CheckEdit)
            //    return "bool";
            //if (control is TextEdit)
            //    return "string";

            if (control is NumericUpDown)
                return "Decimal";
            if (control is CheckBox)
                return "bool";
            if (control is TextBox)
                return "string";
            return "object";
        }

        void InitNamespaceMapping()
        {
            namespaceMapping = new Dictionary<string, string>
            {
                { "", "https://github.com/avaloniaui"},
                { "x", "http://schemas.microsoft.com/winfx/2006/xaml"},
                { "d", "http://schemas.microsoft.com/expression/blend/2008"},
                { "mc", "http://schemas.openxmlformats.org/markup-compatibility/2006"},
                { "mx", "clr-namespace:Eremex.AvaloniaUI.Controls;assembly=Eremex.Avalonia.Controls"},
                { "mxtl", "clr-namespace:Eremex.AvaloniaUI.Controls.TreeList;assembly=Eremex.Avalonia.Controls"},
                { "mxe", "clr-namespace:Eremex.AvaloniaUI.Controls.Editors;assembly=Eremex.Avalonia.Controls"},
                { "mxpg", "clr-namespace:Eremex.AvaloniaUI.Controls.PropertyGrid;assembly=Eremex.Avalonia.Controls"},
                { "mxb", "clr-namespace:Eremex.AvaloniaUI.Controls.Bars;assembly=Eremex.Avalonia.Controls"},
                { "gf", "clr-namespace:Prosoft.ECAD.Drawing.AvaloniaControls;assembly=DeltaDesign.Drawing"},
                { "uib", "clr-namespace:Prosoft.ECAD.UI.Base;assembly=DeltaDesign.UI.Base"},
                { "mxu", "clr-namespace:Eremex.AvaloniaUI.Controls.Utils;assembly=Eremex.Avalonia.Controls"},
                { "p", $"clr-namespace:{rootNamespace}.{rootTypeName}"},
            };
        }

        void InitTypeMapping()
        {
            typesMapping = new Dictionary<Type, string>
            {
/*                { typeof(SimpleButton), "Button" }, // Uncomment for DevExpress controls conversion
                { typeof(LabelControl), "Label" },
                { typeof(SpinEdit), "mxe:SpinEditor" },
                { typeof(GroupControl), "mxe:GroupBox" },*/
                { typeof(GroupBox), "Grid" },
                { typeof(NumericUpDown), "mxe:SpinEditor" },
                //{ typeof(TextBox), "mxe:TextEditor" },
                { typeof(ComboBox), "mxe:ComboBoxEditor" },
                { typeof(CheckBox), "mxe:CheckEditor" },
/*				{ typeof(PanelControl), "Grid" }, // Uncomment for DevExpress controls conversion
                { typeof(XtraTabControl), "mx:MxTabControl" },
                { typeof(XtraTabPage), "mx:MxTabItem" },
                { typeof(CheckEdit), "mxe:CheckEditor" },
                { typeof(TreeList), "mxtl:TreeListControl" },
                { typeof(LayoutControl), "Grid" },
                { typeof(LayoutControlGroup), "mxe:GroupBox" },
                { typeof(TabbedGroup), "mx:MxTabControl" },
                { typeof(TabbedControlGroup), "mx:MxTabControl" },
                { typeof(DropDownButton), "mxe:ComboBoxEditor" },
                { typeof(ComboBoxEdit), "mxe:ComboBoxEditor" },
                { typeof(TextEdit), "mxe:TextEditor" },
                { typeof(MemoEdit), "TextBox" },
                { typeof(ButtonEdit), "mxe:ButtonEditor" },
                { typeof(TreeListColumn), "mxtl:TreeListColumn" },
                { typeof(IGraphicControl), "gf:GraphicPreviewFrameControl" },
                { typeof(IGraphicEditFrameControl), "gf:GraphicPreviewFrameControl" },

                { typeof(RepositoryItemTextEdit), "mxe:TextEditorProperties" },
                { typeof(RepositoryItemButtonEdit), "mxe:ButtonEditorProperties" },
                { typeof(RepositoryItemSpinEdit), "mxe:SpinEditorProperties" },
                { typeof(RepositoryItemCheckEdit), "mxe:CheckEditorProperties" },
                { typeof(RepositoryItemPopupContainerEdit), "mxe:PopupEditorProperties" },
                { typeof(RepositoryItemComboBox), "mxe:ComboBoxEditorProperties" },
                { typeof(CommonFieldsSetControl), "uib:CommonFieldsSetControl" },
                { typeof(SplitContainerControl), "mxe:SplitContainerControl" },
                { typeof(SplitGroupPanel), "mxe:SplitGroupPanel" },
                { typeof(GridControl), "mxtl:TreeListControl" },
                { typeof(VGridControl), "mxpg:PropertyGridControl" },
                { typeof(PopupContainerControl), "mxb:PopupContainer" },

                { typeof(HyperLinkEdit), "mxe:TextEditor" },
                { typeof(HyperlinkLabelControl), "mxe:TextEditor" },*/

            };
        }

        string LoadTemplate(string resourceTemplateName)
        {
            var templateName = Assembly.GetAssembly(typeof(WAForm)).GetManifestResourceNames().Single(x => x.EndsWith(resourceTemplateName));

            //Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceTemplateName);
            Stream resource = Assembly.GetAssembly(typeof(WAForm)).GetManifestResourceStream(templateName);
            if (resource == null)
            {
                return null;
            }
            using (StreamReader reader = new StreamReader(resource))
            {
                return reader.ReadToEnd();
            }
        }

        private static void ProcessImages(string fName, Control target)
        {
            //ProcessImageCollections(fName, target); // Uncomment for DevExpress controls conversion
            //ProcessControlImages(fName, target); 
        }

        /*        private static void ProcessControlImages(string fName, Control target) // Uncomment for DevExpress controls conversion
                {
                    int imageCollectionCounter = 0;
                    var directory = Path.GetDirectoryName(Path.GetFullPath(fName));
                    foreach (var control in target.Controls)
                    {
                        if (control is LabelControl labelControl)
                        {
                            var images = (labelControl.ImageOptions.Images as ImageCollection)?.Images.Cast<Image>() ?? Enumerable.Empty<Image>();
                            if (labelControl.ImageOptions.Image != null)
                            {
                                images = images.Append(labelControl.ImageOptions.Image);
                            }

                            imageCollectionCounter++;
                            SaveImageCollection(target, images, directory, imageCollectionCounter);
                        }
                    }
                }

        private static void ProcessImageCollections(string fName, Control target)
        {
            var components = target.GetType().GetField("components", BindingFlags.Instance | BindingFlags.NonPublic);
            if (components == null) return;
            var componentsInstance = components.GetValue(target) as IContainer;
            if (componentsInstance == null) return;
            int imageCollectionCounter = 0;
            var directory = Path.GetDirectoryName(Path.GetFullPath(fName)) + "/";
            foreach (var c in componentsInstance.Components)
            {
                var images = (c as ImageCollection)?.Images.Cast<Image>()
                             ?? (c as ImageList)?.Images.Cast<Image>();

                if (images != null)
                {
                    imageCollectionCounter++;
                    SaveImageCollection(target, images, directory, imageCollectionCounter);
                }

                var svgImageCollection = c as SvgImageCollection;
                if (svgImageCollection != null)
                {
                    int counter = 0;
                    imageCollectionCounter++;
                    foreach (SvgImage image in svgImageCollection)
                    {
                        try
                        {
                            image.Save(Path.Combine(directory, target.GetType().Name + "_svgimageCollection" + imageCollectionCounter.ToString() + "_" + counter.ToString() + ".svg"));
                            counter++;
                        }
                        catch { };

                    }
                }
            }
        }*/

        private static void SaveImageCollection(Control target, IEnumerable<Image> images, string directory, int imageCollectionCounter)
        {
            int counter = 0;
            foreach (Image image in images)
            {
                try
                {
                    image.Save(Path.Combine(directory,
                        target.GetType().Name + "_imageCollection" + imageCollectionCounter.ToString() + "_" +
                        counter.ToString() + ".png"));
                    counter++;
                }
                catch { }
            }
        }

        private void SetAttribute(XmlElement element, string nameWithPrefix, string value)
        {
            string prefix, name;
            SplitName(nameWithPrefix, out prefix, out name);
            if (string.IsNullOrWhiteSpace(prefix))
                element.SetAttribute(name, value);
            else
                element.SetAttribute(name, namespaceMapping[prefix], value);
        }

        private static void SplitName(string nameWithPrefix, out string prefix, out string name)
        {
            var parts = nameWithPrefix.Split(':');
            if (parts.Length == 2)
            {
                prefix = parts[0];
                name = parts[1];
            }
            else
            {
                prefix = "";
                name = parts[0];
            }
        }

        List<AvaloniaFieldInfo> Fields { get; } = new List<AvaloniaFieldInfo>();

        protected bool IsCommandControl(Control control)
        {
            return control is Button;
            // || control is SimpleButton ||  control is HyperLinkEdit || control is HyperlinkLabelControl; // Uncomment for DevExpress controls conversion
        }

        public void ConvertControl(Control parent, XmlNode convertedParent, XmlDocument doc)
        {
            /*if (parent is LayoutControl layoutControl) // Uncomment for DevExpress LayoutControl conversion
            {
              ConvertLayoutControl(layoutControl, convertedParent, doc);
              return;
            }
            */
            foreach (Control control in parent.Controls)
                ConvertControlCore(control, -1, -1, 1, convertedParent, doc);
        }



        public bool ConvertControlCore(Control control, int row, int column, int colSpan, XmlNode convertedParent, XmlDocument doc)
        {
            if (ignoredControls.Contains(control.GetType().Name))
                return false;
            string currentNodeType;
            currentNodeType = GetMappedType(control.GetType());

            if (currentNodeType == null)
            {
                //ShowError("could not find type mapping for:" + control.GetType().Name, null);
                currentNodeType = control.GetType().Name;
            }
            AvaloniaFieldInfo fi = new AvaloniaFieldInfo() { Type = ExtractTypeName(currentNodeType), Name = control.Name, Control = control };
            Fields.Add(fi);
            var currentNode = CreateElement(doc, currentNodeType);
            ApplyRowAndColumn(currentNode, row, column, colSpan);

            convertedParent.AppendChild(currentNode);
            AssignProperiesCore(currentNode, control);
            if (!string.IsNullOrWhiteSpace(control.Name))
                SetAttribute(currentNode, "x:Name", control.Name);
            //            if (control is HyperlinkLabelControl || control is HyperLinkEdit) // Uncomment for DevExpress controls conversion
            //                SetAttribute(currentNode, "Classes", "LayoutItem Hyperlink");
            //else if (control is BaseEdit || control is Label || control is LabelControl || control is SimpleButton)
            //SetAttribute(currentNode, "Classes", "LayoutItem");
            else if (control is Label)
                SetAttribute(currentNode, "Classes", "LayoutItem");

            if (control is ComboBox || control is CheckBox || control is NumericUpDown)
                SetAttribute(currentNode, "EditorValue", $"{{Binding {fi.PropertyName}}}");

            //if (control is BaseEdit) // Uncomment for DevExpress controls conversion
            //SetAttribute(currentNode, "EditorValue", $"{{Binding {fi.PropertyName}}}");
            if (IsCommandControl(control))
            {
                //                if (control is HyperlinkLabelControl || control is HyperLinkEdit) // Uncomment for DevExpress controls conversion
                //                {
                //                    SetAttribute(currentNode, "mxu:EventToCommandHelper.RoutedEvent", "{x:Static InputElement.PointerPressed}");
                //                    SetAttribute(currentNode, "mxu:EventToCommandHelper.Command", $"{{Binding {fi.CommandName}}}");
                //                }
                //               else
                SetAttribute(currentNode, "Command", $"{{Binding {fi.CommandName}}}");
            }
            //            if (control is TreeList) // Uncomment for DevExpress controls conversion
            //            {
            //                SetAttribute(currentNode, "FocusedItem", $"{{Binding {fi.NameFocusedItem}}}");
            //                SetAttribute(currentNode, "SelectedItems", $"{{Binding {fi.NameSelectedItems}}}");
            //           }
            if (control.Controls.Count > 0 && !stopList.Any(item => control.GetType().IsSubclassOf(item)))
                ConvertControl(control, currentNode, doc);
            return true;
        }
        public void ShowError(string message, Exception e)
        {
            string composedMessage = message + e?.ToString();
            if (silent)
                Debug.WriteLine(composedMessage);
            else
                MessageBox.Show(composedMessage);
        }
        public void StartConvert(Control target)
        {
            try
            {
                if (target.IsDesignerHosted())
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
                foreach (var key in namespaceMapping.Keys)
                    currentNode.SetAttribute("xmlns" + (key.Length > 0 ? ":" + key : ""), namespaceMapping[key]);
                SetAttribute(currentNode, "x:Class", rootClassName);
                if (!isUserControl)
                {
                    SetAttribute(currentNode, "Width", target.Width.ToString());
                    SetAttribute(currentNode, "Height", target.Height.ToString());
                    SetAttribute(currentNode, "Title", $"{{{$"x:Static p:{rootTypeName}.Title"}}}");
                }
                doc.AppendChild(currentNode);
                ConvertControl(target, currentNode, doc);
                string dir = Directory.GetCurrentDirectory() + "\\Converted";
                if (!Directory.Exists(dir))
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
            }
            catch (Exception e)
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
                if (value == null)
                    return value;
                if (value.StartsWith("btn") || value.StartsWith("lbl"))
                    return value.Substring(3);
                int prefixCount = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    if (char.IsUpper(value[i]) && i > 0)
                    {
                        prefixCount = i + 1;
                        break;
                    }
                }
                if (prefixCount <= 3 && value.Length > 5)
                    return value.Substring(prefixCount);
                return value;
            }

            public string CommandFieldName
            {
                get
                {
                    if (char.IsLower(CommandName[0]))
                        return CommandName;
                    return char.ToLower(CommandName[0]) + CommandName.Substring(1);
                }
            }
            public string CommandName
            {
                get
                {
                    return Name + "Command";
                }
            }
            public Control Control { get; set; }
            public string FieldName
            {
                get
                {
                    if (char.IsLower(Name[0]))
                        return Name;
                    return char.ToLower(Name[0]) + Name.Substring(1);
                }
            }

            public string FocusedItemName
            {
                get
                {
                    return FieldName + "FocusedItem";
                }
            }
            public string Name
            {
                get { return name; }
                set
                {
                    if (Name == value)
                        return;
                    name = RemovePrefix(value);
                }
            }
            public string NameFocusedItem
            {
                get
                {
                    return PropertyName + "FocusedItem";
                }
            }
            public string NameSelectedItems
            {
                get
                {
                    return PropertyName + "SelectedItems";
                }
            }
            public string PropertyName
            {
                get
                {
                    if (char.IsUpper(Name[0]))
                        return Name;
                    return char.ToUpper(Name[0]) + Name.Substring(1);
                }
            }
            public string SelectedItemsName
            {
                get
                {
                    return FieldName + "SelectedItems";
                }
            }
            public string Type { get; set; }
        }
    }
}

