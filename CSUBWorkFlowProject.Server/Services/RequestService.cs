using CSUBWorkFlowProject.Data.Repositories;
using CSUBWorkFlowProject.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CSUBWorkFlowProject.Server.Services
{
    public interface IRequestService
    {
        void AddRequest(Request request);
        void ApproveUserRequest(int requestId, int managerid);
        void DenyRequest(int requestId, int managerid);
        List<Request> GetPendingManagerRequests();
        List<Request> GetUserRequests(string userid);
    }

    public class RequestService : IRequestService
    {
        ILoginRepository _loginRepository;
        IRequestRepository _requestRepository;
        IDirectoryRepository _directoryRepository;

        public RequestService(ILoginRepository loginRepository,
                              IRequestRepository requestRepository,
                              IDirectoryRepository directoryRepository)
        {
            _loginRepository = loginRepository;
            _requestRepository = requestRepository;
            _directoryRepository = directoryRepository;
        }

        public List<Request> GetPendingManagerRequests()
        {
            List<Request> requests = new List<Request>();
            requests = _requestRepository.GetPendingRequests();
            return requests;
        }

        public List<Request> GetUserRequests(string userid)
        {
            List<Request> requests = new List<Request>();
            requests = _requestRepository.GetRequestsbyUserId(Convert.ToInt32(userid));
            return requests;
        }

        public void ApproveUserRequest(int requestId, int managerid)
        {
            var request = _requestRepository.GetRequestbyRequestId(requestId);
            var requestBlob= JsonConvert.DeserializeObject<Directory>(request.RequestBlob);

            var oldItem = _directoryRepository.FindDirectoryItemByBlob(requestBlob);

            var oldDirectory = _directoryRepository.GetDirectories();
            oldDirectory.Remove(oldItem);

            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(Directory).GetProperties();

            var directorytype = typeof(Directory);

            PropertyInfo piInstance = directorytype.GetProperty(request.RequestField);
            piInstance.SetValue(oldItem, request.RequestChange);

            oldDirectory.Add(oldItem);

            _directoryRepository.SaveNewDirectory(oldDirectory);

            _requestRepository.ApproveRequest(requestId, managerid);
            _requestRepository.SaveChanges();

        }

        public void DenyRequest(int requestId, int managerid)
        {
            _requestRepository.DenyRequest(requestId, managerid);
            _requestRepository.SaveChanges();
        }

        public void AddRequest(Request request)
        {
            _requestRepository.AddRequest(request);
            _requestRepository.SaveChanges();
        }
    }
}
