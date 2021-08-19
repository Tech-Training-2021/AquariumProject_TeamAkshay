using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace FishStoreLib
{
    public class Mumbai
    {
        private string filePath = @"D:\Fish_Store\FishStoreLib\FishStoreLib\mumbaiData.json";
        public JArray fishDataArray;
        public JArray fishFoodArray;
        public JArray fishTankArray;
        public string accessfilePath
        {
            get { return filePath; }
            set { }
        }

        public void fetchDetails()
        {
            string fishJson = File.ReadAllText(accessfilePath);
            var jObject = JObject.Parse(fishJson);
            fishDataArray = jObject.GetValue("fishData") as JArray;
            fishFoodArray = jObject.GetValue("fishFood") as JArray;
            fishTankArray = jObject.GetValue("fishTank") as JArray;
        }

        public void saveData(int updatedAvailableQuantity, int selectedId, string type)
        {
            string fishData = File.ReadAllText(accessfilePath);
            var fishDataObj = JObject.Parse(fishData);

            switch (type)
            {
                case "fish":
                    var data = fishDataObj.GetValue("fishData") as JArray;

                    foreach (var y in data)
                    {
                        int id = Convert.ToInt32(y["id"]);

                        if (id == selectedId)
                        {
                            y["availableQuantity"] = updatedAvailableQuantity;
                            break;
                        }
                    }
                    fishDataObj["fishData"] = data;
                    string newJsonResult1 = JsonConvert.SerializeObject(fishDataObj, Formatting.Indented);
                    File.WriteAllText(accessfilePath, newJsonResult1);
                    break;
                case "food":
                    var foodData = fishDataObj.GetValue("fishFood") as JArray;

                    foreach (var y in foodData)
                    {
                        int id = Convert.ToInt32(y["id"]);

                        if (id == selectedId)
                        {
                            y["availableQuantity"] = updatedAvailableQuantity;
                            break;
                        }
                    }
                    fishDataObj["fishFood"] = foodData;
                    string newJsonResult2 = JsonConvert.SerializeObject(fishDataObj, Formatting.Indented);
                    File.WriteAllText(accessfilePath, newJsonResult2);
                    break;
                case "tank":
                    var tankData = fishDataObj.GetValue("fishTank") as JArray;

                    foreach (var y in tankData)
                    {
                        int id = Convert.ToInt32(y["id"]);

                        if (id == selectedId)
                        {
                            y["availableQuantity"] = updatedAvailableQuantity;
                            break;
                        }
                    }
                    fishDataObj["fishTank"] = tankData;
                    string newJsonResult3 = JsonConvert.SerializeObject(fishDataObj, Formatting.Indented);
                    File.WriteAllText(accessfilePath, newJsonResult3);
                    break;

                default:
                    Console.WriteLine("Invalid");
                    break;
            }
            fetchDetails();


        }

    }
}
