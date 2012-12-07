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
// <copyright file="Admin.ascx.cs" company="InspectorIT Inc">
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
using DotNetNuke.Framework;
using InspectorIT.DataExpose.Components.Common;

namespace InspectorIT.DataExpose.views
{
    /// <summary>
    /// Class Admin
    /// </summary>
    public partial class Admin : CustomModuleBase
    {
        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            jQuery.RegisterJQuery(this.Page);
            jQuery.RegisterJQueryUI(this.Page);
            ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
        }
    }
}