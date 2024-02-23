using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //// Populate the parts and product lists
            Inventory.AddProduct(new Product(0, "Red Bike", 15, 11.44M, 25, 1));
            Inventory.AddProduct(new Product(1, "Yellow Bike", 19, 9.66M, 20, 1));
            Inventory.AddProduct(new Product(2, "Blue Bike", 5, 12.77M, 25, 1));

            Inventory.AddPart(new InhousePart(0, "Wheel", 12.11M, 15, 25, 5, 4571)); //machine id
            Inventory.AddPart(new OutsourcedPart(1, "Pedal", 8.22M, 11, 25, 5, "Pedal Company")); //company name
            Inventory.AddPart(new InhousePart(2, "Chain", 8.33M, 12, 25, 5, 8647)); //machine id
            Inventory.AddPart(new OutsourcedPart(3, "Seat", 4.55M, 8, 25, 5, "Bikes For All, Inc.")); //company name
            //end populate list

         

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Mainscreen());
        }
    }
}
