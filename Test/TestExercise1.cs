using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ef
{
    [TestClass]
    public class TestExercise1
    {
        [TestMethod]
        public void Ex1_ListProduct()
        {
            using (var db = createDbContext())
            {
                var allProducts = db.Products.Include(t => t.Vat).ToList();

                Assert.IsNotNull(allProducts);
                Assert.IsTrue(allProducts.Count >= 10);

                var testProduct = allProducts.First();
                Assert.IsNotNull(testProduct);
                Assert.IsTrue(testProduct.ID > 0);
                Assert.IsNotNull(testProduct.Name);
                Assert.IsTrue(testProduct.Price > 0);
                Assert.IsTrue(testProduct.Stock > 0);
            }
        }

        [TestMethod]
        public void Ex1_ListVat()
        {
            using (var db = createDbContext())
            {
                var allVat = db.Vat.Include(t => t.Products).ToList();

                Assert.IsNotNull(allVat);
                Assert.IsTrue(allVat.Count >= 3);

                var testVat = allVat.First();
                Assert.IsNotNull(testVat);
                Assert.IsTrue(testVat.ID > 0);
                Assert.IsTrue(testVat.Percentage >= 0);
            }
        }

        [TestMethod]
        public void Ex1_ProductFindOne()
        {
            using (var db = createDbContext())
            {
                var testProduct = db.Products.Include(p => p.Vat).FirstOrDefault(x => x.Name == "Mega Bloks 24 pcs");

                Assert.IsNotNull(testProduct);
                Assert.AreEqual(4325, testProduct.Price);

                Assert.IsNotNull(testProduct.Vat);
                Assert.AreEqual(27, testProduct.Vat.Percentage);
            }
        }

        [TestMethod]
        public void Ex1_VatFindOne()
        {
            using (var db = createDbContext())
            {
                var testVat = db.Vat.Include(t => t.Products).FirstOrDefault(x => x.Percentage == 27);
                Assert.IsNotNull(testVat);

                Assert.IsNotNull(testVat.Products);
                Assert.IsTrue(testVat.Products.Count > 0);
            }
        }

        private ProductDbContext createDbContext()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            contextOptionsBuilder.UseSqlServer(TestConnectionStringHelper.SqlConnectionString);

            return new ProductDbContext(contextOptionsBuilder.Options);
        }
    }
}
