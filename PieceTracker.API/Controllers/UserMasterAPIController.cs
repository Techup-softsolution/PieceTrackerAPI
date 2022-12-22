using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PieceTracker.API.Logger;
using PieceTracker.Common;
using PieceTracker.Model;
using PieceTracker.Service;
using System.Net;

namespace PieceTracker.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserMasterAPIController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IUserService _userService;
        private IConfiguration _config;
        private readonly ApplicationSettings _appSettings;

        public UserMasterAPIController(ILoggerManager logger, IUserService userService, IConfiguration config, IOptions<ApplicationSettings> appSettings)
        {
            _logger = logger;
            _userService = userService;
            _config = config;
            _appSettings = appSettings.Value;
        }
        [HttpGet("getall")]
        public async Task<ApiResponse<GetAllUserMasterResponse>> GetAll()
        {
            ApiResponse<GetAllUserMasterResponse> response = new ApiResponse<GetAllUserMasterResponse>() { Data = new List<GetAllUserMasterResponse>() };
            try
            {
                var result = await _userService.GetAll();
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
        public async Task<ApiPostResponse<GetAllUserMasterResponse>> GetDetailById(int id)
        {
            ApiPostResponse<GetAllUserMasterResponse> response = new ApiPostResponse<GetAllUserMasterResponse>() { Data = new GetAllUserMasterResponse() };
            try
            {
                var data = await _userService.GetDetailById(id);
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
        public async Task<BaseApiResponse> InsertUpdateDetail([FromBody] AddUpdateUserMasterRequest request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _userService.AddUpdateUser(request);
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
        public async Task<BaseApiResponse> DeleteDetail(AddUpdateUserMasterRequest request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                var result = await _userService.DeleteRecord(request);
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