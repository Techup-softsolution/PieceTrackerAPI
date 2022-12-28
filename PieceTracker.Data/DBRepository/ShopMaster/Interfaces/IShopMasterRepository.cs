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
    public interface IShopMasterRepository
    {
        Task<List<GetAllShopMasterResponse>> GetAll();
        Task<GetAllShopMasterResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateShop(AddUpdateShopMasterRequest request);

        Task<GeneralModel> DeleteRecord(AddUpdateShopMasterRequest request);
    }
}
