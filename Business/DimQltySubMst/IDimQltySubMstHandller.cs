using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.DimQltySubMst
{
     public  interface IDimQltySubMstHandller
    {
        Task<Response> GetAllDimQltySubMst(DimQltySubMstPageModel model);

        Task<Response> CreateDimQltySubMst(DimQltySubMstModel model);

        Task<Response> UpdateDimQltySubMst(DimQltySubMstModel model);

        Task<Response> DeleteDimQltySubMst(Guid dimId);

        Task<Response> GetDimQltySubMstById(Guid dimId);

        Task<Response> GetDimQltySubMstByName(string dimQlty);
    }
}
