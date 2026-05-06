using cityshop_api.DTO;
using cityshop_api.Interfaces;
using cityshop_api.Model;
using Microsoft.EntityFrameworkCore;

namespace cityshop_api.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDBContext _context;
        public BillRepository(ApplicationDBContext context)
        {
            _context = context;
        }

    }
}