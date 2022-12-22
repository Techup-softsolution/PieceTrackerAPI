using PieceTracker.Model;
using PieceTracker.Model.Request;
using PieceTracker.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Data.DBRepository
{
    public interface IDeliveryMasterRepository
    {
        Task<List<GetAllDeliveryMasterResponse>> GetAll();
        Task<GetAllDeliveryMasterResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateRecord(AddUpdateDeliveryMasterRequest request);
        Task<GeneralModel> DeleteRecord(AddUpdateDeliveryMasterRequest request);
    }
}
