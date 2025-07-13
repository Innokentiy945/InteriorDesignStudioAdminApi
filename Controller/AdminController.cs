using AdminInteriorDesignStudioApi.Models;
using AdminInteriorDesignStudioApi.Repository;
using Microsoft.AspNetCore.Mvc;


namespace AdminInteriorDesignStudioApi.Controller;
[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;

    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    [HttpGet]
    [Route("allActiveOrders")]
    public async Task<List<ActiveOrdersModel>> GetAllCustomerOrders()
    {
        return await _adminRepository.GetAllCustomerOrders();
    }

    [HttpGet]
    [Route("allArchivedOrders")]
    public async Task<List<ArchivedOrdersModel>> GetAllArchivedOrders()
    {
        return await _adminRepository.GetAllArchivedOrders();
    }

    [HttpGet]
    [Route("searchActive")]
    public async Task<List<ActiveOrdersModel>> SearchOpenedOrder(string searchWord)
    {
        return await _adminRepository.SearchActiveOrder(searchWord);
    }

    [HttpGet]
    [Route("searchArchive")]
    public async Task<List<ArchivedOrdersModel>> SearchArchivedOrders(string searchWord)
    {
        return await _adminRepository.SearchArchivedOrders(searchWord);
    }

    [HttpPost]
    [Route("archive")]
    public async Task ArchiveOrder(ActiveOrdersModel activeOrder)
    {
        await _adminRepository.ArchiveOrder(activeOrder);
    }
    
    [HttpPost]
    [Route("unarchive")]
    public async Task UnarchiveOrder(ArchivedOrdersModel archivedOrdersModel)
    {
        await _adminRepository.UnarchiveOrder(archivedOrdersModel);
    }
    
    [HttpPost]
    [Route("editOrder")]
    public async Task<ActiveOrdersModel> EditOrder(string customerName, string customerEmail, int customerPhone, string orderDetail)
    {
        return await _adminRepository.EditOrder(customerName, customerEmail, customerPhone, orderDetail);
    }

    [HttpDelete]
    [Route("deleteOrder")]
    public async Task DeleteOrder(Guid orderId)
    {
        await _adminRepository.DeleteOrder(orderId);
    }

    [HttpPost]
    [Route("sendOrder")]
    public async Task SendOrder(string customerName, string customerEmail, int customerPhone, string orderDetails)
    {
        await _adminRepository.SendOrder(customerName, customerEmail, customerPhone, orderDetails);
    }
}