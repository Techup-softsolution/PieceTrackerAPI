using PieceTracker.Data.DBRepository;
using System;
using System.Collections.Generic;

namespace PieceTracker.Data
{
    public static class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>
            {
                { typeof(IRoleMasterRepository), typeof(RoleMasterRepository) },
                { typeof(IAuthenticationMasterRepository), typeof(AuthenticationMasterRepository) },
                { typeof(IUserRepository), typeof(UserRepository) },
                { typeof(IProjectItemStatusMasterRepository), typeof(ProjectItemStatusMasterRepository) },
                { typeof(IProjectSummaryMasterRepository), typeof(ProjectSummaryMasterRepository) },
                { typeof(IDeliveryMasterRepository), typeof(DeliveryMasterRepository) },
                { typeof(IProjectItemsMasterRepository), typeof(ProjectItemsMasterRepository) },
                { typeof(IShopMasterRepository), typeof(ShopMasterRepository) }
            };
            return dic;
        }
    }
}
