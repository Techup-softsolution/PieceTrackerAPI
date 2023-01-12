using PieceTracker.Model;
using PieceTracker.Model.Response;

namespace PieceTracker.Service {
    public interface IProjectSummaryMasterService
    {
        Task<List<GetAllProjectSummaryResponse>> GetAll(string SearchString);
        Task<GetAllProjectSummaryResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateRecord(AddUpdateProjectSummaryRequest request);
        Task<GeneralModel> DeleteRecord(AddUpdateProjectSummaryRequest request);
        Task<GetProjectDataWithDeliveryDataResponse> GetAllProjectDataWithDeliveryListAsync(int id);
        Task<List<GetProjectDataWithDeliveryDataResponse>> GetAllProjectDetailsAsync(string SearchString);
    }
}
