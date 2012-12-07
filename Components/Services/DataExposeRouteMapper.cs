// ***********************************************************************
// Assembly         : DataExpose
// Author           : Jonathan Sheely
// Website          : http://inspectorit.com
// License          : New BSD License (BSD)
// Created          : 12-03-2012
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 11-20-2012
// ***********************************************************************
// <copyright file="DataExposeRouteMapper.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Web.Api;

namespace InspectorIT.DataExpose.Components.Services
{
    /// <summary>
    /// Class DataExposeRouteMapper
    /// </summary>
    public class DataExposeRouteMapper : IServiceRouteMapper
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="mapRouteManager">The map route manager.</param>
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("InspectorIT/DataExpose", "default", "{controller}/{action}", new[] { "InspectorIT.DataExpose.Components.Services" });
        }
        
    }
}
