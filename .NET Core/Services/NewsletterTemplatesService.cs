using Hidden.Data;
using Hidden.Data.Providers;
using Hidden.Models;
using Hidden.Models.Domain.NewsletterTemplates;
using Hidden.Models.Requests.NewsletterTemplates;
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
    public class NewsletterTemplatesService : INewsletterTemplatesService
    {
        IDataProvider _data = null;
        public NewsletterTemplatesService(IDataProvider data)
        {
            _data = data;
        }
        public int TemplatesInsert(NewsletterTemplatesInsertRequest model, int userId)
        {
            int id = 0;

            string procName = "[dbo].[NewsletterTemplates_Insert]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@CreatedBy", userId);

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                col.Add(idOut);

            }, returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@Id"].Value;

                int.TryParse(oId.ToString(), out id);
            });

            return id;
        }

        public void TemplatesUpdate(NewsletterTemplatesUpdateRequest model, int userId)
        {
            string procName = "[dbo].[NewsletterTemplates_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@Id", model.Id);
                    col.AddWithValue("@CreatedBy", userId);
                },
                returnParameters: null);
        }

        public Paged<NewsletterTemplates> Pagination(int pageIndex, int pageSize)
        {
            Paged<NewsletterTemplates> pagedResult = null;

            List<NewsletterTemplates> list = null;

            int totalCount = 0;

            string procName = "[dbo].[NewsletterTemplates_SelectAllPaginated]";

            _data.ExecuteCmd(
                procName, delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PageIndex", pageIndex);
                    paramCollection.AddWithValue("@PageSize", pageSize);

                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    NewsletterTemplates mappedTemplate = MapSingleTemplate(reader, ref startingIndex);


                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<NewsletterTemplates>();
                    }

                    list.Add(mappedTemplate);
                }
            );
            if (list != null)
            {
                pagedResult = new Paged<NewsletterTemplates>(list, pageIndex, pageSize, totalCount);
            }

            return pagedResult;

        }

        public Paged<NewsletterTemplates> SearchPagination(int pageIndex, int pageSize, string query)
        {
            Paged<NewsletterTemplates> pagedResult = null;

            List<NewsletterTemplates> list = null;

            int totalCount = 0;

            string procName = "[dbo].[NewsletterTemplates_SelectBy_Search]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@PageIndex", pageIndex);
                    paramCollection.AddWithValue("@PageSize", pageSize);
                    paramCollection.AddWithValue("@Query", query);

                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;

                    NewsletterTemplates mappedTemplate = MapSingleTemplate(reader, ref startingIndex);


                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<NewsletterTemplates>();
                    }

                    list.Add(mappedTemplate);
                }
            );
            if (list != null)
            {
                pagedResult = new Paged<NewsletterTemplates>(list, pageIndex, pageSize, totalCount);
            }

            return pagedResult;

        }

        public void TemplatesDeleteById(int id)
        {
            string procName = "[dbo].[NewsletterTemplates_Delete_ById]";
            {
                _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Id", id);
                }, returnParameters: null
                );
            }
        }

        private static NewsletterTemplates MapSingleTemplate(IDataReader reader, ref int startingIndex)
        {
            NewsletterTemplates templates = new NewsletterTemplates();

            templates.Id = reader.GetSafeInt32(startingIndex++);
            templates.Name = reader.GetSafeString(startingIndex++);
            templates.Description = reader.GetSafeString(startingIndex++);
            templates.PrimaryImage = reader.GetSafeString(startingIndex++);
            templates.DateCreated = reader.GetSafeDateTime(startingIndex++);
            templates.DateModified = reader.GetSafeDateTime(startingIndex++);
            templates.CreatedBy = reader.GetSafeInt32(startingIndex++);
            return templates;
        }

        private static void AddCommonParams(NewsletterTemplatesInsertRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@Description", model.Description);
            col.AddWithValue("@PrimaryImage", model.PrimaryImage);
        }
    }
}
