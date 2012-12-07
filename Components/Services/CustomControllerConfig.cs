// ***********************************************************************
// Assembly         : DataExpose
// Author           : Jonathan Sheely
// Website          : http://inspectorit.com
// License          : New BSD License (BSD)
// Created          : 12-04-2012
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 12-04-2012
// ***********************************************************************
// <copyright file="CustomControllerConfig.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Controllers;

namespace InspectorIT.DataExpose.Components.Services
{
    /// <summary>
    /// Class CustomControllerConfig
    /// </summary>
    public class CustomControllerConfig : Attribute, IControllerConfiguration
    {

        /// <summary>
        /// Callback invoked to set per-controller overrides for this controllerDescriptor.
        /// </summary>
        /// <param name="controllerSettings">The controller settings to initialize.</param>
        /// <param name="controllerDescriptor">The controller descriptor. Note that the <see cref="T:System.Web.Http.Controllers.HttpControllerDescriptor" /> can be associated with the derived controller type given that <see cref="T:System.Web.Http.Controllers.IControllerConfiguration" /> is inherited.</param>
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.Formatters.Add(new CustomXmlFormatter());
            controllerSettings.Formatters.Remove(controllerSettings.Formatters.XmlFormatter);
            controllerSettings.Services.Replace(typeof(IContentNegotiator), new DefaultContentNegotiator(excludeMatchOnTypeOnly: true));

            //controllerSettings.Formatters.XmlFormatter.UseXmlSerializer = true;
            //controllerSettings.Formatters.XmlFormatter.Indent = true;

        }
    }
}