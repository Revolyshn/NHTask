using System;
using System.Collections.Specialized;
using System.IO.Ports;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

class Programs 
{
    private static SerialPort? _port; 
    static bool _continue;
    private static readonly HttpClient client = new HttpClient();
    [STAThread] 
    static void Main(string[] args)
    {
        _port = new SerialPort();
        _port.PortName = Console.ReadLine() ?? string.Empty;
        StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

        _port.Open();
        _continue = true;

        while (_continue)
        {
            Read();
        }

        while (_continue)
        {
            var message = Console.ReadLine();

            if (stringComparer.Equals("quit", message))
            {
                _continue = false;
                
            }
        }
        _port.Close();
    }
    
    private static void Read()
    {
        var buffer = _port?.ReadLine();

        var json = JsonConvert.DeserializeObject(buffer!);
        Console.WriteLine(json.ToString());

        var httpClient = new HttpClient();

        var result = httpClient.PostAsync("http://localhost:8081/userPost",new StringContent(json.ToString(),Encoding.UTF8,"application/json")).Result;
    }
}