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
// <copyright file="ErrorMessageInfo.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace InspectorIT.DataExpose.Components.Entities
{
    /// <summary>
    /// Class ErrorMessageInfo
    /// </summary>
    public class ErrorMessageInfo
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the paramaters.
        /// </summary>
        /// <value>The paramaters.</value>
        public List<ParamaterInfo> Paramaters
        {
            get { return _paramaters; }
            set { _paramaters = value; }
        }
        /// <summary>
        /// The _paramaters
        /// </summary>
        private List<ParamaterInfo> _paramaters = new List<ParamaterInfo>();
    }
}