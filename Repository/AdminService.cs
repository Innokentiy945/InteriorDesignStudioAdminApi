using AdminInteriorDesignStudioApi.Context;
using AdminInteriorDesignStudioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminInteriorDesignStudioApi.Repository;

public class AdminService : IAdminRepository
{
    private AdminStudioContext _adminAdminStudioContext;
    private ILogger<AdminService> _logger;

    public AdminService(AdminStudioContext adminAdminStudioContext, ILogger<AdminService> logger)
    {
        _adminAdminStudioContext = adminAdminStudioContext;
        _logger = logger;
    }
    
    public async Task ArchiveOrder(ActiveOrdersModel activeOrder)
    {
        try
        {
            var dataToMove = _adminAdminStudioContext.Orders
                .Where(s => s.OrderId == activeOrder.OrderId).ToList();
            
            foreach (var item in dataToMove)
            {
                var transferedItem = new ArchivedOrdersModel
                {
                    OrderId = activeOrder.OrderId,
                    OrderDate = activeOrder.OrderDate,
                    OrderDetails = activeOrder.OrderDetails,
                    CustomerName = activeOrder.CustomerName,
                    CustomerPhone = activeOrder.CustomerPhone,
                    CustomerEmail = activeOrder.CustomerEmail,
                };
        
                _adminAdminStudioContext.FinishedOrders.Add(transferedItem);
            }

            _adminAdminStudioContext.Orders.RemoveRange(dataToMove);
            
            await _adminAdminStudioContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task UnarchiveOrder(ArchivedOrdersModel archivedOrder)
    {
        try
        {
            var dataToMove = _adminAdminStudioContext.FinishedOrders
                .Where(s => s.OrderId == archivedOrder.OrderId).ToList();
            
            foreach (var item in dataToMove)
            {
                var transferedItem = new ActiveOrdersModel
                {
                    OrderId = archivedOrder.OrderId,
                    OrderDate = archivedOrder.OrderDate,
                    OrderDetails = archivedOrder.OrderDetails,
                    CustomerName = archivedOrder.CustomerName,
                    CustomerPhone = archivedOrder.CustomerPhone,
                    CustomerEmail = archivedOrder.CustomerEmail
                };
        
                _adminAdminStudioContext.Orders.Add(transferedItem);
            }

            _adminAdminStudioContext.FinishedOrders.RemoveRange(dataToMove);
            
            await _adminAdminStudioContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<List<ActiveOrdersModel>> SearchActiveOrder(string searchWord)
    {
        IQueryable<ActiveOrdersModel> query = _adminAdminStudioContext.Orders;
            
        try
        {
            if (!string.IsNullOrEmpty(searchWord))
            {
                _logger.LogInformation("Searching...");
                query = query.Where(x => x.OrderDate.Equals(searchWord)
                                         || x.OrderId.Equals(searchWord)
                                         || x.OrderDetails.Contains(searchWord)
                                         || x.CustomerEmail.Contains(searchWord)
                                         || x.CustomerName.Contains(searchWord)
                                         || x.CustomerPhone.Equals(searchWord));}

            _logger.LogInformation("Searching finished!");
            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<List<ArchivedOrdersModel>> SearchArchivedOrders(string searchWord)
    {
        IQueryable<ArchivedOrdersModel> query = _adminAdminStudioContext.FinishedOrders;
            
        try
        {
            if (!string.IsNullOrEmpty(searchWord))
            {
                _logger.LogInformation("Searching...");
                query = query.Where(x => 
                     x.OrderDate.Equals(searchWord) 
                    || x.OrderId.Equals(searchWord)
                    || x.OrderDetails.Contains(searchWord)
                    || x.CustomerEmail.Contains(searchWord)
                    || x.CustomerName.Contains(searchWord)
                    || x.CustomerPhone.Equals(searchWord));}

            _logger.LogInformation("Searching finished!");
            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<ActiveOrdersModel> EditOrder(string customerName, string customerEmail, int customerPhone, string orderDetails)
    {
        var result = await _adminAdminStudioContext.Orders.FirstOrDefaultAsync(
            x=>x.OrderDetails == orderDetails 
               || x.CustomerName == customerName
               || x.CustomerEmail == customerEmail
               || x.CustomerPhone == customerPhone);
        
        try
        {
            _logger.LogInformation("Editing orderModel...");
            if (result != null)
            {
                result.OrderDetails = orderDetails;
                result.CustomerEmail = customerEmail;
                result.CustomerName = customerName;
                result.CustomerPhone = customerPhone;
                await _adminAdminStudioContext.SaveChangesAsync();
                return result;
            }
        }
        catch(Exception ex)
        {
            _logger.LogInformation(ex.Message);
            throw;
        }

        return null;
    }
    
    public async Task<ActiveOrdersModel> SendOrder(string customerName, string customerEmail, int customerPhone, string orderDetails)
    {
        try
        {
            _logger.LogInformation("Preparing order");
            var orderModel = new ActiveOrdersModel
            {
                OrderId = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                OrderDetails = orderDetails,
                CustomerName = customerName,
                CustomerEmail = customerEmail,
                CustomerPhone = customerPhone
            };
            
            var result = await _adminAdminStudioContext.AddAsync(orderModel); 
            
            _logger.LogInformation("Order sent!");
            await _adminAdminStudioContext.SaveChangesAsync();
            return result.Entity;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task DeleteOrder(Guid orderId)
    {
        var result = await _adminAdminStudioContext.FinishedOrders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        try
        {
            _logger.LogInformation("Deleting order...");
            _adminAdminStudioContext.FinishedOrders.Remove(result);
            _logger.LogInformation("Order deleted!");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<List<ActiveOrdersModel>> GetAllCustomerOrders()
    {
        try
        {
            return await _adminAdminStudioContext.Orders.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<ArchivedOrdersModel>> GetAllArchivedOrders()
    {
        try
        {
            return await _adminAdminStudioContext.FinishedOrders.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
