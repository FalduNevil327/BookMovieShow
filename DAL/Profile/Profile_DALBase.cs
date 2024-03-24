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
            try
            {
                //if (mST_CinemaModel.CinemaID == 0)
                //{
                //    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cinemas_Insert");
                //    sqlDatabase.AddInParameter(dbCommand, "@CinemaName", DbType.String, mST_CinemaModel.CinemaName);
                //    //sqlDatabase.AddInParameter(dbCommand, "@Location", DbType.String, mST_CinemaModel.Location);
                //    sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, mST_CinemaModel.CityID);
                //    sqlDatabase.AddInParameter(dbCommand, "@Capacity", DbType.Int32, mST_CinemaModel.Capacity);
                //    sqlDatabase.AddInParameter(dbCommand, "@ScreenNumber", DbType.Int32, mST_CinemaModel.ScreenNumber);


                //    Console.WriteLine("SuccessDALAdd");
                //    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                //    return isSuccess;
                //}
                //else
                //{
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Profile_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@UserID", DbType.Int32, profileModel.UserID);
                    sqlDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, profileModel.UserName);
                    sqlDatabase.AddInParameter(dbCommand, "@FullName", DbType.String, profileModel.FullName);
                    sqlDatabase.AddInParameter(dbCommand, "@Email", DbType.String, profileModel.Email);
                    sqlDatabase.AddInParameter(dbCommand, "@PhoneNumber", DbType.Int32, profileModel.PhoneNumber);
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
                    profileModel.PhoneNumber = Convert.ToInt32(dataRow["PhoneNumber"]);
                    profileModel.Address = dataRow["Address"].ToString();
                    profileModel.ProfileImage =dataRow["ProfileImage"].ToString();
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
