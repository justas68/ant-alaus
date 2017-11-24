using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using Alus.Core.Models;

namespace Alus
{
    public interface IBarContainer
    {
        IEnumerable<Bar> GetAll();
        void SetAll(IEnumerable<Bar> bars);
        void Add(Bar bar);
    }

    public class BarFileContainer : IBarContainer
    {
        public string Filename { get; private set; }

        public BarFileContainer(string filename)
        {
            Filename = filename;
        }

        public IEnumerable<Bar> GetAll()
        {
            using (StreamReader fileReader = new StreamReader(Filename))
            {
                string st;
                while ((st = fileReader.ReadLine()) != null)
                {
                    Bar bar = null;
                    try
                    {
                        var stringValues = st.Split(';');
                        bar = new Bar(
                            stringValues[0],
                            stringValues[1],
                            double.Parse(stringValues[2], CultureInfo.InvariantCulture),
                            stringValues[3],
                            stringValues[4],
                            stringValues[5],
                            double.Parse(stringValues[6], CultureInfo.InvariantCulture),
                            Convert.ToInt32(stringValues[7])
                        );
                    }
                    catch (IOException)
                    {
                    }

                    if (bar != null)
                    {
                        yield return bar;
                    }
                }
            }
        }

        public void Add(Bar bar)
        {
            if (File.Exists(Filename))
            {
                using (var streamWriter = new StreamWriter(Filename, true))
                {
                    try
                    {
                        streamWriter.WriteLine(ToString(bar));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private string ToString(Bar bar)
        {
            return string.Format("{0};{1};{2};{3};{4};{5};{6}",
                bar.Name,
                bar.Coordinates,
                bar.OnlineRating.ToString(CultureInfo.InvariantCulture),
                bar.Address,
                bar.PlaceId,
                bar.Evaluation,
                bar.Percentage.ToString(CultureInfo.InvariantCulture),
                bar.BeersBought);
        }

        public void SetAll(IEnumerable<Bar> bars)
        {
            if (File.Exists(Filename))
            {
                using (TextWriter streamWriter = new StreamWriter(Filename, false))
                {
                    foreach (Bar bar in bars)
                    {
                        streamWriter.WriteLine(bar.Name + ";" + bar.Coordinates + ";" + bar.OnlineRating + ";" + bar.Address + ";" + bar.PlaceId + ";" + bar.Evaluation + ";" + bar.Percentage + ";" + bar.BeersBought);
                    }
                }
            }
        }
    }
}
