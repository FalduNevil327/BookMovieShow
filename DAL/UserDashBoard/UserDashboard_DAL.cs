﻿using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.Areas.User.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace BookMovieShow.DAL.UserDashBoard
{
    public class UserDashboard_DAL : UserDashboard_DALBase
    {
        #region PR_UserDashBoard_Filter
        public DataTable PR_UserDashBoard_Filter(UserDashBoard_FilterModel filterModel)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_UserDashBoard_Filter");
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, filterModel.StateID);
                sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, filterModel.CityID);
                sqlDatabase.AddInParameter(dbCommand, "@Title", DbType.String, filterModel.Title);
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

        #region PR_Cinema_ComboBoxByStateIDAndCityID
        public List<MST_CinemaDropDownModel> PR_Cinema_ComboBoxByStateIDAndCityID(int? StateID, int? CityID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnectionString);
                DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("PR_Cinema_ComboBoxByStateIDAndCityID");
                sqlDatabase.AddInParameter(dbCommand, "@StateID", DbType.Int32, StateID);
                sqlDatabase.AddInParameter(dbCommand, "@CityID", DbType.Int32, CityID);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    dataTable.Load(dataReader);
                }
                List<MST_CinemaDropDownModel> listOfCinemas = new List<MST_CinemaDropDownModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    MST_CinemaDropDownModel model = new MST_CinemaDropDownModel();
                    model.CinemaID = Convert.ToInt32(dataRow["CinemaID"]);
                    model.CinemaName = dataRow["CinemaName"].ToString();
                    listOfCinemas.Add(model);
                }
                return listOfCinemas;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
