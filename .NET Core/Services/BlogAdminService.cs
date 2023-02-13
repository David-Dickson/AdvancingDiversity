using Hidden.Data;
using Hidden.Data.Providers;
using Hidden.Models;
using Hidden.Models.Domain.BlogsAdminDomain;
using Hidden.Models.Requests.BlogsAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hidden.Services
{
    public class BlogAdminService : IBlogAdminService
    {

        IDataProvider _data = null;
        public BlogAdminService(IDataProvider data)

        {
            _data = data;
        }

        public void Update(BlogAdminUpdateRequest model, int authorId)
        {
            string procName = "[dbo].[BlogsAdmin_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@Id", model.Id);
                col.AddWithValue("@AuthorId", authorId);
                
            },
            returnParameters: null
            );
        }
        
        public Paged<BlogAdmin> SelectByBlogCategory(string name, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[BlogsAdmin_Select_BlogCategory]";
            Paged<BlogAdmin> pagedList = null;
            List<BlogAdmin> blogList = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, (SqlParameterCollection inputParams) =>
            {
                inputParams.AddWithValue("@Name", name);
                inputParams.AddWithValue("@PageIndex", pageIndex);
                inputParams.AddWithValue("@PageSize", pageSize);
               

            }, (IDataReader reader, short set) =>
            {
                int index = 0;

                BlogAdmin blogAdmin = null;

                blogAdmin = MapSingleBlogAdmin(reader, ref index);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(index++);
                }

                if (blogList == null)
                {
                    blogList = new List<BlogAdmin>();
                }

                blogList.Add(blogAdmin);
            });

            if (blogList != null)
            {
                pagedList = new Paged<BlogAdmin>(blogList, pageIndex, pageSize, totalCount);
            }
            
            return pagedList;
        }
        
        public Paged<BlogAdmin> SelectAll(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[BlogsAdmin_SelectAllPaginated]";
            Paged<BlogAdmin> pagedList = null;
            List<BlogAdmin> blogList = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, (SqlParameterCollection inputParams) =>
            {
                inputParams.AddWithValue("@PageIndex", pageIndex);
                inputParams.AddWithValue("@PageSize", pageSize);

            }, (IDataReader reader, short set) =>
            {
                int index = 0;

                BlogAdmin blogAdmin = null;

                blogAdmin = MapSingleBlogAdmin(reader, ref index);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(index++);
                }

                if (blogList == null)
                {
                    blogList = new List<BlogAdmin>();
                }

                blogList.Add(blogAdmin);
            });

            if (blogList != null)
            {
                pagedList = new Paged<BlogAdmin>(blogList, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }
        
        public BlogAdmin GetBlogAdminById(int id)
        {
            string procName = "[dbo].[BlogsAdmin_Select_ById]";
            BlogAdmin aBlogAdmin = null;
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                aBlogAdmin = MapSingleBlogAdmin(reader, ref startingIndex);
            });
            return aBlogAdmin;
        }
        
        public Paged<BlogAdmin> SelectByCreatedBy(int authorId, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[BlogsAdmin_Select_ByCreatedBy]";

            Paged<BlogAdmin> pagedList = null;
            List<BlogAdmin> blogAdminList = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, (SqlParameterCollection inputParams) =>
            {
                inputParams.AddWithValue("@AuthorId", authorId);
                inputParams.AddWithValue("@PageIndex", pageIndex);
                inputParams.AddWithValue("@PageSize", pageSize);

            }, (IDataReader reader, short set) =>
            {
                int index = 0;

                BlogAdmin blogAdmin = null;

                blogAdmin = MapSingleBlogAdmin(reader, ref index);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(index++);
                }

                if (blogAdminList == null)
                {
                    blogAdminList = new List<BlogAdmin>();
                }

                blogAdminList.Add(blogAdmin);
            });

            if (blogAdminList != null)
            {
                pagedList = new Paged<BlogAdmin>(blogAdminList, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }

        public void Delete(int id)
        {
            string procName = "[dbo].[BlogsAdmin_Delete]";


            _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection paramCollection)
            {

                paramCollection.AddWithValue("@Id", id);

            }, returnParameters: null);

        }
        
        public int Add(BlogAdminAddRequest model, int authorId)
        {
            int id = 0;
            string procName = "[dbo].[Blogs_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@AuthorId", authorId);

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;
                col.Add(idOut);
            },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);

                });
            return id;
        }
        
        private static BlogAdmin MapSingleBlogAdmin(IDataReader reader, ref int startingIndex)
        {
            BlogAdmin aBlogAdmin = new BlogAdmin();
            
            
            aBlogAdmin.Id = reader.GetInt32(startingIndex++);
            aBlogAdmin.BlogTypeId = reader.GetInt32(startingIndex++);
            aBlogAdmin.Name = reader.GetSafeString(startingIndex++);
            aBlogAdmin.AuthorId = reader.GetInt32(startingIndex++);
            aBlogAdmin.AuthorEmail = reader.GetString(startingIndex++);
            aBlogAdmin.Title = reader.GetString(startingIndex++);
            aBlogAdmin.Subject = reader.GetString(startingIndex++);
            aBlogAdmin.Content = reader.GetString(startingIndex++);
            aBlogAdmin.IsPublished = reader.GetBoolean(startingIndex++);
            aBlogAdmin.ImageUrl = reader.GetSafeString(startingIndex++);
            aBlogAdmin.DateCreated = reader.GetDateTime(startingIndex++);
            aBlogAdmin.DateModified = reader.GetDateTime(startingIndex++);

            return aBlogAdmin;
        }
        
        private static void AddCommonParams(BlogAdminAddRequest model, SqlParameterCollection col)
        {
          
            col.AddWithValue("@BlogTypeId", model.BlogTypeId);
            col.AddWithValue("@Title",model.Title);
            col.AddWithValue("@Subject",model.Subject);
            col.AddWithValue("@Content",model.Content);
            col.AddWithValue("@IsPublished",model.IsPublished);
            col.AddWithValue("@ImageUrl", model.ImageUrl);
            
        }                
    }
}
