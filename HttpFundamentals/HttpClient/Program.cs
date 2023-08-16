namespace Client;
public class Program
{
    
    static async Task Main()
    {
        HttpClient httpClient = new HttpClient();
        await ClientLib.DoTask(httpClient);
    }
}