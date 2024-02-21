using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.ShowTime
{
    public class ShowTimeDALBase : Dal_Helper
    {
        #region PR_ShowTimes_SelectAll
        public DataTable PR_ShowTimes_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_ShowTimes_SelectAll");
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

        #region PR_ShowTimes_Insert
        public bool PR_ShowTimes_Insert(ShowTimeModel showTimeModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (showTimeModel.ShowTimeID == null)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_ShowTimes_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, showTimeModel.MovieID);
                    sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, showTimeModel.CinemaID);
                    sqlDatabase.AddInParameter(dbCommand, "@ScreenID", DbType.Int32, showTimeModel.ScreenID);
                    sqlDatabase.AddInParameter(dbCommand, "@ShowTime", DbType.DateTime, showTimeModel.ShowTime);
                    sqlDatabase.AddInParameter(dbCommand, "@AvailableSeats", DbType.Int32, showTimeModel.AvailableSeats);
                    sqlDatabase.AddInParameter(dbCommand, "@Price", DbType.Decimal, showTimeModel.Price);


                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_ShowTimes_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, showTimeModel.MovieID);
                    sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, showTimeModel.CinemaID);
                    sqlDatabase.AddInParameter(dbCommand, "@ScreenID", DbType.Int32, showTimeModel.ScreenID);
                    sqlDatabase.AddInParameter(dbCommand, "@ShowTime", DbType.DateTime, showTimeModel.ShowTime);
                    sqlDatabase.AddInParameter(dbCommand, "@AvailableSeats", DbType.Int32, showTimeModel.AvailableSeats);
                    sqlDatabase.AddInParameter(dbCommand, "@Price", DbType.Decimal, showTimeModel.Price);
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

        #region  PR_ShowTimes_Delete
        public bool PR_ShowTimes_Delete(int ShowTimeID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_ShowTimes_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@ShowTimeID", DbType.Int32, ShowTimeID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PR_ShowTimes_SelectByID
        public ShowTimeModel PR_ShowTimes_SelectByID(int ShowTimeID)
        {
            ShowTimeModel model = new ShowTimeModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_ShowTimes_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@ShowTimeID", DbType.Int32, ShowTimeID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    model.ShowTimeID = Convert.ToInt32(dataRow["ShowTimeID"]);
                    model.MovieID = Convert.ToInt32(dataRow["MovieID"]);
                    model.CinemaID = Convert.ToInt32(dataRow["CinemaID"]);
                    model.ScreenID = Convert.ToInt32(dataRow["ScreenID"]);
                    model.ShowTime = Convert.ToDateTime(dataRow["ShowTime"]).ToUniversalTime();
                    model.AvailableSeats = Convert.ToInt32(dataRow["AvailableSeats"]);
                    model.Price = Convert.ToInt32(dataRow["Price"]);
                }
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
