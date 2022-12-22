using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public interface IUserService
    {
        Task<List<GetAllUserMasterResponse>> GetAll();
        Task<GetAllUserMasterResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateUser(AddUpdateUserMasterRequest request);

        Task<GeneralModel> DeleteRecord(AddUpdateUserMasterRequest request);
    }
}
