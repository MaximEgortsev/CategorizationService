using FluentMigrator;

namespace CategorizationDb.Migrations
{
	[Migration(0)]
	public class AddCategorizationTable : Migration
	{
		public override void Up()
		{
			Create.Table("categories")
				.WithColumn("categoryId").AsInt32().PrimaryKey()
				.WithColumn("categoryName").AsString().NotNullable() //todo: .Unique() ?
				.WithColumn("parentId").AsInt32().Nullable().ForeignKey("categories", "categoryId");
		}

		public override void Down()
		{
			Delete.Table("categories");
		}
	}
}
