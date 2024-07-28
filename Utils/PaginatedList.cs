public class PagedResult<T>
{
  public List<T> Items { get; set; }
  public int TotalItems { get; set; }
  public int CurrentPage { get; set; }
  public int PageSize { get; set; }
  public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
  public int NextPage => (CurrentPage >= TotalPages) ? TotalPages : CurrentPage + 1;
  public int PreviousPage => (CurrentPage <= 1) ? 1 : CurrentPage - 1;

  public PagedResult(List<T> items, int count, int currentPage, int pageSize)
  {
    Items = items;
    TotalItems = count;
    CurrentPage = currentPage;
    PageSize = pageSize;
  }
}
