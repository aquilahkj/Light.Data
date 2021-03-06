﻿using System;
using System.Configuration;

namespace Light.Data
{
	class DataContextSetting
	{
		internal static DataContextSetting CreateSetting (ConnectionStringSettings setting, bool throwOnError)
		{
			if (setting == null) {
				throw new ArgumentException (RE.ConnectionSettingIsNotExists);
			}
			Type type;
			string connection = setting.ConnectionString;
//			string args = null;
//			int index = connection.IndexOf ("--extendparam:");
//			if (index > 1) {
//				args = connection.Substring (index + 14);
//				connection = connection.Substring (0, index).Trim ();
//			}
			if (!string.IsNullOrEmpty (setting.ProviderName)) {
				type = Type.GetType (setting.ProviderName, throwOnError);
			}
			else {
				type = Type.GetType ("Light.Data.Mssql,Light.Data", throwOnError);
			}
			if (type == null) {
				return null;
			}

			if (!throwOnError) {
				Type dataBaseType = typeof(Database);
				if (!TypeHelper.IsParentType (type, dataBaseType)) {
					return null;
				}
			}

			Database dataBase = Activator.CreateInstance (type) as Database;
			if (dataBase == null) {
				if (!throwOnError) {
					return null;
				}
				else {
					throw new LightDataException (string.Format (RE.TypeIsNotDatabaseType, type.FullName));
				}
			}
			ExtendParamCollection extendParams = ConnectionExtendManager.GetExtendParams (setting.Name);
			if (extendParams == null) {
				extendParams = new ExtendParamCollection ();
			}
			dataBase.SetExtendParams (extendParams);
//			dataBase.SetExtentArguments (args);
			DataContextSetting context = new DataContextSetting (connection, setting.Name, dataBase);
			return context;
		}

		readonly Database dataBase;

		public Database DataBase {
			get {
				return dataBase;
			}
		}

		readonly string name;

		public string Name {
			get {
				return name;
			}
		}

		readonly string connection;

		public string Connection {
			get {
				return connection;
			}
		}

		private DataContextSetting (string connection, string name, Database dataBase)
		{
			this.connection = connection;
			this.name = name;
			this.dataBase = dataBase;
		}
	}
}

