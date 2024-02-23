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
    public partial class ModifyProductForm : Form
    {
        public ModifyProductForm()
        {
            InitializeComponent();
            //Get current prod from Mainscreen selected index
            Inventory.CurrentProduct = Inventory.Products[Inventory.CurrProductIndex];

            //Populate fields for selected prod into the Modify form
            MProdIDTextBox.Text = Inventory.CurrentProduct.ProductID.ToString();
            MProdNameTextBox.Text = Inventory.CurrentProduct.Name;
            MProdPriceTextBox.Text = Inventory.CurrentProduct.Price.ToString();
            MProdInventoryTextBox.Text = Inventory.CurrentProduct.InStock.ToString();
            MProdMinTextBox.Text = Inventory.CurrentProduct.Min.ToString();
            MProdMaxTextBox.Text = Inventory.CurrentProduct.Max.ToString();

            MProdDGVParts.AutoGenerateColumns = true;
            MProdDGVAssocParts.AutoGenerateColumns = true;

            MProdDGVParts.DataSource = Inventory.AllParts;
            MProdDGVAssocParts.DataSource = Inventory.CurrentProduct.AssociatedParts;


            dgvFormatter(MProdDGVParts);
            dgvFormatter(MProdDGVAssocParts);
        }

        public static void dgvFormatter(DataGridView dgvStyle)
        {
            dgvStyle.RowHeadersVisible = false;
            dgvStyle.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
            dgvStyle.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dgvStyle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStyle.AllowUserToAddRows = false;
            dgvStyle.ReadOnly = true;
        }

        private void MProdCancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainscreen q = new Mainscreen();
            q.Show();
        }

        private void MProdDGVParts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //xx
        }

        private void MProdDGVAssocParts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //xx
        }

        private void MProdSearchButton_Click(object sender, EventArgs e)
        {
            string searchValue = MProdSearchTextBox.Text;
            foreach (DataGridViewRow row in MProdDGVParts.Rows)
            {
                if ((string)row.Cells[1].Value == searchValue)
                {
                    row.Selected = true;
                }
                else
                {
                    row.Selected = false;
                }
            }
        }

        private void MProdAddButton_Click(object sender, EventArgs e)
        {
            Inventory.CurrentProduct.AddAssociatedPart(Inventory.CurrentPart);
            MProdDGVAssocParts.DataSource = Inventory.CurrentProduct.AssociatedParts;
            MProdDGVParts.DataSource = Inventory.AllParts;
        }

        private void MProdDeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this part?",
               "Confirm", MessageBoxButtons.YesNo);
            if (Inventory.CurrAssocIndex >= 0)
            {
                if (dialogResult == DialogResult.Yes)
                {

                    Inventory.CurrentProduct.RemoveAssociatedPart(Inventory.CurrAssocIndex);

                }

                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("No Part Deleted.", "Cancel");
                }
            }

            else if (Inventory.CurrAssocIndex < 0)
            {
                MessageBox.Show("No Part Found.", "Error");
            }
            MProdDGVAssocParts.DataSource = Inventory.CurrentProduct.AssociatedParts;
        }

    private void MProdSaveButton_Click(object sender, EventArgs e)
    {
            if (Convert.ToInt32(MProdInventoryTextBox.Text) < Convert.ToInt32(MProdMinTextBox.Text) ||
                  Convert.ToInt32(MProdInventoryTextBox.Text) > Convert.ToInt32(MProdMaxTextBox.Text))
            {
                MessageBox.Show("Inventory is out of range.", "Error");
                return;
            }
            if (Convert.ToInt32(MProdMaxTextBox.Text) < Convert.ToInt32(MProdMinTextBox.Text))
            {
                        MessageBox.Show("Your min value must be less than the max value.", "Error");
            }
                else
                {
                    Inventory.CurrentProduct.ProductID = Int32.Parse(MProdIDTextBox.Text);
                    Inventory.CurrentProduct.Name = MProdNameTextBox.Text;
                    Inventory.CurrentProduct.InStock = Int32.Parse(MProdInventoryTextBox.Text);
                    Inventory.CurrentProduct.Price = Decimal.Parse(MProdPriceTextBox.Text);
                    Inventory.CurrentProduct.Max = Int32.Parse(MProdMaxTextBox.Text);
                    Inventory.CurrentProduct.Min = Int32.Parse(MProdMinTextBox.Text);

                    Inventory.UpdateProduct(Convert.ToInt32(Inventory.CurrProductIndex), Inventory.CurrentProduct);
                    this.Hide();
                    Mainscreen p = new Mainscreen();
                    p.Show();
                }
}

        private void MProdDGVParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Inventory.CurrPartIndex = e.RowIndex;
            Inventory.CurrentPart = Inventory.AllParts[Inventory.CurrPartIndex];
        }

        private void MProdDGVAssocParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           Inventory.CurrAssocIndex = e.RowIndex;
            Inventory.CurrentAssocPart = Inventory.CurrentProduct.AssociatedParts[Inventory.CurrAssocIndex];
        }

        private void MProdInventoryTextBox_TextChanged(object sender, EventArgs e)
        {
            //xx
        }

        private void MProdInventoryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                MProdInventoryTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                MProdInventoryTextBox.BackColor = Color.LightGreen;
            }
        }

        private void MProdPriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Please enter a monetary value.", "Error");
                MProdPriceTextBox.BackColor = Color.LightPink;

            }
            else
            {
                e.Handled = false;
                MProdPriceTextBox.BackColor = Color.LightGreen;

            }

        }

        private void MProdMaxTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                MProdMaxTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                MProdMaxTextBox.BackColor = Color.LightGreen;
            }
        }

        private void MProdMinTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                MProdMinTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                MProdMinTextBox.BackColor = Color.LightGreen;
            }
        }
    }
}
