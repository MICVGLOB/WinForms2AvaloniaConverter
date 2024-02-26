using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem
{
    public class InhousePart : Part
    {
        public int MachineID { get; set; }

        public InhousePart() { }

        public InhousePart(int partID, string name, decimal price, int inStock, int max, int min, int machineID)
        : base(partID, name, price, inStock, max, min)
        {
           MachineID = machineID;
        }
    }
}
