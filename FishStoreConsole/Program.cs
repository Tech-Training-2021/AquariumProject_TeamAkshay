using FishStoreLib;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Linq;
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
            Console.Clear();
            Console.Write("1.Mumbai\n2.Delhi\n3.Bangalore\n4.Exit\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
            switch (chooseoption)
            {
                case 1:
                    productsFromMumbai();
                    break;
                case 2: 
                    productsFromDelhi();
                    break;
                case 3: 
                    productsFromBanglore();
                    break;
                default:
                    Main();
                    break;
            }

        }

        public static void productsFromMumbai()
        {
            Console.Clear();
            Location mum = new Location();
            mum.accessfilePath = @"D:\AquariumProject_TeamAkshay\FishStoreLib\mumbaiData.json";
            mum.fetchDetails();

           menuWithinLocation(mum);
        }
        public static void productsFromDelhi()
        {
            Console.Clear();
            Location del = new Location();
            del.accessfilePath = @"D:\AquariumProject_TeamAkshay\FishStoreLib\delhiData.json";
            del.fetchDetails();
            menuWithinLocation(del);
        }

        public static void productsFromBanglore()
        {
            Console.Clear();
            Location ben = new Location();
            ben.accessfilePath = @"D:\AquariumProject_TeamAkshay\FishStoreLib\BangloreData.json";
            ben.fetchDetails();
            menuWithinLocation(ben);
        }

        public static void menuWithinLocation(Location obj)
        {
            Console.Clear();
            Console.Write("1.Buy pet fish\n2.Buy fish food\n3.Buy tank\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
            switch (chooseoption)
            {
                case 1:
                    FishFromLocation(obj.fishDataArray, obj);
                    break;
                case 2:
                    FoodFromLocation(obj.fishFoodArray, obj);
                    break;
                case 3:
                    TankFromLocation(obj.fishTankArray, obj);
                    break;
                default:
                    chooseLocation();
                    break;
            }
        }

        public static void FishFromLocation(JArray fishDataArray, Location obj)
        {
            string type = "fish";
            Console.Clear();
            var table = new ConsoleTable("Id", "Fish Name", "Price", "Available Quantity");
            foreach (var x in fishDataArray)
            {
                table.AddRow(x["id"], x["Name"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy fish : ");
            
            order or = new order();
            or.OrderFromStore(fishDataArray, obj, type);
            ContinueOrExitLocation(obj);
        }

        public static void FoodFromLocation(JArray foodDataArray, Location obj)
        {
            Console.Clear();
            string type = "food";
            var table = new ConsoleTable("id", "Food", "Price", "Available Quantity");
            foreach (var x in foodDataArray)
            {
                table.AddRow(x["id"], x["food"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy food : ");
            order or = new order();
            or.OrderFromStore(foodDataArray, obj, type);
            ContinueOrExitLocation(obj);


        }

        public static void TankFromLocation(JArray tankDataArray, Location obj)
        {
            Console.Clear();
            string type = "tank";

            var table = new ConsoleTable("id", "name", "price", "Available Quantity");
            foreach (var x in tankDataArray)
            {
                table.AddRow(x["id"], x["Name"], x["Price"], x["availableQuantity"]);
            }
            table.Write();
            Console.Write("Choose id to buy tank : ");
            order or = new order();
            or.OrderFromStore(tankDataArray, obj, type);
            ContinueOrExitLocation(obj);
        }
        
        static void Menu()
        {
            Console.Clear();    
            Console.WriteLine("1. Add\n2. Get Customer\n3. Update\n4. Delete\n5. Search a Customer\n6. Exit");
            Console.Write("Choose an option :  ");
            var input = Console.ReadLine();
            PerformSelectedAction(input);
        }

        const string xmlfile = @"D:\AquariumProject_TeamAkshay\FishStoreLib\CustomerDetails1.xml"; 
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
                    Console.Write("Customer ID : " + _id + "\n");
                    Console.Write("\nCustomer Name : ");
                    var name = Console.ReadLine();
                    Console.Write("\nCustomer Email : ");
                    var mail = Console.ReadLine();
                    Console.Write("\nCustomer Password : ");
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
                    Console.Write("Customer ID : ");
                   var  id1 = Console.ReadLine();
                    Console.Write("\nCustomer Email : ");
                     mail = Console.ReadLine();
                    Console.Write("\nCustomer Password : ");
                     pass = Console.ReadLine();
                    cus.updateCustomername(id1, mail , pass);
                    ContinueOrExit();
                    break;
                case "4":
                    Console.Clear();
                    Console.Write("Customer ID : ");
                    id1 = Console.ReadLine();
                    cus.deleteCustomer(id1);

                    ContinueOrExit();
                    break;
                case "5":
                    Console.Write("Customer Name : ");
                    name = Console.ReadLine();
                    cus.SearchCustomer(name);
                    ContinueOrExit();
                    break; 

                default: Console.WriteLine("Exited successfully. "); Main(); break;
            }
        }

        static void ContinueOrExit()
        {
            Console.Write("\nDo you want to continue? y/n :\t");
            var result = Console.ReadLine();
            if (result == "y" || result == "Y") Menu();
            else Main();
        }

        static void ContinueOrExitLocation(Location obj)
        {
            Console.Write("\nDo you want to continue? y/n :\t");
            var result = Console.ReadLine();
            if (result == "y" || result == "Y") menuWithinLocation(obj);
            else chooseLocation();
        }

    }
}
