using System.Collections.Generic;
using System.Linq;

namespace CategorizationService.DTO
{
	public class CategoryNode
	{
		public Category Category { get; set; }

		public bool IsRoot { get; set; }

		public List<CategoryNode> ChildCategories{ get; set; }

		public static CategoryNode Create(ICollection<Category> categories)
		{
			var rootCategory = categories.Where(c => !c.ParentId.HasValue).ToList();

			if (rootCategory.Count != 1)
			{
				//todo: graduate exception, log
				return null;
			}

			var categoriesByParentIds = new Dictionary<int, List<Category>>();

			foreach (var category in categories.Where(c => c.ParentId.HasValue))
			{
				// ReSharper disable once PossibleInvalidOperationException
				if (!categoriesByParentIds.ContainsKey(category.ParentId.Value))
				{
					categoriesByParentIds.Add(category.ParentId.Value, new());
				}

				categoriesByParentIds[category.ParentId.Value].Add(category);
			}

			return CreateReverse(categoriesByParentIds, rootCategory.First(), true);
		}

		private static CategoryNode CreateReverse(
			IDictionary<int, List<Category>> categoriesByParentIds,
			Category category,
			bool isRoot = false)
		{
			var res = new CategoryNode
			{
				Category = category,
				IsRoot = isRoot
			};

			if (categoriesByParentIds.TryGetValue(category.CategoryId, out var categories))
			{
				res.ChildCategories = categories.Select(c => CreateReverse(categoriesByParentIds, c)).ToList();
			}

			return res;
		}
	}
}
