using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.Admin.MST_User
{
    public class MST_UserDALBase : Dal_Helper
    {
        #region PR_User_SelectAll
        public DataTable PR_User_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_SelectAll");
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

        #region PR_User_Insert
        public bool PR_User_Insert(MST_UserModel mST_UserModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (mST_UserModel.UserID == null)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, mST_UserModel.UserName);
                    sqlDatabase.AddInParameter(dbCommand, "@Password", DbType.String, mST_UserModel.Password);
                    sqlDatabase.AddInParameter(dbCommand, "@FullName", DbType.String, mST_UserModel.FullName);
                    sqlDatabase.AddInParameter(dbCommand, "@Email", DbType.String, mST_UserModel.Email);
                    sqlDatabase.AddInParameter(dbCommand, "@PhoneNumber", DbType.Int32, mST_UserModel.PhoneNumber);
                    sqlDatabase.AddInParameter(dbCommand, "@Address", DbType.String, mST_UserModel.Address);
                    //sqlDatabase.AddInParameter(dbCommand, "@RagistrationDate", DbType.DateTime, mST_UserModel.RegistrationDate);
                    //sqlDatabase.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, mST_UserModel.IsAdmin);
                    //sqlDatabase.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, mST_UserModel.IsActive);


                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, mST_UserModel.UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, mST_UserModel.UserName);
                    sqlDatabase.AddInParameter(dbCommand, "@Password", DbType.String, mST_UserModel.Password);
                    sqlDatabase.AddInParameter(dbCommand, "@FullName", DbType.String, mST_UserModel.FullName);
                    sqlDatabase.AddInParameter(dbCommand, "@Email", DbType.String, mST_UserModel.Email);
                    sqlDatabase.AddInParameter(dbCommand, "@PhoneNumber", DbType.Int32, mST_UserModel.PhoneNumber);
                    sqlDatabase.AddInParameter(dbCommand, "@Address", DbType.String, mST_UserModel.Address);
                    //sqlDatabase.AddInParameter(dbCommand, "@RagistrationDate", DbType.DateTime, mST_UserModel.RegistrationDate);
                    //sqlDatabase.AddInParameter(dbCommand, "@IsAdmin", DbType.Boolean, mST_UserModel.IsAdmin);
                    //sqlDatabase.AddInParameter(dbCommand, "@IsActive", DbType.Boolean, mST_UserModel.IsActive);
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

        #region  PR_User_Delete
        public bool PR_User_Delete(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PR_User_SelectByID
        public MST_UserModel PR_User_SelectByID(int UserID)
        {
            MST_UserModel mST_UserModel = new MST_UserModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_User_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    //mST_UserModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                    mST_UserModel.UserName = dataRow["UserName"].ToString();
                    mST_UserModel.Password = dataRow["Password"].ToString();
                    mST_UserModel.FullName = dataRow["FullName"].ToString();
                    mST_UserModel.Email = dataRow["Email"].ToString();
                    mST_UserModel.PhoneNumber = Convert.ToInt32(dataRow["PhoneNumber"].ToString());
                    mST_UserModel.Address = dataRow["Address"].ToString();
                    //mST_UserModel.RegistrationDate = Convert.ToDateTime(dataRow["RagistrationDate"]);
                    //mST_UserModel.IsActive = Convert.ToBoolean(dataRow["IsActive"]);
                    //mST_UserModel.IsAdmin = Convert.ToBoolean(dataRow["IsAdmin"]);
                }
                return mST_UserModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
