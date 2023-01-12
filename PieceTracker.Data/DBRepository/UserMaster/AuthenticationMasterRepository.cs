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
using System.Web.Helpers;

namespace PieceTracker.Data.DBRepository {
    public class AuthenticationMasterRepository : BaseRepository, IAuthenticationMasterRepository {
        private IConfiguration _config;
        private string APIBaseURL;
        public AuthenticationMasterRepository(IConfiguration config, IOptions<DataConfig> dataConfig) : base(dataConfig) {
            _config = config;
            APIBaseURL = dataConfig.Value.FilePath;
        }
        public async Task<AuthenticationDTO> GetUserAuthenticationDetail(AuthenticationMasterRequest request) {
            try {
                var param = new DynamicParameters();
                param.Add("@Mode", "AUTH");
                param.Add("@Email", request.UserEmail);
                param.Add("@Password", request.UserPassword);
                var data = await QueryAsync<AuthenticationDTO>(SPHelper.Authentication, param, commandType: CommandType.StoredProcedure);
                return data.ToList().FirstOrDefault();
            }
            catch (Exception ex) {
                return new AuthenticationDTO();
            }
        }
        //public async Task<GeneralModel> VerifyUserMobile(VerifyUserRequest request)
        //{
        //    GeneralModel modelResponse = new GeneralModel();
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@Mode", "AUTH");
        //        param.Add("@MobileNo", request.MobileNo);
        //        var result = await QueryFirstOrDefaultAsync<GeneralModel>("SPU_Authentication", param, commandType: CommandType.StoredProcedure);
        //        modelResponse.Status = result.Status;
        //        modelResponse.Message = result.Message;
        //        modelResponse.Id = result.Id;
        //        modelResponse.OTP = result.OTP;
        //        return modelResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new GeneralModel
        //        {
        //            Id = 0,
        //            Message = EnumUtility.DisplayName(MessageEnums.GeneralActionMessage.RecordNotFound) + " : " + ex.InnerException.Message.ToString(),
        //            Status = false,
        //            OTP = ""
        //        };
        //    }
        //}
        public async Task<AuthenticationDTO> GetLoggedInUserDetail(int id) {
            try {
                var param = new DynamicParameters();
                param.Add("@Mode", "UP");
                param.Add("@UserId", id);
                var data = await QueryAsync<AuthenticationDTO>(SPHelper.Authentication, param, commandType: CommandType.StoredProcedure);
                return data.ToList().FirstOrDefault();
            }
            catch (Exception ex) {
                return new AuthenticationDTO();
            }
        }
        public async Task<bool> CheckEmailExists(string email) {
            try {
                var param = new DynamicParameters();
                param.Add("@Mode", "CHECK");
                param.Add("@Email", email);
                var data = await QueryAsync<bool>(SPHelper.Authentication, param, commandType: CommandType.StoredProcedure);
                return data.ToList().FirstOrDefault();
            }
            catch (Exception ex) {
                return false;
            }
        }

        public async Task<bool> ChangeUserPassword(AuthenticationMasterRequest model) {
            try {
                var param = new DynamicParameters();
                param.Add("@Mode", "UPDT");
                param.Add("@Email", model.UserEmail);
                param.Add("@Password", model.UserPassword);
                var data = await QueryAsync<bool>(SPHelper.Authentication, param, commandType: CommandType.StoredProcedure);
                return data.ToList().FirstOrDefault();
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
