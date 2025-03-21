using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ExternalApiService
{
    private readonly HttpClient _httpClient;

    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Customer-Key", "1");
        _httpClient.DefaultRequestHeaders.Add("Timestamp",
                "1741849537454");
        _httpClient.DefaultRequestHeaders.Add("Authorization", "11df8551bd077431fac8795a96dfb83f9d6fb6fe8736fa689faef3448469fb00a6f01c6ecd57ccb72fd73cc4fb52bf1b537c4355e808d90598598fefe01e0054");
    }

    public async Task<ActionResult<string>> CallExternalApiAsync(string endpoint, object payload)
    {
        try
        {
            var jsonPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Make the POST request
            var response = await _httpClient.PostAsync(endpoint, content);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch(Exception ex)
        {
            return null;
        }
        // Serialize the payload to JSON
        

        // Read and return the response content
        
    }
}