using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{
    public partial class ModifyPartForm : Form
    {
        public ModifyPartForm()
        {
            InitializeComponent();
            //Get current part from Mainscreen selected index
            Inventory.CurrentPart = Inventory.AllParts[Inventory.CurrPartIndex];

                MPIDTextBox.Text = Inventory.CurrentPart.PartID.ToString();
                MPNameTextBox.Text = Inventory.CurrentPart.Name;
                MPPriceTextBox.Text = Inventory.CurrentPart.Price.ToString();
                MPInventoryTextBox.Text = Inventory.CurrentPart.InStock.ToString();
                MPMinTextBox.Text = Inventory.CurrentPart.Min.ToString();
                MPMaxTextBox.Text = Inventory.CurrentPart.Max.ToString();
             
            
            if (Inventory.CurrentPart is InhousePart)
            {
                MPMachineIDLabel.Text = "Machine ID";
                MPInhouseRadio.Checked = true;
                MPMachineIDTextBox.Text = ((InhousePart)Inventory.CurrentPart).MachineID.ToString();

            }
            else 
            {
                MPMachineIDLabel.Text = "Company Name";
                MPOutsourcedRadio.Checked = true;
                MPMachineIDTextBox.Text = ((OutsourcedPart)Inventory.CurrentPart).CompanyName;

            }
        }

        private void MPCancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainscreen o = new Mainscreen();
            o.Show();
        }

        private void MPInhouseRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (Inventory.CurrentPart is InhousePart)
            {
                MPMachineIDTextBox.Text = ((InhousePart)Inventory.CurrentPart).MachineID.ToString();
            }
            MPMachineIDLabel.Text = "Machine ID";
        }

        private void MPOutsourcedRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (Inventory.CurrentPart is OutsourcedPart)
            {
                MPMachineIDTextBox.Text = ((OutsourcedPart)Inventory.CurrentPart).CompanyName.ToString();
            }
            MPMachineIDLabel.Text = "Company Name";
        }

        private void MPSaveButton_Click(object sender, EventArgs e)
        {
            InhousePart IP = new InhousePart();
            OutsourcedPart OP = new OutsourcedPart();

            if (Convert.ToInt32(MPInventoryTextBox.Text) < Convert.ToInt32(MPMinTextBox.Text) ||
                  Convert.ToInt32(MPInventoryTextBox.Text) > Convert.ToInt32(MPMaxTextBox.Text))
            {
                MessageBox.Show("Inventory is out of range.", "Error");
                return;
            }
            if (Convert.ToInt32(MPMaxTextBox.Text) > Convert.ToInt32(MPMinTextBox.Text))
                {
                    if (MPInhouseRadio.Checked)
                    {
                        IP.PartID = Int32.Parse(MPIDTextBox.Text);
                        IP.Name = MPNameTextBox.Text;
                        IP.InStock = Int32.Parse(MPInventoryTextBox.Text);
                        IP.Price = Decimal.Parse(MPPriceTextBox.Text);
                        IP.Max = Int32.Parse(MPMaxTextBox.Text);
                        IP.Min = Int32.Parse(MPMinTextBox.Text);
                        IP.MachineID = Int32.Parse(MPMachineIDTextBox.Text);

                    Inventory.UpdatePart(Convert.ToInt32(IP.PartID), IP);

                    }
                    else
                    {                 
                        OP.PartID = Int32.Parse(MPIDTextBox.Text);
                        OP.Name = MPNameTextBox.Text;
                        OP.InStock = Int32.Parse(MPInventoryTextBox.Text);
                        OP.Price = Decimal.Parse(MPPriceTextBox.Text);
                        OP.Max = Int32.Parse(MPMaxTextBox.Text);
                        OP.Min = Int32.Parse(MPMinTextBox.Text);
                        OP.CompanyName = (MPMachineIDTextBox.Text);

                    Inventory.UpdatePart(Convert.ToInt32(OP.PartID), OP);

                    }

                    Inventory.DeletePart(Inventory.CurrentPart);

                    this.Hide();
                    Mainscreen o = new Mainscreen();
                    o.Show();
                }
                else
                {
                    MessageBox.Show("Your min value is greater than the max value.", "Error");
                }


        }

        private void MPInventoryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                MPInventoryTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                MPInventoryTextBox.BackColor = Color.LightGreen;
            }
        }

        private void MPMaxTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                MPMaxTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                MPMaxTextBox.BackColor = Color.LightGreen;
            }
        }

        private void MPMinTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                MPMinTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                MPMinTextBox.BackColor = Color.LightGreen;
            }
        }

        private void MPPriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Please enter a monetary value.", "Error");
                MPPriceTextBox.BackColor = Color.LightPink;

            }
            else
            {
                e.Handled = false;
                MPPriceTextBox.BackColor = Color.LightGreen;

            }
        }

        private void MPMachineIDTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MPOutsourcedRadio.Checked)
            {
                MPMachineIDTextBox.BackColor = Color.LightGreen;
            }
            else
            {
                Char chr = e.KeyChar;
                if (!Char.IsDigit(chr) && chr != 8)
                {
                    e.Handled = true;
                    MessageBox.Show("Please enter a numeric value.", "Error");
                    MPMachineIDTextBox.BackColor = Color.LightPink;
                }
                else
                {
                    e.Handled = false;
                    MPMachineIDTextBox.BackColor = Color.LightGreen;
                }

            }
        }

        private void MPNameTextBox_TextChanged(object sender, EventArgs e)
        {
            //xx
        }
    }
}

