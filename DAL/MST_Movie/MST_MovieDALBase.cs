using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace BookMovieShow.DAL.MST_Movie
{
    public class MST_MovieDALBase : Dal_Helper
    {
        #region PR_Movies_SelectAll
        public DataTable PR_Movies_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Movies_SelectAll");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_Movies_Insert
        public bool PR_Movies_Insert(MST_MovieModel mST_MovieModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (mST_MovieModel.MovieID == 0)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Movies_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@Title", DbType.String, mST_MovieModel.Title);
                    sqlDatabase.AddInParameter(dbCommand, "@Description", DbType.String, mST_MovieModel.Description);
                    sqlDatabase.AddInParameter(dbCommand, "@Duration", DbType.Int32, mST_MovieModel.Duration);
                    sqlDatabase.AddInParameter(dbCommand, "@ReleaseDate", DbType.Date, mST_MovieModel.ReleaseDate);
                    sqlDatabase.AddInParameter(dbCommand, "@Language", DbType.String, mST_MovieModel.Language);
                    sqlDatabase.AddInParameter(dbCommand, "@Director", DbType.String, mST_MovieModel.Director);
                    sqlDatabase.AddInParameter(dbCommand, "@Rating", DbType.Decimal, mST_MovieModel.Rating);
                    sqlDatabase.AddInParameter(dbCommand, "@Genre", DbType.String, mST_MovieModel.Genre);
                    sqlDatabase.AddInParameter(dbCommand, "@PosterImageURL", DbType.String, mST_MovieModel.PosterImageURL);
                    sqlDatabase.AddInParameter(dbCommand, "@TrailerURL", DbType.String, mST_MovieModel.TrailerURL);


                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Movies_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, mST_MovieModel.MovieID);
                    sqlDatabase.AddInParameter(dbCommand, "@Title", DbType.String, mST_MovieModel.Title);
                    sqlDatabase.AddInParameter(dbCommand, "@Description", DbType.String, mST_MovieModel.Description);
                    sqlDatabase.AddInParameter(dbCommand, "@Duration", DbType.Int32, mST_MovieModel.Duration);
                    sqlDatabase.AddInParameter(dbCommand, "@ReleaseDate", DbType.Date, mST_MovieModel.ReleaseDate);
                    sqlDatabase.AddInParameter(dbCommand, "@Language", DbType.String, mST_MovieModel.Language);
                    sqlDatabase.AddInParameter(dbCommand, "@Director", DbType.String, mST_MovieModel.Director);
                    sqlDatabase.AddInParameter(dbCommand, "@Rating", DbType.Decimal, mST_MovieModel.Rating);
                    sqlDatabase.AddInParameter(dbCommand, "@Genre", DbType.String, mST_MovieModel.Genre);
                    sqlDatabase.AddInParameter(dbCommand, "@PosterImageURL", DbType.String, mST_MovieModel.PosterImageURL);
                    sqlDatabase.AddInParameter(dbCommand, "@TrailerURL", DbType.String, mST_MovieModel.TrailerURL);
                    Console.WriteLine("SuccessDALUpdate");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region  PR_Movies_Delete
        public bool PR_Movies_Delete(int MovieID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Movies_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, MovieID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

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
