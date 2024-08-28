#region
using HackathonExperiment.Api.Adapters;
using Vonage.Extensions;
#endregion

var configuration = CloudRuntimeConfiguration.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(configuration);
builder.Services.AddVonageClientScoped(configuration.BuildConfiguration());
builder.Services.AddScoped<IAiAdapter, FakeAiAdapter>();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
configuration.RunApplication()(app);

public record CloudRuntimeConfiguration(
    string ApplicationId,
    string PrivateKey,
    string ApiKey,
    string ApiSecret,
    string Port)
{
    private const string KeyApplicationId = "VCR_API_APPLICATION_ID";
    private const string KeyPrivateKey = "VCR_PRIVATE_KEY";
    private const string KeyPort = "VCR_PORT";
    private const string KeyApiKey = "VCR_API_ACCOUNT_ID";
    private const string KeyApiSecret = "VCR_API_ACCOUNT_SECRET";

    internal static CloudRuntimeConfiguration Load() => new CloudRuntimeConfiguration(
        Environment.GetEnvironmentVariable(KeyApplicationId),
        Environment.GetEnvironmentVariable(KeyPrivateKey),
        Environment.GetEnvironmentVariable(KeyApiKey),
        Environment.GetEnvironmentVariable(KeyApiSecret),
        Environment.GetEnvironmentVariable(KeyPort));

    internal IConfiguration BuildConfiguration() => new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string>
        {
            {"vonage:Api.Key", this.ApiKey},
            {"vonage:Api.Secret", this.ApiSecret},
            {"vonage:Application.Id", this.ApplicationId},
            {"vonage:Application.Key", this.PrivateKey},
        })
        .Build();

    internal Action<WebApplication> RunApplication() => this.Port is null ? RunForDefaultUri : this.Run;

    private void Run(WebApplication app)
    {
        var port = $"http://0.0.0.0:{this.Port}";
        Console.WriteLine($"Listening to {port}...");
        app.Run(port);
    }

    private static void RunForDefaultUri(WebApplication app)
    {
        Console.WriteLine("Listening to default URL...");
        app.Run();
    }
}

namespace SampleServerSdkDotnetApi.Api
{
    public class Program
    {
        protected Program()
        {
        }
    }
}