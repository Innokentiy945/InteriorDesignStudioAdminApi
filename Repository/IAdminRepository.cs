using AdminInteriorDesignStudioApi.Models;

namespace AdminInteriorDesignStudioApi.Repository;

public interface IAdminRepository
{
    public Task ArchiveOrder(ActiveOrdersModel activeOrder);
    
    public Task UnarchiveOrder(ArchivedOrdersModel archivedOrder);
    
    public Task<List<ActiveOrdersModel>> SearchActiveOrder(string searchWord);
    
    public Task<List<ArchivedOrdersModel>> SearchArchivedOrders(string searchWord);
    
    public Task<ActiveOrdersModel> EditOrder(string customerName, string customerEmail, int customerPhone, string orderDetails);
    
    public Task<ActiveOrdersModel> SendOrder(string customerName, string customerEmail, int customerPhone, string orderDetails);
    
    public Task DeleteOrder(Guid orderId);
    
    public Task<List<ActiveOrdersModel>> GetAllCustomerOrders();
    
    public Task<List<ArchivedOrdersModel>> GetAllArchivedOrders();
}