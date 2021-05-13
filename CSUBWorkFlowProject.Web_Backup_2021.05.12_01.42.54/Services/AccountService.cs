using CSUBWorkFlowProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSUBWorkFlowProject.Web.Services
{
    public interface IAccountService
    {
        User user { get; }
        Task Initialize();
        Task Logon(User model);
        Task Logout();
    }

    public class AccountService : IAccountService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _loginKey = "login";
        public User user { get; private set; }

        public AccountService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }
        public async Task Initialize()
        {
            user = await _localStorageService.GetItem<User>(_loginKey);
        }

        public async Task Logon(User model)
        {
            user = await _httpService.Post<User>("api/login", model);
            await _localStorageService.SetItem(_loginKey, user);
        }
        public async Task Logout()
        {
            user = null;
            await _localStorageService.RemoveItem(_loginKey);
            _navigationManager.NavigateTo("account/login");
        }
    }
}

