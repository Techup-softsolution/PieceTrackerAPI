using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PieceTracker.API.Logger;
using PieceTracker.Common;
using PieceTracker.Model;
using PieceTracker.Model.Request;
using PieceTracker.Model.Response;
using PieceTracker.Service;
using System.Net;

namespace PieceTracker.API.Controllers {
    [Route("api/projectitems")]
    //[Authorize]
    [ApiController]
    public class ProjectItemsMasterAPIController : ControllerBase {
        private readonly ILoggerManager _logger;
        private IProjectItemsMasterService _roleService;
        private IConfiguration _config;
        private readonly ApplicationSettings _appSettings;

        public ProjectItemsMasterAPIController(ILoggerManager logger, IProjectItemsMasterService roleService, IConfiguration config, IOptions<ApplicationSettings> appSettings) {
            _logger = logger;
            _roleService = roleService;
            _config = config;
            _appSettings = appSettings.Value;
        }

        [HttpGet("getall")]
        public async Task<ApiResponse<GetAllProjectItemsMasterRespose>> GetAll() {
            ApiResponse<GetAllProjectItemsMasterRespose> response = new ApiResponse<GetAllProjectItemsMasterRespose>() { Data = new List<GetAllProjectItemsMasterRespose>() };
            try {
                var result = await _roleService.GetAll();
                if (result == null) {
                    response.Success = false;
                    response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.RecordNotFound);
                    response.StatusCode = HttpStatusCode.NotFound;
                }
                response.Success = true;
                response.Data = result;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchSuccess);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchError);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }

        [HttpGet("getAllByProjectId/{id:int}")]
        public async Task<ApiResponse<GetAllProjectItemsMasterRespose>> GetAllById(int id) {
            ApiResponse<GetAllProjectItemsMasterRespose> response = new ApiResponse<GetAllProjectItemsMasterRespose>() { Data = new List<GetAllProjectItemsMasterRespose>() };
            try {
                var result = await _roleService.GetAllByProjectId(id);
                if (result == null) {
                    response.Success = false;
                    response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.RecordNotFound);
                    response.StatusCode = HttpStatusCode.NotFound;
                }
                response.Success = true;
                response.Data = result;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchSuccess);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchError);
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }


        [HttpGet("getdetail/{id:int}")]
        public async Task<ApiPostResponse<GetAllProjectItemsMasterRespose>> GetDetailById(int id) {
            ApiPostResponse<GetAllProjectItemsMasterRespose> response = new ApiPostResponse<GetAllProjectItemsMasterRespose>() { Data = new GetAllProjectItemsMasterRespose() };
            try {
                var data = await _roleService.GetDetailById(id);
                response.Data = data;
                response.Success = true;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchSuccess);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex) {
                _logger.Information(ex.ToString());
                response.Success = false;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchError);
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
        }

        //
        [HttpGet("getItemsByDeliveryId/{id:int}")]
        public async Task<ApiResponse<GetAllProjectItemsMasterRespose>> GetProjectItemsByDeliveryId(int id) {
            ApiResponse<GetAllProjectItemsMasterRespose> response = new ApiResponse<GetAllProjectItemsMasterRespose>() { Data = new List<GetAllProjectItemsMasterRespose>() };
            try {
                var data = await _roleService.GetProjectItemsByDeliveryId(id);
                response.Data = data;
                response.Success = true;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchSuccess);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex) {
                _logger.Information(ex.ToString());
                response.Success = false;
                response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchError);
                response.StatusCode = HttpStatusCode.BadRequest;
                throw;
            }
        }
        //


        [HttpPost("updatedetail")]
        public async Task<BaseApiResponse> InsertUpdateDetail([FromBody] AddUpdateProjectItemsMasterRequest request) {
            BaseApiResponse response = new BaseApiResponse();
            try {
                var result = await _roleService.AddUpdateRecord(request);
                response.Id = result.Id;
                response.Message = result.Message;
                response.StatusCode = HttpStatusCode.OK;
                response.Success = result.Status;
                return response;
            }
            catch (Exception ex) {
                _logger.Information(ex.ToString());
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = ex.Message;
                throw;
            }
        }


        [HttpPost("updatedetails")]
        public async Task<BaseApiResponse> InsertUpdateDetails([FromBody] List<AddUpdateProjectItemsMasterRequest> requests) {
            BaseApiResponse response = new BaseApiResponse();
            try {
                GeneralModel result = new GeneralModel();
                foreach (var request in requests) {
                    result = await _roleService.AddUpdateRecord(request);
                    if (result.Status == false) {
                        break;
                    }
                }
                response.Message = result.Message;
                response.StatusCode = HttpStatusCode.OK;
                response.Success = result.Status;
                return response;
            }
            catch (Exception ex) {
                _logger.Information(ex.ToString());
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = ex.Message;
                throw;
            }
        }


        [HttpPost("removedetail")]
        public async Task<BaseApiResponse> DeleteDetail(AddUpdateProjectItemsMasterRequest request) {
            BaseApiResponse response = new BaseApiResponse();
            try {
                var result = await _roleService.DeleteRecord(request);
                response.Message = result.Message;
                response.Success = result.Status;

                return response;
            }
            catch (Exception ex) {
                _logger.Information(ex.ToString());
                response.Success = false;
                response.Message = ex.Message;
                throw;
            }
        }
    }
}
