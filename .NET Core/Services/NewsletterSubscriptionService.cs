using Hidden.Data;
using Hidden.Data.Providers;
using Hidden.Models;
using Hidden.Models.Domain.Newsletters;
using Hidden.Models.Requests.Newsletters;
using Hidden.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Services
{
    public class NewsletterSubscriptionService : INewsletterSubscriptionService
    {
        private IDataProvider _data = null;
        public NewsletterSubscriptionService(IDataProvider data)  
        {
            _data = data;
        }

        public int Add(NewsletterSubscriptionAddRequest model)
        {
            int id = 0;

            string procName = "dbo.NewsletterSubscriptions_Insert";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection coll)
            {
                AddCommonParams(model, coll);
                
                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;
                coll.Add(idOut);
            },
             returnParameters: delegate (SqlParameterCollection returnColl)
             {
                 object oId = returnColl["@Id"].Value;

                 int.TryParse(oId.ToString(), out id);
             });
            return id;
        }

        public void Update(NewsletterSubscriptionAddRequest model)
        {
            string procName = "[dbo].[NewsletterSubscriptions_Update]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection coll)
            {
                AddCommonParams(model, coll);
            },
            returnParameters: null);
        }

        public List<NewsletterSubscription> Get_BySubscribed(bool isSubscribed)
        { 
            string procName = "[dbo].[NewsletterSubscriptions_Select_Subscribed]";

            List<NewsletterSubscription> subList = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection coll)
            {
                coll.AddWithValue("@IsSubscribed", isSubscribed);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                NewsletterSubscription aSubscription = MapSingleSubscription(reader, ref startingIndex);

                if (subList == null)
                {
                    subList = new List<NewsletterSubscription>();
                }
                subList.Add(aSubscription);
            });
            return subList;
        }

        public Paged<NewsletterSubscription> GetAll(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[NewsletterSubscriptions_SelectAll]";

            Paged<NewsletterSubscription> pagedList = null;
            List<NewsletterSubscription> subList = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@PageIndex", pageIndex);
                    col.AddWithValue("@PageSize", pageSize);
                }, delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;

                    NewsletterSubscription subscription = MapSingleSubscription(reader, ref startingIndex);

                    if(totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (subList == null)
                    {
                        subList = new List<NewsletterSubscription>();
                    }
                    subList.Add(subscription);
                });
            if (subList != null)
            {
                pagedList = new Paged<NewsletterSubscription>(subList, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<NewsletterSubscription> Get_By_Search(int pageIndex, int pageSize, string query)
        { 
            Paged<NewsletterSubscription> pagedList = null;
            List<NewsletterSubscription> subList = null;
            int totalCount = 0;

            string procName = "[dbo].[NewsletterSubscriptions_SelectBy_Search]";

            _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection coll)
            {
                coll.AddWithValue("@PageIndex", pageIndex);
                coll.AddWithValue("@PageSize", pageSize);
                coll.AddWithValue("@Query", query);
            }, delegate (IDataReader reader, short set)
            {
                int index = 0;

                NewsletterSubscription subscription = MapSingleSubscription(reader, ref index);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(index++);
                }

                if(subList == null)
                {
                    subList = new List<NewsletterSubscription>();
                }
                subList.Add(subscription);
            });
            if (subList != null)
            {
                pagedList = new Paged<NewsletterSubscription>(subList, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        private static void AddCommonParams(NewsletterSubscriptionAddRequest model
                                            , SqlParameterCollection coll)
        {
            coll.AddWithValue("@Email", model.Email);
            coll.AddWithValue("@IsSubscribed", model.IsSubscribed);
        }

        private static NewsletterSubscription MapSingleSubscription(IDataReader reader, ref int index)
        {
            NewsletterSubscription subscription = new NewsletterSubscription();

            subscription.Id = reader.GetSafeInt32(index++);
            subscription.Email = reader.GetSafeString(index++);
            subscription.IsSubscribed = reader.GetSafeBool(index++);
            subscription.DateCreated = reader.GetSafeDateTime(index++);
            subscription.DateModified = reader.GetSafeDateTime(index++);

            return subscription;
        }
    }
}
