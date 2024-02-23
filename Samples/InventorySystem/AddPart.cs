using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{
    public partial class AddPartForm : Form
    {
        public AddPartForm()
        {
            InitializeComponent();
            InhousePart newIPart = new InhousePart();                    
           
            OutsourcedPart newOPart = new OutsourcedPart();
        }

        private bool KeyEnteredIsValid(string key)
        {
            Regex regex;
            regex = new Regex("[^0-9]+$"); //regex that matches disallowed text
            return regex.IsMatch(key);
        }

        private void APCancelButton_Click(object sender, EventArgs e)
        {
            Close();
            Mainscreen p = new Mainscreen();
            p.Show();
        }

        private void APInhouseRadio_CheckedChanged(object sender, EventArgs e)
        {
            APMachineIDLabel.Text = "Machine ID";
            APMachineIDTextBox.Text = "";
        }

        private void APOutsourcedRadio_CheckedChanged(object sender, EventArgs e)
        {
            APMachineIDLabel.Text = "Company Name";
            APMachineIDTextBox.Text = "";
        }

        private void APMachineIDLabel_Click(object sender, EventArgs e)
        {
            APMachineIDLabel.TextAlign = ContentAlignment.TopRight;
        }

        private void APSaveButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(APInventoryTextBox.Text) < Convert.ToInt32(APMinTextBox.Text) ||
                Convert.ToInt32(APInventoryTextBox.Text) > Convert.ToInt32(APMaxTextBox.Text))
            {
                MessageBox.Show("Inventory is out of range.", "Error");
                return;
            }
            if (Convert.ToInt32(APMaxTextBox.Text) > Convert.ToInt32(APMinTextBox.Text) )
                {
                    if (APInhouseRadio.Checked)
                    {

                        InhousePart newIPart = new InhousePart();
                        newIPart.PartID = Inventory.createPartID();
                        newIPart.Name = APNameTextBox.Text;
                        newIPart.InStock = Int32.Parse(APInventoryTextBox.Text);
                        newIPart.Price = Decimal.Parse(APPriceTextBox.Text);
                        newIPart.Max = Int32.Parse(APMaxTextBox.Text);
                        newIPart.Min = Int32.Parse(APMinTextBox.Text);
                        newIPart.MachineID = Int32.Parse(APMachineIDTextBox.Text);
                        Inventory.AddPart(newIPart);

                    }
                    else
                    {
                        OutsourcedPart newOPart = new OutsourcedPart();
                        newOPart.PartID = Inventory.createPartID();
                        newOPart.Name = APNameTextBox.Text;
                        newOPart.InStock = Int32.Parse(APInventoryTextBox.Text);
                        newOPart.Price = Decimal.Parse(APPriceTextBox.Text);
                        newOPart.Max = Int32.Parse(APMaxTextBox.Text);
                        newOPart.Min = Int32.Parse(APMinTextBox.Text);
                        newOPart.CompanyName = (APMachineIDTextBox.Text);
                        Inventory.AddPart(newOPart);
                    }
                }

                else
                {
                    MessageBox.Show("Your min value is greater than the max value.", "Error");
                return;
                }


            Close();
            Mainscreen p = new Mainscreen();
            p.Show();

        }

        private void APIDTextBox_TextChanged(object sender, EventArgs e)
        {
            //xx
        }

        private void VerifyNumeric(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                APInventoryTextBox.BackColor = Color.LightPink;
            }
            else 
            {
                e.Handled = false;
               APInventoryTextBox.BackColor = Color.LightGreen;
            }
        }

        private void APPriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            char ch = e.KeyChar;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Please enter a monetary value.", "Error");
                APMaxTextBox.BackColor = Color.LightPink;
           
            }
            else
            {
                e.Handled = false;
                APMaxTextBox.BackColor = Color.LightGreen;
                
            }


        }

        private void APMaxTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                APMaxTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                APMaxTextBox.BackColor = Color.LightGreen;
            }
        }

        private void APMinTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                APMinTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                APMinTextBox.BackColor = Color.LightGreen;
            }
        }

        private void APMachineIDTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (APOutsourcedRadio.Checked)
            {
                APMachineIDTextBox.BackColor = Color.LightGreen;
            }
            else
            {
                Char chr = e.KeyChar;
                if (!Char.IsDigit(chr) && chr != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please enter a numeric value.", "Error");
                    APMachineIDTextBox.BackColor = Color.LightPink;
                }
                else
                {
                    e.Handled = false;
                    APMachineIDTextBox.BackColor = Color.LightGreen;
                }

            }
        }
    }
}
