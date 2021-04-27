namespace CategorizationService.DTO
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int? ParentId { get; set; }
	}
}
