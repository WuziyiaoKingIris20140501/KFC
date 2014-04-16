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
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Reflection;

namespace HotelVp.Common.DataAccess
{
	partial class dataOperations
	{
		/// <summary>
		/// returns a list of command names this object contains.
		/// </summary>
		/// <returns></returns>
		public IList<string> GetCommandNames()
		{
			if (dataCommandField == null || dataCommandField.Length == 0)
			{
				return new string[0];
			}

			List<string> result = new List<string>(dataCommandField.Length);

			for (int i = 0; i < dataCommandField.Length; i++)
			{
				result.Add(dataCommandField[i].name);
			}
			return result;
		}

	}
	partial class dataOperationsDataCommand
	{
		/// <summary>
		/// returns a new instance of DataCommand this object represents
		/// </summary>
		/// <returns></returns>
		public DataCommand GetDataCommand()
		{
			DbCommand dbCommand = GetDbCommand();
			return new DataCommand(database.ToString(), dbCommand);
		}
		
		/// <summary>
		/// returns a new instance of DbCommand this object represents
		/// </summary>
		/// <returns></returns>
		private DbCommand GetDbCommand()
		{
			DbCommand cmd = DbCommandFactory.CreateDbCommand();
			cmd.CommandText = commandText.Trim();
			cmd.CommandTimeout = timeOut;
			cmd.CommandType = (CommandType) Enum.Parse(typeof(CommandType), this.commandType.ToString());
			// todo: populate cmd
			if (this.parameters != null && parameters.param != null && parameters.param.Length > 0)
			{
				foreach (dataOperationsDataCommandParametersParam param in this.parameters.param)
				{
					cmd.Parameters.Add(param.GetDbParameter());
				}
			}
			return cmd;
		}
	}
	partial class dataOperationsDataCommandParametersParam
	{
		public DbParameter GetDbParameter()
		{
            // use parameterless constructor so that SqlDbType is avoided.
            //string DbType = System.Configuration.ConfigurationManager.AppSettings["DataBaseType"].ToString();

            //if (DbType.Equals("2"))
            //{
            //    OracleParameter oraclpPram = new OracleParameter();
            //    oraclpPram.ParameterName = name;
            //    oraclpPram.Direction = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), this.direction.ToString());
            //    // the default is -1, specified in the schema
            //    if (this.size != -1)
            //    {
            //        oraclpPram.Size = size;
            //    }
            //    //修改支持sql_Variant

            //    DbType OracledbType = (DbType)Enum.Parse(typeof(DbType), this.dbType.ToString());
            //    oraclpPram.DbType = OracledbType;

            //    return oraclpPram;
            //}
           
            SqlParameter param = new SqlParameter();
            param.ParameterName = name;
            param.Direction = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), this.direction.ToString());
            // the default is -1, specified in the schema
            if (this.size != -1)
            {
                param.Size = size;
            }
            //修改支持sql_Variant

            DbType dbType = (DbType)Enum.Parse(typeof(DbType), this.dbType.ToString());
            param.DbType = dbType;
           
			
			return param;
		}

	}
}
