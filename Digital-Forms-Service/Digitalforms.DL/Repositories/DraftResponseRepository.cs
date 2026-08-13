using DigitalForms.BL.Models;
using DigitalForms.DL.Data;
using Microsoft.EntityFrameworkCore;
using DigitalForms.BL.Data;
using DigitalForms.BL.Interfaces;

namespace DigitalForms.DL.Repositories
{
    public class DraftResponseRepository : GenericRepository<DigitalForms.BL.Models.DraftResponse>, IDraftResponseRepository
    {
        public DraftResponseRepository(DFSqlContext context, s255d01dbDfSharedContext readcontext) : base(context, readcontext)
        {
            context.Database.SetCommandTimeout(180);
        }
        public async Task<List<BL.Models.DraftResponse>> GetDraftResponse(string id)
        {
            try
            {               
                var data = await _context.DraftResponse.AsNoTracking().Where(g => g.Id == id).ToListAsync();              
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
