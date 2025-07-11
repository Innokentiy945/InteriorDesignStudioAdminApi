using AdminInteriorDesignStudioApi.Models;
using AdminInteriorDesignStudioApi.Repository;
using Microsoft.AspNetCore.Mvc;
using UserInteriorDesignStudioApi.Models;


namespace AdminInteriorDesignStudioApi.Controller;
[ApiController]
[Route("api/admin")]
public class Controller : ControllerBase
{
    private readonly IAdminRepository _adminRepository;

    public Controller(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    [HttpGet]
    [Route("searchActive")]
    public async Task<List<OrderModel>> SearchOpenedOrder(string searchWord)
    {
        return await _adminRepository.SearchOpenedOrder(searchWord);
    }

    [HttpGet]
    [Route("searchArchive")]
    public async Task<List<ArchivedOrderModel>> SearchArchivedOrders(string searchWord)
    {
        return await _adminRepository.SearchArchivedOrders(searchWord);
    }

    [HttpPost]
    [Route("archive")]
    public async Task ArchiveOrder(OrderModel order)
    {
        await _adminRepository.ArchiveOrder(order);
    }
    
    [HttpPost]
    [Route("unarchive")]
    public async Task UnarchiveOrder(ArchivedOrderModel order)
    {
        await _adminRepository.UnarchiveOrder(order);
    }
    
    [HttpPost]
    [Route("editOrder")]
    public async Task<OrderModel> EditOrder(OrderModel order)
    {
        return await _adminRepository.EditOrder(order);
    }
}