using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hidden.Models;
using Hidden.Models.Domain.Blog;
using Hidden.Models.Requests.Blog;
using Hidden.Services;
using Hidden.Services.Interfaces;
using Hidden.Web.Controllers;
using Hidden.Web.Models.Responses;

namespace Hidden.Web.Api.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogApiController : BaseApiController
    {
        private IBlogService _service = null;
        private IAuthenticationService<int> _authService = null;
        public BlogApiController(IBlogService service
            , ILogger<BlogApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(BlogAddRequest model)
        {

            ObjectResult result = null;

            try
            {
                int id = _service.Create(model);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Blogs>> SelectById(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                Blogs blogs = _service.SelectById(id);

                if (blogs == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application resource not found.");

                }
                else
                {
                    response = new ItemResponse<Blogs>() { Item = blogs };
                }
            }

            catch (Exception ex)
            {
                iCode = 500;
                Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(iCode, response);

        }
        [AllowAnonymous]
        [HttpGet("pages")]
        public ActionResult<ItemResponse<Paged<Blogs>>> SelectAll(int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<Blogs> pagedList = _service.SelectAll(pageIndex, pageSize);

                if (pagedList == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Blogs>> response = new ItemResponse<Paged<Blogs>>();
                    response.Item = pagedList;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }

            return result;
        }
        
        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse>Update(BlogUpdateRequest model)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.Update(model, userId);

                response = new SuccessResponse();
            }

            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(iCode, response);
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                _service.Delete(id);

                response = new SuccessResponse();
            }

            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(iCode, response);

        }
        [AllowAnonymous]
        [HttpGet("query/author")]
        public ActionResult<ItemResponse<Paged<Blogs>>> SelectByCreatedBy(string query, int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<Blogs> pagedList = _service.SelectByCreatedBy(query, pageIndex, pageSize);

                if (pagedList == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Blogs>> response = new ItemResponse<Paged<Blogs>>();
                    response.Item = pagedList;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }

            return result;
        }
        [AllowAnonymous]
        [HttpGet("query")]
        public ActionResult<ItemResponse<Paged<Blogs>>> SelectByBlogCategory(string query, int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<Blogs> pagedList = _service.SelectByBlogCategory(query, pageIndex, pageSize);

                if (pagedList == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Blogs>> response = new ItemResponse<Paged<Blogs>>();
                    response.Item = pagedList;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }

            return result;
        }
    }
}
