using System;
using CategorizationDb.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace CategorizationDb
{
	internal class Program
	{
		private static void Main()
		{
			var serviceProvider = CreateServices();

			using var scope = serviceProvider.CreateScope();
			UpdateDatabase(scope.ServiceProvider);
		}

		private static IServiceProvider CreateServices()
		{
			return new ServiceCollection()
				.AddFluentMigratorCore()
				.ConfigureRunner(rb => rb
					.AddPostgres()
					.WithGlobalConnectionString("User ID=#####;Password=#####;Host=localhost;Port=5432;Database=Categorization;")
					.ScanIn(typeof(AddCategorizationTable).Assembly).For.Migrations())
				.AddLogging(lb => lb.AddFluentMigratorConsole())
				.BuildServiceProvider(false);
		}

		private static void UpdateDatabase(IServiceProvider serviceProvider)
		{
			var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
			
			runner.MigrateUp();
		}
	}
}
