/*****************************************************************
 * Copyright (C) hotelvp Corporation. All rights reserved.
 * 
 * Author:   
 * Create Date:  08/29/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Data;

namespace HotelVp.Common.Entity
{
	public class DataMappingAttribute : Attribute
	{
		private string m_ColumnName;
		private DbType m_DbType;

		public DataMappingAttribute(string columnName, DbType dbType)
		{
			m_ColumnName = columnName;
			m_DbType = dbType;
		}

		public string ColumnName
		{
			get { return m_ColumnName; }
		}

		public DbType DbType
		{
			get { return m_DbType; }
		}
	}
}
