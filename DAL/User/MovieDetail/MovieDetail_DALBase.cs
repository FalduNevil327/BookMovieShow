using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.User.MovieDetail
{
    public class MovieDetail_DALBase : Dal_Helper
    {
        #region PR_Movies_SelectByID
        public MST_MovieModel PR_Movies_SelectByID(int MovieID)
        {
            MST_MovieModel movieModel = new MST_MovieModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Movies_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, MovieID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    movieModel.MovieID = Convert.ToInt32(dataRow["MovieID"]);
                    movieModel.Title = dataRow["Title"].ToString();
                    movieModel.Description = dataRow["Description"].ToString();
                    movieModel.ReleaseDate = Convert.ToDateTime(dataRow["ReleaseDate"]);
                    movieModel.Duration = dataRow["Duration"].ToString();
                    movieModel.Language = dataRow["Language"].ToString();
                    movieModel.Director = dataRow["Director"].ToString();
                    movieModel.Rating = Convert.ToDecimal(dataRow["Rating"]);
                    movieModel.Genre = dataRow["Genre"].ToString();
                    movieModel.PosterImageURL = dataRow["PosterImageURL"].ToString();
                    movieModel.TrailerURL = dataRow["TrailerURL"].ToString();
                    Console.Write("Title");
                }
                return movieModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
