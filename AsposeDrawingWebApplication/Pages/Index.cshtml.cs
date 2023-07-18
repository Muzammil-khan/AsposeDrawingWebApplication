using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace AsposeDrawingWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        public string imageSrc { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            imageSrc = "data:image/png;base64, " + Convert.ToBase64String(Draw(ImageFormat.Png).ToArray());
        }

        static MemoryStream Draw(ImageFormat format)
        {
            // This code example demonstrates how to draw a Region.
            // Create a Bitmap
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            // Initialie Graphics from Bitmap
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Initialize Graphics path
            GraphicsPath path = new GraphicsPath();

            // Add a Polygon
            path.AddPolygon(new Point[] { new Point(100, 400), new Point(500, 100), new Point(900, 400), new Point(500, 700) });

            // Initialize a Region
            Region region = new Region(path);

            // Inner Graphics Path
            GraphicsPath innerPath = new GraphicsPath();

            // Add a Rectangle
            innerPath.AddRectangle(new Rectangle(300, 300, 400, 200));

            // Exclude inner path
            region.Exclude(innerPath);

            // Define a solid brush
            Brush brush = new SolidBrush(Color.Green);

            // Fill region
            graphics.FillRegion(brush, region);

            MemoryStream result = new MemoryStream();
            bitmap.Save(result, format);
            result.Seek(0, SeekOrigin.Begin);
            return result;
        }
    }
}