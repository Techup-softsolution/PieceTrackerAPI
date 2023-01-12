using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PieceTracker.API.Logger;
using PieceTracker.Common;
using PieceTracker.Model;
using PieceTracker.Model.Request;
using PieceTracker.Model.Response;
using PieceTracker.Service;
using System.Net;

namespace PieceTracker.API.Controllers
{
    [Route("api/deliveryitem")]
    //[Authorize]
    [ApiController]
    public class DeliveryMasterAPIController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IDeliveryMasterService _roleService;
        private IConfiguration _config;
        private readonly ApplicationSettings _appSettings;

        public DeliveryMasterAPIController(ILoggerManager logger, IDeliveryMasterService roleService, IConfiguration config, IOptions<ApplicationSettings> appSettings)
        {
            _logger = logger;
            _roleService = roleService;
            _config = config;
            _appSettings = appSettings.Value;
        }

        [HttpGet("getall/{SearchString?}")]
        public async Task<ApiResponse<GetAllDeliveryMasterResponse>> GetAll(string SearchString = null)
        {
            ApiResponse<GetAllDeliveryMasterResponse> response = new ApiResponse<GetAllDeliveryMasterResponse>() { Data = new List<GetAllDeliveryMasterResponse>() };
            try
            {
                var result = await _roleService.GetAll(SearchString);
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
        public async Task<ApiPostResponse<GetAllDeliveryMasterResponse>> GetDetailById(int id)
        {
            ApiPostResponse<GetAllDeliveryMasterResponse> response = new ApiPostResponse<GetAllDeliveryMasterResponse>() { Data = new GetAllDeliveryMasterResponse() };
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
        public async Task<BaseApiResponse> InsertUpdateDetail([FromBody] AddUpdateDeliveryMasterRequest request)
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
        public async Task<BaseApiResponse> DeleteDetail(AddUpdateDeliveryMasterRequest request)
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
        
        [HttpPost("getProjectDeliveriesByDateAndStatus")]
        public async Task<ApiResponse<GetAllDeliveryMasterResponse>> GetProjectDeliveriesByDateAndStatus(GetProjectDeliveriesByDateAndStatusRequest request) {
            ApiResponse<GetAllDeliveryMasterResponse> response = new ApiResponse<GetAllDeliveryMasterResponse>() { Data = new List<GetAllDeliveryMasterResponse>() };
            try {              

                var result = await _roleService.GetProjectDeliveriesByDateAndStatus(request);
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
    }
}
