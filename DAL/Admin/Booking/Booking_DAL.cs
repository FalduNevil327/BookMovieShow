using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.Admin.Booking
{
    public class Booking_DAL : Booking_DALBase
    {
        #region PR_Booking_SearchByUserName
        public DataTable PR_Booking_SearchByUserName(string? UserName)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Booking_SearchByUserName");
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
