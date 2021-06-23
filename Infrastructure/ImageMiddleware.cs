using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace Cookbook.Infrastructure
{
    public class ImageMiddleware
    {
        private RequestDelegate next;
        public ImageMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            string path = httpContext.Request.Path; // dla "https://localhost:5001/dish_img/2.jpg" Path to "/dish_img/2.jpg"
            if (path.StartsWith("/photo") == false) // jeżeli ścieżka jest do obrazu
                return;
            path = path.TrimEnd('/'); // jeżeli ścieżka ma '/' na końcu, to go usuwamy
            string[] parts = path.Split('/'); // rozdzielamy ścieżkę według znaku '/'
            string fileName = parts[parts.Length - 1]; // bierzemy pod uwagę tylko ostatnią część ścieżki
            // jeżeli ma na końcu ".jpg"
            MemoryStream stream = new MemoryStream();
            Bitmap bitmap;
            string filePath = "dish_img/" + fileName;
            if (File.Exists(filePath) == true) // jeżeli plik istnieje
            {
                bitmap = new Bitmap(filePath); // tworzymy bitmapę z pliku o podanej ścieżce, który jest obrazem
                DrawWatermark(bitmap); // rysujemy znak wodny na bitmapie
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg); // zapisujemy bitmapę do strumienia
                httpContext.Response.ContentType = "image/jpeg"; // ustawiamy typ przesyłanych danych w odpowiedzi HTTP
            }
            else // jeżeli plik nie istnieje
            {
                filePath = "wwwroot/img/no_image.png";
                bitmap = new Bitmap(filePath);
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                httpContext.Response.ContentType = "image/png";
            }
            bitmap.Dispose();
            await httpContext.Response.Body.WriteAsync(stream.ToArray(), 0, (int)stream.Length); // zapisujemy bajty ze strumienia do odpowiedzi HTTP
            stream.Close();
        }

        private void DrawWatermark(Bitmap bitmap)
        {
            Color color = Color.FromArgb(128, 0xFF, 0x45, 0x00);
            int watermarkHeight = (int)(bitmap.Height / 16.0); // wysokość liter w znaku wodnym
            using (Graphics graphics = Graphics.FromImage(bitmap)) // tworzymy na podstawie bitmapy obiekt typu Graphics, służący do bezpośredniego przetwarzania bitmapy
            using (Font font = new Font("Arial", watermarkHeight, FontStyle.Regular)) // font tekstu
            using (SolidBrush brush = new SolidBrush(color)) // jeżeli chcemy jednolity kolor tekstu, to tworzymy SolidBrush
                graphics.DrawString("Przepisownia", font, brush, new PointF(0, 0)); // piszemy tekst na bitmapie
        }
    }
    public static class ImageMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageMiddleware(this IApplicationBuilder builder) // metoda rozszerzająca (extension method), żeby w metodzie Configure w Startup.cs można było zarejestrować nasz komponent pośredniczący poprzez wywołanie app.UseImageMiddleware
        {
            return builder.UseMiddleware<ImageMiddleware>();
        }
    }
}
