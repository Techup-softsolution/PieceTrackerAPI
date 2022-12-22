using PieceTracker.Data.DBRepository;
using PieceTracker.Model;
using PieceTracker.Model.Request;
using PieceTracker.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public class ProjectItemsMasterService : IProjectItemsMasterService
    {
        private readonly IProjectItemsMasterRepository _repository;
        public ProjectItemsMasterService(IProjectItemsMasterRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetAllProjectItemsMasterRespose>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<GetAllProjectItemsMasterRespose> GetDetailById(int id)
        {
            return await _repository.GetDetailById(id);
        }
        public async Task<GeneralModel> AddUpdateRecord(AddUpdateProjectItemsMasterRequest request)
        {
            return await _repository.AddUpdateRecord(request);
        }
        public async Task<GeneralModel> DeleteRecord(AddUpdateProjectItemsMasterRequest request)
        {
            return await _repository.DeleteRecord(request);
        }

    }
}
