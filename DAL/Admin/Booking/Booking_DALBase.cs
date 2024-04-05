using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.Admin.Booking
{
    public class Booking_DALBase : Dal_Helper
    {
        #region PR_Booking_SelectAll
        public DataTable PR_Booking_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Booking_SelectAll");
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

        #region PR_Booking_Insert
        public bool PR_Booking_Insert(BookingModel bookingModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (bookingModel.BookingID == null)
                {

                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Booking_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, bookingModel.UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@ShowTimeID", DbType.Int32, bookingModel.ShowTimeID);
                    sqlDatabase.AddInParameter(dbCommand, "@NumberOfTickets", DbType.Int32, bookingModel.NumberOfTickets);
                    sqlDatabase.AddInParameter(dbCommand, "@TotalAmount", DbType.Decimal, bookingModel.TotalAmount);
                    sqlDatabase.AddInParameter(dbCommand, "@SeatNumbers", DbType.String, bookingModel.SeatNumbers);
                    sqlDatabase.AddInParameter(dbCommand, "@BookingStatus", DbType.String, bookingModel.BookingStatus);


                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Booking_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@BookingID", DbType.Int32, bookingModel.BookingID);
                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, bookingModel.UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@ShowTimeID", DbType.Int32, bookingModel.ShowTimeID);
                    sqlDatabase.AddInParameter(dbCommand, "@NumberOfTickets", DbType.Int32, bookingModel.NumberOfTickets);
                    sqlDatabase.AddInParameter(dbCommand, "@TotalAmount", DbType.Decimal, bookingModel.TotalAmount);
                    sqlDatabase.AddInParameter(dbCommand, "@SeatNumbers", DbType.String, bookingModel.SeatNumbers);
                    sqlDatabase.AddInParameter(dbCommand, "@BookingStatus", DbType.String, bookingModel.BookingStatus);
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

        #region  PR_Booking_Delete
        public bool PR_Booking_Delete(int BookingID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Booking_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@BookingID", DbType.Int32, BookingID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PR_Booking_SelectByID
        public BookingModel PR_Booking_SelectByID(int BookingID)
        {
            BookingModel model = new BookingModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Booking_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@BookingID", DbType.Int32, BookingID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    model.UserID = Convert.ToInt32(dataRow["UserID"]);
                    model.ShowTimeID = Convert.ToInt32(dataRow["ShowTimeID"]);
                    model.NumberOfTickets = Convert.ToInt32(dataRow["NumberOfTickets"]);
                    model.SeatNumbers = dataRow["SeatNumbers"].ToString();
                    model.TotalAmount = Convert.ToDecimal(dataRow["TotalAmount"]);
                    model.BookingStatus = dataRow["BookingStatus"].ToString();
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
