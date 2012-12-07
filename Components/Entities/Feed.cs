// ***********************************************************************
// Assembly         : DataExpose
// Author           : Jonathan Sheely
// Website          : http://inspectorit.com
// License          : New BSD License (BSD)
// Created          : 12-03-2012
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 11-28-2012
// ***********************************************************************
// <copyright file="Feed.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InspectorIT.DataExpose.Components.Entities
{
    /// <summary>
    /// Class Feed
    /// </summary>
    public class Feed
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the SQL.
        /// </summary>
        /// <value>The SQL.</value>
        public string SQL { get; set; }
        /// <summary>
        /// Gets or sets the name of the proc.
        /// </summary>
        /// <value>The name of the proc.</value>
        public string ProcName { get; set; }
        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public string Roles { get; set; }
        /// <summary>
        /// Gets or sets the created by user id.
        /// </summary>
        /// <value>The created by user id.</value>
        public int CreatedByUserId { get; set; }
        /// <summary>
        /// Gets or sets the modified by user id.
        /// </summary>
        /// <value>The modified by user id.</value>
        public int ModifiedByUserId { get; set; }
        /// <summary>
        /// Gets or sets the created on date.
        /// </summary>
        /// <value>The created on date.</value>
        public DateTime CreatedOnDate { get; set; }
        /// <summary>
        /// Gets or sets the modified on date.
        /// </summary>
        /// <value>The modified on date.</value>
        public DateTime ModifiedOnDate { get; set; }
    }
}