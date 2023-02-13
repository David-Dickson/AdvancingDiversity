using Newtonsoft.Json;
using Hidden.Data;
using Hidden.Data.Providers;
using Hidden.Models;
using Hidden.Models.Domain.Blog;
using System.Collections.Generic;
using Hidden.Models.Domain;
using Hidden.Models.Requests;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hidden.Models.Requests.Blog;
using Hidden.Services.Interfaces;

namespace Hidden.Services
{
    public class BlogService : IBlogService
    {
        private IDataProvider _data = null;
        public BlogService(IDataProvider data)
        {
            _data = data;
        }
        
        public int Create(BlogAddRequest model)
        {
            int id = 0;

            string procName = "[dbo].[Blogs_Insert]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
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
        
        public Blogs SelectById(int id)
        {
            Blogs blogs = null;

            string procName = "[dbo].[Blogs_Select_ById]";
            _data.ExecuteCmd(procName, (col) =>
            {
                col.AddWithValue("@Id", id);
            }, (reader, set) =>
            {
                int index = 0;
                blogs = MapSingleBlog(reader, ref index);
            });

            return blogs;
        }
        
        public Paged<Blogs> SelectByBlogCategory(string query, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Blogs_Select_BlogCategory]";

            Paged<Blogs> pagedList = null;
            List<Blogs> blogList = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, (inputParams) =>
            {
                inputParams.AddWithValue("@Query", query);
                inputParams.AddWithValue("@PageIndex", pageIndex);
                inputParams.AddWithValue("@PageSize", pageSize);

            }, (reader, set) =>
            {
                int index = 0;

                Blogs blogs = null;

                blogs = MapSingleBlog(reader, ref index);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(index++);
                }

                if (blogList == null)
                {
                    blogList = new List<Blogs>();
                }

                blogList.Add(blogs);
            });

            if (blogList != null)
            {
                pagedList = new Paged<Blogs>(blogList, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }
        
        public Paged<Blogs> SelectAll(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Blogs_SelectAll]";
            Paged<Blogs> pagedList = null;
            List<Blogs> blogList = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, (inputParams) =>
            {
                inputParams.AddWithValue("@PageIndex", pageIndex);
                inputParams.AddWithValue("@PageSize", pageSize);

            }, (reader, set) =>
            {
                int index = 0;

                Blogs blogs = null;

                blogs = MapSingleBlog(reader, ref index);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(index++);
                }

                if (blogList == null)
                {
                    blogList = new List<Blogs>();
                }

                blogList.Add(blogs);
            });

            if (blogList != null)
            {
                pagedList = new Paged<Blogs>(blogList, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }
        
        public void Update(BlogUpdateRequest model, int userId)
        {
            string procName = "dbo.Blogs_Update";

            _data.ExecuteNonQuery(procName, inputParamMapper: (col) =>
            {
                col.AddWithValue("@Id", model.Id);
                AddCommonParams(model, col);

            }, returnParameters: null);
        }
        
        public void Delete(int id)
        {
            string procName = "[dbo].[Blogs_Delete]";

            _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", id);
            });
        }
        
        public Paged<Blogs> SelectByCreatedBy(string query, int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Blogs_Select_ByCreatedBy]";

            Paged<Blogs> pagedList = null;
            List<Blogs> blogList = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, (inputParams) =>
            {
                inputParams.AddWithValue("@Query", query);
                inputParams.AddWithValue("@PageIndex", pageIndex);
                inputParams.AddWithValue("@PageSize", pageSize);

            }, (reader, set) =>
            {
                int index = 0;

                Blogs blogs = null;

                blogs = MapSingleBlog(reader, ref index);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(index++);
                }

                if (blogList == null)
                {
                    blogList = new List<Blogs>();
                }

                blogList.Add(blogs);
            });

            if (blogList != null)
            {
                pagedList = new Paged<Blogs>(blogList, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }
        
        private static void AddCommonParams(BlogAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@BlogTypeId", model.BlogTypeId);
            col.AddWithValue("@AuthorId", model.AuthorId);
            col.AddWithValue("@Title", model.Title);
            col.AddWithValue("@Subject", model.Subject);
            col.AddWithValue("@Content", model.Content);
            col.AddWithValue("@IsPublished", model.IsPublished);
            col.AddWithValue("@ImageUrl", model.ImageUrl);
        }
        
        private static Blogs MapSingleBlog(IDataReader reader, ref int index)
        {
            Blogs blogs = new Blogs();

            blogs.Id = reader.GetSafeInt32(index++);
            blogs.BlogTypeId = reader.GetSafeInt32(index++);
            blogs.BlogTypeName = reader.GetSafeString(index++);
            blogs.AuthorId = reader.GetSafeInt32(index++);
            blogs.AuthorEmail = reader.GetSafeString(index++);
            blogs.Title = reader.GetSafeString(index++);
            blogs.Subject = reader.GetSafeString(index++);
            blogs.Content = reader.GetSafeString(index++);
            blogs.IsPublished = reader.GetSafeBool(index++);
            blogs.ImageUrl = reader.GetSafeString(index++);
            blogs.DateCreated = reader.GetSafeDateTime(index++);
            blogs.DateModified = reader.GetSafeDateTime(index++);

            return blogs;
        }
    }
}
