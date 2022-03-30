using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace XYZExporter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            string filePath = args[0];
        //    string destPath = "";
            string destPath = "C:\\Users\\48501\\OneDrive\\Documents\\APBD\\Jason\\pliki wynikowe";
           string format = "jason";
        //   string format = args[2].ToLower();

            //1 opcja
            // List<string> list = new List<string>();
            // using (StreamReader stream = new StreamReader(@"C:\Users\48501\OneDrive\Documents\APBD\Jason\dane.csv"))
            // {
            //    string line;
            //    while ((line = stream.ReadLine()) != null)
            //   {
            //        list.Add(line);
            //    }
            //  }
            //   using (StreamWriter stream = new StreamWriter("opcja1.txt"))
            //  {
            //      foreach (var item in list)
            //      {
            //          stream.WriteLine(item);
            //      }
            //  }
            //2 opcja
            if (format.Equals("jason"))
            {
                string[] result2 = await File.ReadAllLinesAsync(filePath);

                await File.WriteAllLinesAsync(destPath, result2);

                string a = "";
                string[] b = a.Split(',');
                new Student { };
                //var jObject = new JObject();
                // { }
                var studenci = new JArray();
                // []
                var jProperty = new JProperty("property", 1);
                // nazwaProperty: ""
                //Root: {}, []
                var jObject = new JObject(
                    new JProperty("uczelnia", new JObject(
                        new JProperty("createdAt", DateTime.Today.ToString("dd.MM.yyyy")),
                        new JProperty("author", "Michał Pazio"),
                        new JProperty("studenci", studenci)
                    ))
                );
                Console.WriteLine(jObject);
            }
            else 
            {
                Console.WriteLine("Podano nieprawidłowy format danych wyjściowych");
            }
        }
    }
}
