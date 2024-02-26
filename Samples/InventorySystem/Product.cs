using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace InventorySystem
{
    public class Product
    {

    //Property
    public Product(int productID, string name, int inStock, decimal price,
            int max, int min)
        {
            ProductID = productID;
            Name = name;
            InStock = inStock;
            Price = price;
            Max = max;
            Min = min;

        }

        public Product() { }

        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public BindingList<Part> AssociatedParts = new BindingList<Part>();


        //METHODS

        //Add Associated Part
        public void AddAssociatedPart(Part PartObject)
        {
             AssociatedParts.Add(PartObject);
        }

        //Remove Associated Part
        public bool RemoveAssociatedPart(int xyz)
        {
            try
            {
                AssociatedParts.RemoveAt(xyz);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //Lookup Associated Part
        public Part LookupAssociatedPart(int partID)
        {
            foreach (Part part in AssociatedParts)
            {
                if (part.PartID == partID)   
                {
                    return part;
                }
            }
            return null;
        }

    }
}
 