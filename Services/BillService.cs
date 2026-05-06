using cityshop_api.DTO;
using cityshop_api.Interfaces;

namespace cityshop_api.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

    }
}