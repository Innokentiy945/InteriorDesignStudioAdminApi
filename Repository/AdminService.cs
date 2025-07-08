using InteriorDesignStudioAdminApi.Context;
using InteriorDesignStudioApi.Context;
using InteriorDesignStudioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InteriorDesignStudioAdminApi.Repository;

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
            if (result != null) await _adminAdminStudioContext.FinishedOrders.AddAsync(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task UnarchiveOrder(OrderModel order)
    {
        var result = await _adminAdminStudioContext.FinishedOrders.FindAsync(order);
        try
        {
            _logger.LogInformation("Order returned to active");
            if (result != null) await _userStudioContext.Orders.AddAsync(result);
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
                query = query.Where(x => 
                    x.OrderNumber.Contains(searchWord)
                    || x.OrderDate.Equals(searchWord)
                    || x.OrderDetails.Contains(searchWord)
                    || x.CustomerEmail.Contains(searchWord)
                    || x.CustomerName.Contains(searchWord)
                    || x.CustomerPhone.Contains(searchWord));}

            _logger.LogInformation("Searching finished!");
            return await query.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task<List<OrderModel>> SearchArchivedOrders(string searchWord)
    {
        IQueryable<OrderModel> query = _adminAdminStudioContext.FinishedOrders;
            
        try
        {
            if (!string.IsNullOrEmpty(searchWord))
            {
                _logger.LogInformation("Searching...");
                query = query.Where(x => 
                    x.OrderNumber.Contains(searchWord)
                    || x.OrderDate.Equals(searchWord)
                    || x.OrderDetails.Contains(searchWord)
                    || x.CustomerEmail.Contains(searchWord)
                    || x.CustomerName.Contains(searchWord)
                    || x.CustomerPhone.Contains(searchWord));}

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
                result.OrderNumber = order.OrderNumber;
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
