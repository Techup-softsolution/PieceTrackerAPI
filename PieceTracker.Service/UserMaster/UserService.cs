using PieceTracker.Data.DBRepository;
using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PieceTracker.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;
       
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        //Get user list from repository
        public async Task<List<GetAllUserMasterResponse>> GetAll(string SearchString)
        {
            return await _repository.GetAll(SearchString);
        }

        //Get get user by Id from repository
        public async Task<GetAllUserMasterResponse> GetDetailById(int id)
        {
            return await _repository.GetDetailById(id);
        }

        //Insert or update user by Id from repository
        public async Task<GeneralModel> AddUpdateUser(AddUpdateUserMasterRequest request)
        {
            return await _repository.AddUpdateUser(request);
        }

        //Delete user from repository
        public async Task<GeneralModel> DeleteRecord(AddUpdateUserMasterRequest request)
        {
            return await _repository.DeleteRecord(request);
        }

    }
}
