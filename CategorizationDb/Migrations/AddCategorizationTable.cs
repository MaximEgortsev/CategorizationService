using FluentMigrator;

namespace CategorizationDb.Migrations
{
	[Migration(1)]
	public class AddCategorizationTable : Migration
	{
		public override void Up()
		{
			Create.Table("categories")
				.WithColumn("categoryId").AsInt32().PrimaryKey().Identity()
				.WithColumn("categoryName").AsString().NotNullable()
				.WithColumn("parentId").AsInt32().ForeignKey("categories", "categoryId");
		}

		public override void Down()
		{
			
		}
	}
}
