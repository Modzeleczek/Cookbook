using System.Text;
using Cookbook.DAL;

namespace Cookbook.Models
{
    public class ShoppingCartPageModel : DBPageModel
    {
        public ShoppingCart shoppingCart { get; private set; }
        public ShoppingCartPageModel(CookbookContext dataSource, ShoppingCart shoppingCart) : base(dataSource) // wstrzyknięcie zależności (dependency injection)
        {
            this.shoppingCart = shoppingCart;
        }
        public void LoadCart()
        {
            string cartString = null;
            if (HttpContext.Session.TryGetValue("cart", out byte[] cartBytes) == true)
                cartString = Encoding.ASCII.GetString(cartBytes);
            shoppingCart.Load(cartString); // jeżeli nie uda się odczytać zawartości koszyka z Session, to wyświetlony zostanie pusty koszyk
        }
        public void SaveCart()
        {
            string updatedCartString = shoppingCart.Save();
            byte[] cartBytes = Encoding.ASCII.GetBytes(updatedCartString);
            HttpContext.Session.Set("cart", cartBytes); // zapisujemy w Session zaktualizowaną zawartość koszyka
        }
    }
}
