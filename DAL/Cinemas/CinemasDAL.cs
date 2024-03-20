using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.Cinemas
{
    public class CinemasDAL : CinemasDALBase
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

        #region PR_City_ComboBox
        public List<LOC_CityDropDownModel> PR_City_ComboBox()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_ComboBox");
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<LOC_CityDropDownModel> list = new List<LOC_CityDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    LOC_CityDropDownModel lOC_CityDropDownModel = new LOC_CityDropDownModel();
                    lOC_CityDropDownModel.CityID = Convert.ToInt32(dataRow["CityID"]);
                    lOC_CityDropDownModel.CityName = dataRow["CityName"].ToString();
                    list.Add(lOC_CityDropDownModel);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region PR_Cinemas_Filter
        public DataTable PR_Cinemas_Filter(MST_CinemaFilterModel filterModel)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cinemas_Filter");
                sqlDatabase.AddInParameter(dbCommand, "@CinemaName", DbType.String, filterModel.CinemaName);
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, filterModel.StateID);
                sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.String, filterModel.CityID);
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

        #region PR_City_ComboBoxbyStateID
        public List<LOC_CityDropDownModel> PR_City_ComboBoxbyStateID(int StateID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_City_ComboBoxbyStateID");
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, StateID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<LOC_CityDropDownModel> listOfCity = new List<LOC_CityDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    LOC_CityDropDownModel model = new LOC_CityDropDownModel();
                    model.CityID = Convert.ToInt32(dataRow["CityID"]);
                    model.CityName = dataRow["CityName"].ToString();
                    listOfCity.Add(model);
                }
                return listOfCity;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


    }
}
