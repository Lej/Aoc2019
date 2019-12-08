using Aoc2019.Extensions;
using Aoc2019.Images;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Aoc2019.Tests.Days.Day8
{
    [TestClass]
    public class Day8
    {
        [TestMethod]
        public void SolvePart1()
        {
            var sif = this.ReadEmbedded("Day8.txt").Trim().ToCharArray().Select(x => x.ToDigit());
            var image = new SifImage(25, 6, sif);
            var layerWithFewestZeros = image.Layers.OrderBy(layer => layer.AllPixels.Count(pixel => pixel == 0)).First();
            var ones = layerWithFewestZeros.AllPixels.Count(x => x == 1);
            var twos = layerWithFewestZeros.AllPixels.Count(x => x == 2);
            var value = ones * twos;
            
            Console.WriteLine(value);
            Assert.AreEqual(1677, value);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var width = 25;
            var height = 6;
            var sif = this.ReadEmbedded("Day8.txt").Trim().ToCharArray().Select(x => x.ToDigit());
            var sifImage = new SifImage(width, height, sif);
            using (var stream = File.Create("Day8.png"))
            {
                var bitmap = sifImage.ToBitmap();
                for (var x = 0; x < width; x++)
                {
                    for(var y = 0; y < height; y++)
                    {
                        var color = bitmap.GetPixel(x, y);
                        Assert.AreEqual(_expectedColors[y][x].R, color.R);
                        Assert.AreEqual(_expectedColors[y][x].G, color.G);
                        Assert.AreEqual(_expectedColors[y][x].B, color.B);
                    }
                }
                bitmap.Save(stream, ImageFormat.Png);
            }
        }

        private Color[][] _expectedColors = {
            new [] { Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.White, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.White, Color.White, Color.White, Color.Black, Color.White, Color.White, Color.White, Color.Black, Color.Black },
            new [] { Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.Black, Color.Black, Color.Black, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black },
            new [] { Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.White, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.White, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black },
            new [] { Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.Black, Color.Black, Color.Black, Color.Black, Color.White, Color.White, Color.White, Color.Black, Color.Black },
            new [] { Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.White, Color.Black, Color.Black, Color.Black, Color.Black, Color.White, Color.Black, Color.Black, Color.Black, Color.Black },
            new [] { Color.Black, Color.White, Color.White, Color.Black, Color.Black, Color.White, Color.White, Color.White, Color.Black, Color.Black, Color.Black, Color.White, Color.White, Color.Black, Color.Black, Color.White, Color.Black, Color.Black, Color.Black, Color.Black, Color.White, Color.Black, Color.Black, Color.Black, Color.Black }
        };
    }
}
