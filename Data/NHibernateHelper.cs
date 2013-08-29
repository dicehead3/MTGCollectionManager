﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;


namespace Data.Utils
{
	public class NHibernateHelper
	{
		public static ISessionFactory SessionFactory;
		private static readonly Configuration Configuration;

        /// <summary>
		///     Constructor
		/// </summary>
		/// <remarks>
		///     Static contructors are called automatically before the first instance is created or any static members are referenced.
		///     A static constructor is executed only once.
		/// </remarks>
		static NHibernateHelper()
		{
            
            // Build NHibernate session factory, expensive proces, should only fire when application first starts
			// Configuration is set in .config file
			Configuration = new Configuration();
			//Configuration.SetProperty(NHibernate.Cfg.Environment.ConnectionString, applicationSettings.ConnectionString);
			Configuration.Configure();
			
			SessionFactory = Fluently.Configure(Configuration)
				.Mappings(m =>
				  m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                //.Mappings(m=>m.FluentMappings.ExportTo("c:\\temp"))
				.BuildSessionFactory();
		}

		/// <summary>
		///     Creates database schema based on mappings
		/// </summary>
		public static void CreateDatabaseSchema()
		{
			var databaseTables = Configuration.ClassMappings.Select(x => x.Table).ToList();

			DropAllForeignKeysForTables(databaseTables);
			//DropTables(databaseTables);
			var schemaExport = new SchemaExport(Configuration);
			schemaExport.Drop(false, true);
			schemaExport.Create(false, true);

			// Excute custom sql which is not generated by NHibernate
			ExecuteCustomSql();
		}

		/// <summary>
		///     Executes custom sql statements to complete the creation of the database schema
		/// </summary>
		/// <remarks>
		///     NHibernate's schemaexport only create tables and fields which are mapped to entities.
		///     Other fields (e.g. password field) must be created using custom sql.
		///     All files included in 'CustomSql' directory are processed in alphabetical order.
		///     Make sure files are marked as embedded resources!
		/// </remarks>
		private static void ExecuteCustomSql()
		{
			// Get reference to this assembly
			var assembly = Assembly.GetExecutingAssembly();

			// List of custom sql files
			var sqlFiles = assembly.GetManifestResourceNames().Where(x => x.Contains("CustomSql"));

			// Create database connection
			using (var connection = SessionFactory.OpenSession().Connection)
			{
				// Process files
				foreach (var sqlFile in sqlFiles.OrderBy(x => x))
				{
					// Get resource
					var sqlStream = assembly.GetManifestResourceStream(sqlFile);

					// Create sql command
					var command = connection.CreateCommand();

					var splitter = new[] {"GO"};
					if (sqlStream != null)
					{
						var commandTexts = new StreamReader(sqlStream)
                            .ReadToEnd()
                            .Split(splitter,StringSplitOptions.RemoveEmptyEntries);

						foreach (var commandText in commandTexts)
						{
							command.CommandText = commandText;

							// Execute sql
							command.ExecuteNonQuery();
						}
					}					
				}
			}
		}

		private static void DropAllForeignKeysForTables(IEnumerable<Table> tables)
		{
			var tableNamesFromMappings = tables.Select(x => x.Name);

			var dropAllForeignKeysSql =
				@"
							DECLARE @cmd nvarchar(1000)
							DECLARE @fk_table_name nvarchar(1000)
							DECLARE @fk_name nvarchar(1000)

							DECLARE cursor_fkeys CURSOR FOR
							   SELECT  OBJECT_NAME(fk.parent_object_id) AS fk_table_name,
										fk.name as fk_name
							   FROM    sys.foreign_keys fk  JOIN
									   sys.tables tbl ON tbl.OBJECT_ID = fk.referenced_object_id
							   WHERE OBJECT_NAME(fk.parent_object_id) in ('" + String.Join("','", tableNamesFromMappings) + @"')

							OPEN cursor_fkeys
							FETCH NEXT FROM cursor_fkeys
								INTO @fk_table_name, @fk_name

							WHILE @@FETCH_STATUS=0
							BEGIN
								-- build alter table statement
								SET @cmd = 'ALTER TABLE [' + @fk_table_name + '] DROP CONSTRAINT [' + @fk_name + ']'
								-- execute it
								exec dbo.sp_executesql @cmd
	
								FETCH NEXT FROM cursor_fkeys
								INTO @fk_table_name, @fk_name
							END
							CLOSE cursor_fkeys
							DEALLOCATE cursor_fkeys
						;";

			using (var connection = SessionFactory.OpenSession().Connection)
			{
				var command = connection.CreateCommand();
				command.CommandText = dropAllForeignKeysSql;
				command.ExecuteNonQuery();
			}

		}
    }
}