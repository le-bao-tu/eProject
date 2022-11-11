using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Goldk
{
    public interface IGoldkHandller
    {
        Task<Response> GetAllGoldk(GoldkPageModel model);

        Task<Response> CreateGoldk(GoldkModel model);

        Task<Response> UpdateGoldk(GoldkModel model);

        Task<Response> DeleteGoldk(Guid goldkId);

        Task<Response> GetGoldkById(Guid goldkId);

        Task<Response> GetGoldkByName(string goldkName);

    }
}
