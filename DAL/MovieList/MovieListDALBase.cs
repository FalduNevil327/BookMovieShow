using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.MovieList
{
    public class MovieListDALBase : Dal_Helper
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

        #region PR_Movies_SelectByStateCityAndCinemaID
        public DataTable PR_Movies_SelectByStateCityAndCinemaID(int StateID, int CityID, int CinemaID)
        {
            MST_MovieModel movieModel = new MST_MovieModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Movies_SelectByStateCityAndCinemaID");
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, StateID);
                sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, CityID);
                //sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, CinemaID);
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

    }
}
