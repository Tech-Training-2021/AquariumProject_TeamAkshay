using FishStoreLib;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Linq;
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
            
            Console.WriteLine("-------------------------------------     WELCOME TO OUR FISH STORE.    ---------------------------------------");

            Console.Write("1.customer Information\n2.Choose store Location\nChoose one option :   ");
            int chooseoption = int.Parse(Console.ReadLine());
             switch (chooseoption)
             {
                 case 1: Menu(); break;
                case 2: ChoseItem(); break;
             }
        }

        #region code for later use
		/*   public static int chooseAction()
        {
            int choice = 0;

            Console.WriteLine("1. Add Products to inventory\n2. Add Products to cart\n3. Checkout\nChoose one option: ");
            choice = int.Parse(Console.ReadLine());
            return choice;
        } */ 
	#endregion

        static void ChoseItem()
        {
            string[] F_types = { "Goldfish", "Angelfish", "African Leaf Fish", "Oscar", "Tiger barb", "Rummy nose tetra", "Neotetra" };
            decimal[] F_price = { 100.00m, 150.00m, 200.00m, 250.00m, 300.00m, 350.00m, 400.00m };
            string[] F_food = { "Flake Food", "Pellets", "Freeze-Dried", "Frozen", "Spirulina/Dried Seaweed" };
            decimal[] Food_price = { 200.00m, 300.00m, 400.00m, 500.00m, 550.00m};
            string[] F_tank = { "Coldwater Aquarium", "Freshwater Tropical Aquarium", "Marine (Saltwater) Aquarium", "Brackish Aquarium", "Betta Fish Tank", "Breeder Tank", "Large Tank", "Kids' Tank" };
            decimal[] Tank_price = { 1000.00m, 1500.00m, 2000.00m, 2500.00m, 3000.00m, 3500.00m, 4000.00m , 5000.00m};
            int choose_Item;
            Fish fishobj = new Fish();

            string Temp = "y";
            do
            {
                Console.Clear();
                Console.Write("1.Buy pet fish\n2.Buy pet food\n3. Buy fish tank.\nChoose one option :   ");
                choose_Item = int.Parse(Console.ReadLine());

                switch (choose_Item)
                {
                    case 1:
                            fishobj.fishName=  Choosefish(F_types);
                        for (int i = 0; i < F_types.Length; i++)
                            if (fishobj.fishName == F_types[i])
                               fishobj.Price = F_price[i];                            
                            break;
                    case 2:  fishobj.fishFood =  choosefood(F_food);
                        for (int i = 0; i < F_food.Length; i++)
                            if (fishobj.fishFood == F_food[i])
                                fishobj.foodPrice = Food_price[i];
                        break;
                    case 3: fishobj.fishTank =  choosetank(F_tank);
                        for (int i = 0; i < F_tank.Length; i++)
                            if (fishobj.fishTank == F_tank[i])
                                fishobj.TankPrice = Tank_price[i];
                        break;

                }

                Console.Write("Do you want to continue shopping?(Choon 'y' if yes.)\t");
                Temp = Console.ReadLine();

            } while(Temp == "y");

            string strJson = JsonConvert.SerializeObject(fishobj);
            File.WriteAllText(@"FishDetails.json", strJson);
            Console.WriteLine("Data Saved successfull!!\n");


            strJson = String.Empty;
            strJson = File.ReadAllText(@"FishDetails.json");
            Fish result = JsonConvert.DeserializeObject<Fish>(strJson);



            store storeObj = new store();

            storeObj.ShoppingCart.Add(fishobj);

            decimal total = storeObj.Check_out();
            storeObj.orderSummary(fishobj , total);
           

            

        }

        public static string Choosefish(string[] a)
        {
            
            string name = "";
            Console.Write("\nChoose your pet fish :\n");
            for (int i=0;i< a.Length; i++)
            Console.WriteLine(@$"{i+1}. {a[i]}");

            int item = int.Parse(Console.ReadLine());
            for (int i = 0; i < a.Length; i++)
             if ((i+1) == item)
                {
                    name = a[i];
                    
                }

            return name;

            
        }

        static string choosefood(string[] c)
        {
            string food = "";
            Console.Write("\nChoose the fish food :\n");
            for (int i = 0; i < c.Length; i++)
            {
                Console.WriteLine(@$"{i + 1}. {c[i]}");
            }

            int item = int.Parse(Console.ReadLine());
            for (int i = 0; i < c.Length; i++)
                if ((i + 1) == item)
                {
                    food = c[i];

                }

            return food;


        }

        static string choosetank(string[] d)
        {
            string tank = "";
            Console.Write("\nChoose the fish tank :\n");
            for (int i = 0; i < d.Length; i++)
            {
                Console.WriteLine(@$"{i + 1}. {d[i]}");
            }

            int item = int.Parse(Console.ReadLine());
            for (int i = 0; i < d.Length; i++)
                if ((i + 1) == item)
                {
                    tank = d[i];

                }

            return tank;

        }

     //   const string xmlfile = @"D:\Fish_Store\FishStoreLib\FishStoreLib\CustomerDetails1.xml";

        static void Menu()
        {
            Console.WriteLine("1. Add\n2. Get Details\n3. Update\n4. Delete\n5. Exit");
            Console.Write("Choose an ooption :  ");
            var input = Console.ReadLine();
            PerformSelectedAction(input);
        }

        static void PerformSelectedAction(string input)
        {
            customer cus = new customer();
            switch (input)
            {
                case "1": Console.Clear();
                    Console.WriteLine("Customer ID : ");
                    var id = Console.ReadLine();
                    Console.WriteLine("Customer Name : ");
                    var name = Console.ReadLine();
                    Console.WriteLine("Customer Email : ");
                    var mail = Console.ReadLine();
                    Console.WriteLine("Customer Password : ");
                    var pass = Console.ReadLine();
                    cus.AddCustomer(new customer(int.Parse(id), name , mail, pass));
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
                     id = Console.ReadLine();
                    Console.WriteLine("Customer Email : ");
                     mail = Console.ReadLine();
                    Console.WriteLine("Customer Password : ");
                     pass = Console.ReadLine();
                    cus.updateCustomername(id, mail , pass);
                    ContinueOrExit();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Customer ID : ");
                    id = Console.ReadLine();
                    cus.deleteCustomer(id);

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
        

    }
}
