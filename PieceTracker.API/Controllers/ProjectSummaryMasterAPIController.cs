using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PieceTracker.API.Logger;
using PieceTracker.Common;
using PieceTracker.Model;
using PieceTracker.Service;
using System.Net;

namespace PieceTracker.API.Controllers {
    [Route("api/projectsummary")]
    [ApiController]
    public class ProjectSummaryMasterAPIController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IProjectSummaryMasterService _roleService;
        private IProjectItemsMasterService _itemsMasterService;
        private IConfiguration _config;
        private readonly ApplicationSettings _appSettings;

        public ProjectSummaryMasterAPIController(ILoggerManager logger, IProjectSummaryMasterService roleService, IProjectItemsMasterService itemsMasterService, IConfiguration config, IOptions<ApplicationSettings> appSettings)
        {
            _logger = logger;
            _roleService = roleService;
            _itemsMasterService = itemsMasterService;
            _config = config;
            _appSettings = appSettings.Value;
        }
        [HttpGet("getall")]
        public async Task<ApiResponse<GetAllProjectSummaryResponse>> GetAll()
        {
            ApiResponse<GetAllProjectSummaryResponse> response = new ApiResponse<GetAllProjectSummaryResponse>() { Data = new List<GetAllProjectSummaryResponse>() };
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
        public async Task<ApiPostResponse<GetAllProjectSummaryResponse>> GetDetailById(int id)
        {
            ApiPostResponse<GetAllProjectSummaryResponse> response = new ApiPostResponse<GetAllProjectSummaryResponse>() { Data = new GetAllProjectSummaryResponse() };
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
        public async Task<BaseApiResponse> InsertUpdateDetail([FromBody] PieceTracker.Model.AddUpdateProjectSummaryRequest request)
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
        public async Task<BaseApiResponse> DeleteDetail(AddUpdateProjectSummaryRequest request)
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
        [HttpPost("createupdateProject")]
        public async Task<BaseApiResponse> CreateProjectDetail([FromBody] AddUpdateProjectRequest request)
        {
            BaseApiResponse response = new BaseApiResponse();
            try
            {
                AddUpdateProjectSummaryRequest summaryRequest = new AddUpdateProjectSummaryRequest
                {
                    Id = request.Id,
                    ProjectName = request.ProjectName,
                    ContractAmount = request.ContractAmount,
                    CustomerName = request.CustomerName,
                    EstimatedHours = request.EstimatedHours,
                    EstimatedCost = request.EstimatedCost,
                    SiteAddress = request.SiteAddress
                };
                var result = await _roleService.AddUpdateRecord(summaryRequest);
                if (result.Status)
                {
                    AddUpdateProjectItemsMasterRequest itemsMasterRequest = new AddUpdateProjectItemsMasterRequest
                    {
                        Id = request.ItemMasterId,
                        PartNumber = request.PartNumber,
                        Description = request.Description,
                        ProjectId = request.Id,
                        Quantity = request.Quantity,
                        Weight = request.Weight,
                        ProjectLocation = request.ProjectLocation,
                        BatchNumberProd = request.BatchNumberProd,
                        ItemType = request.ItemType,
                        SubItemType = request.SubItemType,
                        EstimateLabourHours = request.EstimateLabourHours,
                        ReferSheetNumber = request.ReferSheetNumber,
                        CoatingType = request.CoatingType,
                        ScheduleFabricateDate = request.ScheduleFabricateDate,
                        ScheduleCoatingDate = request.ScheduleCoatingDate,
                        ActualFabricateDate = request.ActualFabricateDate,
                        ActualCoatingDate = request.ActualCoatingDate,
                        FabricatorName = request.FabricatorName,
                        CoatingVendorName = request.CoatingVendorName,
                        ItemStatus = request.ItemStatus
                    };
                    var result1 = _itemsMasterService.AddUpdateRecord(itemsMasterRequest);
                    response.Id = result.Id;
                    response.Message = result.Message;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Success = result.Status;
                }
                else
                {
                    var delResult = _roleService.DeleteRecord(summaryRequest);
                    response.Id = result.Id;
                    response.Message = "Error in creating a project.";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Success = false;
                }
                
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
    }
}
