namespace Invoice.Framework.GenericFilters
{
    public class SearchQueryRequest : GridifyQuery
    {       
        public SearchQueryRequest(int page = 1, int pageSize = 8, string orderBy = "", string filter = "")
        {
            PageSize = pageSize;
            OrderBy = orderBy;
            Filter = filter;
            Page = page;
        }
    }
}
