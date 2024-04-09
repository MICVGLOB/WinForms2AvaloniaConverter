using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAConverter
{
    public partial class XamlConverter
    {
        const string avaloniaVersion = "11.0.10";

        const string controlsVersion = "0.0.99-demo";

        private string[] ignoredControls = new string[]
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
            "VScrollBar",
            "UpDownButtons",
            "UpDownEdit"
        };

        private void InitTypeMapping()
        {
            typesMapping = new Dictionary<Type, string>
            {
                { typeof(GroupBox), "mxe:GroupBox" },
                { typeof(TableLayoutPanel), "Grid" },
                { typeof(ProgressBar), "ProgressBar" },

                { typeof(NumericUpDown), "mxe:SpinEditor" },
                { typeof(ComboBox), "mxe:ComboBoxEditor" },
                { typeof(CheckBox), "mxe:CheckEditor" },
                { typeof(DataGridView), "mxtl:TreeListControl" },
                { typeof(TextBox), "mxe:TextEditor" },
                { typeof(MonthCalendar), "mxe:CalendarControl" },
                { typeof(Form), "StackPanel" },
                { typeof(UserControl), "StackPanel" },
                { typeof(RootScrollViewer), "ScrollViewer" },

            };
            editorValueSuffixMapping = new Dictionary<Type, string>
            {
                { typeof(ComboBox), "SelectedValue" },
                { typeof(CheckBox), "Checked" },
                { typeof(RadioButton), "Checked" },
                { typeof(NumericUpDown), "Value" },
                { typeof(TextBox), "TextValue" },
            };
        }

        private void InitNamespaceMapping()
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

        private static object[,] propertiesMapping = new object[,]
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
                new string[,] {{ "Properties.ReadOnly", "ReadOnly" } }
            },
            {
                "TextBox",
                new string[,] { { "Properties.ReadOnly", "IsReadOnly" } }
            },
            {
                "mxe:ButtonEditor",
                new string[,] { { "Properties.ReadOnly", "ReadOnly" } }
            },
            {
                "mxe:SpinEditor",
                new string[,] { { "Properties.ReadOnly", "ReadOnly" }, { "Minimum", "Minimum" }, { "Maximum", "Maximum" }, }
            },
            {
                "ProgressBar",
                new string[,] { { "Minimum", "Minimum" }, { "Maximum", "Maximum" }, }
            },
            {
                "RadioButton",
                new string[,] { { "Text", "Content" },}
            },
            {
                "mxe:ComboBoxEditor",
                new string[,] { { "Properties.ReadOnly", "ReadOnly" } }
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


        #region Attributes and properties generation rules
        private bool ShouldAddMinWidthAttribute(Control control) => control is TextBox || control is ComboBox || control is NumericUpDown || control is ProgressBar;

        private bool ShouldAddEditorValue(Control control) => control is ComboBox || control is CheckBox || control is NumericUpDown || ((control is TextBox) && !control.GetType().IsSubclassOf(typeof(TextBox)));

        private bool ShouldAddItemsSource(Control control) => control is ComboBox;

        private bool ShouldAddValue(Control control) => control is ProgressBar;

        private bool ShouldAddCommand(Control control) => control is Button;

        #endregion

        private string GetEditorValuePropertyType(Control control)
        {
            if (control is NumericUpDown)
                return "Decimal";
            else if (control is CheckBox)
                return "bool";
            else if (control is TextBox)
                return "string";
            return "object";
        }

        #region Utils
        private Dictionary<string, object> ConvertToDictionary(object[,] mappings)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            for (int i = 0; i < mappings.GetLength(0); i++)
            {
                var value = mappings[i, 1];
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
                var value = mappings[i, 1];
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
        #endregion
    }
}
