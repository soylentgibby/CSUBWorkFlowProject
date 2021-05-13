using CSUBWorkFlowProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSUBWorkFlowProject.Web.Services
{
    public interface ISearchService
    {
        Task<List<Directory>> Get();
        Task<List<Directory>> Get(string searchtype, string searchvalue);
    }

    public class SearchService : ISearchService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private List<Directory> directories;

        public SearchService(IHttpService httpService, NavigationManager navigationManager)
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
        }
        public async Task<List<Directory>> Get()
        {
            directories = new List<Directory>();
            directories = await _httpService.Get<List<Directory>>($"api/directory/");
            return directories;
        }

            public async Task<List<Directory>> Get(string searchtype, string searchvalue)
        {
            directories = new List<Directory>();

            if (searchtype == "t")
                directories = await _httpService.Get<List<Directory>>($"api/directory/title/{searchvalue}");

            if (searchtype == "f")
                directories = await _httpService.Get<List<Directory>>($"api/directory/firstname/{searchvalue}");

            if (searchtype == "l")
                directories = await _httpService.Get<List<Directory>>($"api/directory/lastname/{searchvalue}");


            return directories;
        }
    }
}
