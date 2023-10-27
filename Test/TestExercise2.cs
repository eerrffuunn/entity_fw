using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ef
{
    [TestClass]
    public class TestExercise2
    {
        [TestMethod]
        public void Ex2_List()
        {
            var repo = new ProductRepository(TestConnectionStringHelper.SqlConnectionString);
            var allProducts = repo.List();

            Assert.IsNotNull(allProducts);
            Assert.IsTrue(allProducts.Count >= 10);

            var testProduct = allProducts.First();
            Assert.IsNotNull(testProduct);
            Assert.IsTrue(testProduct.ID > 0);
            Assert.IsNotNull(testProduct.Name);
            Assert.IsTrue(testProduct.Price > 0);
            Assert.IsTrue(testProduct.Stock > 0);
        }

        [TestMethod]
        public void Ex2_FindOne()
        {
            var repo = new ProductRepository(TestConnectionStringHelper.SqlConnectionString);

            var testProduct = repo.List().FirstOrDefault(x => x.Name == "Mega Bloks 24 pcs");

            Assert.IsNotNull(testProduct);
            Assert.AreEqual(4325, testProduct.Price);

            Assert.IsNotNull(testProduct.VatPercentage);
            Assert.AreEqual(27, testProduct.VatPercentage);
        }

        [TestMethod]
        public void Ex2_Insert()
        {
            var repo = new ProductRepository(TestConnectionStringHelper.SqlConnectionString);

            var (productName, vatPercentage, newitem) = getRandomProduct();
            var insertedId = repo.Insert(newitem);

            Assert.AreNotEqual(0, insertedId);
            Assert.IsTrue(repo.List().Any(p => p.Name == productName && p.VatPercentage == vatPercentage && p.ID == insertedId));
        }

        [TestMethod]
        public void Ex2_DeleteOne()
        {
            var repo = new ProductRepository(TestConnectionStringHelper.SqlConnectionString);

            var (_, _, newitem) = getRandomProduct();
            var insertedId = repo.Insert(newitem);

            var success = repo.Delete(insertedId);
            Assert.IsTrue(success);
        }

        private static (string ProductName, int VatPercentage, Product Item) getRandomProduct()
        {
            var rand = new System.Random(System.DateTime.UtcNow.Millisecond);
            var productName = $"Test Product {rand.Next()}";
            var vatPercentage = rand.Next();
            var item = new Product()
            {
                Name = productName,
                Price = 999,
                Stock = 999,
                VatPercentage = vatPercentage
            };
            return (productName, vatPercentage, item);
        }
    }
}
