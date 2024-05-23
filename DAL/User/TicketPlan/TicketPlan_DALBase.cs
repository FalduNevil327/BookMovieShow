using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.Areas.User.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.User.TicketPlan
{
    public class TicketPlan_DALBase : Dal_Helper
    {
        #region PR_Showtimes_ByMovieID
        public List<TicketPlanModel> PR_Showtimes_ByMovieID(int MovieID)
        {
            try
            {
                List<TicketPlanModel> ticketPlanModel = new List<TicketPlanModel>();

                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Showtimes_ByMovieID");
                sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, MovieID);

                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        TicketPlanModel model = new TicketPlanModel();
                        model.Title = dataReader["Title"].ToString();
                        model.Genre = dataReader["Genre"].ToString();
                        // Fetch other details like showtime and cinema name for the first row

                        do
                        {
                            // For each row, create a new TicketPlanModel and set its properties
                            TicketPlanModel rowModel = new TicketPlanModel();
                            rowModel.Title = model.Title;
                            rowModel.Genre = model.Genre;
                            rowModel.CinemaName = dataReader["CinemaName"].ToString();
                            rowModel.ShowTime = Convert.ToDateTime(dataReader["ShowTime"]);
                            rowModel.ShowTimeID = Convert.ToInt32(dataReader["ShowTimeID"]);
                            rowModel.MovieID = Convert.ToInt32(dataReader["MovieID"]);
                            rowModel.CinemaID = Convert.ToInt32(dataReader["CinemaID"]);

                            // Add other properties as needed

                            ticketPlanModel.Add(rowModel);
                        } while (dataReader.Read());
                    }
                }

                return ticketPlanModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion


        //#region PR_Showtimes_ByMovieID
        //public List<TicketPlanModel> PR_Showtimes_ByMovieID(int MovieID)
        //{
        //    try
        //    {
        //        List<TicketPlanModel> ticketPlanModel = new List<TicketPlanModel>();

        //        SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
        //        DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Showtimes_ByMovieID");
        //        sqlDatabase.AddInParameter(dbCommand, "@MovieID", DbType.Int32, MovieID);

        //        using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
        //        {
        //            while (dataReader.Read())
        //            {
        //                TicketPlanModel model = new TicketPlanModel();
        //                model.CinemaName = dataReader["CinemaName"].ToString();
        //                model.ShowTime = Convert.ToDateTime(dataReader["ShowTime"]);
        //                // Add other properties as needed

        //                ticketPlanModel.Add(model);
        //            }
        //        }

        //        return ticketPlanModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion
    }
}
