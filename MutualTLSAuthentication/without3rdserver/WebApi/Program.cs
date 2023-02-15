using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(x => 
{
    x.Listen(IPAddress.Any, 443, listenOptions =>
    {
        var serverCertificate = new X509Certificate2("server.p12", "abc123");
        var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions()
        {
            // must provide a valid certificate for authentication
            ClientCertificateMode = ClientCertificateMode.RequireCertificate,
            SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
            
            ClientCertificateValidation = (cer, chain, error) =>
            {
                // valid the client certificate by you way.
                return CusSSLLib.CaHelper.Valid(cer, chain, error);
            },
            ServerCertificate = serverCertificate
        };
        listenOptions.UseHttps(httpsConnectionAdapterOptions);
    });
});

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
