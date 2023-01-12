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

namespace PieceTracker.Data.DBRepository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private IConfiguration _config;
        private string APIBaseURL;
        public UserRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig)
        {
            _config = config;
            APIBaseURL = dataConfig.Value.FilePath;
        }

        //Method to get user list from SP
        public async Task<List<GetAllUserMasterResponse>> GetAll(string SearchString)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "S");
                if(!string.IsNullOrWhiteSpace(SearchString))
                    param.Add("@Search", SearchString);
                var data = await QueryAsync<GetAllUserMasterResponse>(SPHelper.UserDetail, param, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
            catch (Exception ex)
            {
                return new List<GetAllUserMasterResponse>();
            }
        }

        //Method to get user by Id from SP
        public async Task<GetAllUserMasterResponse> GetDetailById(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "SI");
                param.Add("@Id", id);
                var data = await QueryAsync<GetAllUserMasterResponse>(SPHelper.UserDetail, param, commandType: CommandType.StoredProcedure);
                return data.FirstOrDefault(); ;
            }
            catch (Exception ex)
            {
                return new GetAllUserMasterResponse();
            }
        }

        //Method to Insert or update user by Id from SP
        public async Task<GeneralModel> AddUpdateUser(AddUpdateUserMasterRequest request)
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
                param.Add("@FullName", request.FullName);
                param.Add("@Mobile", request.Mobile);
                param.Add("@Email", request.Email);
                param.Add("@Password", request.Password);

                param.Add("@RoleId", request.RoleId);

                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.CreatedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.UserDetail, param, commandType: CommandType.StoredProcedure);
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

        //Method to Delete user from SP
        public async Task<GeneralModel> DeleteRecord(AddUpdateUserMasterRequest request)
        {
            GeneralModel response = new GeneralModel();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Mode", "D");
                param.Add("@Id", request.Id);
                param.Add("@IsActive", request.IsActive);
                param.Add("@LoggedInUser", request.ModifiedBy);
                var result = await QueryFirstOrDefaultAsync<GeneralModel>(SPHelper.UserDetail, param, commandType: CommandType.StoredProcedure);
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
