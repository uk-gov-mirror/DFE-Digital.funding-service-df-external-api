using DigitalForms.BL.Models;
using DigitalForms.BL.Serialized.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalForms.BL.Interfaces
{
    public interface IDraftResponseRepository : IGenericRepository<DigitalForms.BL.Models.DraftResponse>
    {
        Task<List<BL.Models.DraftResponse>> GetDraftResponse(string id);

    }
}
