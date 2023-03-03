using CatalogAPI.Entities;
using MongoDB.Driver;

namespace CatalogAPI.Data
{
    public class CatalogContextSedd
    {
        public static void SeedData(IMongoCollection<Product> productCollettion)
        {
            bool existProduct = productCollettion.Find(p => true).Any();
            if (!existProduct) 
            {
                productCollettion.InsertManyAsync(GetMyProducts());
            }
        }

        private static IEnumerable<Product> GetMyProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "f01d9df2-b972-11ed-afa1-0242ac120002",
                    Name = "Iphone 14",
                    Description = "Telephone",
                    Image = "product-1.png",
                    Price = 7000.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Id = "7ce5bfa8-b973-11ed-afa1-0242ac120002",
                    Name = "PS5",
                    Description = "Console",
                    Image = "product-2.png",
                    Price = 4000.00M,
                    Category = "Console"
                }
            };
        }
    }
}
