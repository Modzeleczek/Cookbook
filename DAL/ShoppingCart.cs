using System.Collections.Generic;

namespace Cookbook.DAL
{
    public class ShoppingCart
    {
        private List<int> ids;

        public void Load(string cartString)
        {
            ids = new List<int>(); // tworzymy pustą listę
            if(cartString == null) // jeżeli przekazano pustą wartość ciągu koszyka
                return; // kończymy metodę
            else
            {
                var idStrings = cartString.Split('i'); // rodzielamy ciąg według znaku 'i'
                for (int i = 0; i < idStrings.Length - 1; ++i) // na końcu ciągu jest i, więc ostatni element idStrings będzie pustym stringiem
                {
                    int id = int.Parse(idStrings[i]);
                    ids.Add(id); // dodajemy id 
                }
            }
        }
        public string Save()
        {
            string result = "";
            foreach (var id in ids)
                result = result + $"{id}i"; // i jest separatorem między dwoma wartościami id produktów
            return result;
        }
        public void Add(int id)
        {
            ids.Add(id);
        }
        public void Remove(int id)
        {
            ids.Remove(id); // nie trzeba wykonywać RemoveAll, bo wartości id w liście są unikalne (nie można 2 razy dodać tego samego produktu do koszyka)
            // ids.RemoveAll(x => (id == x));
        }
        public bool Contains(int id)
        {
            return ids.Contains(id);
        }
        public List<int> List()
        {
            return ids;
        }
        public void Clear()
        {
            ids.Clear();
        }
    }
}
