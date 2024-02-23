using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem
{
    public class OutsourcedPart : Part
    {
        public string CompanyName { get; set; }

        public OutsourcedPart() { }

        public OutsourcedPart(int partID, string name, decimal price, int inStock, int max, int min, string companyName)
            : base (partID, name, price, inStock, max, min)
        {
           CompanyName = companyName;
        }
    }
}
