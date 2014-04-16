/*****************************************************************
 * Copyright (C) hotelvp Corporation. All rights reserved.
 * 
 * Author:   
 * Create Date:  10/14/2006 17:21:01
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace HotelVp.Common.DataAccess
{
	internal static class DbCommandFactory
	{
		public static DbCommand CreateDbCommand()
		{
			return new SqlCommand();
		}
	}
}
