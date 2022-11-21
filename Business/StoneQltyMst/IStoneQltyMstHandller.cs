using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.StoneQltyMst
{
    public interface IStoneQltyMstHandller
    {
        Task<Response> GetAllStoneQlty(StoneQltyMsPageModel model);

        Task<Response> CreateStoneQlty(StoneQltyMstModel model);

        Task<Response> UpdateStoneQlty(StoneQltyMstModel model);

        Task<Response> DeleteStoneQlty(Guid stoneId);

        Task<Response> GetStoneQltyById(Guid stoneId);

        Task<Response> GetStoneQltyByName(int stoneQlty);

    }
}
