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
// <copyright file="ResultsInfo.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace InspectorIT.DataExpose.Components.Entities
{
    //[DataContract(Name = "Hmm",Namespace = "http://wwww.inspectorit.com")]
    //[XmlRoot("ZomgWork")]
    /// <summary>
    /// Class ResultsInfo
    /// </summary>
    public class ResultsInfo
    {
        //[XmlElement("Zomg")]
        /// <summary>
        /// Gets or sets the result data.
        /// </summary>
        /// <value>The result data.</value>
        public DataSet ResultData { get; set; }
        //[XmlIgnore]
        //[DataMember(Name = "Wtf")]
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public ErrorMessageInfo ErrorMessage { get; set; }
    }
}