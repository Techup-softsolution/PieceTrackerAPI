using PieceTracker.Common;
using PieceTracker.Model;
using PieceTracker.Service;
using PieceTracker.API.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PieceTracker.API.Controllers
{
    [Route("api/projectitemstatus")]
    [ApiController]

    public class ProjectItemStatusMasterAPIController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IProjectItemStatusService _roleService;
        private IConfiguration _config;
        private readonly ApplicationSettings _appSettings;

        public ProjectItemStatusMasterAPIController(ILoggerManager logger, IProjectItemStatusService roleService, IConfiguration config, IOptions<ApplicationSettings> appSettings)
        {
            _logger = logger;
            _roleService = roleService;
            _config = config;
            _appSettings = appSettings.Value;
        }
        [HttpGet("getall")]
        public async Task<ApiResponse<GetAllProjectItemStatusResponse>> GetAll()
        {
            ApiResponse<GetAllProjectItemStatusResponse> response = new ApiResponse<GetAllProjectItemStatusResponse>() { Data = new List<GetAllProjectItemStatusResponse>() };
            try
            {
                var result = await _roleService.GetAll();
                if (result == null)
                {
                    response.Success = false;
                    response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.RecordNotFound);
                    response.StatusCode = HttpStatusCode.NotFound;
                }
                response.Success = true;
                response.Data = result;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchSuccess);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchError);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }
        [HttpGet("getdetail/{id:int}")]
        public async Task<ApiPostResponse<GetAllProjectItemStatusResponse>> GetDetailById(int id)
        {
            ApiPostResponse<GetAllProjectItemStatusResponse> response = new ApiPostResponse<GetAllProjectItemStatusResponse>() { Data = new GetAllProjectItemStatusResponse() };
            try
            {
                var data = await _roleService.GetDetailById(id);
                response.Data = data;
                response.Success = true;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchSuccess);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                _logger.Information(ex.ToString());
                response.Success = false;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchError);
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
        }
        [HttpPost("updatedetail")]
        public async Task<BaseApiResponse> InsertUpdateDetail([FromBody] AddUpdateProjectItemStatusRequest request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _roleService.AddUpdateRecord(request);
                response.Id = result.Id;
                response.Message = result.Message;
                response.StatusCode = HttpStatusCode.OK;
                response.Success = result.Status;
                return response;
            }
            catch (Exception ex)
            {
                _logger.Information(ex.ToString());
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = ex.Message;
                throw;
            }
        }
        [HttpPost("removedetail")]
        public async Task<BaseApiResponse> DeleteDetail(AddUpdateProjectItemStatusRequest request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _roleService.DeleteRecord(request);
                response.Message = result.Message;
                response.Success = result.Status;

                return response;
            }
            catch (Exception ex)
            {
                _logger.Information(ex.ToString());
                response.Success = false;
                response.Message = ex.Message;
                throw;
            }
        }
    }
}
