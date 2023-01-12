using PieceTracker.Model;
using PieceTracker.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Data.DBRepository
{
    public interface IProjectSummaryMasterRepository
    {
        Task<List<GetAllProjectSummaryResponse>> GetAll(string SearchString);
        Task<GetAllProjectSummaryResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateRecord(AddUpdateProjectSummaryRequest request);
        Task<GeneralModel> DeleteRecord(AddUpdateProjectSummaryRequest request);
        Task<GetProjectDataWithDeliveryDataResponse> GetAllProjectDataWithDeliveryListAsync(int id);
        Task<List<GetProjectDataWithDeliveryDataResponse>> GetAllProjectDetailsAsync(string SearchString);
    }
}
