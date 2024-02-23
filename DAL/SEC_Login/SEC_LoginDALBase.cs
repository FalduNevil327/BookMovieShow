using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace BookMovieShow.DAL.SEC_Login
{
    public class SEC_LoginDALBase: Dal_Helper
    {

        #region Method: PR_SEC_Login_SelectByUserNamePassword
        public DataTable PR_SEC_Login_SelectByUserNamePassword(string UserName, string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_Login_SelectByUserNamePassword");
                sqlDB.AddInParameter(dbCMD, "Username", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: SEC_User_Register
        public bool SEC_User_Register(string UserName, string Password, string FullName, int PhoneNumber, string Email,String Address)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("User_SelectUserName");
                sqlDatabase.AddInParameter(dbCommand, "UserName", DbType.String, UserName);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                if (dataTable.Rows.Count > 0)
                {

                    return false;
                }
                else
                {
                    DbCommand dbCommand1 = sqlDatabase.GetStoredProcCommand("PR_User_Insert");
                    sqlDatabase.AddInParameter(dbCommand1, "UserName", DbType.String, UserName);
                    sqlDatabase.AddInParameter(dbCommand1, "Password", DbType.String, Password);
                    sqlDatabase.AddInParameter(dbCommand1, "FullName", DbType.String, FullName);
                    sqlDatabase.AddInParameter(dbCommand1, "PhoneNumber", DbType.Int32, PhoneNumber);
                    sqlDatabase.AddInParameter(dbCommand1, "Email", DbType.String, Email);
                    sqlDatabase.AddInParameter(dbCommand1, "Address", DbType.String, Address);
                    sqlDatabase.AddInParameter(dbCommand1, "isAdmin", DbType.Boolean, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand1, "IsActive", DbType.Boolean, DBNull.Value);
                    sqlDatabase.AddInParameter(dbCommand1, "RegistrationDate", SqlDbType.DateTime, DBNull.Value);
                    if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand1)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion



    }
}
