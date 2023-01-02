using Dapper;
using Microsoft.Extensions.Configuration;
using Dapper;
using PieceTracker.Common;
using PieceTracker.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PieceTracker.Model.Response;
using PieceTracker.Model.Request;

namespace PieceTracker.Data.DBRepository
{
    public class ProjectItemsMasterRepository : BaseRepository, IProjectItemsMasterRepository
    {
        private IConfiguration _config;
        private string APIBaseURL;
        public ProjectItemsMasterRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
            APIBaseURL = dataConfig.Value.FilePath;
        }

        public async Task<List<GetAllProjectItemsMasterRespose>> GetAll()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "S");
                var data = await QueryAsync<GetAllProjectItemsMasterRespose>(SPHelper.ProjectItems, param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                return new List<GetAllProjectItemsMasterRespose>();
            }
        }

        public async Task<List<GetAllProjectItemsMasterRespose>> GetAllByProjectId(int id) {
            try {
                var param = new DynamicParameters();
                param.Add("@Mode", "SPID");
                param.Add("@Id", id);
                var data = await QueryAsync<GetAllProjectItemsMasterRespose>(SPHelper.ProjectItems, param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex) {
                return new List<GetAllProjectItemsMasterRespose>();
            }
        }

        public async Task<GetAllProjectItemsMasterRespose> GetDetailById(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "SI");
                param.Add("@Id", id);
                var data = await QueryAsync<GetAllProjectItemsMasterRespose>(SPHelper.ProjectItems, param, commandType: CommandType.StoredProcedure);
                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new GetAllProjectItemsMasterRespose();
            }
        }
        public async Task<GeneralModel> AddUpdateRecord(AddUpdateProjectItemsMasterRequest request)
        {
            GeneralModel modelResponse = new GeneralModel();
            try
            {
                var param = new DynamicParameters();
                if (request.Id == 0)
                {
                    param.Add("@Mode", "I");
                }
                else
                {
                    param.Add("@Mode", "U");
                    param.Add("@Id", request.Id);
                }
                param.Add("@PartNumber", request.PartNumber);
                param.Add("@Description", request.Description);
                param.Add("@ProjectId", request.ProjectId);
                param.Add("@DeliveryId", request.DeliveryId);
                param.Add("@Quantity", request.Quantity);
                param.Add("@Weight", request.Weight);
                param.Add("@ProjectLocation", request.ProjectLocation);
                param.Add("@BatchNumberProd", request.BatchNumberProd);
                param.Add("@ItemType", request.ItemType);
                param.Add("@SubItemType", request.SubItemType);
                param.Add("@EstimateLabourHours", request.EstimateLabourHours);
                param.Add("@ReferSheetNumber", request.ReferSheetNumber);
                param.Add("@CoatingType", request.CoatingType);
                param.Add("@ScheduleFabricateDate", request.ScheduleFabricateDate);
                param.Add("@ScheduleCoatingDate", request.ScheduleCoatingDate);
                param.Add("@ActualFabricateDate", request.ActualFabricateDate);
                param.Add("@ActualCoatingDate", request.ActualCoatingDate);
                param.Add("@FabricatorName", request.FabricatorName);
                param.Add("@CoatingVendorName", request.CoatingVendorName);
                //param.Add("@ItemStatus", request.ItemStatus);
                param.Add("@ItemQRCode", request.ItemQRCode);
                param.Add("@PainterName", request.PainterName);
                param.Add("@VendorComplete", request.VendorComplete);
                param.Add("@IsReadyForFabrication", request.IsReadyForFabrication);
                param.Add("@IsFabricated", request.IsFabricated);
                param.Add("@IsPainted", request.IsPainted);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.CreatedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.ProjectItems, param, commandType: CommandType.StoredProcedure);
                modelResponse.Status = result.Status;
                modelResponse.Message = Utility.GetResponseMessage(result.Status, request.Id, (int)Enums.ActionName.AddUpdate);
                modelResponse.Id = result.Id;
                return modelResponse;
            }
            catch (Exception ex)
            {
                return new GeneralModel
                {
                    Id = 0,
                    Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.RecordNotFound),
                    Status = false,
                    RecordId = 0
                };
            }
        }
        public async Task<GeneralModel> DeleteRecord(AddUpdateProjectItemsMasterRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "D");
                param.Add("@Id", request.Id);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.ModifiedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.ProjectItems, param, commandType: CommandType.StoredProcedure);
                response.Status = result.Status;
                response.Message = Utility.GetResponseMessage(result.Status, request.Id, (int)Enums.ActionName.Delete);
                response.Id = request.Id;
            }
            catch (Exception ex)
            {
                return new GeneralModel
                {
                    Id = request.Id,
                    Status = false,
                    Message = (EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.DeleteErrorMessage)) + " : " + ex.InnerException.Message.ToString()
                };
            }
            return response;
        }
    }
}
