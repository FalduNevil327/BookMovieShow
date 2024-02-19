using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.Screen
{
    public class ScreenDALBase : Dal_Helper
    {
        #region PR_Screens_SelectAll
        public DataTable PR_Cinemas_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Screens_SelectAll");
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

        #region PR_Screens_Insert
        public bool PR_Screens_Insert(ScreenModel screenModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (screenModel.ScreenID == null)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Screens_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@ScreenName", DbType.String, screenModel.ScreenName);
                    //sqlDatabase.AddInParameter(dbCommand, "@Location", DbType.String, mST_CinemaModel.Location);
                    sqlDatabase.AddInParameter(dbCommand, "@Capacity", DbType.Int32, screenModel.Capacity);
                    sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, screenModel.CinemaID);
                    sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, screenModel.MovieID);


                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Screens_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@ScreenID", DbType.Int32, screenModel.ScreenID);
                    sqlDatabase.AddInParameter(dbCommand, "@ScreenName", DbType.String, screenModel.ScreenName);
                    sqlDatabase.AddInParameter(dbCommand, "@Capacity", DbType.Int32, screenModel.Capacity);
                    sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, screenModel.CinemaID);
                    sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, screenModel.MovieID);
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

        #region  PR_Screens_Delete
        public bool PR_Screens_Delete(int ScreenID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Screens_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@ScreenID", DbType.Int32, ScreenID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PR_Screens_SelectByID
        public ScreenModel PR_Screens_SelectByID(int ScreenID)
        {
            ScreenModel screenModel = new ScreenModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Screens_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@ScreenID", DbType.Int32, ScreenID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    screenModel.ScreenID = Convert.ToInt32(dataRow["CinemaID"]);
                    screenModel.ScreenName = dataRow["ScreenName"].ToString();
                    screenModel.Capacity = Convert.ToInt32(dataRow["Capacity"]);
                    screenModel.CinemaID = Convert.ToInt32(dataRow["CinemaID"]);
                    screenModel.MovieID = Convert.ToInt32(dataRow["MovieID"]);
                }
                return screenModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
