

using InteriorDesignStudioApi.Models;

namespace InteriorDesignStudioAdminApi.Repository;

public interface IAdminRepository
{
    public Task ArchiveOrder(OrderModel order);
    
    public Task UnarchiveOrder(OrderModel order);
    
    public Task<List<OrderModel>> SearchOpenedOrder(string searchWord);
    
    public Task<List<OrderModel>> SearchArchivedOrders(string searchWord);
    
    public Task<OrderModel> EditOrder(OrderModel order);
    
    
}