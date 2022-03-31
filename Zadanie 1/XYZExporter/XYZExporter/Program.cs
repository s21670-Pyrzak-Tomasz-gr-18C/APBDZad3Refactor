using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace XYZExporter
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using StreamWriter sw = new StreamWriter("../../../Files/log.txt");
            if (args.Length != 3)
            {
                throw new Exception("wrong number of arguments");
            }

            string pathToDataFile = "";
            string pathToOutputFile = "";

            if (!File.Exists(Path.GetFullPath(args[0])))
            {
                sw.WriteLine($"File not found: {args[0]}");
            }
            else
            {
                pathToDataFile = Path.GetFullPath(args[0]);
                Console.WriteLine(pathToDataFile);
            }

            try
            {
                using (File.Create(args[1] + ".jason")) ;
                pathToOutputFile = Path.GetFullPath(args[1] + ".jason");
                Console.WriteLine(pathToOutputFile);
            }
            catch (Exception e)
            {
                sw.WriteLine(e.Message);
            }

            if (args[2] == "jason")
            {
                string[] result2 = await File.ReadAllLinesAsync(pathToDataFile);
                bool validated = true;

                var studenci = new JArray();

                foreach (string line in result2)
                {
                    string alineData = line;

                    string[] studentData = alineData.Split(',');

                    if ((studentData.Length != 9))
                    {
                        validated = false;
                        sw.WriteLine($"Not described by 9 data columns: {alineData}");
                    }
                    else
                    {
                        foreach (string data in studentData)
                        {
                            if (data == "")
                            {
                                validated = false;
                                sw.WriteLine($"One or more columns are empty: {alineData}");
                            }
                        }
                    }

                    if (validated)
                    {
                        var newStudent = new JObject(
                            new JProperty("indexNumber", "s" + studentData[4]),
                            new JProperty("fname", studentData[0]),
                            new JProperty("lname", studentData[1]),
                            new JProperty("birthdate", studentData[5]),
                            new JProperty("email", studentData[6]),
                            new JProperty("mothersName", studentData[7]),
                            new JProperty("fathersName", studentData[8]),
                            new JProperty("studiesName", studentData[2]),
                            new JProperty("studiesMode", studentData[3])
                            );

                        if (studenci.Count == 0)
                        {
                            studenci.Add(newStudent);
                        }

                        if (studenci.Contains(newStudent))
                        {
                            sw.WriteLine("Student exist" + newStudent.ToString());
                        }
                        else
                        {
                            studenci.Add(newStudent);
                        }
                    }
                    var jObject = new JObject(
                    new JProperty("uczelnia", new JObject(
                        new JProperty("createdAt", DateTime.Today.ToString("dd.MM.yyyy")),
                        new JProperty("author", "Tomasz Pyrzak"),
                        new JProperty("studenci", studenci)
                    ))
                    );
                    File.WriteAllText(pathToOutputFile, jObject.ToString());
                }
            }
        }
    }
}