using PieceTracker.Data.DBRepository;
using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public class RoleMasterService: IRoleMasterService
    {
        private readonly IRoleMasterRepository _repository;
        public RoleMasterService(IRoleMasterRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetAllRoleMasterResponse>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<GetAllRoleMasterResponse> GetDetailById(int id)
        {
            return await _repository.GetDetailById(id);
        }
        public async Task<GeneralModel> AddUpdateRecord(AddUpdateRoleMasterRequest request)
        {
            return await _repository.AddUpdateRecord(request);
        }
        public async Task<GeneralModel> DeleteRecord(AddUpdateRoleMasterRequest request)
        {
            return await _repository.DeleteRecord(request);
        }
    }
}
