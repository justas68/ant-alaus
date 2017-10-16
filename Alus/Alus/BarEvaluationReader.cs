using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{
    class BarEvaluationReader
    {
        public List<VisitedBars> _visitedBarsList;
        public VisitedBars visitedBar;
        public List<VisitedBars> ReadFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Alus.BarEvaluation.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                line = reader.ReadLine();
                _visitedBarsList = new List<VisitedBars>();
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    visitedBar = new VisitedBars(values[0], values[1]);
                    _visitedBarsList.Add(visitedBar);
                }
            }
            return _visitedBarsList;
        }
    }
}
