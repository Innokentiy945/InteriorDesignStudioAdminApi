using AdminInteriorDesignStudioApi.Context;
using AdminInteriorDesignStudioApi.Models;
using Microsoft.EntityFrameworkCore;
using UserInteriorDesignStudioApi.Context;
using UserInteriorDesignStudioApi.Models;

namespace AdminInteriorDesignStudioApi.Repository;

public class AdminService : IAdminRepository
{
    private AdminStudioContext _adminAdminStudioContext;
    private UserStudioContext _userStudioContext;
    private ILogger<AdminService> _logger;

    public AdminService(AdminStudioContext adminAdminStudioContext, UserStudioContext userStudioContext, ILogger<AdminService> logger)
    {
        _adminAdminStudioContext = adminAdminStudioContext;
        _userStudioContext = userStudioContext;
        _logger = logger;
    }
    
    public async Task ArchiveOrder(OrderModel order)
    {
        var result = await _userStudioContext.Orders.FindAsync(order);
        try
        {
            _logger.LogInformation("Order placed to finished");
            if (result != null)
            {
                var orderModel = new OrderModel
                {
                    OrderId = result.OrderId,
                    OrderDate = result.OrderDate,
                    OrderDetails = result.OrderDetails,
                    CustomerEmail = result.CustomerEmail,
                    CustomerName = result.CustomerName,
                    CustomerPhone = result.CustomerPhone
                };

                await _userStudioContext.Orders.AddAsync(orderModel);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task UnarchiveOrder(ArchivedOrderModel archivedOrder)
    {
        ArchivedOrderModel? result = await _adminAdminStudioContext.FinishedOrders.FindAsync(archivedOrder);
    
        try
        {
            _logger.LogInformation("Order returned to active");
        
            if (result != null)
            {
                var orderModel = new OrderModel
                {
                    OrderId = result.OrderId,
                    OrderDate = result.OrderDate,
                    OrderDetails = result.OrderDetails,
                    CustomerEmail = result.CustomerEmail,
                    CustomerName = result.CustomerName,
                    CustomerPhone = result.CustomerPhone
                };

                await _userStudioContext.Orders.AddAsync(orderModel);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<List<OrderModel>> SearchOpenedOrder(string searchWord)
    {
        IQueryable<OrderModel> query = _userStudioContext.Orders;
            
        try
        {
            if (!string.IsNullOrEmpty(searchWord))
            {
                _logger.LogInformation("Searching...");
                query = query.Where(x => x.OrderDate.Equals(searchWord)
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

    public async Task<List<ArchivedOrderModel>> SearchArchivedOrders(string searchWord)
    {
        IQueryable<ArchivedOrderModel> query = _adminAdminStudioContext.FinishedOrders;
            
        try
        {
            if (!string.IsNullOrEmpty(searchWord))
            {
                _logger.LogInformation("Searching...");
                query = query.Where(x => 
                     x.OrderDate.Equals(searchWord)
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

    public async Task<OrderModel> EditOrder(OrderModel order)
    {
        var result = await _userStudioContext.Orders.FirstOrDefaultAsync(x=>x.OrderId==order.OrderId);
        
        try
        {
            _logger.LogInformation("Editing order...");
            if (result != null)
            {
                result.OrderDate = order.OrderDate;
                result.OrderDetails = order.OrderDetails;
                result.CustomerEmail = order.CustomerEmail;
                result.CustomerName = order.CustomerName;
                result.CustomerPhone = order.CustomerPhone;
                await _userStudioContext.SaveChangesAsync();
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
}
