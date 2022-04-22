// Adam Dernis © 2022

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;

namespace ColorExtractor
{
    public static class ImageParser
    {
        public static async Task<Image<Argb32>> GetImage(string uri)
        {
            return (await Image.LoadAsync(await GetImageStreamAsync(uri))).CloneAs<Argb32>();
        }
        private static async Task<Stream> GetImageStreamAsync(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                return null;
            }

            var response = await HttpWebRequest.CreateHttp(uri).GetResponseAsync();
            return response.GetResponseStream();
        }

        public static Vector3[,] GetImageColors(
            Image<Argb32> image)
        {
            Vector3[,] colors = new Vector3[image.Height, image.Width];

            for (int row = 0; row < image.Height; row++)
            {
                Span<Argb32> rowPixels = image.GetPixelRowSpan(row);
                for (int col = 0; col < image.Width; col++)
                {
                    float b = rowPixels[col].B / 255f;
                    float g = rowPixels[col].G / 255f;
                    float r = rowPixels[col].R / 255f;
                    //float a = rowPixels[col].A / 255;

                    Vector3 color = new Vector3(r, g, b);

                    colors[row, col] = color;
                }
            }

            return colors;
        }

        public static Vector3[] SampleImage(Vector3[,] colors, int rowSamples, int colSamples)
        {
            int rows = colors.GetLength(0);
            int cols = colors.GetLength(1);
            int nthRow = rows / rowSamples;
            int nthCol = cols / colSamples;

            Vector3[] output = new Vector3[rowSamples * colSamples];

            int pos = 0;
            for (int row = 0; row < rows; row += nthRow)
            {
                for (int col = 0; col < cols; col += nthCol)
                {
                    if (pos == output.Length)
                    {
                        return output;
                    }

                    output[pos] = colors[row, col];
                    pos++;
                }
            }

            return output;
        }
    }
}
