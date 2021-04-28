using System.Threading.Tasks;
using CategorizationService.DTO;
using CategorizationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategorizationService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategorizationController
	{
		private readonly ICategorizationService _categorizationService;

		public CategorizationController(ICategorizationService categorizationService)
		{
			_categorizationService = categorizationService;
		}

		[HttpGet("tree")]
		public async Task<CategoryNode> GetCategorizationTree(int categoryId)
		{
			return await _categorizationService.GetCategoriesTree(categoryId);
		}

		[HttpPost("add")]
		public async Task AddCategory(int id, string name, int? parentId)
		{
			await _categorizationService.AddCategory(id, name, parentId);
		}

		[HttpPost("update")]
		public async Task UpdateCategory(int id, string name, int? parentId)
		{
			await _categorizationService.UpdateCategory(id, name, parentId);
		}
	}
}
