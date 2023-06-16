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

    //Esse middleware captura exce��es n�o tratadas durante o processamento de uma requisi��o e exibe uma p�gina de exce��o detalhada com informa��es de diagn�stico e depura��o de erros.
    app.UseDeveloperExceptionPage();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.DefaultModelsExpandDepth(-1);
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

//Recomendado para processar corretamente os cabe�alhos encaminhados
app.UseForwardedHeaders();

app.UseRouting();
//Configurar autoriza��o na aplica��o
app.UseAuthorization();

//Verifica se a requisi��o que est� sendo feita usa o protocolo HTTP e, em caso afirmativo, redireciona para a mesma URL mas usando o protocolo HTTPS
app.UseHttpsRedirection();

//Configurar as rotas
app.UseEndpoints(endpoints =>
{
    //Mapeia os controllers como endpoints da aplica��o
    endpoints.MapControllers();
});

app.Run();
