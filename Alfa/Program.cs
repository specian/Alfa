using Convertor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ConvertService, ConvertService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string apiPath = builder.Configuration.GetValue<string>("ApiPath");
string fileRoot = builder.Configuration.GetValue<string>("FileRoot");

ApiMapping.Setup(app, apiPath, fileRoot);

app.Run();
