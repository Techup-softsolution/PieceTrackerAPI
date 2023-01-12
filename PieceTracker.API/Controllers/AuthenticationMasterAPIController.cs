using PieceTracker.API.Logger;
using PieceTracker.Common;
using PieceTracker.Model;
using PieceTracker.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Azure;
using System.Web.Helpers;

namespace PieceTracker.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationMasterAPIController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IAuthenticationMasterService _authService;
        private IConfiguration _config;
        private readonly ApplicationSettings _appSettings;

        public AuthenticationMasterAPIController(ILoggerManager logger, IAuthenticationMasterService authService, IConfiguration config, IOptions<ApplicationSettings> appSettings)
        {
            _logger = logger;
            _authService = authService;
            _config = config;
            _appSettings = appSettings.Value;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ApiPostResponse<AuthenticationDTO>> GetAdminLoginDetail(AuthenticationMasterRequest model)
        {
            ApiPostResponse<AuthenticationDTO> response = new ApiPostResponse<AuthenticationDTO>() { Data = new AuthenticationDTO() };
            try
            {
                var data = await _authService.GetUserAuthenticationDetail(model);
                response.Data = data;
                if (response.Data != null)
                {
                    response.Success = true;
                    response.Message = (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.LoginSuccess));
                    var secret = _config["Jwt:secret"];
                    var audience = _config["Jwt:Audience"];
                    var issuer = _config["Jwt:Issuer"];
                    response.JWTToken = JWTToken.GenerateJSONWebToken(model.UserEmail, data.Id.ToString(), secret, issuer, audience);
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.NotAuthorised));
                    response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.Information(ex.ToString());
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                throw;
            }
        }


        //[HttpPost("verifyuser")]
        //public async Task<BaseApiResponse> VerifyUser(VerifyUserRequest request)
        //{
        //    BaseApiResponse response = new BaseApiResponse();
        //    try
        //    {
        //        var result = await _authService.VerifyUserMobile(request);
        //        response.Id = result.Id;
        //        response.Message = result.Message;
        //        response.StatusCode = HttpStatusCode.OK;
        //        response.Success = result.Status;
        //        response.OTP = result.OTP;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Information(ex.ToString());
        //        response.Success = false;
        //        response.StatusCode = HttpStatusCode.BadRequest;
        //        response.Message = ex.Message;
        //        throw;
        //    }
        //}


        [HttpGet("userdetail")]
        //[Authorize(AuthenticationSchemes="Bearer")]
        public async Task<ApiPostResponse<AuthenticationDTO>> GetDetailById(int id)
        {
            ApiPostResponse<AuthenticationDTO> response = new ApiPostResponse<AuthenticationDTO>() { Data = new AuthenticationDTO() };
            try
            {
                var data = await _authService.GetLoggedInUserDetail(id);
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

        [HttpGet("CheckEmailExists/{email}")]
        public async Task<ApiPostResponse<bool>> CheckEmailExists(string email) {
            ApiPostResponse<bool> response = new ApiPostResponse<bool>() { Data = false };
            try {
                var data = await _authService.CheckEmailExists(email);
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

        [HttpPost]
        [Route("ChangeUserPassword")]
        public async Task<ApiPostResponse<bool>> ChangeUserPassword(AuthenticationMasterRequest model) {
            ApiPostResponse<bool> response = new ApiPostResponse<bool>() { Data = false };
            try {
                var data = await _authService.ChangeUserPassword(model);
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
    }
}
