namespace Talpa_Api.Models
{
	public class Page(int pageNumber, int maxPages, int pageSize = 10)
	{
		public int PageNumber { get; } = pageNumber;

		public int PageSize { get; } = pageSize;

		public int MaxPages { get; } = maxPages;
	}
}
