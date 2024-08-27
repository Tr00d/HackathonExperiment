using Vonage.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVonageClientScoped(builder.Configuration);
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

