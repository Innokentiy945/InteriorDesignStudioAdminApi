using InteriorDesignStudioAdminApi.Repository;
using InteriorDesignStudioApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace InteriorDesignStudioAdminApi.Controller;
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
    public async Task<List<OrderModel>> SearchOrder(string searchWord)
    {
        return await _adminRepository.SearchOpenedOrder(searchWord);
    }

    [HttpGet]
    [Route("searchArchive")]
    public async Task<List<OrderModel>> SearchArchivedOrders(string searchWord)
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
    public async Task UnarchiveOrder(OrderModel order)
    {
        await _adminRepository.UnarchiveOrder(order);
    }
    
    [HttpPost]
    [Route("editOrder")]
    public async Task<InteriorDesignStudioApi.Models.OrderModel> EditOrder(OrderModel order)
    {
        return await _adminRepository.EditOrder(order);
    }
}