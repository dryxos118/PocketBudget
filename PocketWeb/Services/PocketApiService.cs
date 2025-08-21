namespace PocketWeb.Services;

public interface IPocketApiService
{
    Task<T?> GetAsync<T>(string endpoint);
    Task<T?> PostAsync<T, V>(string endpoint, V data);
    Task<T?> PutAsync<T, V>(string endpoint, V data);
    Task<bool> DeleteAsync(string endpoint);
    Task<byte[]> ExportExpense(string endpoint);
}

public class PocketApiService : IPocketApiService
{
    public Task<T?> GetAsync<T>(string endpoint)
    {
        throw new NotImplementedException();
    }

    public Task<T?> PostAsync<T, V>(string endpoint, V data)
    {
        throw new NotImplementedException();
    }

    public Task<T?> PutAsync<T, V>(string endpoint, V data)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string endpoint)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> ExportExpense(string endpoint)
    {
        throw new NotImplementedException();
    }
}