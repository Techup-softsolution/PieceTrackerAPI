using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PieceTracker.Common;
using PieceTracker.Model;
using System.Data;

namespace PieceTracker.Data.DBRepository
{
    public class ProjectItemStatusMasterRepository : BaseRepository, IProjectItemStatusMasterRepository
    {
        private IConfiguration _config;
        private string APIBaseURL;
        public ProjectItemStatusMasterRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
            APIBaseURL = dataConfig.Value.FilePath;
        }

        public async Task<List<GetAllProjectItemStatusResponse>> GetAll()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "S");
                var data = await QueryAsync<GetAllProjectItemStatusResponse>(SPHelper.ProjectItemStatus, param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                return new List<GetAllProjectItemStatusResponse>();
            }
        }
        public async Task<GetAllProjectItemStatusResponse> GetDetailById(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "SI");
                param.Add("@Id", id);
                var data = await QueryAsync<GetAllProjectItemStatusResponse>(SPHelper.ProjectItemStatus, param, commandType: CommandType.StoredProcedure);
                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new GetAllProjectItemStatusResponse();
            }
        }
        public async Task<GeneralModel> AddUpdateRecord(AddUpdateProjectItemStatusRequest request)
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
                param.Add("@StatusName", request.StatusName);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.CreatedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.ProjectItemStatus, param, commandType: CommandType.StoredProcedure);
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
        public async Task<GeneralModel> DeleteRecord(AddUpdateProjectItemStatusRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "D");
                param.Add("@Id", request.Id);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.ModifiedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.ProjectItemStatus, param, commandType: CommandType.StoredProcedure);
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

