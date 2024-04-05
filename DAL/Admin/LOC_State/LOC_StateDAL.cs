using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.Admin.LOC_State
{
    public class LOC_StateDAL : LOC_StateDALBase
    {
        #region PR_State_ComboBox
        public List<LOC_StateDropDownModel> PR_State_ComboBox()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_ComboBox");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    LOC_StateDropDownModel lOC_StateDropDownModel = new LOC_StateDropDownModel();
                    lOC_StateDropDownModel.StateID = Convert.ToInt32(dataRow["StateID"]);
                    lOC_StateDropDownModel.StateName = dataRow["StateName"].ToString();
                    list.Add(lOC_StateDropDownModel);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_State_Filter
        public DataTable PR_State_Filter(LOC_StateFilterModel filterModel)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_State_Filter");
                sqlDatabase.AddInParameter(dbCommand, "@StateCode", DbType.String, filterModel.StateCode);
                sqlDatabase.AddInParameter(dbCommand, "@StateName", DbType.String, filterModel.StateName);
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
    }
}
