using PieceTracker.Service;
using System;
using System.Collections.Generic;

namespace PieceTracker.Service
{
    public static class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dic = new Dictionary<Type, Type>
            {
                { typeof(IRoleMasterService), typeof(RoleMasterService) },
                { typeof(IAuthenticationMasterService), typeof(AuthenticationMasterService) },
                { typeof(IUserService), typeof(UserService) },
                { typeof(IProjectItemStatusService), typeof(ProjectItemStatusService)},
                { typeof(IProjectSummaryMasterService), typeof(ProjectsummaryMasterService)},
                { typeof(IDeliveryMasterService), typeof(DeliveryMasterService)},
                { typeof(IProjectItemsMasterService), typeof(ProjectItemsMasterService)},
                { typeof(IShopService), typeof(ShopService)}
            };
            return dic;
        }
    }
}
