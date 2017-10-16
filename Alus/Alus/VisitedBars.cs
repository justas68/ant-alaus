using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alus
{
    class VisitedBars
    {
        public VisitedBars(string name, string rating)
        {
            Name = name;
            Rating = rating;
        }

        public string Name { get; set; }
        public string Rating { get; set; }
    }
}
