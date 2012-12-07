// ***********************************************************************
// Assembly         : DataExpose
// Author           : Jonathan Sheely
// Website          : http://inspectorit.com
// License          : New BSD License (BSD)
// Created          : 12-03-2012
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 12-03-2012
// ***********************************************************************
// <copyright file="FeedController.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.Services.Exceptions;
using InspectorIT.DataExpose.Components.Entities;
using InspectorIT.DataExpose.Components.Common;

namespace InspectorIT.DataExpose.Components.Controllers
{
    /// <summary>
    /// Class FeedController
    /// </summary>
    public class FeedController
    {
        /// <summary>
        /// Gets the feed list.
        /// </summary>
        /// <returns>List{Feed}.</returns>
        public static List<Feed>  GetFeedList()
        {
            return CBO.FillCollection<Feed>(DataProvider.Instance().ExecuteReader(Constants.DbPrefix + "Get_Feeds"));
        }

        /// <summary>
        /// Gets the name of the feed by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Feed.</returns>
        public static Feed GetFeedByName(string name)
        {
            return CBO.FillObject<Feed>(DataProvider.Instance().ExecuteReader(Constants.DbPrefix + "Get_FeedByName", name));
        }

        /// <summary>
        /// Gets the feed details.
        /// </summary>
        /// <param name="feedId">The feed id.</param>
        /// <returns>Feed.</returns>
        public static Feed GetFeedDetails(int feedId)
        {
            return CBO.FillObject<Feed>(DataProvider.Instance().ExecuteReader(Constants.DbPrefix + "Get_Feed", feedId));
        }

        /// <summary>
        /// Saves the specified obj feed.
        /// </summary>
        /// <param name="objFeed">The obj feed.</param>
        /// <returns>System.Int32.</returns>
        public static int Save(Feed objFeed)
        {
            return DataProvider.Instance().ExecuteScalar<int>(Constants.DbPrefix + "Update_Feed", objFeed.ID, objFeed.Name,objFeed.Description,objFeed.ProcName, objFeed.SQL,objFeed.Roles,objFeed.ModifiedByUserId);
        }

        /// <summary>
        /// Executes the feed.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="feedParams">The feed params.</param>
        /// <returns>ResultsInfo.</returns>
        public static ResultsInfo ExecuteFeed(string name, object[] feedParams)
        {
            ResultsInfo resultInfo = new ResultsInfo();
            if(feedParams!=null)
            {

                try
                {
                    resultInfo.ResultData= DataProvider.Instance().ExecuteDataSet(name, feedParams);

                }
                catch (SqlException ex)
                {

                    resultInfo.ErrorMessage = StoredProcParams(ex, name);
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                    resultInfo.ErrorMessage = new ErrorMessageInfo() { Message = ex.ToString() };
                }
                
            }
            else
            {
                try
                {
                    resultInfo.ResultData = DataProvider.Instance().ExecuteDataSet(name);
                }
                catch (SqlException ex)
                {

                    resultInfo.ErrorMessage = StoredProcParams(ex, name);
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                    resultInfo.ErrorMessage = new ErrorMessageInfo() {Message = ex.ToString()};
                }
                
            }
            
            return resultInfo;

        }

        /// <summary>
        /// Executes the feed SQL.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <param name="feedParams">The feed params.</param>
        /// <returns>ResultsInfo.</returns>
        public static ResultsInfo ExecuteFeedSQL(string SQL, object[] feedParams)
        {
            ResultsInfo resultsInfo = new ResultsInfo();

            
            if (feedParams != null)
            {
                string formattedSQL = string.Format(SQL, feedParams);
               resultsInfo.ResultData = Utils.ConvertDataReaderToDataSet(
                    DataProvider.Instance().ExecuteSQL(formattedSQL));
            }
            else
            {
                resultsInfo.ResultData = Utils.ConvertDataReaderToDataSet(
                     DataProvider.Instance().ExecuteSQL(SQL));
            }

            return resultsInfo;
        }


        /// <summary>
        /// Deletes the feed.
        /// </summary>
        /// <param name="feedId">The feed id.</param>
        public static void DeleteFeed(int feedId)
        {
            DataProvider.Instance().ExecuteNonQuery(Constants.DbPrefix + "Delete_Feed", feedId);
        }


        /// <summary>
        /// Storeds the proc params.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="name">The name.</param>
        /// <returns>ErrorMessageInfo.</returns>
        private static ErrorMessageInfo StoredProcParams(Exception error, string name)
        {
            if (error.Message.Contains("Procedure"))
            {
                SqlCommand myCommand = new SqlCommand();
                myCommand.Connection = new SqlConnection(Config.GetConnectionString());
                myCommand.CommandText = Config.GetDataBaseOwner() + Config.GetObjectQualifer() + name;
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Connection.Open();
                SqlCommandBuilder.DeriveParameters(myCommand);
                myCommand.Connection.Close();
                
                ErrorMessageInfo errorMsg = new ErrorMessageInfo();

                errorMsg.Message = "Required Paramaters.";
                
                foreach(SqlParameter param in myCommand.Parameters)
                {
                    if(param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                    {
                        errorMsg.Paramaters.Add(new ParamaterInfo() {Name=param.ParameterName, DataType = param.SqlDbType.ToString()});
                    }
                }

                return errorMsg;
            }
            return new ErrorMessageInfo() {Message=HttpContext.Current.Server.HtmlEncode(error.ToString())};
        }

        
    }
}