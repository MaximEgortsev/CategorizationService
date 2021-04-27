using System.Threading.Tasks;
using CategorizationService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CategorizationService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategorizationController
	{
		private readonly ICategorizationStoreManager _categorizationStoreManager;

		public CategorizationController(ICategorizationStoreManager categorizationStoreManager)
		{
			_categorizationStoreManager = categorizationStoreManager;
		}

		[HttpGet("tree")]
		public async Task<CategoryNode> GetCategorizationTree(int categoryId)
		{
			return await _categorizationStoreManager.GetCategoriesTree(categoryId);
		}

		[HttpPost("add")]
		public async Task AddCategory(int id, string name, int? parentId)
		{
			await _categorizationStoreManager.AddCategory(id, name, parentId);
		}

		[HttpPost("update")]
		public async Task UpdateCategory(int id, string name, int? parentId)
		{
			await _categorizationStoreManager.UpdateCategory(id, name, parentId);
		}
	}
}
