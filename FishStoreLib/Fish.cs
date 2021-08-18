using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FishStoreLib
{
    public class Fish
    {   // Fish data.
        public string fishName { get; set; }
        public decimal Price { get; set; }

        // Fish food data.
        public string fishFood { get; set; }

        public decimal foodPrice { get; set; }
        public decimal foodQuantity { get; set; }

        // fish tank data.

        public string fishTank { get; set; }
        public decimal TankPrice { get; set; }


        public Fish ()
        {
            fishName = "Not Chosen";
            Price = 0.00m;
            fishFood = "Not Chosen";
            foodQuantity = 50.00m;
            fishTank = "Not Chosen";
            TankPrice = 0.00m;
            foodPrice = 0.00m;

        }


        public override string ToString()
        {
           return string.Format("\nOrder Information:\n\tName: {0}\tFood: {1}\tTank: {2}\tPrice: {3}", fishName,fishFood,fishTank, Price);
        }


    }


    }

