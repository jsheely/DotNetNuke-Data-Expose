// ***********************************************************************
// Assembly         : DataExpose
// Author           : Jonathan Sheely
// Website          : http://inspectorit.com
// License          : New BSD License (BSD)
// Created          : 12-03-2012
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 12-06-2012
// ***********************************************************************
// <copyright file="ServicesController.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using DotNetNuke.Security.Membership;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Authentication;
using DotNetNuke.Web.Api;
using InspectorIT.DataExpose.Components.Controllers;
using InspectorIT.DataExpose.Components.Entities;

namespace InspectorIT.DataExpose.Components.Services
{
    /// <summary>
    /// Class ServicesController
    /// </summary>
    [AllowAnonymous]
    [CustomControllerConfig]
    public class ServicesController : DnnApiController
    {
        /// <summary>
        /// The obj user
        /// </summary>
        private UserInfo objUser = null;
        /// <summary>
        /// The output type
        /// </summary>
        private string outputType = "text/xml";

        /// <summary>
        /// Executes the specified feed.
        /// </summary>
        /// <param name="feed">The feed.</param>
        /// <param name="feedParams">The feed params.</param>
        /// <returns>HttpResponseMessage.</returns>
        [HttpGet]
        public HttpResponseMessage Execute(string feed, string feedParams)
        {
            setContentType();
            return FeedResponse(feed, feedParams);
        }

        /// <summary>
        /// Executes the specified auth token.
        /// </summary>
        /// <param name="authToken">The auth token.</param>
        /// <param name="feed">The feed.</param>
        /// <param name="feedParams">The feed params.</param>
        /// <returns>HttpResponseMessage.</returns>
        [HttpGet]
        public HttpResponseMessage Execute(string authToken, string feed, string feedParams)
        {
            setContentType();
            FormsAuthenticationTicket authenticationTicket;
            try
            {
                authenticationTicket = FormsAuthentication.Decrypt(authToken);    
            }
            catch(HttpException ex)
            {
                ResultsInfo resultInfo = new ResultsInfo();
                resultInfo.ErrorMessage = new ErrorMessageInfo() { Message = ex.Message };
                return Request.CreateResponse(HttpStatusCode.OK, resultInfo); ;
            }
            
            string userName = authenticationTicket.UserData;
            objUser = UserController.GetUserByName(0, userName);

            return FeedResponse(objUser,feed, feedParams);
        }

        /// <summary>
        /// Executes the specified portal id.
        /// </summary>
        /// <param name="portalId">The portal id.</param>
        /// <param name="authToken">The auth token.</param>
        /// <param name="feed">The feed.</param>
        /// <param name="feedParams">The feed params.</param>
        /// <returns>HttpResponseMessage.</returns>
        [HttpGet]
        public HttpResponseMessage Execute(int portalId, string authToken, string feed, string feedParams)
        {
            setContentType();
            FormsAuthenticationTicket authenticationTicket;
            try
            {
                authenticationTicket = FormsAuthentication.Decrypt(authToken);
            }
            catch (HttpException ex)
            {
                ResultsInfo resultInfo = new ResultsInfo();
                resultInfo.ErrorMessage = new ErrorMessageInfo() { Message = ex.Message };
                return Request.CreateResponse(HttpStatusCode.OK, resultInfo); ;
            }

            string userName = authenticationTicket.UserData;
            objUser = UserController.GetUserByName(portalId, userName);

            return FeedResponse(objUser, feed, feedParams);
        }

        /// <summary>
        /// Feeds the response.
        /// </summary>
        /// <param name="feed">The feed.</param>
        /// <param name="feedParams">The feed params.</param>
        /// <returns>HttpResponseMessage.</returns>
        private HttpResponseMessage FeedResponse(string feed, string feedParams)
        {
            return FeedResponse(UserInfo, feed, feedParams);
        }

        /// <summary>
        /// Feeds the response.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="feed">The feed.</param>
        /// <param name="feedParams">The feed params.</param>
        /// <returns>HttpResponseMessage.</returns>
        private HttpResponseMessage FeedResponse(UserInfo user, string feed, string feedParams)
        {
            Feed feedObj = FeedController.GetFeedByName(feed);
            
            if(ValidateRoles(user, feedObj))
            {
                if (feedObj != null)
                {
                    if (feedObj.ProcName != "")
                    {
                        return Request.CreateResponse(
                            HttpStatusCode.OK, 
                            FeedController.ExecuteFeed(
                                feedObj.ProcName, feedParams != null && feedParams != "" ? feedParams.Split(',').ToArray() : null), 
                                new MediaTypeHeaderValue(outputType));
                    }
                    else if (feedObj.SQL != "")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, FeedController.ExecuteFeedSQL(feedObj.SQL, feedParams != null && feedParams != "" ? feedParams.Split(',').ToArray() : null), new MediaTypeHeaderValue(outputType));
                    }
                }
            }else
            {
                ResultsInfo resultInfo = new ResultsInfo();
                resultInfo.ErrorMessage = new ErrorMessageInfo() { Message = "User does not have permission to run this method" };
                return Request.CreateResponse(HttpStatusCode.OK, resultInfo, new MediaTypeHeaderValue(outputType)); ;
            }


            return Request.CreateResponse(HttpStatusCode.OK, "failure", new MediaTypeHeaderValue(outputType)); ;
        }

        /// <summary>
        /// Validates the roles.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="feedObj">The feed obj.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool ValidateRoles(UserInfo user, Feed feedObj)
        {
            if(feedObj.Roles!="" && !user.IsSuperUser && !user.IsInRole("Administrators"))
            {
                bool isValid = false;
                foreach (string s in  feedObj.Roles.Split(','))
                {
                    if(user.IsInRole(s.Trim()) || s.Trim()=="All Users")
                    {
                        isValid = true;
                        break;
                    }
                    
                }
                return isValid;
            }

            return true;
        }

        /// <summary>
        /// Class AuthenticateDTO
        /// </summary>
        public class AuthenticateDTO
        {
            /// <summary>
            /// Gets or sets the portal id.
            /// </summary>
            /// <value>The portal id.</value>
            public int portalId { get; set; }
            /// <summary>
            /// Gets or sets the username.
            /// </summary>
            /// <value>The username.</value>
            public string username { get; set; }
            /// <summary>
            /// Gets or sets the password.
            /// </summary>
            /// <value>The password.</value>
            public string password { get; set; }
            

        }

        /// <summary>
        /// Authenticates the specified auth.
        /// </summary>
        /// <param name="auth">The auth.</param>
        /// <returns>HttpResponseMessage.</returns>
        [HttpPost]
        public HttpResponseMessage Authenticate(AuthenticateDTO auth)
        {
            setContentType();
            var loginStatus = UserLoginStatus.LOGIN_FAILURE;
            objUser = UserController.ValidateUser(auth.portalId, auth.username, auth.password, "DNN", string.Empty, PortalSettings.PortalName, AuthenticationLoginBase.GetIPAddress(), ref loginStatus);

            if(loginStatus == UserLoginStatus.LOGIN_SUCCESS)
            {
                UserController.UserLogin(auth.portalId, objUser, PortalSettings.PortalName, AuthenticationLoginBase.GetIPAddress(), true);
                FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket(1, objUser.Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, objUser.Username, FormsAuthentication.FormsCookiePath);
                var encryptedAuthTicket = FormsAuthentication.Encrypt(authenticationTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedAuthTicket)
                {
                    Expires = authenticationTicket.Expiration,
                    Domain = PortalSecurity.GetCookieDomain(objUser.PortalID),
                    Path = FormsAuthentication.FormsCookiePath,
                    Secure = FormsAuthentication.RequireSSL
                };

                return Request.CreateResponse(HttpStatusCode.OK, authCookie.Value, new MediaTypeHeaderValue(outputType));
            }


            return Request.CreateResponse(HttpStatusCode.OK, "success", new MediaTypeHeaderValue(outputType));
        }


        /// <summary>
        /// Gets the feed list.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DnnAuthorize] 
        public HttpResponseMessage GetFeedList()
        {
            setContentType();
            return Request.CreateResponse(HttpStatusCode.OK, FeedController.GetFeedList(), new MediaTypeHeaderValue(outputType));
        }

        /// <summary>
        /// Class FeedDetailsDTO
        /// </summary>
        public class FeedDetailsDTO
        {
            /// <summary>
            /// Gets or sets the feed id.
            /// </summary>
            /// <value>The feed id.</value>
            public int feedId { get; set; }
        }

        /// <summary>
        /// Gets the feed details.
        /// </summary>
        /// <param name="feedDetailsDTO">The feed details DTO.</param>
        /// <returns>HttpResponseMessage.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [DnnAuthorize]
        public HttpResponseMessage GetFeedDetails(FeedDetailsDTO feedDetailsDTO)
        {
            setContentType();
            return Request.CreateResponse(HttpStatusCode.OK, FeedController.GetFeedDetails(feedDetailsDTO.feedId), new MediaTypeHeaderValue(outputType));
        }

        /// <summary>
        /// Saves the feed.
        /// </summary>
        /// <param name="feed">The feed.</param>
        /// <returns>HttpResponseMessage.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DnnAuthorize] 
        public HttpResponseMessage SaveFeed(Feed feed)
        {
            setContentType();
            feed.ModifiedByUserId = UserInfo.UserID;
            int feedId = FeedController.Save(feed);
            return Request.CreateResponse(HttpStatusCode.OK, FeedController.GetFeedDetails(feedId), new MediaTypeHeaderValue(outputType));
        }
        /// <summary>
        /// Deletes the feed.
        /// </summary>
        /// <param name="feedDetailsDTO">The feed details DTO.</param>
        /// <returns>HttpResponseMessage.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DnnAuthorize]
        public HttpResponseMessage DeleteFeed(FeedDetailsDTO feedDetailsDTO)
        {
            setContentType();
            FeedController.DeleteFeed(feedDetailsDTO.feedId);
            return Request.CreateResponse(HttpStatusCode.OK, "success", new MediaTypeHeaderValue(outputType));
        }

        /// <summary>
        /// Sets the type of the content.
        /// </summary>
        private void setContentType()
        {
            if(Request.Headers.Accept.ToString().Contains("json"))
            {
                outputType = "text/json";
            }
        }

    }
}