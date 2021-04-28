using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategorizationService.DTO;
using Dapper;
using Npgsql;

namespace CategorizationService.Db
{
	public class CategorizationDbManager : ICategorizationStoreManager
	{
		private readonly string _connectionString;

		public CategorizationDbManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<List<Category>> GetCategoriesTree(int id)
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
			return (await connection.QueryAsync<Category>(query, new {id})).ToList();
		}


		public async Task AddCategory(Category category)
		{
			const string query = @"
INSERT INTO categories 
VALUES (@CategoryId, @CategoryName, @ParentId);";

			//todo: add exc handling
			await using var connection = new NpgsqlConnection(_connectionString);
			await connection.ExecuteAsync(query, category);
		}

		public async Task UpdateCategory(Category category)
		{
			//			const string query = @"
			//UPDATE categories 
			//SET categoryName = @name, parentId = @parentId
			//WHERE categoryId = @id;";

			var query =
				"UPDATE categories" + Environment.NewLine +
				"SET \"categoryName\" = @CategoryName, \"parentId\" = @ParentId" + Environment.NewLine +
				"WHERE \"categoryId\" = @CategoryId;";

			//todo: add exc handling
			await using var connection = new NpgsqlConnection(_connectionString);
			await connection.ExecuteAsync(query, category);
		}
	}
}
