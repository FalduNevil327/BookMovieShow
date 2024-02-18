using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.LOC_City
{
    public class LOC_CityDALBase :Dal_Helper
    {
        #region  PR_City_SelectAll
        public DataTable PR_City_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_SelectAll");
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

        #region City Delete
        public bool PR_City_Delete(int CityID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, CityID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region PR_City_Insert
        public bool PR_City_Insert(LOC_CityModel modelCity)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (modelCity.CityID == null)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@CityName", DbType.String, modelCity.CityName);
                    sqlDatabase.AddInParameter(dbCommand, "@CityCode", DbType.String, modelCity.CityCode);
                    sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, modelCity.StateID);
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {

                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, modelCity.CityID);
                    sqlDatabase.AddInParameter(dbCommand, "@CityName", DbType.String, modelCity.CityName);
                    sqlDatabase.AddInParameter(dbCommand, "@CityCode", DbType.String, modelCity.CityCode);
                    sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, modelCity.StateID);
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

        #region PR_City_SelectByPK
        public LOC_CityModel PR_City_SelectByPK(int? CityID)
        {
            LOC_CityModel modelCity = new LOC_CityModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, CityID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    modelCity.CityName = dataRow["CityName"].ToString();
                    modelCity.CityCode = dataRow["CityCode"].ToString();
                    modelCity.StateID = Convert.ToInt32(dataRow["StateID"]);
                }
                return modelCity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
