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
    public interface IProjectItemsMasterRepository 
    {
        Task<List<GetAllProjectItemsMasterRespose>> GetAll();
        Task<List<GetAllProjectItemsMasterRespose>> GetAllByProjectId(int id);
        Task<List<GetAllProjectItemsMasterRespose>> GetProjectItemsByDeliveryId(int deliveryId);
        Task<GetAllProjectItemsMasterRespose> GetDetailById(int id);
        Task<GeneralModel> AddUpdateRecord(AddUpdateProjectItemsMasterRequest request);
        Task<GeneralModel> DeleteRecord(AddUpdateProjectItemsMasterRequest request);
    }
}
