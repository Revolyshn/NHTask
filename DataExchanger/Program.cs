using System;
using System.Collections.Specialized;
using System.IO.Ports;
using System.Net;
using System.Text;

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
        _port.ReadTimeout = 500;
        _port.WriteTimeout = 500;
        
        _port.Open();
        _continue = true;
        
        _port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        
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

    private static async void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        var myJson = _port.ReadExisting();
        using var httpClient = new HttpClient();
        var response = await httpClient.PostAsync(
            "http://localhost:7174/", 
            new StringContent(myJson, Encoding.UTF8, "application/json"));
    } 
}