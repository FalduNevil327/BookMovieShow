namespace BookMovieShow.DAL
{
    public class Dal_Helper
    {
        #region Connection String
        public static string ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("ConnectionString");
        #endregion
    }
}
