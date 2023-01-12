using PieceTracker.Common;
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
    public class DeliveryMasterService : IDeliveryMasterService
    {
        private readonly IDeliveryMasterRepository _repository;
        public DeliveryMasterService(IDeliveryMasterRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetAllDeliveryMasterResponse>> GetAll(string SearchString = null)
        {
            return await _repository.GetAll(SearchString);
        }
        public async Task<GetAllDeliveryMasterResponse> GetDetailById(int id)
        {
            return await _repository.GetDetailById(id);
        }
        public async Task<GeneralModel> AddUpdateRecord(AddUpdateDeliveryMasterRequest request)
        {
            return await _repository.AddUpdateRecord(request);
        }
        public async Task<GeneralModel> DeleteRecord(AddUpdateDeliveryMasterRequest request)
        {
            return await _repository.DeleteRecord(request);
        }

        public async Task<List<GetAllDeliveryMasterResponse>> GetProjectDeliveriesByDateAndStatus(GetProjectDeliveriesByDateAndStatusRequest request) {
            return await _repository.GetProjectDeliveriesByDateAndStatus(request);
        }
    }
}
