using System.Net.Http.Json;
using FrontSite.Models;

namespace FrontSite.Services;

public class AdService
{
    private readonly HttpClient _http;

    public AdService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Ad>> GetAdsAsync()
    {
        try
        {
            var ads = await _http.GetFromJsonAsync<List<Ad>>("http://localhost:5027/api/ads");
            Console.WriteLine($"Получено {ads?.Count} объявлений");
            return ads ?? new List<Ad>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки API: {ex.Message}");
            return new List<Ad>();
        }
    }

    public async Task<bool> CreateAdAsync(Ad ad)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("http://localhost:5027/api/ads", ad);
            Console.WriteLine($"POST статус: {response.StatusCode}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка отправки API: {ex.Message}");
            return false;
        }
    }
}