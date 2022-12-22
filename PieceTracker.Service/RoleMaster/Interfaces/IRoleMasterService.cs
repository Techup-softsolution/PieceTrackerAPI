using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public interface IRoleMasterService
    {
        Task<List<GetAllRoleMasterResponse>> GetAll();
        Task<GetAllRoleMasterResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateRecord(AddUpdateRoleMasterRequest request);
        Task<GeneralModel> DeleteRecord(AddUpdateRoleMasterRequest request);
    }
}
