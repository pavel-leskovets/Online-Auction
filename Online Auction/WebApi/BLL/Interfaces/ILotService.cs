using BLL.ModelsDTO;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ILotService
    {
        IEnumerable<LotDTO> GetLots();

        LotDTO GetLot(int? id);

        LotDTO CreateLot(LotDTO item);

        void Update(LotDTO item);

        void Delete(int id);

        void Dispose();
    }
}
