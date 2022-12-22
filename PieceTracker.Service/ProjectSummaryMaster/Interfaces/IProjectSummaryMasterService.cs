﻿using PieceTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieceTracker.Service
{
    public interface IProjectSummaryMasterService
    {
        Task<List<GetAllProjectSummaryResponse>> GetAll();
        Task<GetAllProjectSummaryResponse> GetDetailById(int id);
        Task<GeneralModel> AddUpdateRecord(AddUpdateProjectSummaryRequest request);
        Task<GeneralModel> DeleteRecord(AddUpdateProjectSummaryRequest request);
    }
}
