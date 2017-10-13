using System;

namespace Alus
{
    class Bar
    {
        public Bar()
        {
        }

        public Bar(string name, string coordinates)
        {
            Name = name;
            Coordinates = coordinates;
        }

        public string Name { get; private set; }
        public string Coordinates { get; private set; }
    }
}
