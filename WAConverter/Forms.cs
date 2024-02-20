using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAConverter
{
    public class WAForm : Form
    {
        bool converted = false;
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (!converted)
            {
                try
                {
                    new XamlConverter() { BaseType = "Window" }.StartConvert(this);
                }
                catch
                {
                }
                converted = true;
            }
        }
    }

    public class WAUserControl : UserControl
    {
        bool converted = false;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!converted)
            {
                try
                {
                    new XamlConverter() { BaseType = "UserControl" }.StartConvert(this);
                }
                catch
                {
                }
                converted = true;
            }
        }
    }
}