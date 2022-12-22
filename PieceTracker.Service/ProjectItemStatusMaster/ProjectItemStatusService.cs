using PieceTracker.Data.DBRepository;
using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service

{
    public class ProjectItemStatusService : IProjectItemStatusService
{
    private readonly IProjectItemStatusMasterRepository _repository;
    public ProjectItemStatusService(IProjectItemStatusMasterRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<GetAllProjectItemStatusResponse>> GetAll()
    {
        return await _repository.GetAll();
    }
    public async Task<GetAllProjectItemStatusResponse> GetDetailById(int id)
    {
        return await _repository.GetDetailById(id);
    }
    public async Task<GeneralModel> AddUpdateRecord(AddUpdateProjectItemStatusRequest request)
    {
        return await _repository.AddUpdateRecord(request);
    }
    public async Task<GeneralModel> DeleteRecord(AddUpdateProjectItemStatusRequest request)
    {
        return await _repository.DeleteRecord(request);
    }
}
}
