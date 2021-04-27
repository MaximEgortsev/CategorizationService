using System.Threading.Tasks;
using CategorizationService.DTO;

namespace CategorizationService
{
	public interface ICategorizationStoreManager
	{
		Task<CategoryNode> GetCategoriesTree(int id);

		Task AddCategory(int id, string name, int? parentId);

		Task UpdateCategory(int id, string name, int? parentId);
	}
}
