using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;

public class ApiService
{
    private readonly HttpClient _httpClient;
    // Reemplaza con la dirección de tu API
    private readonly string _apiUrl = "https://apianalisis.up.railway.app/graphql";

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RegisterUser(string nameUsers, string lastName, string email, string password, string phone, string rol, string address, string birthdate)
    {
        var query = new
        {
            query = @"
                mutation CreateUser($nameUsers: String!, $lastName: String!, $email: String!, $password: String!, $phone: String!, $rol: String!, $address: String!, $birthdate: String!) {
                  createUser(NameUsers: $nameUsers, LastName: $lastName, Email: $email, Password: $password, Phone: $phone, Rol: $rol, Address: $address, Birthdate: $birthdate) {
                    message
                  }
                }",
            variables = new
            {
                nameUsers,
                lastName,
                email,
                password,
                phone,
                rol,
                address,
                birthdate
            }
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var jsonRequest = JsonSerializer.Serialize(query, jsonOptions);

        Console.WriteLine("JSON de la solicitud:");
        Console.WriteLine(jsonRequest);

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(_apiUrl, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Código de estado de la respuesta: {response.StatusCode}");
            Console.WriteLine("Contenido de la respuesta:");
            Console.WriteLine(jsonResponse);

            response.EnsureSuccessStatusCode();

            return jsonResponse; // Retorna el mensaje de éxito o error
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al realizar la solicitud HTTP: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Detalle: {ex.InnerException.Message}");
            }
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Detalle: {ex.InnerException.Message}");
            }
            throw;
        }
    }

    // Nuevo método para iniciar sesión
    public async Task<string> LoginUser(string email, string password)
    {
        var query = new
        {
            query = @"
                mutation Login($email: String!, $password: String!) {
                  login(Email: $email, Password: $password) {
                    message
                    token
                  }
                }",
            variables = new
            {
                email,
                password
            }
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var jsonRequest = JsonSerializer.Serialize(query, jsonOptions);

        Console.WriteLine("JSON de la solicitud:");
        Console.WriteLine(jsonRequest);

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(_apiUrl, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Código de estado de la respuesta: {response.StatusCode}");
            Console.WriteLine("Contenido de la respuesta:");
            Console.WriteLine(jsonResponse);

            response.EnsureSuccessStatusCode();

            return jsonResponse; // Retorna el resultado de la solicitud
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al realizar la solicitud HTTP: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Detalle: {ex.InnerException.Message}");
            }
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Detalle: {ex.InnerException.Message}");
            }
            throw;
        }
    }
}



