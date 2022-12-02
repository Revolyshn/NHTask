using System;
using System.Collections.Specialized;
using System.IO.Ports;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

class Programs 
{
    private static SerialPort? _port; 
    const string pattern = @"^\s+|^\w+\s+";
    const string substitution = @"";
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
            ReadSerial();
        }

        while (_continue)
        {
            var message = Console.ReadLine();
            if (stringComparer.Equals("test",message))
            {
                ReadTest();
            }
            else if(stringComparer.Equals("quit", message))
            {
                _continue = false;
            }
        }
        _port.Close();
    }

    private static void ReadSerial()
    {
        var str = _port?.ReadLine();
        var currentString = str.Replace("*", "");
        
        var options = RegexOptions.Multiline;
        var regex = new Regex(pattern, options);

        currentString = regex.Replace(currentString, substitution);

        var stringArray = currentString.Split("\n");
        var counter = 0;

        var values = new string[10];

        //if (stringArray.Length != 16) Console.WriteLine("Not correct data");
      

        for (var i = 5; i < 15; i++)
        {
            values[counter++] = stringArray[i];
        }

        currentString.Remove(0);
        currentString = string.Join("\n", values);

        currentString = regex.Replace(currentString, substitution);
        currentString = currentString.Replace("\r", "");
        //stringArray = currentString.Split("\n");

        var httpClient = new HttpClient();
        var result = httpClient.PostAsync("http://localhost:8081/userPost",new StringContent(currentString,Encoding.UTF8,"application/json")).Result;
    }

    private static void ReadTest()
    {
        var buffer = _port?.ReadLine();

        var json = JsonConvert.DeserializeObject(buffer!);
        Console.WriteLine(json.ToString());

        var httpClient = new HttpClient();

        var result = httpClient.PostAsync("http://localhost:8081/userPost",new StringContent(json.ToString(),Encoding.UTF8,"application/json")).Result;

        Console.WriteLine(result);
    }
}