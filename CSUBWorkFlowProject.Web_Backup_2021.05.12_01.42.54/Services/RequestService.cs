using CSUBWorkFlowProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSUBWorkFlowProject.Web.Services
{
    public interface IRequestService
    {
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
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
