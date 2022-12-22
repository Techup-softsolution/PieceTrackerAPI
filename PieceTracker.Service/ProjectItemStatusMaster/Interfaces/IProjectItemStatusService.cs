using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public interface IProjectItemStatusService
    {
        Task<List<GetAllProjectItemStatusResponse>> GetAll();
        Task<GetAllProjectItemStatusResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateRecord(AddUpdateProjectItemStatusRequest request);
        Task<GeneralModel> DeleteRecord(AddUpdateProjectItemStatusRequest request);
    }
}
