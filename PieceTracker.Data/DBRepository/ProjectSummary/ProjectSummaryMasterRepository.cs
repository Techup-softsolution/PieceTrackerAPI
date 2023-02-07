using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PieceTracker.Common;
using PieceTracker.Model;
using PieceTracker.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Common.Configuration;

namespace PieceTracker.Data.DBRepository {
    public class ProjectSummaryMasterRepository : BaseRepository, IProjectSummaryMasterRepository {
        private IConfiguration _config;
        private string APIBaseURL;
        public ProjectSummaryMasterRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig) {
            _config = config;
            APIBaseURL = dataConfig.Value.FilePath;
        }

        public async Task<List<GetAllProjectSummaryResponse>> GetAll(string SearchString) {
            try {
                var param = new DynamicParameters();
                param.Add("@Mode", "S");
                if (!string.IsNullOrWhiteSpace(SearchString))
                    param.Add("@Search", SearchString);
                var data = await QueryAsync<GetAllProjectSummaryResponse>(SPHelper.ProjectSummary, param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex) {
                return new List<GetAllProjectSummaryResponse>();
            }
        }
        public async Task<GetAllProjectSummaryResponse> GetDetailById(int id) {
            try {
                var param = new DynamicParameters();
                param.Add("@Mode", "SI");
                param.Add("@Id", id);
                var data = await QueryAsync<GetAllProjectSummaryResponse>(SPHelper.ProjectSummary, param, commandType: CommandType.StoredProcedure);
                return data.FirstOrDefault();
            }
            catch (Exception ex) {
                return new GetAllProjectSummaryResponse();
            }
        }
        public async Task<GeneralModel> AddUpdateRecord(AddUpdateProjectSummaryRequest request) {
            GeneralModel modelResponse = new GeneralModel();
            try {
                var param = new DynamicParameters();
                if (request.Id == 0) {
                    param.Add("@Mode", "I");
                }
                else {
                    param.Add("@Mode", "U");
                    param.Add("@Id", request.Id);
                }
                param.Add("@ProjectName", request.ProjectName);
                param.Add("@ContractAmount", request.ContractAmount);
                param.Add("@CustomerName", request.CustomerName);
                param.Add("@EstimatedHours", request.EstimatedHours);
                param.Add("@EstimatedCost", request.EstimatedCost);
                param.Add("@SiteAddress", request.SiteAddress);
                //param.Add("@EstimatedCostAndTime", request.EstimatedCostAndTime);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.CreatedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.ProjectSummary, param, commandType: CommandType.StoredProcedure);
                modelResponse.Status = result.Status;

                if (result.Status == false)
                    modelResponse.Message = Utility.GetResponseMessage(result.Status, request.Id, (int)Enums.ActionName.AddUpdate, result.Message);
                else
                    modelResponse.Message = Utility.GetResponseMessage(result.Status, request.Id, (int)Enums.ActionName.AddUpdate);

                modelResponse.Id = result.Id;
                return modelResponse;
            }
            catch (Exception ex) {
                return new GeneralModel {
                    Id = 0,
                    Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.RecordNotFound),
                    Status = false,
                    RecordId = 0
                };
            }
        }
        public async Task<GeneralModel> DeleteRecord(AddUpdateProjectSummaryRequest request) {
            GeneralModel response = new GeneralModel();
            try {
                var param = new DynamicParameters();
                param.Add("@Mode", "D");
                param.Add("@Id", request.Id);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.ModifiedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.ProjectSummary, param, commandType: CommandType.StoredProcedure);
                response.Status = result.Status;
                response.Message = Utility.GetResponseMessage(result.Status, request.Id, (int)Enums.ActionName.Delete);
                response.Id = request.Id;
            }
            catch (Exception ex) {
                return new GeneralModel {
                    Id = request.Id,
                    Status = false,
                    Message = (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.DeleteErrorMessage)) + " : " + ex.InnerException.Message.ToString()
                };
            }
            return response;
        }

        /// <summary>
        /// Get Project Delivery Details With ProjectItems
        /// </summary>
        /// <param name="id">ProjectId</param>
        /// <returns></returns>
        public async Task<GetProjectDataWithDeliveryDataResponse> GetAllProjectDataWithDeliveryListAsync(int id) {
            try {
                GetProjectDataWithDeliveryDataResponse response = new GetProjectDataWithDeliveryDataResponse();
                var param = new DynamicParameters();
                param.Add("@Mode", "SI");
                param.Add("@Id", id);
                var data = await QueryAsync<GetAllProjectSummaryResponse>(SPHelper.ProjectSummary, param, commandType: CommandType.StoredProcedure);
                response.Project = data.FirstOrDefault();



                param = new DynamicParameters();
                param.Add("@Mode", "SPID");
                param.Add("@Id", id);
                var projectItems = await QueryAsync<GetAllProjectItemsMasterRespose>(SPHelper.ProjectItems, param, commandType: CommandType.StoredProcedure);

                var distDeliveryIds = projectItems.Select(x => x.DeliveryId.Value).ToList().Distinct();

                foreach (var item in distDeliveryIds.Where(c => c > 0)) {

                    param = new DynamicParameters();
                    param.Add("@Mode", "SI");
                    param.Add("@Id", item);
                    var deliveryData = await QueryAsync<GetAllDeliveryMasterResponse>(SPHelper.Delivery, param, commandType: CommandType.StoredProcedure);


                    ProjectDeliveryData delData = new ProjectDeliveryData();
                    delData.DeliveryData = deliveryData.FirstOrDefault();
                    delData.ProjectItems = projectItems.Where(c => c.DeliveryId == item).ToList();
                    response.DeliveryDatas.Add(delData);
                }

                response.NoDeliveryData = projectItems.Where(c => c.DeliveryId == null || c.DeliveryId == 0).ToList();
                response.ProjectItems = projectItems.ToList();
                return response;
            }
            catch (Exception ex) {
                return new GetProjectDataWithDeliveryDataResponse();
            }
        }

        public async Task<List<GetProjectDataWithDeliveryDataResponse>> GetAllProjectDetailsAsync(string SearchString) {
            try {
                List<GetProjectDataWithDeliveryDataResponse> responseList = new List<GetProjectDataWithDeliveryDataResponse>();
                var param = new DynamicParameters();
                param.Add("@Mode", "S");
                if (!string.IsNullOrWhiteSpace(SearchString))
                    param.Add("@Search", SearchString);
                var projectData = await QueryAsync<GetAllProjectSummaryResponse>(SPHelper.ProjectSummary, param, commandType: CommandType.StoredProcedure);


                param = new DynamicParameters();
                param.Add("@Mode", "S");
                var projectItems = await QueryAsync<GetAllProjectItemsMasterRespose>(SPHelper.ProjectItems, param, commandType: CommandType.StoredProcedure);


                foreach (var item in projectData.ToList()) {
                    var proj = new GetProjectDataWithDeliveryDataResponse();
                    proj.Project = item;
                    proj.ProjectItems = projectItems.Where(c => c.ProjectId == proj.Project.Id).ToList();
                    responseList.Add(proj);
                }

                return responseList;
            }
            catch (Exception ex) {
                return new List<GetProjectDataWithDeliveryDataResponse>();
            }
        }
    }
}


