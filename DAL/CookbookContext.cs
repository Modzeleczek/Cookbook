using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cookbook.Models;

namespace Cookbook.DAL
{
    public class CookbookContext : DbContext
    {
        public CookbookContext (DbContextOptions<CookbookContext> options) : base(options) { }
        public DbSet<Models.Category> Category { get; set; }
        public DbSet<Models.Dish> Dish { get; set; }
        public DbSet<Models.Ingredient> Ingredient { get; set; }
        public DbSet<Models.DishIngredient> DishIngredient { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Category>().ToTable("Category");
            modelBuilder.Entity<Models.Dish>().ToTable("Dish");
            modelBuilder.Entity<Models.Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<Models.DishIngredient>().ToTable("DishIngredient");

            modelBuilder.Entity<Models.DishIngredient>().HasKey(di => new { di.dishId, di.ingredientId }); // ustawiamy klucz główny tabeli DishIngredient złożony z kolumn dishId i ingredientId
            foreach (var property in modelBuilder.Model.GetEntityTypes() // ustawiamy typ kolumny ingredientAmount jako DECIMAL(12, 5)
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // property.SetColumnType("DECIMAL(12, 5)");
                property.SetPrecision(12);
                property.SetScale(5);
            }
            base.OnModelCreating(modelBuilder);
        }

        public void AddCategory(Category c)
        {
            Category.Add(c);
            SaveChanges();
        }
        public async Task<List<Category>> GetAllCategories() => await Category.ToListAsync();
        public Category GetCategory(int id) => Category.Find(id); // await Category.FirstOrDefaultAsync(c => (c.id == id));
        public void UpdateCategory(Category c)
        {
            Category.Update(c);
            SaveChanges();
        }
        public void DeleteCategory(int id)
        {
            var c = Category.Find(id);
            if (c == null) return;
            Category.Remove(c);
            SaveChanges();
        }

        public void AddDish(Dish d)
        {
            Dish.Add(d);
            SaveChanges();
        }
        public async Task<List<Dish>> GetAllDishes() => await Dish.ToListAsync();
        public Dish GetDish(int id) => Dish.Find(id);
        public void UpdateDish(Dish d)
        {
            var original = GetDish(d.id);
            original.name = d.name;
            original.recipe = d.recipe;
            original.Category = d.Category;
            Entry(original).Collection(e => e.DishIngredient).Load();
            var origDi = original.DishIngredient;
            for (int i = 0; i < origDi.Count; ++i)
            {
                bool isInUpdatedDish = false;
                foreach (var di in d.DishIngredient)
                {
                    if (di.dishId == origDi[i].dishId && di.ingredientId == origDi[i].ingredientId)
                        isInUpdatedDish = true;
                }
                if (isInUpdatedDish == false)
                    DishIngredient.Remove(origDi[i]);
            }
            original.DishIngredient = d.DishIngredient;
            SaveChanges();
        }
        public void DeleteDish(int id)
        {
            var d = Dish.Find(id);
            if (d == null) return;
            Dish.Remove(d);
            SaveChanges();
        }

        public void AddIngredient(Ingredient i)
        {
            Ingredient.Add(i);
            SaveChanges();
        }
        public async Task<List<Ingredient>> GetAllIngredients() => await Ingredient.ToListAsync();
        public Ingredient GetIngredient(int id) => Ingredient.Find(id);
        public void UpdateIngredient(Ingredient i)
        {
            Ingredient.Update(i);
            SaveChanges();
        }
        public void DeleteIngredient(int id)
        {
            var ing = Ingredient.Find(id);
            if (ing == null) return;
            Entry(ing).Collection(e => e.DishIngredient).Load();
            var ingDi = ing.DishIngredient;
            for (int i = 0; i < ingDi.Count; ++i)
            {
                var dish = Dish.Find(ingDi[i].dishId);
                Dish.Remove(dish);
                DishIngredient.Remove(ingDi[i]);
            }
            Ingredient.Remove(ing);
            SaveChanges();
        }
    }
}