namespace OrderService.Dtos;

public class CreateOrderDto
{
    public int ProductId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Quantity { get; set; }
}
