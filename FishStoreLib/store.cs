using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishStoreLib
{
    public class store : Fish
    {
        public List<Fish> Inventory { get; set; }

        public List<Fish> ShoppingCart { get; set; }

        public store()
        {
            Inventory = new List<Fish>();
            ShoppingCart = new List<Fish>();
        }

        public decimal Check_out()
        {   // initialize value of total cost
            decimal totalCost = 0.00m;

            foreach( var c in ShoppingCart)
            {
                totalCost += c.Price + c.foodPrice + c.TankPrice;
            }
            ShoppingCart.Clear();
            return totalCost;
        }
        
        public void orderSummary(Fish f , decimal cost)
        {
            Console.Clear();
            Console.WriteLine("Your Purchase is as following : \n\n");
            var table = new ConsoleTable("Fish type", "Fish food", "Fish tank" , "Total Cost");
            table.AddRow( f.fishName,f.fishFood, f.fishTank,cost);

            table.Write();
           // Console.WriteLine();

            Console.WriteLine("Thanks for Shopping with us.\n Please visit us again.\n HAVE A NICE DAY AHEAD :)");
           /* var rows = Enumerable.Repeat(new Something(), 10);

            ConsoleTable
                .From<Something>(rows)
                .Configure(o => o.NumberAlignment = Alignment.Right)
                .Write(Format.Alternative);   */
        }
    
    }
}
