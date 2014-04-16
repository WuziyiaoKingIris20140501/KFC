/*****************************************************************
 * Copyright (C) hotelvp Corporation. All rights reserved.
 * 
 * Author:   
 * Create Date:  08/26/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace HotelVp.Common.DataConfiguration
{
	/*
	 todo: 
		currently xml serializer only support public class. whereas DatabaseInstance class 
		should be internal only.
		to solve this issue, a dedicated helpclass can be added that uses DOM to deserialize 
		an instance of this class.
	 */
	[XmlRoot("database")]
	public class DatabaseInstance
	{
		private string m_Name;
		private string m_ConnectionString;

		[XmlAttribute("name")]
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}
		[XmlElement("connectionString")]
		public string ConnectionString
		{
			get { return m_ConnectionString; }
			set { m_ConnectionString = value; }
		}
	}
}
