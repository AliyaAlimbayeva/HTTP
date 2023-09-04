using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Listener;
public class ListenerLib
{
    public static async Task Listen(HttpListener listener)
    {
        while (true)// получаем контекст
        {
            var context = await listener.GetContextAsync();
            var request = context.Request;  // получаем данные запроса
            var response = context.Response;    // получаем объект для установки ответа
            string path = request.RawUrl.ToString();
            Console.WriteLine(path);
            string responseString = path == "/MyName/" ? "Aliya" : path;
            Console.WriteLine(responseString);
            SetStatus(response, path);
            GetMyNameByHeader(response, path);
            MyNameByCookies(response, path);

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            await output.WriteAsync(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }
    }

    private static void MyNameByCookies(HttpListenerResponse response, string path)
    {
        if (ParsePath(path) == "MyNameByCookies") 
        {
            response.Cookies.Add(new Cookie("MyNameCookie", "Aliya"));
        }
    }
    private static void SetStatus(HttpListenerResponse response, string path)
    {
        switch (ParsePath(path))
        {
            case "Information":
                response.StatusCode = 200;
                break;
            case "Success":
            case "MyNameByHeader":
            case "MyNameByCookies":
                response.StatusCode = 200;
                break;
            case "Redirection":
                response.StatusCode = 307;
                break;
            case "ClientError":
                response.StatusCode = 400;
                break;
            case "ServerError":
                response.StatusCode = 500;
                break;
            default:
                response.StatusCode = 200;
                break;
        }
    }
    private static string ParsePath(string? path) => path.Trim('/');
    private static void GetMyNameByHeader(HttpListenerResponse response, string path)
    {
        if (ParsePath(path) == "MyNameByHeader")
        {
            response.AddHeader("X-MyName", "Aliya");
        } 
    }
}
