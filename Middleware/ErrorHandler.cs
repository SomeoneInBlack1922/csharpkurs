using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

public class RespondWithError{
    RequestDelegate next{get;set;}
    ILogger logger{get;set;}
    public RespondWithError(RequestDelegate next, ILogger<MesureResponseTime> logger){
        this.next = next;
        this.logger = logger;
    }
    public async Task InvokeAsync(HttpContext context){
        try{
            await next.Invoke(context);
        }
        catch(Microsoft.EntityFrameworkCore.DbUpdateException e){
            if (e.InnerException is Microsoft.Data.Sqlite.SqliteException){
                Microsoft.Data.Sqlite.SqliteException intern = e.InnerException as Microsoft.Data.Sqlite.SqliteException;
                await SendTextResponce(409,intern.Message);
                // switch (intern.SqliteErrorCode){
                //     case 19:{
                //         await SendTextResponce(409,intern.Message);
                //         break;
                //     }
                //     default:{
                //         await SendTextResponce(409);
                //         break;
                //     }
                // }
            }
        }
        catch (Exception e){
            await SendTextResponce(501,$"{e.Message}\n{e.Data}");
        }
        async Task SendTextResponce(int StatusCode, string Message=""){
            context.Response.StatusCode = StatusCode;
            context.Response.ContentType = "text/utf-8";
            await context.Response.WriteAsync(Message);
        }
    }
}