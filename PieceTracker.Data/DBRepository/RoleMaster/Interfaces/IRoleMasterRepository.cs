using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Data.DBRepository
{
    public interface IRoleMasterRepository
    {
        Task<List<GetAllRoleMasterResponse>> GetAll();
        Task<GetAllRoleMasterResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateRecord(AddUpdateRoleMasterRequest request);
        Task<GeneralModel> DeleteRecord(AddUpdateRoleMasterRequest request);
    }
}
