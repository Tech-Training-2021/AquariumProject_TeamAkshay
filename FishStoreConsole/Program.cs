using FishStoreLib;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConsoleTables;

namespace FishStoreConsole
{
  public  class Program
    {
      public  static void Main()
        {
            Console.Clear();
          //  store s = new store();
          //  customer cus = new customer();
            
            Console.WriteLine("-------------------------------------      WELCOME TO OUR FISH STORE.       ---------------------------------------");

            Console.Write("1.customer Information\n2.Choose store Location\n3.Exit\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
             switch (chooseoption)
             {
                 case 1: 
                    Menu(); 
                    break;
                 case 2:
                    chooseLocation();
                    break;
                default:
                    Console.WriteLine("-------------------------------------        You have exited successfully.      ---------------------------------------");
                    Console.WriteLine("-------------------------------------    THANKS FOR VISITING OUR AQUARIUM STORE.    -----------------------------------"); break;
             }
        }

        public static void chooseLocation()
        {
            Console.Write("1.Mumbai\n2.Delhi\n3.Bangalore\n4.Exit\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
            switch (chooseoption)
            {
                case 1:
                    productsFromMumbai();
                    break;
                case 2: productsFromDelhi();
                    break;
                case 3: productsFromBanglore();
                    break;
                default:
                    Main();
                    break;
            }

        }

        public static void productsFromMumbai()
        {
            //string filePath = @"C:\dotnet\Project\FishStoreLib\FishStoreConsole\mumbaiData.json";
            //string fishJson=File.ReadAllText(filePath);

            //var jObject = JObject.Parse(fishJson);
            //var fishDataArray = jObject.GetValue("fishData") as JArray;
            //var fishFoodArray = jObject.GetValue("fishFood") as JArray;
            //var fishTankArray = jObject.GetValue("fishTank") as JArray;
            //Console.WriteLine(fishDataArray);

            Mumbai mu = new Mumbai();
            mu.fetchDetails();
            //Console.WriteLine(mu.fishDataArray);
            menuWithinLocation(mu);
        }
        public static void productsFromDelhi()
        {
            Delhi del = new Delhi();
            del.fetchDetails();
            menuWithinLocation(del);
        }

        public static void productsFromBanglore()
        {
            Banglore bu = new Banglore();
            bu.fetchDetails();
            menuWithinLocation(bu);
        }


        public static void menuWithinLocation(Banglore bu)
        {
            Console.Write("1.Buy pet fish\n2.Buy fish food\n3.Buy tank\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
            switch (chooseoption)
            {
                case 1:
                    FishFromBanglore(bu.fishDataArray, bu);
                    break;
                case 2:
                    FoodFromBanglore(bu.fishFoodArray, bu);
                    break;
                case 3:
                    TankFromBanglore(bu.fishTankArray, bu);
                    break;
                default:
                    chooseLocation();
                    break;
            }
        }
        public static void FishFromBanglore(JArray fishDataArray, Banglore bu)
        {
            string type = "fish";


            var table = new ConsoleTable("Id", "Fish Name", "Price", "Available Quantity");
            foreach (var x in fishDataArray)
            {
                table.AddRow(x["id"], x["Name"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy fish : ");
            int selectedId = int.Parse(Console.ReadLine());
            foreach (var y in fishDataArray)
            {
                int id = Convert.ToInt32(y["id"]);

                if (id == selectedId)
                {
                    Console.WriteLine($"You bought {y["Name"]} which cost you {y["Price"]}");
                    int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]) - 1;
                    Console.WriteLine("Updated Quantity " + updatedAvailableQuantity);
                    bu.saveData(updatedAvailableQuantity, selectedId, type);
                    ContinueOrExitLocation(bu);
                    break;
                }
            }

        }

        public static void FoodFromBanglore(JArray foodDataArray, Banglore bu)
        {
            string type = "food";
            var table = new ConsoleTable("id", "Food", "Price", "Available Quantity");
            foreach (var x in foodDataArray)
            {
                table.AddRow(x["id"], x["food"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy food : ");
            int selectedId = int.Parse(Console.ReadLine());
            foreach (var y in foodDataArray)
            {
                int id = Convert.ToInt32(y["id"]);

                if (id == selectedId)
                {
                    Console.WriteLine($"You bought {y["food"]} which cost you {y["Price"]}");
                    int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]) - 1;
                    //Console.WriteLine("Updated Quantity " + updatedAvailableQuantity);
                    bu.saveData(updatedAvailableQuantity, selectedId, type);
                    ContinueOrExitLocation(bu);
                    break;
                }
            }


        }


        public static void TankFromBanglore(JArray tankDataArray, Banglore bu)
        {
            string type = "tank";

            var table = new ConsoleTable("id", "name", "price", "Available Quantity");
            foreach (var x in tankDataArray)
            {
                table.AddRow(x["id"], x["Name"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy tank : ");
            int selectedId = int.Parse(Console.ReadLine());
            foreach (var y in tankDataArray)
            {
                int id = Convert.ToInt32(y["id"]);

                if (id == selectedId)
                {
                    Console.WriteLine($"You bought {y["Name"]} which cost you {y["Price"]}");
                    int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]);
                    Console.WriteLine("Enter the Quantity Of order in number");
                    int UserInputQuantity = int.Parse(Console.ReadLine());
                    if(UserInputQuantity<11 && UserInputQuantity<updatedAvailableQuantity)
                    {
                        Console.WriteLine("Added to your order.Here is your Purchase data");
                        int updatedAfterQuantity = Convert.ToInt32(y["availableQuantity"]) - UserInputQuantity;
                        bu.saveData(updatedAfterQuantity, selectedId, type);
                    }
                    else if(UserInputQuantity>11 || UserInputQuantity>updatedAvailableQuantity)
                    {
                        Console.WriteLine("Invalid Input/Out of Stock");
                    }
                    //int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]) - 1;
                    //Console.WriteLine("Updated Quantity " + updatedAvailableQuantity);
                   // bu.saveData(updatedAfterQuantity, selectedId, type);
                    ContinueOrExitLocation(bu);
                    break;
                }
            }
        }

        public static void menuWithinLocation(Mumbai mu)
        {
            Console.Write("1.Buy pet fish\n2.Buy fish food\n3.Buy tank\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
            switch (chooseoption)
            {
                case 1:
                    FishFromMumbai(mu.fishDataArray, mu);
                    break;
                case 2:
                    FoodFromMumbai(mu.fishFoodArray, mu);
                    break;
                case 3:
                    TankFromMumbai(mu.fishTankArray, mu);
                    break;
                default:
                    chooseLocation();
                    break;
            }
        }
        public static void FishFromMumbai(JArray fishDataArray, Mumbai mu)
        {
            string type = "fish";


            var table = new ConsoleTable("Id", "Fish Name", "Price", "Available Quantity");
            foreach (var x in fishDataArray)
            {
                table.AddRow(x["id"], x["Name"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy fish : ");
            int selectedId = int.Parse(Console.ReadLine());
            foreach (var y in fishDataArray)
            {
                int id = Convert.ToInt32(y["id"]);

                if (id == selectedId)
                {
                    Console.WriteLine($"You bought {y["Name"]} which cost you {y["Price"]}");
                    int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]) - 1;
                    Console.WriteLine("Updated Quantity " + updatedAvailableQuantity);
                    mu.saveData(updatedAvailableQuantity, selectedId, type);
                    ContinueOrExitLocation(mu);
                    break;
                }
            }

        }

        public static void FoodFromMumbai(JArray foodDataArray, Mumbai mu)
        {
            string type = "food";
            var table = new ConsoleTable("id", "Food", "Price", "Available Quantity");
            foreach (var x in foodDataArray)
            {
                table.AddRow(x["id"], x["food"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy food : ");
            int selectedId = int.Parse(Console.ReadLine());
            foreach (var y in foodDataArray)
            {
                int id = Convert.ToInt32(y["id"]);

                if (id == selectedId)
                {
                    Console.WriteLine($"You bought {y["food"]} which cost you {y["Price"]}");
                    int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]) - 1;
                    //Console.WriteLine("Updated Quantity " + updatedAvailableQuantity);
                    mu.saveData(updatedAvailableQuantity, selectedId, type);
                    ContinueOrExitLocation(mu);
                    break;
                }
            }


        }


        public static void TankFromMumbai(JArray tankDataArray, Mumbai mu)
        {
            string type = "tank";

            var table = new ConsoleTable("id", "name", "price", "Available Quantity");
            foreach (var x in tankDataArray)
            {
                table.AddRow(x["id"], x["Name"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy tank : ");
            int selectedId = int.Parse(Console.ReadLine());
            foreach (var y in tankDataArray)
            {
                int id = Convert.ToInt32(y["id"]);

                if (id == selectedId)
                {
                    Console.WriteLine($"You bought {y["Name"]} which cost you {y["Price"]}");
                    int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]) - 1;
                    //Console.WriteLine("Updated Quantity " + updatedAvailableQuantity);
                    mu.saveData(updatedAvailableQuantity, selectedId, type);
                    ContinueOrExitLocation(mu);
                    break;
                }
            }
        }
        public static void menuWithinLocation(Delhi del)
        {
            Console.Write("1.Buy pet fish\n2.Buy fish food\n3.Buy tank\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
            switch (chooseoption)
            {
                case 1:
                    FishFromDelhi(del.fishDataArray, del);
                    break;
                case 2:
                    FoodFromDelhi(del.fishFoodArray, del);
                    break;
                case 3:
                    TankFromDelhi(del.fishTankArray, del);
                    break;
                default:
                    chooseLocation();
                    break;
            }
        }
       /* public static bool checkQuantity(int quantity)
        {
            if(quantity >1 && quantity <= 10)
            {
                newQuantity = quantity;
                return true;
            }
            else
            {
                Console.WriteLine("You can make purchase of maximum 10 fishes \n Please enter quantity between 1 to 10 ");
                quantity = int.Parse(Console.ReadLine());
                checkQuantity(quantity);
                return true;
            }
        }*/
        public static void FishFromDelhi(JArray fishDataArray, Delhi del)
        {
            string type = "fish";


            var table = new ConsoleTable("Id", "Fish Name", "Price", "Available Quantity");
            foreach (var x in fishDataArray)
            {
                table.AddRow(x["id"], x["Name"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy fish : ");
            int selectedId = int.Parse(Console.ReadLine());
            //Console.Write("Enter the number of fish you want - ");
            //int quantity = int.Parse(Console.ReadLine());
            //if (checkQuantity(quantity) )
            //{
 
                foreach (var y in fishDataArray)
                {
                    int id = Convert.ToInt32(y["id"]);

                    if (id == selectedId)
                    {
                        Console.WriteLine($"You bought {y["Name"]} which cost you {y["Price"]}");
                        int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]) - 1;
                        Console.WriteLine("Updated Quantity " + updatedAvailableQuantity);
                        del.saveData(updatedAvailableQuantity, selectedId, type);
                        ContinueOrExitLocation(del);
                        break;
                    }
                }
            //}
            /*else
            {
                Console.WriteLine("You can make purchase of maximum 10 fishes \n Please enter quantity between 1 to 10 ");
                quantity = int.Parse(Console.ReadLine());
                checkQuantity(quantity);
            }*/

        }
        public static void FoodFromDelhi(JArray foodDataArray, Delhi del)
        {
            string type = "food";
            var table = new ConsoleTable("id", "Food", "Price", "Available Quantity");
            foreach (var x in foodDataArray)
            {
                table.AddRow(x["id"], x["food"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy food : ");
            int selectedId = int.Parse(Console.ReadLine());
            foreach (var y in foodDataArray)
            {
                int id = Convert.ToInt32(y["id"]);

                if (id == selectedId)
                {
                    Console.WriteLine($"You bought {y["food"]} which cost you {y["Price"]}");
                    int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]) - 1;
                    //Console.WriteLine("Updated Quantity " + updatedAvailableQuantity);
                    del.saveData(updatedAvailableQuantity, selectedId, type);
                    ContinueOrExitLocation(del);
                    break;
                }
            }


        }


        public static void TankFromDelhi(JArray tankDataArray, Delhi del)
        {
            string type = "tank";

            var table = new ConsoleTable("id", "name", "price", "Available Quantity");
            foreach (var x in tankDataArray)
            {
                table.AddRow(x["id"], x["Name"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy tank : ");
            int selectedId = int.Parse(Console.ReadLine());
            foreach (var y in tankDataArray)
            {
                int id = Convert.ToInt32(y["id"]);

                if (id == selectedId)
                {
                    Console.WriteLine($"You bought {y["Name"]} which cost you {y["Price"]}");
                    int updatedAvailableQuantity = Convert.ToInt32(y["availableQuantity"]) - 1;
                    //Console.WriteLine("Updated Quantity " + updatedAvailableQuantity);
                    del.saveData(updatedAvailableQuantity, selectedId, type);
                    ContinueOrExitLocation(del);
                    break;
                }
            }
        }

        static void Menu()
        {
            Console.WriteLine("1. Add\n2. Get Details\n3. Update\n4. Delete\n5. Search a Customer\n6. Exit");
            Console.Write("Choose an option :  ");
            var input = Console.ReadLine();
            PerformSelectedAction(input);
        }

        const string xmlfile = @"D:\Fish_Store\FishStoreLib\FishStoreLib\CustomerDetails1.xml"; 
        static void PerformSelectedAction(string input)
        {
            customer cus = new customer();
            switch (input)
            {
                case "1": Console.Clear();
                    
                    //var id = Console.ReadLine();
                    int _id = 0;
                    var xdoc = XDocument.Load(xmlfile);
                    var getcust = xdoc.Root.Descendants("CUSTOMER").Select(x => new customer(int.Parse(x.Attribute("id").Value), x.Element("NAME").Value, x.Element("EMAIL").Value, x.Element("PASS").Value));
                    foreach (var m in getcust)
                    {
                        _id = m.id;
                    }
                    _id++;
                    Console.WriteLine("Customer ID : " + _id);
                    Console.WriteLine("Customer Name : ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Customer Email : ");
                    var mail = Console.ReadLine();
                    Console.WriteLine("Customer Password : ");
                    var pass = Console.ReadLine();
                    cus.AddCustomer(new customer(_id, name , mail, pass));
                    ContinueOrExit();
                    break;

                case "2":
                    Console.Clear();
                    cus.GetCustomer();
                    ContinueOrExit();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Customer ID : ");
                   var  id1 = Console.ReadLine();
                    Console.WriteLine("Customer Email : ");
                     mail = Console.ReadLine();
                    Console.WriteLine("Customer Password : ");
                     pass = Console.ReadLine();
                    cus.updateCustomername(id1, mail , pass);
                    ContinueOrExit();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Customer ID : ");
                    id1 = Console.ReadLine();
                    cus.deleteCustomer(id1);

                    ContinueOrExit();
                    break;
                case "5":
                    Console.WriteLine("Customer Name : ");
                    name = Console.ReadLine();
                    cus.SearchCustomer(name);
                    ContinueOrExit();
                    break; 

                default: Console.WriteLine("Exited successfully. "); Main(); break;
            }
        }

        static void ContinueOrExit()
        {
            Console.WriteLine("Do you want to continue? y/n");
            var result = Console.ReadLine();
            if (result == "y" || result == "Y") Menu();
            else Main();
        }

        static void ContinueOrExitLocation(Mumbai mu)
        {
            Console.WriteLine("Do you want to continue? y/n");
            var result = Console.ReadLine();
            if (result == "y" || result == "Y") menuWithinLocation(mu);
            else chooseLocation();
        }
        static void ContinueOrExitLocation(Delhi del)
        {
            Console.WriteLine("Do you want to continue? y/n");
            var result = Console.ReadLine();
            if (result == "y" || result == "Y") menuWithinLocation(del);
            else chooseLocation();
        }
        static void ContinueOrExitLocation(Banglore bu)
        {
            Console.WriteLine("Do you want to continue? y/n");
            var result = Console.ReadLine();
            if (result == "y" || result == "Y") menuWithinLocation(bu);
            else chooseLocation();
        }

    }
}
