using CSUBWorkFlowProject.Data.Context;
using CSUBWorkFlowProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSUBWorkFlowProject.Data.Repositories
{
    public interface ILoginRepository
    {
        User GetLogin(string username, string password);
        string GetUserNameByID(int id);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly LoginContext _loginContext;
        public LoginRepository(LoginContext loginContext)
        {
            _loginContext = loginContext;
        }

        public User GetLogin(string username, string password)
        {
            if (!_loginContext.logins.Where(x => x.Username == username && password == x.Password).Any())
                return null;

            return _loginContext.logins.Where(x => x.Username == username && password == x.Password).FirstOrDefault();

        }

        public string GetUserNameByID(int id)
        {
            if (!_loginContext.logins.Where(x => x.userid == id).Any())
                return null;

            return _loginContext.logins.Where(x => x.userid == id).Select(x => x.Username).FirstOrDefault();

        }
    }
}
