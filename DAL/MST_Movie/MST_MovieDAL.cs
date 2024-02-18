using BookMovieShow.Areas.Admin.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace BookMovieShow.DAL.MST_Movie
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
    }
}
