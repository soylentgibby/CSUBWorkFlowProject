using CSUBWorkFlowProject.Data.Repositories;
using CSUBWorkFlowProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSUBWorkFlowProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController : ControllerBase
    {

        private readonly IDirectoryRepository _directoryRepository;

        public DirectoryController(IDirectoryRepository directoryRepository)
        {
            _directoryRepository = directoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Directory>>> GetAll()
        {
            try
            {
                var directories = _directoryRepository.GetDirectories();
                return Ok(directories);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("firstname/{value}")]
        public async Task<ActionResult<List<Directory>>> SearchFirstName(string value)
        {
            try
            {
                var directories = _directoryRepository.SearchDirectoryByFirstName(value);
                return Ok(directories);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("lastname/{value}")]
        public async Task<ActionResult<List<Directory>>> SearchLastName(string value)
        {
            try
            {
                var directories = _directoryRepository.SearchDirectoryByLastName(value);
                return Ok(directories);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("name/{value}")]
        public async Task<ActionResult<List<Directory>>> SearchFullName(string value)
        {
            try
            {
                var directories = _directoryRepository.SearchDirectoryByFullName(value);
                return Ok(directories);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("department/{value}")]
        public async Task<ActionResult<List<Directory>>> SearchDepartment(string value)
        {
            try
            {
                var directories = _directoryRepository.SearchDirectoryByDepartment(value);
                return Ok(directories);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [HttpGet("email/{value}")]
        public async Task<ActionResult<List<Directory>>> SearchEmails(string value)
        {
            try
            {
                var directories = _directoryRepository.SearchDirectoryByEmail(value);
                return Ok(directories);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("ext/{value}")]
        public async Task<ActionResult<List<Directory>>> SearchExt(string value)
        {
            try
            {
                var directories = _directoryRepository.SearchDirectoryByExtension(value);
                return Ok(directories);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [HttpGet("title/{value}")]
        public async Task<ActionResult<List<Directory>>> SearchTitle(string value)
        {
            try
            {
                var directories = _directoryRepository.SearchDirectoryByTitle(value);
                return Ok(directories);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }
}
