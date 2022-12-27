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
    public class ShopMasterRepository : BaseRepository, IShopMasterRepository
    {
        private IConfiguration _config;
        private string APIBaseURL;
        public ShopMasterRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
            APIBaseURL = dataConfig.Value.FilePath;
        }

        //Method to get shop list from SP
        public async Task<List<GetAllShopMasterResponse>> GetAll()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "S");
                var data = await QueryAsync<GetAllShopMasterResponse>(SPHelper.ShopDetails, param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                return new List<GetAllShopMasterResponse>();
            }
        }

        //Method to get shop by Id from SP
        public async Task<GetAllShopMasterResponse> GetDetailById(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "SI");
                param.Add("@Id", id);
                var data = await QueryAsync<GetAllShopMasterResponse>(SPHelper.ShopDetails, param, commandType: CommandType.StoredProcedure);
                return data.FirstOrDefault(); ;
            }
            catch (Exception ex)
            {
                return new GetAllShopMasterResponse();
            }
        }

        //Method to Insert or update shop by Id from SP
        public async Task<GeneralModel> AddUpdateShop(AddUpdateShopMasterRequest request)
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
                param.Add("@ShopName", request.ShopName);
                param.Add("@PartNumber", request.PartNumber);
                param.Add("@ProjectId", request.ProjectId);
                param.Add("@ProjectLocation", request.ProjectLocation);

                param.Add("@Quantity", request.Quantity);
                param.Add("@Weight", request.Weight);
                param.Add("@SubItem", request.SubItem);
                param.Add("@StatusId", request.StatusId);

                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.CreatedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.ShopDetails, param, commandType: CommandType.StoredProcedure);
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

        //Method to Delete shop from SP
        public async Task<GeneralModel> DeleteRecord(AddUpdateShopMasterRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "D");
                param.Add("@Id", request.Id);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.ModifiedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.ShopDetails, param, commandType: CommandType.StoredProcedure);
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
