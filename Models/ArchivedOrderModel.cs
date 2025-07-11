using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminInteriorDesignStudioApi.Models;

[Table("ArchivedOrders")]
public class ArchivedOrderModel
{
    [Key]
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderDetails { get; set; }
    private string _customerEmail;
    public string CustomerName { get; set; }
    public int CustomerPhone { get; set; }

    public string CustomerEmail
    {
        get => _customerEmail;
        set
        {
            var split = value.Split("@");
            if (!string.IsNullOrEmpty(value) && value.Contains("@") && value.EndsWith(".com") && split[0].Length > 0)
            {
                _customerEmail = value;  
            }
            else
            {
                throw new ArgumentException("Invalid email address.");
            }
        }
    }
}
