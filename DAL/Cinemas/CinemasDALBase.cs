using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using BookMovieShow.Areas.Admin.Model;

namespace BookMovieShow.DAL.Cinemas
{
    public class CinemasDALBase : Dal_Helper
    {
        #region PR_Cinemas_SelectAll
        public DataTable PR_Cinemas_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cinemas_SelectAll");
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

        #region PR_Cinemas_Insert
        public bool PR_Cinemas_Insert(MST_CinemaModel mST_CinemaModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (mST_CinemaModel.CinemaID == 0)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cinemas_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@CinemaName", DbType.String, mST_CinemaModel.CinemaName);
                    //sqlDatabase.AddInParameter(dbCommand, "@Location", DbType.String, mST_CinemaModel.Location);
                    sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, mST_CinemaModel.CityID);
                    sqlDatabase.AddInParameter(dbCommand, "@Capacity", DbType.Int32, mST_CinemaModel.Capacity);
                    sqlDatabase.AddInParameter(dbCommand, "@ScreenNumber", DbType.Int32, mST_CinemaModel.ScreenNumber);


                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cinemas_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, mST_CinemaModel.CinemaID);
                    sqlDatabase.AddInParameter(dbCommand, "@CinemaName", DbType.String, mST_CinemaModel.CinemaName);
                    //sqlDatabase.AddInParameter(dbCommand, "@Location", DbType.String, mST_CinemaModel.Location);
                    sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, mST_CinemaModel.CityID);
                    sqlDatabase.AddInParameter(dbCommand, "@Capacity", DbType.Int32, mST_CinemaModel.Capacity);
                    sqlDatabase.AddInParameter(dbCommand, "@ScreenNumber", DbType.Int32, mST_CinemaModel.ScreenNumber);
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

        #region  PR_Cinemas_Delete
        public bool PR_Cinemas_Delete(int CinemaID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cinemas_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, CinemaID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PR_Cinemas_SelectByID
        public MST_CinemaModel PR_Cinemas_SelectByID(int CinemaID)
        {
            MST_CinemaModel mST_CinemaModel = new MST_CinemaModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cinemas_SelectByID");
                sqlDatabase.AddInParameter(dbCommand, "@CinemaID", DbType.Int32, CinemaID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    mST_CinemaModel.CinemaID = Convert.ToInt32(dataRow["CinemaID"]);
                    mST_CinemaModel.CityID = Convert.ToInt32(dataRow["CityID"]);
                    mST_CinemaModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    mST_CinemaModel.CinemaName = dataRow["CinemaName"].ToString();
                    mST_CinemaModel.Capacity = Convert.ToInt32(dataRow["Capacity"]);
                    mST_CinemaModel.ScreenNumber = Convert.ToInt32(dataRow["ScreenNumber"].ToString());
                }
                return mST_CinemaModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
