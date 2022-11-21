using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.ItemDimMst
{
    public interface IDimMstHandller
    {
        Task<Response> GetAllDimMst(DimMstPageModel model);

        Task<Response> CreateDimMst(DimMstModel model);

        Task<Response> UpdateDimMst(DimMstModel model);

        Task<Response> DeleteDimMst(string dimId);

        Task<Response> GetDimMstById(string dimId);

        Task<Response> GetDimMstByName(string dimName);
    }
}
