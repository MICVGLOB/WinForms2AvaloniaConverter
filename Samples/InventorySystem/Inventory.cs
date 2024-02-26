using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace InventorySystem
{
    public class Inventory
    {
        //Properties
        public static BindingList<Product> Products = new BindingList<Product>();
        public static BindingList<Part> AllParts = new BindingList<Part>();

        public static int CurrPartIndex { get; set; }
        public static int CurrProductIndex { get; set; }

        public static int CurrAssocIndex { get; set; }

        public static Part CurrentPart { get; set; }
        public static Product CurrentProduct { get; set; }

        public static Part CurrentAssocPart { get; set; }

        
        //Auto increment part and product ID
        public static int createPartID()
        {
            int highestID = 0;
            foreach (Part p in AllParts)
            {
                if (p.PartID > highestID)
                    highestID = p.PartID;
            }
            return highestID + 1;
        }

        public static int createProductID()
        {
            int highestID = 0;
            foreach (Product p in Products)
            {
                if (p.ProductID > highestID)
                    highestID = p.ProductID;
            }
            return highestID + 1;
        }

        //PRODUCT METHODS

        //Add Product
        public static void AddProduct(Product product)
        {
            Products.Add(product);
        }

        //Remove Product
        public static bool RemoveProduct(int index)
        {
            Products.RemoveAt(index);

            return true;

        }
        //Lookup Product
        public static Product LookupProduct(int productID)
        {
            foreach (Product prod in Products)
            {
                if (prod.ProductID == productID)
                {
                    return prod;
                }
            }
            return null;
        }

        //Update Product
        public static void UpdateProduct(int productID, Product updatedProduct)
        {
            Inventory.RemoveProduct(CurrProductIndex);
            Inventory.AddProduct(CurrentProduct);

        }

        //PARTS METHODS-----------------

        //Add Part
        public static void AddPart(Part part)
        {
            AllParts.Add(part);
        }

        //Remove Part
        public static bool DeletePart(Part part)
        {
            try
            {
                AllParts.Remove(part);
                return true;
            }
            catch
            {
                return false;
            }

        }

        //Lookup Part
        public static Part LookupPart(int partID)
        {
            foreach (Part part in AllParts)
            {
                if (part.PartID == partID)
                {
                    return part;
                }
            }
            //MessageBox.Show("Part was not found.");
            return null;

        }

        public static void UpdatePart(int partID, Part part)
        {

            Inventory.DeletePart(part);
            Inventory.AddPart(part);

        }

    }
}
