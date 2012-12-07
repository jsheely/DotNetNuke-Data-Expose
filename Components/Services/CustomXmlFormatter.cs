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
// <copyright file="CustomXmlFormatter.cs" company="InspectorIT Inc">
//     Copyright (c) InspectorIT Inc. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;


using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace InspectorIT.DataExpose.Components.Services
{
    /// <summary>
    /// Class CustomXmlFormatter
    /// </summary>
    public class CustomXmlFormatter : XmlMediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomXmlFormatter" /> class.
        /// </summary>
        public CustomXmlFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml"));
            encoding = new UTF8Encoding(false, true);
        }

        /// <summary>
        /// Queries whether the <see cref="T:System.Net.Http.Formatting.XmlMediaTypeFormatter" /> can deserializean object of the specified type.
        /// </summary>
        /// <param name="type">The type to deserialize.</param>
        /// <returns>true if the <see cref="T:System.Net.Http.Formatting.XmlMediaTypeFormatter" /> can deserialize the type; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public override bool CanReadType(Type type)
        {
            if (type == (Type)null)
                throw new ArgumentNullException("type");

            return true;
        }

        /// <summary>
        /// Queries whether the  <see cref="T:System.Net.Http.Formatting.XmlMediaTypeFormatter" /> can serializean object of the specified type.
        /// </summary>
        /// <param name="type">The type to serialize.</param>
        /// <returns>true if the <see cref="T:System.Net.Http.Formatting.XmlMediaTypeFormatter" /> can serialize the type; otherwise, false.</returns>
        public override bool CanWriteType(Type type)
        {
            return true;
        }

        /// <summary>
        /// Called during deserialization to read an object of the specified type from the specified readStream.
        /// </summary>
        /// <param name="type">The type of object to read.</param>
        /// <param name="readStream">The <see cref="T:System.IO.Stream" /> from which to read.</param>
        /// <param name="content">The <see cref="T:System.Net.Http.HttpContent" /> for the content being read.</param>
        /// <param name="formatterLogger">The <see cref="T:System.Net.Http.Formatting.IFormatterLogger" /> to log events to.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> whose result will be the object instance that has been read.</returns>
        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {

            return Task.Factory.StartNew(() =>
            {
                using (var streamReader = new StreamReader(readStream, encoding))
                {
                    var serializer = new XmlSerializer(type);
                    return serializer.Deserialize(streamReader);
                }
            });
            //return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
        }

        /// <summary>
        /// Called during serialization to write an object of the specified type to the specified writeStream.
        /// </summary>
        /// <param name="type">The type of object to write.</param>
        /// <param name="value">The object to write.</param>
        /// <param name="writeStream">The <see cref="T:System.IO.Stream" /> to which to write.</param>
        /// <param name="content">The <see cref="T:System.Net.Http.HttpContent" /> for the content being written.</param>
        /// <param name="transportContext">The <see cref="T:System.Net.TransportContext" />.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that will write the value to the stream.</returns>
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            var serializer = new XmlSerializer(type);
            return Task.Factory.StartNew(() =>
            {
                using (var streamWriter = new StreamWriter(writeStream, encoding))
                {
                    serializer.Serialize(streamWriter, value);
                }
            });
            //return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }



        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        private Encoding encoding { get; set; }
    }
}