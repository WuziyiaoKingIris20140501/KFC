/*****************************************************************
 * Copyright (C) hotelvp Corporation. All rights reserved.
 * 
 * Author:   
 * Create Date:  09/05/2006 09:59:03
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Diagnostics;
using System.Text;

namespace HotelVp.Common.Entity
{
	internal static class EntityBuilderLogger
	{
		private const string LogCategory = "Framework.EntityBuilder";
		#region event ids
		private const int AddTypeInfo = 1;	// not logged.
		private const int GetPropertyBindingInfo = 2; // not logged.
		#endregion

		//[Conditional("TRACE")]
		public static Exception LogAddTypeInfo(Type type,Exception sourceException)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Add type info to Cache: ");
			sb.Append(type.ToString());
			//Logger.LogEvent(LogCategory, AddTypeInfo, sb.ToString());
            Exception result = new Exception(sb.ToString(), sourceException);
            return result;

		}

		//[Conditional("TRACE")]
		public static Exception LogGetPropertyBindingInfoException(Type type, string columnName, Exception e)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Faile to get binding Info. Type: ");
			sb.Append(type.ToString());
			sb.Append(". Column name: ");
			sb.Append(columnName + System.Environment.NewLine);
			//sb.Append("Exception: " +e.ToString());
            Exception result = new Exception(sb.ToString(), e);
            return result;
			//Logger.LogEvent(LogCategory, GetPropertyBindingInfo, sb.ToString());
		}
	}
}
