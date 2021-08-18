using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsoleTables;

namespace FishStoreLib
{
    public class customer
    {    
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public customer()
        {
            id = 0;
            name = "N/A";
            email = "N/A";
            password = "N/A";
        }
        public customer(int _id, string _title, string _mail, string _pass)
        {
            id = _id;
            name = _title;
            email = _mail;
            password = _pass;
        }
        const string xmlfile = @"D:\Fish_Store\FishStoreLib\FishStoreLib\CustomerDetails1.xml";

        public void deleteCustomer(string id)
        {
            var xdoc = XDocument.Load(xmlfile);
            var tgtcustomer = xdoc.Root.Descendants("CUSTOMER").FirstOrDefault(x => x.Attribute("id").Value == id);
            tgtcustomer.Remove();
            xdoc.Save(xmlfile);

            Console.WriteLine("Customer details deleted successfully.");

        }
        public void updateCustomername(string id, string newEmail, string newPass)
        {
            var xdoc = XDocument.Load(xmlfile);
            var tgtUpdate = xdoc.Root.Descendants("CUSTOMER").FirstOrDefault(x => x.Attribute("id").Value == id);
            tgtUpdate.Element("EMAIL").Value = newEmail;
            tgtUpdate.Element("PASS").Value = newPass;
            xdoc.Save(xmlfile);
            Console.WriteLine("Name updated successfully.");

        }
        public void GetCustomer()
        {
            var xdoc = XDocument.Load(xmlfile);
            var getcust = xdoc.Root.Descendants("CUSTOMER").Select(x => new customer(int.Parse(x.Attribute("id").Value), x.Element("NAME").Value, x.Element("EMAIL").Value, x.Element("PASS").Value));

            Console.WriteLine("The Customer Data is as follows : \n\n");
            var table = new ConsoleTable("ID", "Name", "Email");
            foreach (var m in getcust)
            {
                table.AddRow(m.id, m.name, m.email);
            }
            table.Write();

        }
        public void AddCustomer(customer cust)
        {
            // var cust = new customer(3 , "Albert");
            var xdoc = XDocument.Load(xmlfile);
            var xelement = new XElement("CUSTOMER", new XAttribute("id", cust.id), new XElement("NAME", cust.name), new XElement("EMAIL", cust.email), new XElement("PASS", cust.password));
            xdoc.Root.Add(xelement);
            xdoc.Save(xmlfile);
            Console.WriteLine("Customer details added");
        }

    }
}
