namespace ef
{
    public static class TestConnectionStringHelper
    {
        public static string SqlConnectionString
        {
            // You may change the connection string here if needed
            get { return @"data source=(localdb)\mssqllocaldb;initial catalog=HZV17L;integrated security=true"; }
        }
    }
}
