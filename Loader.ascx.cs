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
// <copyright file="Loader.ascx.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using InspectorIT.DataExpose.Components.Common;

namespace InspectorIT.DataExpose
{
    /// <summary>
    /// Class Loader
    /// </summary>
    public partial class Loader : CustomModuleBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var adminControl = LoadControl("~/DesktopModules/InspectorIT/DataExpose/views/Admin.ascx") as views.Admin;
            adminControl.ModuleConfiguration = this.ModuleConfiguration;
            phOutput.Controls.Add(adminControl);
        }
    }
}