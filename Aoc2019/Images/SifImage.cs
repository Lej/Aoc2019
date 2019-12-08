using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Aoc2019.Images
{
    public class SifImage
    {
        public int Width { get; }
        public int Height { get; }
        public SifLayer[] Layers { get; }

        public SifImage(int width, int height, IEnumerable<int> sif)
        {
            var numPixelsPerLayer = width * height;
            var sifPixelsPerLayer = sif.Select((x, i) => new { Index = i, Pixel = x })
                .GroupBy(x => x.Index / numPixelsPerLayer)
                .Select(x => x.Select(x => x.Pixel).ToArray()).ToArray();
            if (sifPixelsPerLayer.Last().Length != numPixelsPerLayer) throw new ArgumentException(nameof(sif));
            Layers = sifPixelsPerLayer.Select(x => new SifLayer(width, height, x)).ToArray();
            Width = width;
            Height = height;
        }

        public Bitmap ToBitmap()
        {
            var merged = new int?[Height][];
            for (var x = 0; x < Height; x++)
            {
                merged[x] = new int?[Width];
            }

            foreach (var layer in Layers)
            {
                for (var x = 0; x < Width; x++)
                {
                    for (var y = 0; y < Height; y++)
                    {
                        merged[y][x] = !merged[y][x].HasValue || merged[y][x] == 2
                            ? layer.Pixels[y][x]
                            : merged[y][x];
                    }
                }
            }

            var bitmap = new Bitmap(Width, Height);
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var color = merged[y][x] == 0 ? Color.Black
                        : merged[y][x] == 1 ? Color.White
                        : Color.Magenta;
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
    }
}
