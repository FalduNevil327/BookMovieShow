using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.Admin.Ticket
{
    public class Ticket_DALBase : Dal_Helper
    {
        #region PR_Tickets_SelectAll
        public DataTable PR_Ticket_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Tickets_SelectAll");
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

        #region PR_Tickets_Insert
        public bool PR_Tickets_Insert(TicketModel ticketModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (ticketModel.TicketID == null)
                {

                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Tickets_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, ticketModel.UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@ShowTimeID", DbType.Int32, ticketModel.ShowTimeID);
                    sqlDatabase.AddInParameter(dbCommand, "@BookingID", DbType.Int32, ticketModel.BookingID);
                    sqlDatabase.AddInParameter(dbCommand, "@SeatNumbers", DbType.String, ticketModel.SeatNumbers);
                    sqlDatabase.AddInParameter(dbCommand, "@QRCode", DbType.String, ticketModel.QRCode);
                    sqlDatabase.AddInParameter(dbCommand, "@TicketMode", DbType.String, ticketModel.TicketMode);
                    sqlDatabase.AddInParameter(dbCommand, "@TicketStatus", DbType.String, ticketModel.TicketStatus);


                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Tickets_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, ticketModel.UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@ShowTimeID", DbType.Int32, ticketModel.ShowTimeID);
                    sqlDatabase.AddInParameter(dbCommand, "@BookingID", DbType.Int32, ticketModel.BookingID);
                    sqlDatabase.AddInParameter(dbCommand, "@SeatNumbers", DbType.String, ticketModel.SeatNumbers);
                    sqlDatabase.AddInParameter(dbCommand, "@QRCode", DbType.String, ticketModel.QRCode);
                    sqlDatabase.AddInParameter(dbCommand, "@TicketMode", DbType.String, ticketModel.TicketMode);
                    sqlDatabase.AddInParameter(dbCommand, "@TicketStatus", DbType.String, ticketModel.TicketStatus);
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

        #region  PR_Tickets_Delete
        public bool PR_Tickets_Delete(int TicketsID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Tickets_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@TicketsID", DbType.Int32, TicketsID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PR_Tickets_SelectByID
        public TicketModel PR_Tickets_SelectByID(int TicketID)
        {
            TicketModel model = new TicketModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Tickets_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@TicketID", DbType.Int32, TicketID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    model.UserID = Convert.ToInt32(dataRow["UserID"]);
                    model.ShowTimeID = Convert.ToInt32(dataRow["ShowTimeID"]);
                    model.BookingID = Convert.ToInt32(dataRow["BookingID"]);
                    model.SeatNumbers = dataRow["SeatNumbers"].ToString();
                    model.QRCode = dataRow["QRCode"].ToString();
                    model.TicketMode = dataRow["TicketMode"].ToString();
                    model.TicketStatus = dataRow["TicketStatus"].ToString();
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
