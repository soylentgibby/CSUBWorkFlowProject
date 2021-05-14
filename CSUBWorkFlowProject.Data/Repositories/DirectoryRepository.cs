using CSUBWorkFlowProject.Data.Context;
using CSUBWorkFlowProject.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSUBWorkFlowProject.Data.Repositories
{
    public interface IDirectoryRepository
    {
        List<Directory> GetDirectories();
        List<Directory> SearchDirectoryByDepartment(string value);
        List<Directory> SearchDirectoryByEmail(string value);
        List<Directory> SearchDirectoryByExtension(string value);
        List<Directory> SearchDirectoryByFirstName(string value);
        List<Directory> SearchDirectoryByFullName(string value);
        List<Directory> SearchDirectoryByLastName(string value);
        List<Directory> SearchDirectoryByTitle(string value);
        void SaveNewDirectory(List<Directory> newDirectory);
        Directory FindDirectoryItemByBlob(Directory value);
    }

    public class DirectoryRepository : IDirectoryRepository
    {
        private DirectoryContext _directoryContext;
        public DirectoryRepository(DirectoryContext directoryContext)
        {
            _directoryContext = directoryContext;
        }
         
        public List<Directory> GetDirectories()
        {
            return _directoryContext.Directories.ToList();
        }

        public Directory FindDirectoryItemByBlob(Directory value)
        {
            return _directoryContext.Directories.Where(x => x.b == value.b && x.d == value.d && x.e == value.e && x.f == value.f && x.l == value.l && x.m == value.m && x.o == value.o && x.r == value.r && x.u == value.u && x.t == value.t).First();
        }

        public List<Directory> SearchDirectoryByFirstName(string value)
        {
            return _directoryContext.Directories.Where(x => x.f.ToLower().StartsWith(value.ToLower())).ToList();
        }

        public List<Directory> SearchDirectoryByLastName(string value)
        {
            return _directoryContext.Directories.Where(x => x.l.ToLower().StartsWith(value.ToLower())).ToList();
        }

        public List<Directory> SearchDirectoryByFullName(string value)
        {
            return _directoryContext.Directories.Where(x => x.l.ToLower().StartsWith(value.ToLower()) || x.f.ToLower().StartsWith(value.ToLower())).ToList();
        }

        public List<Directory> SearchDirectoryByTitle(string value)
        {
            return _directoryContext.Directories.Where(x => x.t.ToLower().StartsWith(value.ToLower())).ToList();
        }

        public List<Directory> SearchDirectoryByEmail(string value)
        {
            return _directoryContext.Directories.Where(x => x.m.ToLower().StartsWith(value.ToLower())).ToList();
        }

        public List<Directory> SearchDirectoryByDepartment(string value)
        {
            return _directoryContext.Directories.Where(x => x.d.ToLower().StartsWith(value.ToLower())).ToList();
        }

        public List<Directory> SearchDirectoryByExtension(string value)
        {
            return _directoryContext.Directories.Where(x => x.e.ToLower().StartsWith(value.ToLower())).ToList();
        }

        public void SaveNewDirectory(List<Directory> newDirectory)
        {
            _directoryContext.SaveChanges(newDirectory);
        }
    }
}
