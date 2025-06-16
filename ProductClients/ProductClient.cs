using System.Text;
using System.Text.Json;
using OrderService.Dtos;

namespace OrderService.ProductClients;

public interface IProductClient
{
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<bool> UpdateProductStockAsync(int productId, int newStock);
}


public class ProductClient : IProductClient
{
    private readonly HttpClient _httpClient;

    public ProductClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/product/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProductDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<bool> UpdateProductStockAsync(int productId, int newStock)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { Id = productId, Stock = newStock }),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PutAsync("/api/product", content);
        return response.IsSuccessStatusCode;
    }
}

