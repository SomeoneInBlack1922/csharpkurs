using System.Diagnostics;
using System.Text;

public class MesureResponseTime{
    RequestDelegate next;
    ILogger<MesureResponseTime> logger;
    public MesureResponseTime(RequestDelegate next, ILogger<MesureResponseTime> logger){
        this.next = next;
        this.logger = logger;
    }
    public async Task InvokeAsync(HttpContext context){
        Stopwatch timer = new Stopwatch();
        timer.Start();
        await next(context);
        timer.Stop();
        TimeSpan time = timer.Elapsed;
        logger.LogInformation($"Path\t{context.Request.Path}\nRqst\t{context.Request.QueryString.Value}\nMeth\t{context.Request.Method}\nHost\t{context.Request.Host}\nTime\t"+"{0:00}.{1:00}.{2:00},{3:000}.{4:000}.{5:000}\nResp\t{}",time.Hours,time.Minutes,time.Seconds,time.Milliseconds,time.Microseconds,time.Nanoseconds, context.Response.StatusCode);
    }
}