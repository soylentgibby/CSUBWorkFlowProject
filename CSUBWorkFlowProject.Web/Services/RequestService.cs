using CSUBWorkFlowProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSUBWorkFlowProject.Web.Services
{
    public interface IRequestService
    {
        Task ApproveRequest(Request request, int managerid);
        Task DenyRequest(Request request, int managerid);
        Task<List<Request>> GetPendingRequests();
        Task<List<Request>> GetUserRequests(int userid);
        Task RequestChange(Directory directory, int userid);
    }

    public class RequestService : IRequestService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;

        public RequestService(IHttpService httpService, NavigationManager navigationManager)
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
        }


        public async Task RequestChange(Directory directory, int userid)
        {

            var newRequest = new Request
            {
                //would of been nice to have them change different fields
                RequestField = "t",
                RequestDate = DateTime.Now,
                UserId = userid,
                RequestBlob = JsonConvert.SerializeObject(directory),
                RequestChange = directory.t,
                isDenied = false,
                isApproved = false
            };

            await _httpService.Post<Request>($"api/request/add", newRequest);

        }

        public async Task ApproveRequest(Request request, int managerid)
        {
            request.ManagerUserId = managerid;

            await _httpService.Put<Request>($"api/request/approve", request);
        }
        public async Task DenyRequest(Request request, int managerid)
        {
            request.ManagerUserId = managerid;

            await _httpService.Put<Request>($"api/request/deny", request);
        }
        public async Task<List<Request>> GetUserRequests(int userid)
        {
            List<Request> requests = new List<Request>();
            requests = await _httpService.Get<List<Request>>($"api/request/{userid}");
            return requests;
        }
        public async Task<List<Request>> GetPendingRequests()
        {
            List<Request> requests = new List<Request>();
            requests = await _httpService.Get<List<Request>>($"api/request");
            return requests;
        }
    }
}
