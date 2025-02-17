using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Clover.Shared
{
    public static class ImagingExtensions
    {
        public static Bitmap Resize(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }
        public static byte[] ToByteArray(this Image input)
        {
            if (input != null)
            {
                using (var stream = new MemoryStream())
                {
                    input.Save(stream, ImageFormat.Jpeg);
                    return stream.ToArray();
                }
            }
            else
            {
                return null;
            }
        }
        public static Image ToImage(this byte[] input)
        {
            if (input != null)
            {
                var stream = new MemoryStream(input);
                return Image.FromStream(stream);
            }
            else
            {
                return null;
            }
        }
    }

    public static class MiscellaneousExtensions
    {
        public static void Restart(this System.Windows.Forms.Timer timer)
        {
            timer.Stop();
            timer.Start();
        }
        public static BindingList<T> ToBindingList<T>(this IEnumerable<T> input)
        {
            return new BindingList<T>(input.ToList());
        }
    }

    public static class StringExtensions
    {
        public static string DecodeBase64(this string input)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }
        public static string EncodeBase64(this string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }
        public static string FirstLetterToUpperCase(this string input)
        {
            char[] array = input.ToCharArray();
            array[0] = char.ToUpper(array[0]);
            return new string(array);
        }
        public static string NullIfEmpty(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            else
            {
                return input;
            }
        }
        public static int? ParseInteger(this string input)
        {
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int result))
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        public static string RemoveIllegalCharacters(this string input)
        {
            return Regex.Replace(input, @"[^A-Za-z0-9 ÑñÁáÉéÍíÓóÚú]+", "").Trim();
        }
        public static string ToStringPreferIntegerFormat(this decimal input)
        {
            if ((input - Math.Truncate(input)) == 0)
            {
                return ((int)(input)).ToString();
            }
            else
            {
                return input.ToString("N2");
            }
        }
    }
}