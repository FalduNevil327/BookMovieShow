using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using BookMovieShow.BAL;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BookMovieShow.DAL.Profile
{
    public class Profile_DALBase : Dal_Helper
    {
        #region PR_Profile_SelectAll
        public DataTable PR_Profile_SelectAll(int UserID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Profile_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
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

        #region PR_Profile_Update
        public bool PR_Profile_Update(ProfileModel profileModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            if (profileModel.ProfilePhoto != null)
            {
                string FilePath = "wwwroot\\images";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, profileModel.ProfilePhoto.FileName);

                profileModel.ProfileImage = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + profileModel.ProfilePhoto.FileName;

                using (FileStream fileStream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    profileModel.ProfilePhoto.CopyTo(fileStream);
                }
            }
            try
            {
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Profile_Update");

                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, profileModel.UserID);
                sqlDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, profileModel.UserName);
                sqlDatabase.AddInParameter(dbCommand, "@FullName", DbType.String, profileModel.FullName);
                sqlDatabase.AddInParameter(dbCommand, "@Email", DbType.String, profileModel.Email);
                sqlDatabase.AddInParameter(dbCommand, "@PhoneNumber", DbType.String, profileModel.PhoneNumber);
                sqlDatabase.AddInParameter(dbCommand, "@Address", DbType.String, profileModel.Address);
                sqlDatabase.AddInParameter(dbCommand, "@ProfileImage", DbType.String, profileModel.ProfileImage);
                Console.WriteLine("SuccessDALUpdate");
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
                //}
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PR_Profile_SelectByID
        public ProfileModel PR_Profile_SelectByID(int UserID)
        {
            ProfileModel profileModel = new ProfileModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Profile_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, UserID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    profileModel.UserID = Convert.ToInt32(dataRow["UserID"]);
                    profileModel.UserName = dataRow["UserName"].ToString();
                    profileModel.Password = dataRow["Password"].ToString();
                    profileModel.FullName = dataRow["FullName"].ToString();
                    profileModel.Email = dataRow["Email"].ToString();
                    profileModel.PhoneNumber = dataRow["PhoneNumber"].ToString();
                    profileModel.Address = dataRow["Address"].ToString();
                    profileModel.ProfileImage = dataRow["ProfileImage"].ToString();
                }
                return profileModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
