using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace BookMovieShow.DAL.CinemaWithMovies
{
    public class CinemaWithMoviesDALBase : Dal_Helper
    {

        #region PR_CinemaWithMovies_SelectAll
        public DataTable PR_CinemaWithMovies_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CinemaWithMovies_SelectAll");
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

        #region PR_CinemaWithMovies_Insert
        public bool PR_CinemaWithMovies_Insert(CinemaWithMoviesModel cWM)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (cWM.ID == 0)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CinemaWithMovies_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, cWM.CinemaID);
                    sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, cWM.MovieID);
                    //sqlDatabase.AddInParameter(dbCommand, "@MovieIDs", DbType.Int32, cWM.MovieIDs);
                    
                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CinemaWithMovies_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@ID", DbType.Int32, cWM.ID);
                    sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, cWM.CinemaID);
                    sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, cWM.MovieID);
                    //sqlDatabase.AddInParameter(dbCommand, "@MovieIDs", DbType.Int32, cWM.MovieIDs);
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

        #region PR_CinemaWithMovies_SelectByID
        public CinemaWithMoviesModel PR_CinemaWithMovies_SelectByID(int ID)
        {
            CinemaWithMoviesModel cWM = new CinemaWithMoviesModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CinemaWithMovies_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@ID", DbType.Int32, ID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    cWM.ID = Convert.ToInt32(dataRow["ID"]);
                    cWM.CinemaID = Convert.ToInt32(dataRow["CinemaID"]);
                    cWM.MovieID = Convert.ToInt32(dataRow["MovieID"]);
                }
                return cWM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  PR_CinemaWithMovies_Delete
        public bool PR_CinemaWithMovies_Delete(int ID)
            {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CinemaWithMovies_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@ID", DbType.Int32, ID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        




    }
}
