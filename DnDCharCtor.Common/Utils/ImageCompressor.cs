using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using SkiaSharp;

namespace DnDCharCtor.Common.Utils;

public static class ImageCompressor
{
    public const int ImageDimension = 100;

    public static async Task<string> CompressImageAndEncodeToBase64Async(Stream imageStream)
    {
        var compressedImage = await CompressImageAsync(imageStream, ImageDimension, ImageDimension);
        return Convert.ToBase64String(compressedImage);
    }

    public static async Task<byte[]> CompressImageAsync(Stream inputStream, int newWidth, int newHeight)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            await inputStream.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            using var managedStream = new SKManagedStream(memoryStream);
            using var original = SKBitmap.Decode(managedStream);
            using var resized = original.Resize(new SKImageInfo(newWidth, newHeight), SKSamplingOptions.Default);
            using var image = SKImage.FromBitmap(resized);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 75);
            return data.ToArray();
        }
        catch (Exception ex)
        {
            Debug.Write(ex);
            Debugger.Break();
        }

        return [];
    }
}
