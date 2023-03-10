using PieceTracker.Data.DBRepository;
using PieceTracker.Model;
using PieceTracker.Model.Request;
using PieceTracker.Model.Response;
using PieceTracker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public class ShopService : IShopService
    {
        private readonly IShopMasterRepository _repository;

        public ShopService(IShopMasterRepository repository)
        {
            _repository = repository;
        }

        ////Get shop list from repository
        //public async Task<List<GetAllShopMasterResponse>> GetAll()
        //{
        //    return await _repository.GetAll();
        //}

     
        public async Task<List<GetProjectDataWithDeliveryDataResponse>> GetDetailByUserId(int userId, string SearchString) {
            return await _repository.GetDetailByUserId(userId,SearchString);
        }

        //Insert or update shop by Id from repository
        public async Task<GeneralModel> AddUpdateShop(AddUpdateShopMasterRequest request)
        {
            return await _repository.AddUpdateShop(request);
        }

        ////Delete shop from repository
        //public async Task<GeneralModel> DeleteRecord(AddUpdateShopMasterRequest request)
        //{
        //    return await _repository.DeleteRecord(request);
        //}
    }
}
