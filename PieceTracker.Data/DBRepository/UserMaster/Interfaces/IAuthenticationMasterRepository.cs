using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Data.DBRepository
{
    public interface IAuthenticationMasterRepository
    {
        Task<AuthenticationDTO> GetUserAuthenticationDetail(AuthenticationMasterRequest request);
        //Task<GeneralModel> VerifyUserMobile(VerifyUserRequest request);
        Task<AuthenticationDTO> GetLoggedInUserDetail(int id);
        Task<bool> CheckEmailExists(string email);
        Task<bool> ChangeUserPassword(AuthenticationMasterRequest model);
    }
}
