using CSUBWorkFlowProject.Data.Context;
using CSUBWorkFlowProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSUBWorkFlowProject.Data.Repositories
{
    public interface IRequestRepository
    {
        void AddRequest(Request request);
        void ApproveRequest(int requestId, int ManagerId);
        void DenyRequest(int requestid, int ManagerId);
        List<Request> GetPendingRequests();
        List<Request> GetRequestsbyUserId(int userid);
        Request GetRequestbyRequestId(int requestid);
        void SaveChanges();
    }

    public class RequestRepository : IRequestRepository
    {
        private RequestContext _requestContext;
        public RequestRepository(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public void AddRequest(Request request)
        {
            _requestContext.requests.Add(request);
        }

        public void DenyRequest(int requestid, int ManagerId)
        {
            Request request = _requestContext.requests.Where(x => x.RequestID == requestid).FirstOrDefault();
            request.ResponseDate = DateTime.Now;
            request.ManagerUserId = ManagerId;
            request.isDenied = true;
        }

        public void ApproveRequest(int requestid, int ManagerId)
        {
            Request request = _requestContext.requests.Where(x => x.RequestID == requestid).FirstOrDefault();
            request.ResponseDate = DateTime.Now;
            request.ManagerUserId = ManagerId;
            request.isApproved = true;
        }

        public List<Request> GetPendingRequests()
        {
            return _requestContext.requests.Where(x => x.isApproved == false && x.isDenied == false).ToList();
        }

        public List<Request> GetRequestsbyUserId(int userid)
        {
            return _requestContext.requests.Where(x => x.UserId == userid).ToList();
        }

        public Request GetRequestbyRequestId(int requestid)
        {
            return _requestContext.requests.Where(x => x.RequestID == requestid).First();
        }

        public void SaveChanges()
        {
            _requestContext.SaveChanges();
        }
    }
}
