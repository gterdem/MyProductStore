using AutoMapper;
using MyProductStore.Products;
using Volo.Abp.AutoMapper;

namespace MyProductStore;

public class MyProductStoreApplicationAutoMapperProfile : Profile
{
    public MyProductStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(q => q.Id);
        CreateMap<UpdateProductDto, Product>()
            .IgnoreFullAuditedObjectProperties();
    }
}
