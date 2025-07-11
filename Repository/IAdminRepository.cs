using AdminInteriorDesignStudioApi.Models;
using UserInteriorDesignStudioApi.Models;

namespace AdminInteriorDesignStudioApi.Repository;

public interface IAdminRepository
{
    public Task ArchiveOrder(OrderModel order);
    
    public Task UnarchiveOrder(ArchivedOrderModel archivedOrder);
    
    public Task<List<OrderModel>> SearchOpenedOrder(string searchWord);
    
    public Task<List<ArchivedOrderModel>> SearchArchivedOrders(string searchWord);
    
    public Task<OrderModel> EditOrder(OrderModel order);
    
    
}