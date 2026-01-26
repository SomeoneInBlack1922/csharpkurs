using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("publicType")]
public class PublicTypeController : ControllerBase{
    Library db{ get; set; }
    public PublicTypeController([FromServices]Library db){
        this.db = db;
    }
    [HttpPost("new")]
    public ActionResult NewType(string name, string? CategoryName){
        Category? search = null;
        if (CategoryName != null){
            search = db.categorys.FirstOrDefault(t=>t.name==CategoryName);
            if (search == null){
                return NotFound($"Caregory: {CategoryName}");
            }
            else{
                search.typeAm+=1;
            }
        }
        Type born = new(){name=name, categoryNav=search, order=db.types.Count()};
        db.types.Add(born);
        db.SaveChanges();
        return Created("", born);
    }
    [HttpGet("fetch")]
    public ActionResult FetchTypeByName(string typeName){
        Type? search = db.types.FirstOrDefault(t=>t.name == typeName);
        return search is null? NotFound($"Type: {typeName}") : Ok(search);
    }
    [HttpGet("get")]
    public ActionResult GetTypes(int amount, int skip = 0){
        if (amount<=0 || skip<0){
            return BadRequest();
        }
        var types = db.types.Where(t=>t.categoryNav != null).OrderBy(t=>t.order);
        return types.Count() == 0 ? NoContent() : Ok(types);
    }
    [HttpGet("getFromCategory")]
    public ActionResult GetTypesFromCategory(int amount, string CategoryName, int skip = 0){
        if (amount<=0 || skip<0){
            return BadRequest();
        }
        Category? search = db.categorys.FirstOrDefault(t=>t.name == CategoryName);
        if (search is null){
            return NotFound($"Category: {CategoryName}");
        }
        else{
            var types = db.types.Where(t=>t.categoryNav == search).OrderBy(t=>t.order);
            return types.Count() == 0 ? NoContent() : Ok(types);
        }
    }
    CustomTextResult Modifyed(string message){
        return new CustomTextResult(message,202);
    }
}