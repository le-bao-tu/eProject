using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.StoneMst
{
    public interface IStoneMstHandller
    {
        Task<Response> GetAllStoneMst(StoneMstPageModel model);

        Task<Response> CreateStoneMst(StoneMstModel model);

        Task<Response> UpdateStoneMst(StoneMstModel model);

        Task<Response> DeleteStoneMstById(string stoId);

        Task<Response> GetStoneMstById(string stoId);

        Task<Response> GetStoneMstByName(string stoneName);
    }
}
