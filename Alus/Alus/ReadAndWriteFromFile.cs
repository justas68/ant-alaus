using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{
    public class ReadAndWriteFromFile
    {
        private static string _resourceName = "./../../../BarList.txt";
        private List<Bar> _barList = new List<Bar>();

        public List<Bar> ReadFile()
        {
            using (StreamReader fileReader = new StreamReader(_resourceName))
            {
                string st;
                while ((st = fileReader.ReadLine()) != null)
                {
                    var stringValues = st.Split(';');
                    try
                    {
                        _barList.Add(new Bar(stringValues[0], stringValues[1], Convert.ToDouble(stringValues[2]), stringValues[3], stringValues[4], stringValues[5]));
                    }
                    catch(IOException)
                    {
                        return null;
                    }
                }
                return _barList;
            }
        }

        public void WriteLineToFile(Bar bar)
        {
            if (File.Exists(_resourceName))
            {
                using (var streamWriter = new StreamWriter(_resourceName, true))
                {
                    try
                    {
                        streamWriter.WriteLine(bar.Name + ";" + bar.Coordinates + ";" + bar.OnlineRating + ";" + bar.Address + ";" + bar.PlaceId + ";" + bar.Evaluation);
                        _barList.Add(bar);
                    }
                    catch (Exception)
                    {                       
                    }
                }
            }
        }

        public void WriteListToFile(List<Bar> barList)
        {
            if (File.Exists(_resourceName))
            {
                using (TextWriter streamWriter = new StreamWriter(_resourceName, false))
                {
                    foreach (Bar bar in barList)
                    {
                        streamWriter.WriteLine(bar.Name + ";" + bar.Coordinates + ";" + bar.OnlineRating + ";" + bar.Address + ";" + bar.PlaceId + ";" + bar.Evaluation);
                    }
                }
            }
        }
    }
}
