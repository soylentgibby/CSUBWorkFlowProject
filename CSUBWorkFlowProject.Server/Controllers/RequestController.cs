using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSUBWorkFlowProject.Data.Repositories;
using CSUBWorkFlowProject.Shared.Models;
using CSUBWorkFlowProject.Server.Services;

namespace CSUBWorkFlowProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public ActionResult<List<Request>> GetPendingRequests()
        {
            try
            {
                var pendingrequests = _requestService.GetPendingManagerRequests();
                return Ok(pendingrequests);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet()]
        [Route("{userid:int}")]
        public ActionResult<List<Request>> GetUserRequests(string userid)
        {
            try
            {
                var pendingrequests = _requestService.GetUserRequests(userid);
                return Ok(pendingrequests);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost("add")]
        public ActionResult AddRequest([FromBody] Request request)
        {
            try
            {
                try
                {
                    _requestService.AddRequest(request);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("deny")]
        public ActionResult DenyRequest([FromBody] Request request)
        {
            try
            {
                try
                {
                    _requestService.DenyRequest(request.RequestID, request.ManagerUserId);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("approve")]
        public ActionResult ApproveRequest([FromBody] Request request)
        {
            try
            {
                try
                {
                    _requestService.ApproveUserRequest(request.RequestID, request.ManagerUserId);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
