using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;

public class Program
{
    public static void Main()
    {
        // Load the image
        using var image = Image.Load("/Users/steven/Downloads/original-512x512-30.webp");
        // Define the font and text options for your watermark
        var fonts = new FontCollection();
        var fontFamily = fonts.Add("/Users/steven/Downloads/Press_Start_2P/PressStart2P-Regular.ttf");
        var font = fontFamily.CreateFont(10, FontStyle.Regular);

        var text = "SGR #000000001";

        var textSize = TextMeasurer.MeasureSize(text, new TextOptions(font));
        var textLocation =
            new PointF(image.Width - textSize.Width - 10,
                image.Height - textSize.Height - 10); // Adjust padding as needed

        var backgroundRectangle = new RectangularPolygon(textLocation.X - 5, textLocation.Y - 5, textSize.Width + 10, textSize.Height + 10); // Adjust padding as needed
        image.Mutate(x => x.Fill(Color.White.WithAlpha(0.6f), backgroundRectangle)); // Set your desired background color and opacity


        // Apply the watermark
        image.Mutate(x =>
            x.DrawText(
                text,
                font,
                Color.Black.WithAlpha(0.6f),
                textLocation
            )
        );

        // int newWidth = image.Width * 8;
        // int newHeight = image.Height * 8;
        //
        // // Resize the image to 8 times its original size
        // image.Mutate(x => x.Resize(newWidth, newHeight, KnownResamplers.NearestNeighbor)); // Lanczos3 is a good choice for high-quality resizing

        // Save the watermarked image
        image.Save("/Users/steven/Downloads/original-512x512-30-wm.webp");
    }
}