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
    /// <summary>
    /// Interface for lots service.
    /// Contains methods for managing lots.
    /// </summary>
    public interface ILotService : IDisposable
    {
        /// <summary>
        /// Method for getting all lots.
        /// </summary>
        /// <returns>Collection of lots DTOs.</returns>
        IEnumerable<LotDTO> GetAllLots();

        /// <summary>
        /// Method for getting lot by ID.
        /// </summary>
        /// <param name="lotId">The lot ID.</param>
        /// <returns>Lot DTO.</returns>
        LotDTO GetLot(int lotId);

        /// <summary>
        /// Method for creating lot.
        /// </summary>
        /// <param name="lot">The lot DTO.</param>
        /// <returns>Created lot DTO.</returns>
        LotDTO CreateLot(LotDTO lot);

        /// <summary>
        /// Method for updating lot.
        /// </summary>
        /// <param name="lot">The lot DTO.</param>
        void UpdateLot(LotDTO lot);

        /// <summary>
        /// Method for deleting lot.
        /// </summary>
        /// <param name="lotId">The lot ID.</param>
        /// <returns>The Task.</returns>
        void DeleteLot(int lotId);

    }
}
