using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("publicCategory")]
public class PublicCategoryController : ControllerBase{
    Library db{ get; set; }
    public PublicCategoryController(Library db){
        this.db = db;
    }
    // [HttpGet("getCategoryAm")]
    // public ActionResult getCategoryAmount(){

    // }
    [HttpGet("getTree")]
    public ActionResult GetTree(){
        List<object> ret = new();
        foreach(Category category in db.categorys.OrderBy(x=>x.order)){
            var retCategory = new{
                name = category.name,
                typeAm = category.typeAm,
                typeList = new List<Type>()
            };
            foreach(Type type in db.types.Where(x=>x.categoryName == category.name).OrderBy(x=>x.order)){
                retCategory.typeList.Add(type);
            }
            ret.Add(retCategory);
        }
        if(ret.Count == 0){
            return NoContent();
        }
        else{
            return Ok(ret);
        }
    }
    [HttpGet("get")]
    public ActionResult getCategorys(int amount, int skip = 0){
        if (amount<=0 || skip<0){
            return BadRequest();
        }
        int count = db.categorys.Count();
        if (count-skip <= 0){
            return NoContent();
        }
        else{
            // Where(t=>t.removed != true).OrderBy(t=>t.order)
            return Ok(db.categorys.Skip(skip).Take(amount).ToList());
        }
    }
    [HttpGet("fetch")]
    public ActionResult FetchCategory(string name){
        Category? search = db.categorys.FirstOrDefault(t=>t.name==name);
        if (search == null){
            return NotFound();
        }
        return Ok(search);
    }
}
public class CustomTextResult : ActionResult{
    public string text {get;set;}
    public int StatusCode {get;set;}
    public CustomTextResult(string text, int StatusCode){
        this.text = text;
        this.StatusCode = StatusCode;
    }
    public override Task ExecuteResultAsync(ActionContext context)
    {
        HttpContext income = context.HttpContext;
        income.Response.StatusCode = StatusCode;
        income.Response.ContentType = "text/utf-8";
        return income.Response.WriteAsync(text);
    }
}
public class CustomJsonResult<T> : ActionResult{
    public T toConvert {get;set;}
    public int StatusCode {get;set;}
    public CustomJsonResult(T toConvert, int StatusCode){
        this.toConvert = toConvert;
        this.StatusCode = StatusCode;
    }
    public override Task ExecuteResultAsync(ActionContext context)
    {
        HttpContext income = context.HttpContext;
        income.Response.StatusCode = StatusCode;
        income.Response.ContentType = "text/utf-8";
        return income.Response.WriteAsJsonAsync<T>(toConvert);
    }
}
// application/json

public class CustomResult : ActionResult{
    public object body {get;set;}
    public int StatusCode {get;set;}
    public string ContentType {get;set;}
    public CustomResult(object body, int StatusCode, string ContentType){
        this.body = body;
        this.StatusCode = StatusCode;
        this.ContentType = ContentType;
    }
    // public override Task ExecuteResultAsync(ActionContext context)
    // {
    //     HttpContext income = context.HttpContext;
    //     income.Response.StatusCode = StatusCode;
    //     return income.Response.Body.WriteAsync(ByteConverter);
    // }
    
}