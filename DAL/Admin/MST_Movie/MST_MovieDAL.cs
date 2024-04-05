using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace BookMovieShow.DAL.Admin.MST_Movie
{
    public class MST_MovieDAL : MST_MovieDALBase
    {
        #region PR_Movies_Filter
        public DataTable PR_Movies_Filter(MST_MovieFilterModel filterModel)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Movies_Filter");
                sqlDatabase.AddInParameter(dbCommand, "@Language", DbType.String, filterModel.Language);
                sqlDatabase.AddInParameter(dbCommand, "@Genre", DbType.String, filterModel.Genre);
                sqlDatabase.AddInParameter(dbCommand, "@Rating", DbType.String, filterModel.Rating);
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
