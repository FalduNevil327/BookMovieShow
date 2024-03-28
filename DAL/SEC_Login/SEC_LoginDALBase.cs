using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.Areas.SEC_Login.Models;

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
        public bool SEC_User_Register(SEC_LoginModel sEC_LoginModel)
        {
            
            
            if (sEC_LoginModel.ProfilePhoto != null)
            {
                string FilePath = "wwwroot\\images";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, sEC_LoginModel.ProfilePhoto.FileName);

                sEC_LoginModel.ProfileImage = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + sEC_LoginModel.ProfilePhoto.FileName;

                using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    sEC_LoginModel.ProfilePhoto.CopyTo(fileStream);
                }
            }
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("User_SelectUserName");
                sqlDatabase.AddInParameter(dbCommand, "UserName", DbType.String, sEC_LoginModel.UserName);
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
                    sqlDatabase.AddInParameter(dbCommand1, "UserName", DbType.String, sEC_LoginModel.UserName);
                    sqlDatabase.AddInParameter(dbCommand1, "Password", DbType.String, sEC_LoginModel.Password);
                    sqlDatabase.AddInParameter(dbCommand1, "FullName", DbType.String, sEC_LoginModel.FullName);
                    sqlDatabase.AddInParameter(dbCommand1, "PhoneNumber", DbType.Int32, sEC_LoginModel.PhoneNumber);
                    sqlDatabase.AddInParameter(dbCommand1, "Email", DbType.String, sEC_LoginModel.Email);
                    sqlDatabase.AddInParameter(dbCommand1, "Address", DbType.String, sEC_LoginModel.Address);
                    sqlDatabase.AddInParameter(dbCommand1, "ProfileImage", DbType.String, sEC_LoginModel.ProfileImage);
                    //sqlDatabase.AddInParameter(dbCommand1, "isAdmin", DbType.Boolean, DBNull.Value);
                    //sqlDatabase.AddInParameter(dbCommand1, "IsActive", DbType.Boolean, DBNull.Value);
                    //sqlDatabase.AddInParameter(dbCommand1, "RegistrationDate", SqlDbType.DateTime, DBNull.Value);
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
