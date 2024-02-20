using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.ShowTime
{
    public class ShowTimeDAL : ShowTimeDALBase
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

        #region PR_Screens_ComboBox
        public List<Screens_DropDownModel> PR_Screens_ComboBox()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Screens_ComboBox");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<Screens_DropDownModel> list = new List<Screens_DropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Screens_DropDownModel dD = new Screens_DropDownModel();
                    dD.ScreenID = Convert.ToInt32(dataRow["ScreenID"]);
                    dD.ScreenName = dataRow["ScreenName"].ToString();
                    list.Add(dD);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
