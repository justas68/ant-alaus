using System;
using System.Collections.Generic;
using System.Drawing;

namespace Alus
{
    public interface IColorPicker
    {
        Color GetColor(string id);
    }

    public class ColorPicker : IColorPicker
    {
        private readonly Random random = new Random();
        private readonly Dictionary<string, Color> colors = new Dictionary<string, Color>();

        public Color GetColor(string id)
        {
            if (!colors.TryGetValue(id, out Color color))
            {
                var col = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                colors[id] = col;
                return col;
            }
            return color;
        }
    }
}
