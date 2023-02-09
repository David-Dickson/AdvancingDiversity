using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hidden.Models;
using Hidden.Models.Domain.NewsletterTemplates;
using Hidden.Models.Requests.NewsletterTemplates;
using Hidden.Services;
using Hidden.Services.Interfaces;
using Hidden.Web.Controllers;
using Hidden.Web.Models.Responses;
using System;

namespace Hidden.Web.Api.Controllers
{
    [Route("api/newslettertemplates")]
    [ApiController]
    public class NewsletterTemplatesApiController : BaseApiController
    {
        private INewsletterTemplatesService _service = null;
        private IAuthenticationService<int> _authService = null;
        public NewsletterTemplatesApiController (INewsletterTemplatesService service
            , ILogger<NewsletterTemplatesApiController> logger
            , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> TemplateInsert(NewsletterTemplatesInsertRequest model)
        {
            ObjectResult result = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                int id = _service.TemplatesInsert(model, userId);

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
        public ActionResult<ItemResponse<int>> TemplatesUpdate(NewsletterTemplatesUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();

                _service.TemplatesUpdate(model, userId);

                response = new SuccessResponse();

            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> TemplatesDeleteById(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _service.TemplatesDeleteById(id);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<NewsletterTemplates>>> Pagination(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<NewsletterTemplates> result = _service.Pagination(pageIndex, pageSize);

                if (result == null)
                {
                    code = 404;
                    response = new ErrorResponse("Records not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<NewsletterTemplates>> { Item = result };
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
        [HttpGet("paginate/search")]
        public ActionResult<ItemResponse<Paged<NewsletterTemplates>>> SearchPagination(int pageIndex, int pageSize, string query)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<NewsletterTemplates> result = _service.SearchPagination(pageIndex, pageSize, query);

                if (result == null)
                {
                    code = 404;
                    response = new ErrorResponse("Records not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<NewsletterTemplates>> { Item = result };
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
    }
}
