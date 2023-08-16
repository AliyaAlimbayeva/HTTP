using System;
using System.Net;
using System.Threading.Tasks;

namespace Listener;

public class Program
{
    private static readonly HttpListener Listener = new HttpListener();

    private static async Task Main()
    {
        Listener.Prefixes.Add("http://localhost:8888/");
        Listener.Start();
        Console.WriteLine("Listening...");
        await ListenerLib.Listen(Listener);
        Listener.Stop();
    }
}