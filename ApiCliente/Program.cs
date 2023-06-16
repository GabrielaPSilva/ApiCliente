using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Cliente",
        Description = "",
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                       builder =>
                       {
                           builder.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowAnyOrigin();
                       });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

app.Use((context, next) =>
{
    context.Request.Scheme = "https";
    return next(context);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.PreSerializeFilters.Add((swagger, req) =>
        {
            swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{req.Host}" } };
        });
    });

    //Esse middleware captura exceções não tratadas durante o processamento de uma requisição e exibe uma página de exceção detalhada com informações de diagnóstico e depuração de erros.
    app.UseDeveloperExceptionPage();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.DefaultModelsExpandDepth(-1);
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

//Recomendado para processar corretamente os cabeçalhos encaminhados
app.UseForwardedHeaders();

app.UseRouting();
//Configurar autorização na aplicação
app.UseAuthorization();

//Verifica se a requisição que está sendo feita usa o protocolo HTTP e, em caso afirmativo, redireciona para a mesma URL mas usando o protocolo HTTPS
app.UseHttpsRedirection();

//Configurar as rotas
app.UseEndpoints(endpoints =>
{
    //Mapeia os controllers como endpoints da aplicação
    endpoints.MapControllers();
});

app.Run();
