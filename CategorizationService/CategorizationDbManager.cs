using System;
using System.Linq;
using System.Threading.Tasks;
using CategorizationService.DTO;
using Dapper;
using Npgsql;

namespace CategorizationService
{
	public class CategorizationDbManager : ICategorizationStoreManager
	{
		private readonly string _connectionString;

		public CategorizationDbManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<CategoryNode> GetCategoriesTree(int id)
		{
//			const string query = @"
//WITH RECURSIVE cat(categoryId, categoryName, parentId) AS (
//    SELECT *
//    FROM categories c1 WHERE categoryId = @id
//        UNION
//    SELECT c2.categoryId, c2.categoryName, c2.parentId
//    FROM categories c2, cat  
//	WHERE c2.parentId = cat.categoryId OR c2.categoryId = cat.parentId
//)
//SELECT * FROM cat;";

			var query =
				"WITH RECURSIVE cat(categoryId, categoryName, parentId) AS (" + Environment.NewLine +
				"SELECT *" + Environment.NewLine +
				"FROM categories c1 WHERE \"categoryId\" = @id" + Environment.NewLine +
				"UNION" + Environment.NewLine +
				"SELECT c2.\"categoryId\", c2.\"categoryName\", c2.\"parentId\"" + Environment.NewLine +
				"FROM categories c2, cat" + Environment.NewLine +
				"WHERE c2.\"parentId\" = cat.categoryId OR c2.\"categoryId\" = cat.parentId)" + Environment.NewLine +
				"SELECT * FROM cat";

			await using var connection = new NpgsqlConnection(_connectionString);

			var categories = (await connection.QueryAsync<Category>(query, new {id})).ToList();

			if (categories.Count == 0)
			{
				//todo: log or send response  that categoryId does not exist
				return null;
			}

			var t =  CategoryNode.Create(categories);
			return t;
		}


		public async Task AddCategory(int id, string name, int? parentId)
		{
			const string query = @"
INSERT INTO categories 
VALUES (@id, @name, @parentId);";

			await using var connection = new NpgsqlConnection(_connectionString);

			//data consistency is checked at the database level (unique categoryId, correct parentId)
			//todo: Should category name be unique? 
			//todo: add exc handling \ additional checks to log or send response 

			await connection.ExecuteAsync(query, new { id, name, parentId});
		}

		public async Task UpdateCategory(int id, string name, int? parentId)
		{
			//			const string query = @"
			//UPDATE categories 
			//SET categoryName = @name, parentId = @parentId
			//WHERE categoryId = @id;";

			var query =
				"UPDATE categories" + Environment.NewLine +
				"SET \"categoryName\" = @name, \"parentId\" = @parentId" + Environment.NewLine +
				"WHERE \"categoryId\" = @id;";

			await using var connection = new NpgsqlConnection(_connectionString);

			//data consistency is checked at the database level (unique categoryId, correct parentId)
			//todo: add exc handling \ additional checks to log or send response
			//todo: maybe we want update just one field or update not only by categoryId

			await connection.ExecuteAsync(query, new { id, name, parentId });
		}
	}
}
