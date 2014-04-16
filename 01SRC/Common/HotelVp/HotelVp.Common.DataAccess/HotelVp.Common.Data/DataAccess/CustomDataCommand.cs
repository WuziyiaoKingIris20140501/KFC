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
using System.Data;

namespace HotelVp.Common.DataAccess
{
	public class CustomDataCommand : DataCommand
	{
        internal CustomDataCommand(string databaseAliasName)
            : base(databaseAliasName, DbCommandFactory.CreateDbCommand())
		{
		}

        internal CustomDataCommand(string databaseAliasName, CommandType commandType)
            : this(databaseAliasName) 
		{
			CommandType = commandType;
		}

        internal CustomDataCommand(string databaseAliasName, CommandType commandType, string commandText)
            : this(databaseAliasName, commandType)
		{
			CommandText = commandText;
		}

		public CommandType CommandType
		{
			get { return m_DbCommand.CommandType; }
			set { m_DbCommand.CommandType = value; }
		}

		#region add parameter
		public void AddInputParameter(string name, DbType dbType, Object value)
		{
            object val = value;
            if (value == null)
            {
                val = DBNull.Value;
            }
			ActualDatabase.AddInParameter(m_DbCommand, name, dbType, val);
		}

		public void AddInputParameter(string name, DbType dbType)
		{
			ActualDatabase.AddInParameter(m_DbCommand, name, dbType);
		}

        public void AddOutParameter(string name, DbType dbType, int size)
        {
            ActualDatabase.AddOutParameter(m_DbCommand, name, dbType, size);
        }

		#endregion


		public string CommandText
		{
			get { return m_DbCommand.CommandText; }
			set { m_DbCommand.CommandText = value; }
		}

        public int CommandTimeout
        {
            get { return m_DbCommand.CommandTimeout; }
            set { m_DbCommand.CommandTimeout = value; }
        }

        public string DatabaseAliasName
        {
            get { return m_DatabaseName; }
            set { m_DatabaseName = value.ToString(); }
        }
	}
}
