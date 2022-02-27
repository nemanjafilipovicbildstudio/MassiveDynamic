using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MassiveDynamic.Configs;
using MassiveDynamic.Data.Models;
using MassiveDynamic.Data.Repositories;
using MassiveDynamic.DTOs;
using MassiveDynamic.Data.ModelConfigs;
using Microsoft.AspNetCore.Authorization;

namespace UploadFilesServer.Controllers
{
    [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Secretary + "," + RoleNames.Client)]
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IRepository<Document, string> _documentRepository;
        private readonly IMapper _mapper;
        private readonly UploadConfig _uploadConfig;

        public DocumentController(
            IRepository<Document, string> documentRepository,
            IOptions<UploadConfig> options,
            IMapper mapper
        ) {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _uploadConfig = options.Value;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            using var memoryStream = new MemoryStream();
            await Request.Form.Files[0].CopyToAsync(memoryStream);

            var extension = Path.GetExtension(Request.Form.Files[0].FileName);
            if (!_uploadConfig.AllowedExtensions.Any(x => x == extension))
            {
                return BadRequest("Unsupported extension.");
            }
            if (memoryStream.Length > _uploadConfig.MaxFileSizeMB * 1024 * 1024)
            {
                return BadRequest("File is too large.");
            }

            var file = new Document()
            {
                CompanyId = Request.QueryString.ToString().Substring(Request.QueryString.ToString().IndexOf('=') + 1),
                Name = Request.Form.Files[0].FileName,
                Content = memoryStream.ToArray(),
                UploadTime = DateTime.UtcNow
            };

            await _documentRepository.InsertAsync(file);
            return Ok();
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Secretary)]
        [HttpGet("download")]
        public async Task<IActionResult> Download(string id)
        {
            var document = await _documentRepository.GetAsync(id);
            var extension = document.Name.Substring(document.Name.LastIndexOf('.') + 1);
            return File(document.Content, $"application/{extension}", document.Name);
        }

        [Authorize(Roles = RoleNames.Admin + "," + RoleNames.Secretary)]
        [HttpGet("getCompanyDocuments")]
        public async Task<IEnumerable<GetDocumentDto>> GetCompanyDocuments(string companyId)
        {
            var documents = await _documentRepository.GetQueryable().Where(x => x.CompanyId == companyId).ToListAsync();
            return _mapper.Map<IEnumerable<GetDocumentDto>>(documents);
        }

        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete]
        public async Task Delete(string id)
        {
            await _documentRepository.DeleteAsync(id);
        }
    }
}