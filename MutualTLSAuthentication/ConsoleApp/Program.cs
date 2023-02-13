using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");

DoOk();

DoError();

void DoOk()
{
    var handler = new HttpClientHandler();
    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
    handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls | SslProtocols.None | SslProtocols.Tls11;
    try
    {
        //加载客户端证书
        var crt = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "client.p12"), "123456");
        handler.ClientCertificates.Add(crt);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

    handler.ServerCertificateCustomValidationCallback = (message, cer, chain, errors) =>
    {
        return CusSSLLib.CaHelper.Valid(cer, chain, errors);
    };

    var client = new HttpClient(handler);
    var url = "https://localhost/WeatherForecast";
    var response = client.GetAsync(url).Result;
    Console.WriteLine(response.IsSuccessStatusCode);
    var result = response.Content.ReadAsStringAsync().Result;
    Console.WriteLine(result);
}

void DoError()
{
    var handler = new HttpClientHandler();
    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
    handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls | SslProtocols.None | SslProtocols.Tls11;
    try
    {
        //加载客户端证书
        var crt = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "client2.p12"), "098765");
        handler.ClientCertificates.Add(crt);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    //用chain.Build验证服务器证书
    handler.ServerCertificateCustomValidationCallback = (message, cer, chain, errors) =>
    {
        return CusSSLLib.CaHelper.Valid(cer, chain, errors);
    };

    var client = new HttpClient(handler);
    var url = "https://localhost/WeatherForecast";
    var response = client.GetAsync(url).Result;
    Console.WriteLine(response.IsSuccessStatusCode);
    var result = response.Content.ReadAsStringAsync().Result;
    Console.WriteLine(result);
}
