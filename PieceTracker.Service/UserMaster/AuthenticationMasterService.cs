using PieceTracker.Data.DBRepository;
using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public class AuthenticationMasterService: IAuthenticationMasterService
    {
        private readonly IAuthenticationMasterRepository _repository;
        public AuthenticationMasterService(IAuthenticationMasterRepository repository)
        {
            _repository = repository;
        }
        public async Task<AuthenticationDTO> GetUserAuthenticationDetail(AuthenticationMasterRequest request)
        {
            return await _repository.GetUserAuthenticationDetail(request);
        }
     
        //public async Task<GeneralModel> VerifyUserMobile(VerifyUserRequest request)
        //{
        //    return await _repository.VerifyUserMobile(request);
        //}
        public async Task<AuthenticationDTO> GetLoggedInUserDetail(int id)
        {
            return await _repository.GetLoggedInUserDetail(id);
        }
        public async Task<bool> CheckEmailExists(string email) {
            return await _repository.CheckEmailExists(email);
        }
        public async Task<bool> ChangeUserPassword(AuthenticationMasterRequest model) {
            return await _repository.ChangeUserPassword(model);
        }
    }
}
