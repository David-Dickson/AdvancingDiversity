using Hidden.Models;
using Hidden.Models.Domain.Newsletters;
using Hidden.Models.Requests.Newsletters;
using System.Collections.Generic;

namespace Hidden.Services.Interfaces
{
    public interface INewsletterSubscriptionService
    {
        int Add(NewsletterSubscriptionAddRequest model);

        void Update(NewsletterSubscriptionAddRequest model);

        List<NewsletterSubscription> Get_BySubscribed(bool isSubscribed);

        Paged<NewsletterSubscription> GetAll(int pageIndex, int pageSize);

        Paged<NewsletterSubscription> Get_By_Search(int pageIndex, int pageSize, string query);
    }
}
