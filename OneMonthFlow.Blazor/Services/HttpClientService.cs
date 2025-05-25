namespace OneMonthFlow.Blazor.Services;

public class HttpClientService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HttpClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<T> ExecuteAsync<T>(
        string endpoint,
        EnumHttpMethod httpMethod,
        object? requestModel = null
    )
    {
        HttpResponseMessage? response = null;
        HttpContent? content = null;

        if (requestModel is not null)
        {
            string json = requestModel.ToJson();
            content = new StringContent(json, Encoding.UTF8, Application.Json);
        }

        //var getToken = _httpContextAccessor?.HttpContext?.User.Claims
        //    .FirstOrDefault(c => c.Type == "AccessToken")?.Value;

        //var accessToken = !getToken.IsNullOrEmpty() ? getToken : "";

        //if (!accessToken.IsNullOrEmpty())
        //{
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //}

        switch (httpMethod)
        {
            case EnumHttpMethod.Get:
                response = await _httpClient.GetAsync(endpoint);
                break;
            case EnumHttpMethod.Post:
                response = await _httpClient.PostAsync(endpoint, content);
                break;
            case EnumHttpMethod.Put:
                response = await _httpClient.PutAsync(endpoint, content);
                break;
            case EnumHttpMethod.Patch:
                response = await _httpClient.PatchAsync(endpoint, content);
                break;
            case EnumHttpMethod.Delete:
                response = await _httpClient.DeleteAsync(endpoint);
                break;
            case EnumHttpMethod.None:
            default:
                throw new Exception("Invalid Method.");
        }   

        var responseJson = await response.Content.ReadAsStringAsync();

        var model = responseJson.ToObject<T>();

        return model!;
    }
}

public enum EnumHttpMethod
{
    None,
    Get,
    Post,
    Put,
    Patch,
    Delete
}