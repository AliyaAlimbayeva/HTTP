namespace Client;
public class ClientLib
{
    public static async Task DoTask(HttpClient httpClient)
    {
        while (true)
        {
            var requests = new List<string>
            {
                "http://localhost:8888/MyName/",
                "http://localhost:8888/Information/",
                "http://localhost:8888/Success/",
                "http://localhost:8888/Redirection/",
                "http://localhost:8888/ClientError/",
                "http://localhost:8888/ServerError/",
                "http://localhost:8888/MyNameByHeader/",
                "http://localhost:8888/MyNameByCookies/"
            };
            Console.WriteLine("Choose request to send (1 - 8)");
            Console.WriteLine();
            int i = Convert.ToInt32(Console.ReadLine()) - 1;
            var response = await httpClient.GetAsync(requests[i]);

            Console.WriteLine($"Request: {requests[i]}");
            Console.WriteLine("Response:");

            var text = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine($"  Content: {text}");
            Console.WriteLine($"  Status Code: {response.StatusCode}");
            var myNameHeader = response.Headers
                .FirstOrDefault(h => h.Key.Equals("X-MyName"))
                .Value?
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(myNameHeader))
            {
                Console.WriteLine($"Headers: {myNameHeader}");
            }

            var myNameCookie = response.Headers
                .FirstOrDefault(h => h.Key.Equals("Set-Cookie"))
                .Value?
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(myNameCookie))
            {
                Console.WriteLine($"Headers: {myNameCookie}");
            }
        }
    }
}
