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
    public partial class AddProductForm : Form
    {
        public int currPart { get; private set; }

        public AddProductForm()
        {
            InitializeComponent();

            Product newProd = new Product();
            Inventory.CurrentProduct = newProd;

            AProdDGVParts.AutoGenerateColumns = true;
            AProdDGVAssocParts.AutoGenerateColumns = true;

            AProdDGVParts.DataSource = Inventory.AllParts;
            AProdDGVAssocParts.DataSource = Inventory.CurrentProduct.AssociatedParts;

            dgvFormatter(AProdDGVParts);
            dgvFormatter(AProdDGVAssocParts);
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

        private void AProdCancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainscreen o = new Mainscreen();
            o.Show();

        }

        private void AProdDGVParts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           //xx
        }

        private void AProdDGVAssocParts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //xx
        }

        private void AProdSearchButton_Click(object sender, EventArgs e)
        {
            string searchValue = AProdSearchTextBox.Text;
            foreach (DataGridViewRow row in AProdDGVParts.Rows)
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

        private void AProdAddButton_Click(object sender, EventArgs e)
        {
            Inventory.CurrentProduct.AddAssociatedPart(Inventory.CurrentPart);
            AProdDGVAssocParts.DataSource = Inventory.CurrentProduct.AssociatedParts;
            AProdDGVParts.DataSource = Inventory.AllParts;
        }

        private void AProdDeleteButton_Click(object sender, EventArgs e)
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
                AProdDGVAssocParts.DataSource = Inventory.CurrentProduct.AssociatedParts;

        }

        private void AProdSaveButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(AProdInventoryTextBox.Text) < Convert.ToInt32(AProdMinTextBox.Text) ||
                  Convert.ToInt32(AProdInventoryTextBox.Text) > Convert.ToInt32(AProdMaxTextBox.Text))
            {
                MessageBox.Show("Inventory is out of range.", "Error");
                return;
            }
            if (Convert.ToInt32(AProdMaxTextBox.Text) < Convert.ToInt32(AProdMinTextBox.Text))
            {

                    MessageBox.Show("Your min value must be less than the max value.", "Error");

            }
                else
                {
                    Inventory.CurrentProduct.ProductID = Inventory.createProductID();
                    Inventory.CurrentProduct.Name = AProdNameTextBox.Text;
                    Inventory.CurrentProduct.InStock = Int32.Parse(AProdInventoryTextBox.Text);
                    Inventory.CurrentProduct.Price = Decimal.Parse(AProdPriceTextBox.Text);
                    Inventory.CurrentProduct.Max = Int32.Parse(AProdMaxTextBox.Text);
                    Inventory.CurrentProduct.Min = Int32.Parse(AProdMinTextBox.Text);

                    Inventory.AddProduct(Inventory.CurrentProduct);

                    Close();
                    Mainscreen p = new Mainscreen();
                    p.Show();
                }
        }

        private void AProdDGVParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Inventory.CurrPartIndex = e.RowIndex;
            Inventory.CurrentPart = Inventory.AllParts[Inventory.CurrPartIndex];
        }

        private void AProdDGVAssocParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Inventory.CurrAssocIndex = e.RowIndex;
            Inventory.CurrentAssocPart = Inventory.CurrentProduct.AssociatedParts[Inventory.CurrAssocIndex];
                
                //Inventory.AllParts[Inventory.CurrPartIndex];
                
                //CurrentProduct.AssociatedParts[Product.CurrAssocIndex];
        }

        private void AProdInventoryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                AProdInventoryTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                AProdInventoryTextBox.BackColor = Color.LightGreen;
            }
        }

        private void AProdPriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
                MessageBox.Show("Please enter a monetary value.", "Error");
                AProdPriceTextBox.BackColor = Color.LightPink;

            }
            else
            {
                e.Handled = false;
                AProdPriceTextBox.BackColor = Color.LightGreen;

            }
        }

        private void AProdMaxTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                AProdMaxTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                AProdMaxTextBox.BackColor = Color.LightGreen;
            }
        }

        private void AProdMinTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("Please enter a numeric value.", "Error");
                AProdMinTextBox.BackColor = Color.LightPink;
            }
            else
            {
                e.Handled = false;
                AProdMinTextBox.BackColor = Color.LightGreen;
            }
        }
    }
}
