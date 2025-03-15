using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using frontend.Models;

namespace FrontSite.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    public async Task<bool> Login(LoginModel login)
    {
        var response = await _httpClient.PostAsJsonAsync("api/users/login", login);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            if (result != null && result.ContainsKey("token"))
            {
                var token = result["token"];
                await _localStorage.SetItemAsync("authToken", token); // Используем LocalStorage
                ((CustomAuthStateProvider)_authStateProvider).MarkUserAsAuthenticated(token);
                return true;
            }
        }
        return false;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken"); // Очистка токена
        ((CustomAuthStateProvider)_authStateProvider).MarkUserAsLoggedOut();
    }
}