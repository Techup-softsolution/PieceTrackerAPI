using PieceTracker.Data.DBRepository;
using PieceTracker.Model;
using PieceTracker.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public class ProjectsummaryMasterService : IProjectSummaryMasterService
    {
        private readonly IProjectSummaryMasterRepository _repository;
        public ProjectsummaryMasterService(IProjectSummaryMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllProjectSummaryResponse>> GetAll(string SearchString)
        {
            return await _repository.GetAll(SearchString);
        }
        public async Task<GetAllProjectSummaryResponse> GetDetailById(int id)
        {
            return await _repository.GetDetailById(id);
        }
        public async Task<GeneralModel> AddUpdateRecord(AddUpdateProjectSummaryRequest request)
        {
            return await _repository.AddUpdateRecord(request);
        }
        public async Task<GeneralModel> DeleteRecord(AddUpdateProjectSummaryRequest request)
        {
            return await _repository.DeleteRecord(request);
        }
        public async Task<GetProjectDataWithDeliveryDataResponse> GetAllProjectDataWithDeliveryListAsync(int id)
        {
            return await _repository.GetAllProjectDataWithDeliveryListAsync(id);
        }

        public async Task<List<GetProjectDataWithDeliveryDataResponse>> GetAllProjectDetailsAsync(string SearchString) {
            return await _repository.GetAllProjectDetailsAsync(SearchString);
        }
    }
}
