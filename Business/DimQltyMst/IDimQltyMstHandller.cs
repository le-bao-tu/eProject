using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.DimQltyMst
{
   public interface IDimQltyMstHandller
    {
        Task<Response> GetAllDimQltyMst(DimQltyMstPageModel model);

        Task<Response> CreateDimQltyMst(DimQltyMstModel model);

        Task<Response> UpdateDimQltyMst(DimQltyMstModel model);

        Task<Response> DeleteDimQltyMst(Guid dimId);

        Task<Response> GetDimQltyMstById(Guid dimId);

        Task<Response> GetDimQltyMstByName(string dimQlty);
    }
}
