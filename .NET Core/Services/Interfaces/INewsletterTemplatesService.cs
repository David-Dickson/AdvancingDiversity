using Hidden.Models;
using Hidden.Models.Domain.NewsletterTemplates;
using Hidden.Models.Requests.NewsletterTemplates;
namespace Hidden.Services.Interfaces
{
    public interface INewsletterTemplatesService
    {
        int TemplatesInsert(NewsletterTemplatesInsertRequest model, int userId);

        void TemplatesUpdate(NewsletterTemplatesUpdateRequest model, int userId);

        Paged<NewsletterTemplates> Pagination(int pageIndex, int pageSize);

        Paged<NewsletterTemplates> SearchPagination(int pageIndex, int pageSize, string query);

        void TemplatesDeleteById(int id);
    }
}
