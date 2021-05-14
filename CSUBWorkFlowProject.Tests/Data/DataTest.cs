using CSUBWorkFlowProject.Data.Context;
using CSUBWorkFlowProject.Data.Repositories;
using CSUBWorkFlowProject.Server.Services;
using CSUBWorkFlowProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Directory = CSUBWorkFlowProject.Shared.Models.Directory;

namespace CSUBWorkFlowProject.Tests.Data
{
    [TestFixture]
    public class DataTest
    {
        public DataTest()
        {
        }

        [SetUp]
        public void SetUp()
        {
        }

        [TestCase]
        public async Task CanReadDataWhenInDifferntFolder()
        {
            var teldatafile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), @".\Seed\teldata.json");

            var jsonData = System.IO.File.ReadAllText(teldatafile);
            Assert.IsTrue(!string.IsNullOrEmpty(jsonData));
        }

        [TestCase]
        public async Task CanReadDataAndDeserialize()
        {
            var teldatafile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), @".\Seed\teldata.json");

            var jsonData = System.IO.File.ReadAllText(teldatafile);
            Assert.IsTrue(!string.IsNullOrEmpty(jsonData));

            List<Shared.Models.Directory> directory = JsonConvert.DeserializeObject<List<Shared.Models.Directory>>(jsonData);

            Assert.AreEqual(directory.Count, 1640);
        }

        [TestCase]
        public async Task CanUseRepository()
        {
            DirectoryContext directoryContext = new DirectoryContext();
            IDirectoryRepository directoryRepository = new DirectoryRepository(directoryContext);

            Assert.AreEqual(directoryRepository.SearchDirectoryByLastName("Gibson").Count, 2);
        }

        [TestCase]
        public async Task CanLoginAsAUser()
        {
            LoginContext loginContext = new LoginContext();
            ILoginRepository loginRepository = new LoginRepository(loginContext);

            var user = loginRepository.GetLogin("auser", "testuser");

            Assert.AreEqual(user.isManager, false);
        }

        [TestCase]
        public async Task CanLoginAsAManager()
        {
            LoginContext loginContext = new LoginContext();
            ILoginRepository loginRepository = new LoginRepository(loginContext);

            var user = loginRepository.GetLogin("amanager", "testmanager");

            Assert.AreEqual(user.isManager, true);
        }

        [TestCase]
        public async Task CanAddRequest()
        {
            LoginContext loginContext = new LoginContext();
            DirectoryContext directoryContext = new DirectoryContext();
            RequestContext requestContext = new RequestContext();

            ILoginRepository _loginRepository = new LoginRepository(loginContext);
            IRequestRepository _requestRepository = new RequestRepository(requestContext);
            IDirectoryRepository _directoryRepository = new DirectoryRepository(directoryContext);

            var requestservice = new RequestService( _loginRepository, _requestRepository, _directoryRepository);

            var directoryitem = _directoryRepository.SearchDirectoryByEmail("aabbott2@csub.edu");

            Assert.True(directoryitem.Count == 1);

            string blob = JsonConvert.SerializeObject(directoryitem[0]);

            directoryitem[0].t = "Teacher Associate";
            string newblob = JsonConvert.SerializeObject(directoryitem[0]);

            var newRequest = new Request {
                RequestField = "t",
                RequestDate = DateTime.Now,
                UserId = 1,
                OldRequestBlob = blob,
                NewRequestBlob = newblob,
                RequestChange = "Teacher Associate",
                isDenied = false,
                isApproved = false
            };

            requestservice.AddRequest(newRequest);

            var pendingquests = _requestRepository.GetPendingRequests();
            Assert.IsTrue(pendingquests.Count > 0);
        }

        [TestCase]
        public async Task CanMakeChangeRequestAndSaveToFile()
        {
            LoginContext loginContext = new LoginContext();
            DirectoryContext directoryContext = new DirectoryContext();
            RequestContext requestContext = new RequestContext();

            ILoginRepository _loginRepository = new LoginRepository(loginContext);
            IRequestRepository _requestRepository = new RequestRepository(requestContext);
            IDirectoryRepository _directoryRepository = new DirectoryRepository(directoryContext);

            var request = _requestRepository.GetRequestbyRequestId(2);
            var requestBlob = JsonConvert.DeserializeObject<Directory>(request.OldRequestBlob);

            var oldItem = _directoryRepository.FindDirectoryItemByBlob(requestBlob);

            var oldDirectory = _directoryRepository.GetDirectories();
            Assert.IsTrue(oldDirectory.Remove(oldItem));
            Assert.AreEqual(oldDirectory.Count, 1639);

            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(Directory).GetProperties();

            var directorytype = typeof(Directory);

            PropertyInfo piInstance = directorytype.GetProperty(request.RequestField);
            piInstance.SetValue(oldItem, request.RequestChange);

            oldDirectory.Add(oldItem);

            Assert.AreEqual(oldDirectory.Count, 1640);

            var directoryitem = oldDirectory.Find(x=>x.m == "aabbott2@csub.edu");

            Assert.AreEqual(directoryitem.t, "Teacher Associate");

            string teldatafile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "CompareOutput.json");

            if (File.Exists(teldatafile))
                File.Delete(teldatafile);
            
            string output = JsonConvert.SerializeObject(oldDirectory);

            File.WriteAllText(teldatafile, output);

            Assert.IsTrue(File.Exists(teldatafile));
        }

        [TestCase]
        public async Task CanOverWriteOutFile()
        {
            LoginContext loginContext = new LoginContext();
            DirectoryContext directoryContext = new DirectoryContext();
            RequestContext requestContext = new RequestContext();

            ILoginRepository _loginRepository = new LoginRepository(loginContext);
            IRequestRepository _requestRepository = new RequestRepository(requestContext);
            IDirectoryRepository _directoryRepository = new DirectoryRepository(directoryContext);

            var request = _requestRepository.GetRequestbyRequestId(2);
            var requestBlob = JsonConvert.DeserializeObject<Directory>(request.OldRequestBlob);

            var oldItem = _directoryRepository.FindDirectoryItemByBlob(requestBlob);

            var oldDirectory = _directoryRepository.GetDirectories();
            Assert.IsTrue(oldDirectory.Remove(oldItem));
            Assert.AreEqual(oldDirectory.Count, 1639);

            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(Directory).GetProperties();

            var directorytype = typeof(Directory);

            PropertyInfo piInstance = directorytype.GetProperty(request.RequestField);
            piInstance.SetValue(oldItem, request.RequestChange);

            oldDirectory.Add(oldItem);

            Assert.AreEqual(oldDirectory.Count, 1640);

            var directoryitem = oldDirectory.Find(x => x.m == "aabbott2@csub.edu");

            Assert.AreEqual(directoryitem.t, "Teacher Associate");

            _directoryRepository.SaveNewDirectory(oldDirectory);

            var directoryRepository = new DirectoryRepository(directoryContext);

            var newItem = directoryRepository.SearchDirectoryByEmail("aabbott2@csub.edu");

            Assert.AreEqual(newItem[0].t, "Teacher Associate");
        }

        [TestCase]
        public async Task CanApproveRequest()
        {
            LoginContext loginContext = new LoginContext();
            DirectoryContext directoryContext = new DirectoryContext();
            RequestContext requestContext = new RequestContext();

            ILoginRepository _loginRepository = new LoginRepository(loginContext);
            IRequestRepository _requestRepository = new RequestRepository(requestContext);
            IDirectoryRepository _directoryRepository = new DirectoryRepository(directoryContext);

            RequestService requestService = new RequestService(_loginRepository, _requestRepository, _directoryRepository);
            requestService.ApproveUserRequest(2, 2);

            var directoryRepository = new DirectoryRepository(directoryContext);

            var newItem = directoryRepository.SearchDirectoryByEmail("aabbott2@csub.edu");

            Assert.AreEqual(newItem[0].t, "Teacher Associate");
        }
    }
}
