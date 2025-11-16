namespace Catalog.Specifications;

/**
 * will be used to pass the parameters for filtering, sorting, searching, and pagination,
 * it is used as class to wrap all the parameters,
 * to avoid the long list of parameters in the method signature.
 * and also easy to extend in the future.
 */

public class CatalogSpecParams
{
    private const int MaxPageSize = 70;
    private int _pageSize = 10;
    public int PageIndex { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? BrandId { get; set; }
    public string? TypeId { get; set; }
    public string? Sort { get; set; }
    public string? Search { get; set; }
}