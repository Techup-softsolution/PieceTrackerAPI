using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public interface IAuthenticationMasterService
    {
        Task<AuthenticationDTO> GetUserAuthenticationDetail(AuthenticationMasterRequest request);
        //Task<GeneralModel> VerifyUserMobile(VerifyUserRequest request);
        Task<AuthenticationDTO> GetLoggedInUserDetail(int id);
    }
}
