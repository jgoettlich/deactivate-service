using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DeactivationService.Procs
{
	public abstract class AbstractStoredProcCall
	{
		protected string ProcedureName;
		protected string ConnectionString;
		protected Dictionary<string, object> SqlParams;

		public AbstractStoredProcCall(string ProcedureName, string connectionString)
		{
			ConnectionString = connectionString;
			this.ProcedureName = ProcedureName;
			SqlParams = new Dictionary<string, object>();
		}

		protected DataTable Execute()
		{
			DataTable dt = new DataTable();
			using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
			{
				string sql = ProcedureName;
				using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
				{
					sqlCmd.CommandType = CommandType.StoredProcedure;
					foreach (KeyValuePair<string, object> p in SqlParams) {
						string pName = (p.Key.StartsWith("@"))? p.Key : "@" + p.Key;
						sqlCmd.Parameters.AddWithValue(pName, p.Value);
					}
					
					sqlConn.Open();
					using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
					{
						sqlAdapter.Fill(dt);
					}
				}
			}

			return dt;
		}

		protected List<Object> MapData(DataTable dt, Type objectType)
		{
			if (dt == null) { return new List<Object>(); }

			Type listType = typeof(List<>);
			Type constructedListType = listType.MakeGenericType(objectType);

			IList listInstance = (IList)typeof(List<>)
				  .MakeGenericType(objectType)
				  .GetConstructor(Type.EmptyTypes)
				  .Invoke(null);

			List<Object> values = listInstance.Cast<Object>().ToList();
			// Loop through each row of the data table
			foreach (DataRow row in dt.Rows)
			{
				// Create an instance of the object to map to
				// and get all the properties of that object
				Object o = Activator.CreateInstance(objectType);
				PropertyInfo[] props = o.GetType().GetProperties();
				// Loop through each of the columns of the data table
				foreach (DataColumn col in dt.Columns)
				{
					// Go through the properties to match them to the column names
					foreach (PropertyInfo prop in props)
					{
						// If the prop and the column name are the same set
						// the property to the value in the datatable
						if (prop.Name.ToLower() == col.ColumnName.ToLower())
						{
							prop.SetValue(o, (row[col] == DBNull.Value)? null : row[col]);
							break;
						}
					}
				}

				// Add the populated object to the return list
				values.Add(o);
			}

			return values;
		}
	}
}
