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
            
          //  store s = new store();
          //  customer cus = new customer();
            
            Console.WriteLine("-------------------------------------      WELCOME TO OUR FISH STORE.       ---------------------------------------");

            Console.Write("1.customer Information\n2.Choose store Location\n3.Exit\nChoose one option :   ");
            try
            {
            int chooseoption = int.Parse(Console.ReadLine());
             switch (chooseoption)
             {
                 case 1: Console.Clear();
                    Menu(); 
                    break;
                 case 2: Console.Clear();
                    chooseLocation();
                    break;
                default:
                    Console.WriteLine("-------------------------------------        You have exited successfully.      ---------------------------------------");
                    Console.WriteLine("-------------------------------------    THANKS FOR VISITING OUR AQUARIUM STORE.    -----------------------------------"); break;
             }
            }
           catch (FormatException )
            {
                Console.WriteLine("The entered option is not valid. Please enter a valid option\n");
                Main();
                 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unexpected error: { ex.Message }");
                
            }


        }

        public static void chooseLocation()
        {
            try{
            Console.Write("1.Mumbai\n2.Delhi\n3.Bangalore\n4.Exit\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
            switch (chooseoption)
            {
                case 1: Console.Clear();
                    productsFromMumbai();
                    break;
                case 2: Console.Clear();
                    productsFromDelhi();
                    break;
                case 3: Console.Clear();
                    productsFromBanglore();
                    break;
                default: Console.Clear();
                    Main();
                    break;
            }
            }
            catch (FormatException )
            {
                Console.WriteLine("The entered option is not valid. Please enter again."); 
                chooseLocation();
                 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unexpected error: { ex.Message }");
                
            }
            

        }

        public static void productsFromMumbai()
        {
        
            Location mum = new Location();
            mum.accessfilePath = @"C:\Users\user\Project_FishStore\AquariumProject_TeamAkshay\FishStoreLib\mumbaiData.json";
            mum.fetchDetails();
            Console.Clear();
           menuWithinLocation(mum);
        }
        public static void productsFromDelhi()
        {
            Location del = new Location();
            del.accessfilePath = @"C:\Users\user\Project_FishStore\AquariumProject_TeamAkshay\FishStoreLib\delhiData.json";
            del.fetchDetails();
            Console.Clear();
            menuWithinLocation(del);
        }

        public static void productsFromBanglore()
        {
            Location ben = new Location();
            ben.accessfilePath = @"C:\Users\user\Project_FishStore\AquariumProject_TeamAkshay\FishStoreLib\BangloreData.json";
            ben.fetchDetails();
            Console.Clear();
            menuWithinLocation(ben);
        }

        public static void menuWithinLocation(Location obj)
        {
            try
            {
            Console.Write("1.Buy pet fish\n2.Buy fish food\n3.Buy tank\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
            switch (chooseoption)
            {
                case 1: Console.Clear();
                    FishFromLocation(obj.fishDataArray, obj);
                    break;
                case 2: Console.Clear();
                    FoodFromLocation(obj.fishFoodArray, obj);
                    break;
                case 3: Console.Clear();
                    TankFromLocation(obj.fishTankArray, obj);
                    break;
                default: Console.Clear();
                    chooseLocation();
                    break;
            }
            }
            catch (FormatException )
            {
                Console.WriteLine("The entered option is not valid.");
                 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unexpected error: { ex.Message }");
                
            }
        }

        public static void FishFromLocation(JArray fishDataArray, Location obj)
        {
            string type = "fish";
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
            Console.WriteLine("1. Add\n2. Get Customer\n3. Update\n4. Delete\n5. Search a Customer\n6. Exit");
            Console.Write("Choose an option :  ");
            try
            {
            var input = Console.ReadLine();
            PerformSelectedAction(input);
            }
            catch (FormatException )
            {
                Console.WriteLine("The entered option is not valid.");
                Menu();
                 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unexpected error: { ex.Message }");
                
            }
        }

        const string xmlfile = @"C:\Users\user\Project_FishStore\AquariumProject_TeamAkshay\FishStoreLib\CustomerDetails1.xml"; 
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
                    var statusName= Validation.IsNameNull(name);
                    if(statusName == true)
                    {
                        Console.WriteLine("Name can't be empty! Please Input your name once more");
                        name = Console.ReadLine();
                    }
                    Console.Write("\nCustomer Email : ");
                    var mail = Console.ReadLine();
                    var status= Validation.IsValidAddress(mail);
                    if(status == false)
                    {
                        Console.WriteLine("Invalid Email Address! please Input your email once more");
                        mail = Console.ReadLine();
                    }
                    Console.Write("\nCustomer Password : ");
                    var pass = Console.ReadLine();
                    var statusPass = Validation.IsValidPassword(pass);
                    if(statusPass == false)
                    {
                        Console.WriteLine("Password must contains at least one lower case, one upper case, one number and special character.");
                        pass = Console.ReadLine();
                    }
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
                    var id1 = Console.ReadLine();
                    Console.Write("\nCustomer Email : ");
                    mail = Console.ReadLine();
                    var statusEmail = Validation.IsValidAddress(mail);
                    if(statusEmail == false)
                    {
                        Console.WriteLine("Invalid Email Address! please Input your email once more");
                        mail = Console.ReadLine();
                    }
                    Console.Write("\nCustomer Password : ");
                    pass = Console.ReadLine();
                    var statusPassword = Validation.IsValidPassword(pass);
                    if(statusPassword == false)
                    {
                        Console.WriteLine("Password must contains at least one lower case, one upper case, one number and special character.");
                        pass = Console.ReadLine();
                    }
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
                    statusName= Validation.IsNameNull(name);
                    if(statusName == true)
                    {
                        Console.WriteLine("Name can't be empty! Please Input your name once more");
                        name = Console.ReadLine();
                    }
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
            if (result == "y" || result == "Y")
            {
                Console.Clear();
                Menu();
            } 
            else 
            {
                Console.Clear();
                Main();
            }
        }

        static void ContinueOrExitLocation(Location obj)
        {
            Console.Write("\nDo you want to continue? y/n :\t");
            var result = Console.ReadLine();
            if (result == "y" || result == "Y") 
            {
                Console.Clear();
                menuWithinLocation(obj);
            }
            else 
            {
                Console.Clear();
                chooseLocation();
            }
        }

    }
}
