using System;
using Core.Entities;

namespace Core.Specification;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSepcParams sepcParams) : base(x => 
        (string.IsNullOrEmpty(sepcParams.Search) || x.Name.Contains(sepcParams.Search)) &&
        (sepcParams.Brands.Count == 0 || sepcParams.Brands.Contains(x.Brand)) &&
        (sepcParams.Types.Count == 0 || sepcParams.Types.Contains(x.Type))
    )
    {
        ApplyPaging(sepcParams.PageSize * (sepcParams.PageIndex -1), sepcParams.PageSize);
        switch (sepcParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}
