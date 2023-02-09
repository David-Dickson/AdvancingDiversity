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
    public class NewsletterService : INewsletterService
    {
        IDataProvider _data = null;

        public NewsletterService(IDataProvider data)
        {
            _data = data;
        }

        public Paged<Newsletters> GetNewslettersPaginated(int pageIndex, int pageSize)
        {
            Paged<Newsletters> pagedResult = null;

            List<Newsletters> result = null;

            int totalCount = 0;

            string pageLetter = "[dbo].[Newsletters_SelectAll]";
            _data.ExecuteCmd(pageLetter, inputParamMapper: delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@PageIndex", pageIndex);
                parameterCollection.AddWithValue("@PageSize", pageSize);

            },
             singleRecordMapper: delegate (IDataReader reader, short set)
             {
                 int startingIndex = 0;
                 Newsletters newsletter = MapSingleNewsletter(reader, ref startingIndex);

                 if (totalCount == 0)
                 {
                     totalCount = reader.GetSafeInt32(startingIndex++);
                 }

                 if (result == null)
                 {
                     result = new List<Newsletters>();
                 }
                 result.Add(newsletter);
             });
            if (result != null)
            {
                pagedResult = new Paged<Newsletters>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }

        public void DeleteNewsletter(int id)
        {
            string procName = "[dbo].[Newsletters_Delete_By_Id]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", id);
            }, returnParameters: null);
        }

        public void UpdateNewsletter(NewsletterUpdateRequest model, int userId)
        {
            string procName = "[dbo].[Newsletters_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommmonParams(model, userId, col);
                col.AddWithValue("@Id", model.Id);
            },

           returnParameters: null);

        }

        public int AddNewsletter(NewsletterAddRequest model,int userId)
        {
            string procName = "[dbo].[Newsletters_Insert]";

            int id = 0;

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommmonParams(model, userId, col);


                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                col.Add(idOut);

            }, returnParameters: delegate (SqlParameterCollection returnCol)
            {
                object idObject = returnCol["@Id"].Value;
                int.TryParse(idObject.ToString(), out id);
            });

            return id;

        }

        private static Newsletters MapSingleNewsletter(IDataReader reader, ref int startingIndex)
        {
            Newsletters onNewsletter = new Newsletters();

            onNewsletter.Id = reader.GetSafeInt32(startingIndex++);
            onNewsletter.TemplateId = reader.GetSafeInt32(startingIndex++);
            onNewsletter.Name = reader.GetSafeString(startingIndex++);
            onNewsletter.CoverPhoto = reader.GetSafeString(startingIndex++);
            onNewsletter.DateToPublish = reader.GetSafeDateTime(startingIndex++);
            onNewsletter.DateToExpire = reader.GetSafeDateTime(startingIndex++);
            onNewsletter.DateCreated = reader.GetSafeDateTime(startingIndex++);
            onNewsletter.DateModified = reader.GetSafeDateTime(startingIndex++);
            onNewsletter.CreatedBy = reader.GetSafeInt32(startingIndex++);

            return onNewsletter;

        }

        private static void AddCommmonParams(NewsletterAddRequest model, int userId, SqlParameterCollection col)
        {
            col.AddWithValue("@TemplateId", model.TemplateId);
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@CoverPhoto", model.CoverPhoto);
            col.AddWithValue("@DateToPublish", model.DateToPublish);
            col.AddWithValue("@DateToExpire", model.DateToExpire);
            col.AddWithValue("@CreatedBy", userId);
        }


    }
}
