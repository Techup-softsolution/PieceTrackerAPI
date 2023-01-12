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
    [Route("api/shops")]
    [ApiController]

    public class ShopMasterAPIController : ControllerBase {
        private readonly ILoggerManager _logger;
        private IShopService _shopService;
        private IConfiguration _config;
        private readonly ApplicationSettings _appSettings;

        public ShopMasterAPIController(ILoggerManager logger, IShopService shopService, IConfiguration config, IOptions<ApplicationSettings> appSettings) {
            _logger = logger;
            _shopService = shopService;
            _config = config;
            _appSettings = appSettings.Value;
        }

        //[HttpGet("getall")]
        //public async Task<ApiResponse<GetAllShopMasterResponse>> GetAll()
        //{
        //    ApiResponse<GetAllShopMasterResponse> response = new ApiResponse<GetAllShopMasterResponse>() { Data = new List<GetAllShopMasterResponse>() };
        //    try
        //    {
        //        var result = await _shopService.GetAll();
        //        if (result == null)
        //        {
        //            response.Success = false;
        //            response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.RecordNotFound);
        //            response.StatusCode = HttpStatusCode.NotFound;
        //        }
        //        response.Success = true;
        //        response.Data = result;
        //        response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchSuccess);
        //        response.StatusCode = HttpStatusCode.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchError);
        //        response.StatusCode = HttpStatusCode.BadRequest;
        //    }
        //    return response;
        //}
        //[HttpGet("getdetail/{id:int}")]
        //public async Task<ApiPostResponse<GetAllShopMasterResponse>> GetDetailById(int id)
        //{
        //    ApiPostResponse<GetAllShopMasterResponse> response = new ApiPostResponse<GetAllShopMasterResponse>() { Data = new GetAllShopMasterResponse() };
        //    try
        //    {
        //        var data = await _shopService.GetDetailById(id);
        //        response.Data = data;
        //        response.Success = true;
        //        response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchSuccess);
        //        response.StatusCode = HttpStatusCode.OK;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Information(ex.ToString());
        //        response.Success = false;
        //        response.Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.FetchError);
        //        response.StatusCode = HttpStatusCode.BadRequest;
        //        throw;
        //    }
        //}
        //[HttpPost("updatedetail")]
        //public async Task<BaseApiResponse> InsertUpdateDetail([FromBody] AddUpdateShopMasterRequest request)
        //{
        //    BaseApiResponse response = new BaseApiResponse();
        //    try
        //    {
        //        var result = await _shopService.AddUpdateShop(request);
        //        response.Id = result.Id;
        //        response.Message = result.Message;
        //        response.StatusCode = HttpStatusCode.OK;
        //        response.Success = result.Status;
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
        //[HttpPost("removedetail")]
        //public async Task<BaseApiResponse> DeleteDetail(AddUpdateShopMasterRequest request)
        //{
        //    BaseApiResponse response = new BaseApiResponse();
        //    try
        //    {
        //        var result = await _shopService.DeleteRecord(request);
        //        response.Message = result.Message;
        //        response.Success = result.Status;

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Information(ex.ToString());
        //        response.Success = false;
        //        response.Message = ex.Message;
        //        throw;
        //    }
        //}


        [HttpPost("AddShopDetail")]
        public async Task<BaseApiResponse> InsertUpdateDetail([FromBody] AddUpdateShopMasterRequest request) {
            BaseApiResponse response = new BaseApiResponse();
            try {
                var result = await _shopService.AddUpdateShop(request);
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

        [HttpGet("getDetailByUser/{userId:int}/{SearchString?}")]
        public async Task<ApiPostResponse<List<GetProjectDataWithDeliveryDataResponse>>> GetDetailById(int userId, string SearchString = null) {
            ApiPostResponse<List<GetProjectDataWithDeliveryDataResponse>> response = new ApiPostResponse<List<GetProjectDataWithDeliveryDataResponse>>();
            try {

                var data = await _shopService.GetDetailByUserId(userId, SearchString);
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
