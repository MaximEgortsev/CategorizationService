using System.Collections.Generic;
using System.Threading.Tasks;
using CategorizationService.DTO;

namespace CategorizationService.Db
{
	public interface ICategorizationStoreManager
	{
		Task<List<Category>> GetCategoriesTree(int id);

		Task AddCategory(Category category);

		Task UpdateCategory(Category category);
	}
}
