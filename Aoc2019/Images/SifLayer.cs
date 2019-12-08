using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Images
{
    public class SifLayer
    {
        public int Width { get; }
        public int Height { get; }

        public int[][] Pixels { get; }
        public IEnumerable<int> AllPixels => Pixels.SelectMany(x => x);

        public SifLayer(int width, int height, IEnumerable<int> sif)
        {
            Width = width;
            Height = height;
            Pixels = sif.Select((x, i) => new { Index = i, Pixel = x })
                .GroupBy(x => x.Index / width)
                .Select(x => x.Select(x => x.Pixel).ToArray())
                .ToArray();
            if (Pixels.Last().Length != width) throw new ArgumentException(nameof(sif));
        }
    }
}
