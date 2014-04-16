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
		currently xml serializer only support public class. whereas this class 
		should be internal only.
		to solve this issue, a dedicated helpclass can be added that uses DOM to deserialize 
		an instance of this class.
	 */
    [XmlRoot("databaseList", Namespace = "http://www.hotelvp.com/DatabaseList")]
    public class DatabaseList
	{
		private DatabaseInstance[] m_DatabaseInstances;

		[XmlElement("database")]
		public DatabaseInstance[] DatabaseInstances
		{
			get { return m_DatabaseInstances; }
			set { m_DatabaseInstances = value; }
		}
    }
}
