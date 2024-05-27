using BookMovieShow.Areas.User.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.User.BookingDetail
{
    public class BookingDetailDALBase : Dal_Helper
    {
        #region PR_Bookings_GetDetails
        public BookingDetailModel PR_Bookings_GetDetails(int BookingID,int ShowtimeID)
        {
            BookingDetailModel model = new BookingDetailModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Bookings_GetDetails");
                sqlDatabase.AddInParameter(dbCommand, "@BookingID", DbType.Int32, BookingID);
                sqlDatabase.AddInParameter(dbCommand, "@ShowtimeID", DbType.Int32, ShowtimeID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dr = dataTable.Rows[0];
                    //model.BookingID = Convert.ToInt32(dr["BookingID"]);
                    //model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.Title = dr["Title"].ToString();
                    model.Language = dr["Language"].ToString();
                    model.Genre = dr["Genre"].ToString();
                    model.CinemaName = dr["CinemaName"].ToString();
                    model.NumberOfTickets = Convert.ToInt32(dr["NumberOfTickets"]);
                    model.Showtime = Convert.ToDateTime(dr["Showtime"]);
                    model.price = Convert.ToDecimal(dr["Showtime"]);
                    model.SeatNumbers = dr["SeatNumbers"].ToString().Split(',').ToList();
                    model.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                    model.BookingDate = Convert.ToDateTime(dr["BookingDate"]);
                    //model.BookingStatus = dr["BookingStatus"].ToString();
                }
                return model;
            }
            catch (Exception ex)
            {
                // Log the exception
                return null;
            }
        }
        #endregion


    }
}
