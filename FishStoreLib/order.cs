using ConsoleTables;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishStoreLib
{
    public class order
    {
        #region later use
        //  public List<Fish> Inventory { get; set; }

        //  public List<Fish> ShoppingCart { get; set; }

        /*  public store()
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
                  .Write(Format.Alternative);   
          } */
        #endregion

        public void OrderFromStore(JArray DataArray, Location bu, string type)
        {

            int selectedId = int.Parse(Console.ReadLine());
            foreach (var y in DataArray)
            {
                int id = Convert.ToInt32(y["id"]);

                if (id == selectedId)
                {
                    string type1 = type;
                    Console.WriteLine(type1);
                    Console.WriteLine($"You bought {y["Name"]} which cost you {y["Price"]}");
                    int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]);
                    Console.WriteLine("Enter the Quantity Of order in number.(Upto 10)");
                    int UserInputQuantity = int.Parse(Console.ReadLine());
                    if (updatedAvailableQuantity == 0)
                    {
                        Console.WriteLine("OUT OF STOCK");
                    }
                    else if (UserInputQuantity < 11 && UserInputQuantity < updatedAvailableQuantity)
                    {
                        Console.WriteLine("Your order is placed  successfully.");
                        int updatedAfterQuantity = Convert.ToInt32(y["availableQuantity"]) - UserInputQuantity;
                        bu.saveData(updatedAfterQuantity, selectedId, type1);
                    }
                    else if (UserInputQuantity > 11)
                    {
                        Console.WriteLine("You can Only buy upto 10 products");
                        Console.WriteLine("Enter the Quantity Of order in number");
                        UserInputQuantity = int.Parse(Console.ReadLine());
                        Console.WriteLine("Your order is placed  successfully.");
                        int updatedAfterQuantity = Convert.ToInt32(y["availableQuantity"]) - UserInputQuantity;
                        bu.saveData(updatedAfterQuantity, selectedId, type1);
                    }


                    break;
                }
            }

        }
    }
}
