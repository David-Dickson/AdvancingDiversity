using Hidden.Models;
using Hidden.Models.Domain.Newsletters;
using Hidden.Models.Requests.Newsletters;

namespace Hidden.Services.Interfaces
{
    public interface INewsletterService
    {
        int AddNewsletter(NewsletterAddRequest model, int currentUser);
        
        void DeleteNewsletter(int id);
        
        Paged<Newsletters> GetNewslettersPaginated(int pageIndex, int pageSize);
        
        void UpdateNewsletter(NewsletterUpdateRequest model, int currentUser);
    }
}
