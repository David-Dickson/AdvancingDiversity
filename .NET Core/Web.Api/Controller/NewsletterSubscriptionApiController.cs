using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hidden.Models;
using Hidden.Models.Domain.Newsletters;
using Hidden.Models.Requests.Newsletters;
using Hidden.Services;
using Hidden.Services.Interfaces;
using Hidden.Web.Controllers;
using Hidden.Web.Models.Responses;
using System;
using System.Collections.Generic;

namespace Hidden.Web.Api.Controllers
{
    [Route("api/newslettersubscriptions")]
    [ApiController]
    public class NewsletterSubscriptionApiController : BaseApiController
    {
        private INewsletterSubscriptionService _service = null;
        private IAuthenticationService<int> _authService = null;

        public NewsletterSubscriptionApiController(INewsletterSubscriptionService service
                                                    , ILogger<NewsletterSubscriptionApiController> logger
                                                    , IAuthenticationService<int> authService) : base(logger)
        {
            _service = service;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<ItemResponse<int>> Create(NewsletterSubscriptionAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                int id = _service.Add(model);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                result = Created201(response);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }
            return result;
        }

        [HttpPut]
        public ActionResult<SuccessResponse> Update(NewsletterSubscriptionAddRequest model)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                _service.Update(model);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }
            return StatusCode(iCode, response);
        }

        [HttpGet]
        public ActionResult<ItemsResponse<NewsletterSubscription>> Get_BySubscribed(bool isSubscribed)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                List<NewsletterSubscription> subList = _service.Get_BySubscribed(isSubscribed);

                if (subList == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<NewsletterSubscription> { Items = subList };
                }
            }
            catch (Exception ex)
            {

                iCode = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }
            return StatusCode(iCode, response);
        }

        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<NewsletterSubscription>>> GetAll_Paginated(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<NewsletterSubscription> subscription = _service.GetAll(pageIndex, pageSize);

                if (subscription == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    code = 200;
                    response = new ItemResponse<Paged<NewsletterSubscription>> { Item = subscription };
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

        [HttpGet("search")]
        public ActionResult<ItemResponse<Paged<NewsletterSubscription>>> Get_By_Search_Paginated(int pageIndex, int pageSize, string query)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<NewsletterSubscription> subscription = _service.Get_By_Search(pageIndex, pageSize, query);

                if (subscription == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<NewsletterSubscription>> { Item = subscription };
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
