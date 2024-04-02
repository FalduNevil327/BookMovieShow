using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.LOC_State
{
    public class LOC_StateDALBase : Dal_Helper
    {
        #region PR_State_SelectAll
        public DataTable PR_State_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_SelectAll");
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

        #region PR_State_Insert
        public bool PR_State_Insert(LOC_StateModel lOC_StateModel)
        {
            SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
            try
            {
                if (lOC_StateModel.StateID == null)
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_Insert");
                    sqlDatabase.AddInParameter(dbCommand, "@StateName", DbType.String, lOC_StateModel.StateName);
                    sqlDatabase.AddInParameter(dbCommand, "@StateCode", DbType.String, lOC_StateModel.StateCode);

                    Console.WriteLine("SuccessDALAdd");
                    bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                    return isSuccess;
                }
                else
                {
                    DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_Update");

                    sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, lOC_StateModel.StateID);
                    sqlDatabase.AddInParameter(dbCommand, "@StateName", DbType.String, lOC_StateModel.StateName);
                    sqlDatabase.AddInParameter(dbCommand, "@StateCode", DbType.String, lOC_StateModel.StateCode);

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

        #region  PR_State_Delete
        public bool PR_State_Delete(int StateID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_Delete");
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, StateID);
                bool isSuccess = Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
                return isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region PR_State_SelectByPK
        public LOC_StateModel PR_State_SelectByPK(int StateID)
        {
            LOC_StateModel stateModel = new LOC_StateModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_SelectByPK");
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, StateID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    stateModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    stateModel.StateName = dataRow["StateName"].ToString();
                    stateModel.StateCode = dataRow["StateCode"].ToString();
                }
                return stateModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
