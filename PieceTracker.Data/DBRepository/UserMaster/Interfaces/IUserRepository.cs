using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Data.DBRepository
{
    public interface IUserRepository
    {
        Task<List<GetAllUserMasterResponse>> GetAll(string SearchString);
        Task<GetAllUserMasterResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateUser(AddUpdateUserMasterRequest request);

        Task<GeneralModel> DeleteRecord(AddUpdateUserMasterRequest request);
    }
}
