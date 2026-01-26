using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
[ApiController]
[Route("Title")]
public class TitleController:ControllerBase{
    public FullLibrary db {get;set;}
    public TitleController(FullLibrary db){
        this.db = db;
    }
    [HttpGet("get")]
    public ActionResult GetTitles(int amount, int skip=0){
        if (amount <= 0 || skip < 0){return BadRequest();}
        else{
            var titles = db.titles.Where(x => x.type != null).Skip(skip).Take(amount);
            return titles.Count() == 0? NoContent() : Ok(titles);
        }
    }
    [HttpGet("getFromType")]
    public ActionResult GetTypesFromCategory(int amount, string TypeName, int skip = 0){
        if (amount<=0 || skip<0){
            return BadRequest();
        }
        TypeF? search = db.types.FirstOrDefault(t=>t.name == TypeName);
        if (search is null){
            return NotFound($"Type: {TypeName}");
        }
        else{
            var titles = db.titles.Where(t=>t.type == search).OrderBy(t=>t.date);
            return titles.Count() == 0 ? NoContent() : Ok(titles);
        }
    }
    [HttpGet("fetch")]
    public ActionResult SearchTitle (string name){
        TitleF? title = db.titles.FirstOrDefault(x => x.name == name);
        return title is null ? NotFound() : Ok(title);
    }
    
}