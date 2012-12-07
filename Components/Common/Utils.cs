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
// <copyright file="Utils.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace InspectorIT.DataExpose.Components.Common
{
    /// <summary>
    /// Class Utils
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Converts the data reader to data set.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>DataSet.</returns>
        public static DataSet ConvertDataReaderToDataSet(IDataReader reader)
        {
            DataSet ds = new DataSet();
            DataTable dataTable = new DataTable();

            DataTable schemaTable = reader.GetSchemaTable();
            DataRow row;

            string columnName;
            DataColumn column;
            int count = schemaTable.Rows.Count;

            for (int i = 0; i < count; i++)
            {
                row = schemaTable.Rows[i];
                columnName = (string)row["ColumnName"];

                column = new DataColumn(columnName, (Type)row["DataType"]);
                dataTable.Columns.Add(column);
            }

            ds.Tables.Add(dataTable);

            object[] values = new object[count];

            try
            {
                dataTable.BeginLoadData();
                while (reader.Read())
                {
                    reader.GetValues(values);
                    dataTable.LoadDataRow(values, true);
                }
            }
            finally
            {
                dataTable.EndLoadData();
                reader.Close();
            }

            return ds;
        }

        /// <summary>
        /// Converts the data set to XML.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns>XmlDocument.</returns>
        public static XmlDocument ConvertDataSetToXml(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            string rowValue = "";
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            xmlSettings.Indent = true;

            using(XmlWriter xmlWrite = XmlWriter.Create(sb,xmlSettings))
            {
                xmlWrite.WriteStartDocument();
                xmlWrite.WriteStartElement("NewDataSet");
                //xmlWrite.WriteAttributeString("xmlns", "xsi", "", "http://www.w3.org/2001/XMLSchema-instance");
                //xmlWrite.WriteAttributeString("xmlns", "xsd", "", "http://www.w3.org/2001/XMLSchema");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    xmlWrite.WriteStartElement("Table");
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++ )
                    {
                        rowValue = row[i].ToString();
                        rowValue = HttpContext.Current.Server.HtmlDecode(rowValue);

                        if(rowValue.Contains("<"))
                        {
                            xmlWrite.WriteStartElement(ds.Tables[0].Columns[i].ColumnName);
                            xmlWrite.WriteCData(rowValue);
                            xmlWrite.WriteEndElement();
                        }
                        else
                        {
                            xmlWrite.WriteElementString(ds.Tables[0].Columns[i].ColumnName, rowValue);
                        }
                        
                    }
                    xmlWrite.WriteEndElement();
                }
                xmlWrite.WriteEndDocument();
                xmlWrite.Close();
            }

            string strXML = sb.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXML);

            return xmlDoc;


        }
    }
}