using System;
using System.Collections.Generic;
using System.Text;

namespace PieceTracker.Model.Response {
    public class GetProjectDataWithDeliveryDataResponse : ResponseBaseModel {
        public GetAllProjectSummaryResponse Project { get; set; }
        public List<ProjectDeliveryData> DeliveryDatas { get; set; }
        public List<GetAllProjectItemsMasterRespose> NoDeliveryData { get; set; }
        public List<GetAllProjectItemsMasterRespose> ProjectItems { get; set; }

        public GetProjectDataWithDeliveryDataResponse() {
            this.Project = new GetAllProjectSummaryResponse();
            this.DeliveryDatas = new List<ProjectDeliveryData>();
            this.NoDeliveryData = new List<GetAllProjectItemsMasterRespose>();
            this.ProjectItems = new List<GetAllProjectItemsMasterRespose>();
        }
    }

    public class ProjectDeliveryData {
        public GetAllDeliveryMasterResponse DeliveryData { get; set; }
       public List<GetAllProjectItemsMasterRespose> ProjectItems { get; set; }

        public ProjectDeliveryData() {
            this.DeliveryData = new GetAllDeliveryMasterResponse();
            this.ProjectItems = new List<GetAllProjectItemsMasterRespose>();
        }
    }
}
