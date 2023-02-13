using Hidden.Models;
using Hidden.Models.Domain.BlogsAdminDomain;
using Hidden.Models.Requests.BlogsAdmin;

namespace Hidden.Services
{
    public interface IBlogAdminService
    {
        public int Add(BlogAdminAddRequest model, int authorId);
        
        public void Delete(int id);
        
        public void Update(BlogAdminUpdateRequest model, int authorId);
        
        public BlogAdmin GetBlogAdminById(int id);
        
        public Paged<BlogAdmin> SelectAll(int pageIndex, int pageSize);
        
        public Paged<BlogAdmin> SelectByCreatedBy(int authorId, int pageIndex, int pageSize);
        
        public Paged<BlogAdmin> SelectByBlogCategory(string Name, int pageIndex, int pageSize);
    }
}
