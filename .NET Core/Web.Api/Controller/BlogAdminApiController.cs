using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Utilities;
using Microsoft.Extensions.Logging;
using Hidden.Models;
using Hidden.Models.Domain.BlogsAdminDomain;
using Hidden.Models.Requests.BlogsAdmin;
using Hidden.Services;
using Hidden.Web.Controllers;
using Hidden.Web.Models.Responses;
using System;

namespace Hidden.Web.Api.Controllers
{
    [Route("api/BlogsAdmin")]
    [ApiController]
    public class BlogAdminApiController : BaseApiController
    {
        private IBlogAdminService _service = null;
        private IAuthenticationService<int> _authenticationService = null;
        public BlogAdminApiController(IBlogAdminService service, ILogger<BlogAdminApiController> logger,
            IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authenticationService = authService;
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

        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(BlogAdminAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                int userId = _authenticationService.GetCurrentUserId();
                int id = _service.Add(model, userId);
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
        
        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(BlogAdminUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authenticationService.GetCurrentUserId();
                _service.Update(model, userId);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }
        
        [HttpGet("categories")]
        public ActionResult<ItemResponse<Paged<BlogAdmin>>> SelectByBlogCategory(string name, int pageIndex, int pageSize)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                Paged<BlogAdmin> blogAdmin = _service.SelectByBlogCategory( name, pageIndex, pageSize);

                if (blogAdmin == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application resource not found.");

                }
                else
                {
                    response = new ItemResponse<Paged<BlogAdmin>>() { Item = blogAdmin };

                }
            }

            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(iCode, response);

        }

        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<BlogAdmin>> Get(int id)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                BlogAdmin blogAdmin = _service.GetBlogAdminById(id);
                if (blogAdmin == null)
                {
                    code = 404;
                    response = new ErrorResponse("Application Resource Not Found");
                }
                else
                {
                    response = new ItemResponse<BlogAdmin> { Item = blogAdmin };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(code, response);
        }
        
        [HttpGet("blogs")]
        public ActionResult<ItemResponse<Paged<BlogAdmin>>> SelectAll(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;
            try
            {
                Paged<BlogAdmin> page = _service.SelectAll(pageIndex, pageSize);
                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<BlogAdmin>> { Item = page };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(code, response);
        }
        
        [HttpGet("authors/{authorId:int}")]
        public ActionResult<ItemResponse<Paged<BlogAdmin>>> SelectByCreatedBy(int authorId, int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<BlogAdmin> pagedList = _service.SelectByCreatedBy(authorId, pageIndex, pageSize);

                if (pagedList == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<BlogAdmin>> response = new ItemResponse<Paged<BlogAdmin>>();
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
