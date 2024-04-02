using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.Payment
{
    public class Payment_DALBase : Dal_Helper
    {
        #region PR_Payment_SelectAll
        public DataTable PR_Payment_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Payment_SelectAll");
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

        #region PR_Payment_Insert
        public bool PR_Payment_Insert(PaymentModel paymentModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (paymentModel.PaymentID == null)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Payment_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, paymentModel.UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@ShowTimeID", DbType.Int32, paymentModel.ShowTimeID);
                    //sqlDatabase.AddInParameter(dbCommand, "@TransactionID", DbType.Int32, paymentModel.TransactionID);
                    sqlDatabase.AddInParameter(dbCommand, "@Amount", DbType.Decimal, paymentModel.Amount);
                    //sqlDatabase.AddInParameter(dbCommand, "@PaymentDate", DbType.DateTime, paymentModel.PaymentDate);
                    sqlDatabase.AddInParameter(dbCommand, "@PaymentStatus", DbType.String, paymentModel.PaymentStatus);


                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Payment_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@PaymentID", DbType.Int32, paymentModel.PaymentID);
                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, paymentModel.UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@ShowTimeID", DbType.Int32, paymentModel.ShowTimeID);
                    //sqlDatabase.AddInParameter(dbCommand, "@TransactionID", DbType.Int32, paymentModel.TransactionID);
                    sqlDatabase.AddInParameter(dbCommand, "@Amount", DbType.Decimal, paymentModel.Amount);
                    //sqlDatabase.AddInParameter(dbCommand, "@PaymentDate", DbType.DateTime, paymentModel.PaymentDate);
                    sqlDatabase.AddInParameter(dbCommand, "@PaymentStatus", DbType.String, paymentModel.PaymentStatus);
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

        #region  PR_Payment_Delete
        public bool PR_Payment_Delete(int PaymentID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Payment_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@PaymentID", DbType.Int32, PaymentID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PR_Payment_SelectByID
        public PaymentModel PR_Payment_SelectByID(int PaymentID)
        {
            PaymentModel model = new PaymentModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Payment_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@PaymentID", DbType.Int32, PaymentID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    model.UserID = Convert.ToInt32(dataRow["UserID"]);
                    model.ShowTimeID = Convert.ToInt32(dataRow["UserID"]);
                    model.Amount = Convert.ToDecimal(dataRow["Amount"]);
                    model.PaymentStatus = dataRow["PaymentStatus"].ToString();
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
