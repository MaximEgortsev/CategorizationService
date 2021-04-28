using System.Threading.Tasks;
using CategorizationService.Db;
using CategorizationService.DTO;

namespace CategorizationService.Services
{
	public class CategorizationService : ICategorizationService
	{
		private readonly ICategorizationStoreManager _categorizationStoreManager;

		public CategorizationService(ICategorizationStoreManager categorizationStoreManager)
		{
			_categorizationStoreManager = categorizationStoreManager;
		}

		public async Task<CategoryNode> GetCategoriesTree(int id)
		{
			//todo: validate

			var categories = await _categorizationStoreManager.GetCategoriesTree(id);

			if (categories.Count == 0)
			{
				//todo: log or send response  that categoryId does not exist
				return null;
			}

			var categoryNode = CategoryNode.Create(categories);

			return categoryNode;
		}

		public async Task AddCategory(int id, string name, int? parentId)
		{
			//todo: validate

			var category = new Category
			{
				CategoryId = id,
				CategoryName = name,
				ParentId = parentId
			};

			await _categorizationStoreManager.AddCategory(category);

			//todo: send response
		}
		

		public async Task UpdateCategory(int id, string name, int? parentId)
		{
			//todo: validate

			var category = new Category
			{
				CategoryId = id,
				CategoryName = name,
				ParentId = parentId
			};

			await _categorizationStoreManager.UpdateCategory(category);

			//todo: send response
		}
	}
}
