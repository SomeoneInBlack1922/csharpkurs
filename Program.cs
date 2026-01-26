using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.FileProviders;

//Network config
// WebHost.CreateDefaultBuilder(args).UseUrls("http://*:5051").UseStartup<Startup>().Build().Run();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FullLibrary>();
builder.Services.AddDbContext<Library>();
var app = builder.Build();
app.UseCors(b=>b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    // app.UseMiddleware<MesureResponseTime>();
    app.UseSwagger();
    app.UseSwaggerUI(o=>{
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        o.RoutePrefix = "swagger";
    });
    app.UseMiddleware<MesureResponseTime>();
}
app.UseMiddleware<RespondWithError>();
// app.UseDefaultFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(app.Environment.ContentRootPath,"db","images")),
            RequestPath = "/images"
        });
app.MapControllers();
// app.MapGet("imageUpload", (HttpContext context)=>context.Response.SendFileAsync("wwwroot/fileUploadTest.html"));
// app.MapGet("description", (HttpContext context)=>context.Response.SendFileAsync("wwwroot/index.html"));

//This is supposed to surve static files of a web site for this backend
app.MapGet("",(HttpContext context)=>context.Response.SendFileAsync("wwwroot/index.html"));
app.Run();