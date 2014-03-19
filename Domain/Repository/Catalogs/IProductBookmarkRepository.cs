using Domain.Entity.Catalogs;
using Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Catalogs
{
    public interface IProductBookmarkRepository: IRepository<ProductBookmark>
    {
        IQueryable<Product> ReadProductsByUser(int userId);
        bool IsIsset(int userId, int productId);
        ProductBookmark Read(int userId, int productId);
    }
}
