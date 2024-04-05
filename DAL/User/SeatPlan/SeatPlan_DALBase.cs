using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.Areas.User.Model;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.User.SeatPlan
{
    public class SeatPlan_DALBase : Dal_Helper
    {
        //public List<SeatPlanModel> PR_Showtimes_ForSeatPlan(int MovieID)
        //{
        //    try
        //    {
        //        List<SeatPlanModel> seatPlanModels = new List<SeatPlanModel>();

        //        SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
        //        DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Showtimes_ForSeatPlan");
        //        sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, MovieID);

        //        using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
        //        {
        //            while (dataReader.Read())
        //            {
        //                SeatPlanModel Model = new SeatPlanModel();
        //                Model.Title = dataReader["MovieTitle"].ToString();
        //                Model.Genre = dataReader["Genre"].ToString();
        //                Model.Language = dataReader["Language"].ToString();
        //                Model.ShowTime = Convert.ToDateTime(dataReader["ShowTime"]);
        //                Model.CinemaName = dataReader["CinemaName"].ToString();

        //                seatPlanModels.Add(Model);
        //            }
        //        }

        //        return seatPlanModels;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        #region PR_Showtimes_ForSeatPlan
        public SeatPlanModel PR_Showtimes_ForSeatPlan(int ShowTimeID)
        {
            SeatPlanModel Model = new SeatPlanModel();
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Showtimes_ForSeatPlan");
                sqlDatabase.AddInParameter(dbCommand, "@ShowTimeID", DbType.Int32, ShowTimeID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                foreach (DataRow dr in dataTable.Rows)
                {
                    Model.Title = dr["MovieTitle"].ToString();
                    Model.Genre = dr["Genre"].ToString();
                    Model.Language = dr["Language"].ToString();
                    Model.ShowTime = Convert.ToDateTime(dr["ShowTime"]);
                    Model.CinemaName = dr["CinemaName"].ToString();
                    Model.Price = Convert.ToInt32(dr["Price"]);
                }
                return Model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}
