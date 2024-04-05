using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace BookMovieShow.DAL.Admin.CinemaWithMovies
{
    public class CinemaWithMoviesDAL : CinemaWithMoviesDALBase
    {
        #region PR_Cinemas_ComboBox
        public List<MST_CinemaDropDownModel> PR_Cinemas_ComboBox()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cinemas_ComboBox");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<MST_CinemaDropDownModel> list = new List<MST_CinemaDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    MST_CinemaDropDownModel mST_CinemaDropDown = new MST_CinemaDropDownModel();
                    mST_CinemaDropDown.CinemaID = Convert.ToInt32(dataRow["CinemaID"]);
                    mST_CinemaDropDown.CinemaName = dataRow["CinemaName"].ToString();
                    list.Add(mST_CinemaDropDown);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_Movies_ComboBox
        public List<MST_MoviesDropDownModel> PR_Movies_ComboBox()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Movies_ComboBox");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<MST_MoviesDropDownModel> list = new List<MST_MoviesDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    MST_MoviesDropDownModel mST_MoviesDropDown = new MST_MoviesDropDownModel();
                    mST_MoviesDropDown.MovieID = Convert.ToInt32(dataRow["MovieID"]);
                    mST_MoviesDropDown.Title = dataRow["Title"].ToString();
                    list.Add(mST_MoviesDropDown);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_CinemasWithMovies_Filter
        public DataTable PR_CinemasWithMovies_Filter(CinemaWithMovies_FilterModel filterModel)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_CinemaWithMovies_Filter");
                sqlDatabase.AddInParameter(dbCommand, "@CinemaName", DbType.String, filterModel.CinemaName);
                sqlDatabase.AddInParameter(dbCommand, "@Title", DbType.String, filterModel.Title);
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

        #region PR_Movie_ComboBoxbyCinemaID
        public List<MST_MoviesDropDownModel> PR_Movie_ComboBoxbyCinemaID(int cinemaID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Movie_ComboBoxbyCinemaID");
                sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, cinemaID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<MST_MoviesDropDownModel> listOfMovies = new List<MST_MoviesDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    MST_MoviesDropDownModel model = new MST_MoviesDropDownModel();
                    model.MovieID = Convert.ToInt32(dataRow["MovieID"]);
                    model.Title = dataRow["Title"].ToString();
                    listOfMovies.Add(model);
                }
                return listOfMovies;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion




    }
}
