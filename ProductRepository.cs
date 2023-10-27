using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ef
{
    public class ProductRepository
    {
        private readonly string connectionString;

        // Do not change the constructor
        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Do not change this method
        private ProductDbContext createDbContext()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            contextOptionsBuilder.UseSqlServer(connectionString);
            return new ProductDbContext(contextOptionsBuilder.Options);
        }


        public IReadOnlyCollection<Product> List()
        {
            using (var db = createDbContext())
            {
                var result = new List<Product>();

                var prodList = from product in db.Products
                             select new Product()
                             {
                                 Name = product.Name,
                                 ID = product.ID,
                                 Stock = product.Stock,
                                 Price = product.Price,
                                 VatPercentage = product.Vat.Percentage
                             };

                foreach (var product in prodList)
                {
                    result.Add(product);
                }
                return result;
            }
            throw new System.NotImplementedException();
        }

        public int Insert(Product value)
        {
            using (var db = createDbContext())
            {
                var inserted = db.Vat.Where(vat => vat.Percentage == value.VatPercentage).FirstOrDefault();
                if (inserted == null)
                {
                    inserted = new DbVat()
                    {
                        Percentage = value.VatPercentage
                    };
                    db.Vat.Add(inserted);
                }
                var newProd = new DbProduct()
                {
                    Name = value.Name,
                    Price = value.Price,
                    Stock = value.Stock,
                    Vat = inserted
                };
                db.Products.Add(newProd);
                db.SaveChanges();
                return newProd.ID;
            }
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            using (var db = createDbContext())
            {
                var prodDel = db.Products.Where(product => product.ID == id).FirstOrDefault();
                if (prodDel == null)
                {
                    return false;
                }

                db.Products.Remove(prodDel);
                db.SaveChanges();
                return true;
            }
            throw new System.NotImplementedException();
        }
    }
}
