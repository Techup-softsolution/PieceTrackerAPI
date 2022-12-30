using Dapper;
using PieceTracker.Common;
using PieceTracker.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Data;
using PieceTracker.Model.Response;
using PieceTracker.Model.Request;

namespace PieceTracker.Data.DBRepository
{
    public class DeliveryMasterRepository : BaseRepository, IDeliveryMasterRepository
    {
        private IConfiguration _config;
        private string APIBaseURL;
        public DeliveryMasterRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
            APIBaseURL = dataConfig.Value.FilePath;
        }

        public async Task<List<GetAllDeliveryMasterResponse>> GetAll()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "S");
                var data = await QueryAsync<GetAllDeliveryMasterResponse>(SPHelper.Delivery, param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                return new List<GetAllDeliveryMasterResponse>();
            }
        }
        public async Task<GetAllDeliveryMasterResponse> GetDetailById(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "SI");
                param.Add("@Id", id);
                var data = await QueryAsync<GetAllDeliveryMasterResponse>(SPHelper.Delivery, param, commandType: CommandType.StoredProcedure);
                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new GetAllDeliveryMasterResponse();
            }
        }
        public async Task<GeneralModel> AddUpdateRecord(AddUpdateDeliveryMasterRequest request)
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
                param.Add("@ProjectId", request.ProjectId);
                param.Add("@Vehicle", request.Vehicle);
                param.Add("@Instructions", request.Instructions);
                param.Add("@IsProjectAddress", request.IsProjectAddress);
                param.Add("@DeliveryAddress", request.DeliveryAddress);
                param.Add("@Status", request.Status);
                param.Add("@TotalWeight", request.TotalWeight);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.CreatedBy);
                param.Add("@DeliveryName", request.DeliveryName);
                param.Add("@DeliveryDate", request.DeliveryDate);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.Delivery, param, commandType: CommandType.StoredProcedure);
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
        public async Task<GeneralModel> DeleteRecord(AddUpdateDeliveryMasterRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "D");
                param.Add("@Id", request.Id);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.ModifiedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.Delivery, param, commandType: CommandType.StoredProcedure);
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