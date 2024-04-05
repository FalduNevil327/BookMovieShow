using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.Admin.Ticket
{
    public class Ticket_DAL : Ticket_DALBase
    {
        #region PR_Tickets_SearchByUserName
        public DataTable PR_Tickets_SearchByUserName(string? UserName)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Tickets_SearchByUserName");
                sqlDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, UserName);
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
