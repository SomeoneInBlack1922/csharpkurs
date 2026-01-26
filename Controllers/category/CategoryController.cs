using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("Category")]
public class CategoryController : ControllerBase{
    FullLibrary db {get;set;} 
    public CategoryController(FullLibrary db){
        this.db = db;
    }
    [HttpGet("fetch")]
    public ActionResult FetchCategory(string name){
        CategoryF? search = db.categorys.FirstOrDefault(t=>t.name==name);
        if (search == null){
            return NotFound();
        }
        return Ok(search);
    }
    [HttpGet("get")]
    public ActionResult fullGetCategorys(int amount, int skip = 0){
        if (amount<=0 || skip<0){
            return BadRequest();
        }
        int count = db.categorys.Count();
        if (count-skip <= 0){
            return NoContent();
        }
        else{
            // var selected = db.categorys.Skip(skip).Take(amount);
            return Ok(db.categorys.Skip(skip).Take(amount).OrderBy(t=>t.order));
        }
    }
    [HttpPost("new")]
    public ActionResult NewCategory(string name){
        CategoryF category = new(){name = name, order = db.categorys.Count()};
        db.categorys.Add(category);
        db.SaveChanges();
        return Created("", category);
        // return new CustomTextResult($"Category: {name}",201);
    }
    
    [HttpPut("editOrder")]
    public ActionResult EditOrder(string name, long order){
        if (order<0 || order >db.categorys.Count()-1){return BadRequest("order");}
        CategoryF? toPut = db.categorys.FirstOrDefault(t=>t.name==name);
        if (toPut==null){return NotFound($"Category: {name}");}
        else{
            if (toPut.order == order){return NoContent();}
            else{
                List<CategoryF> movedCategorys = new();
                if (toPut.order > order){
                    foreach(var elem in db.categorys.Where(t=>t.order<toPut.order && t.order >= order)){
                        elem.order+=1;
                        movedCategorys.Add(elem);
                        db.categorys.Remove(elem);
                    }
                }
                else{
                    foreach(var elem in db.categorys.Where(t=>t.order>toPut.order && t.order <= order)){
                        elem.order-=1;
                        movedCategorys.Add(elem);
                        db.categorys.Remove(elem);
                    }
                }
                db.categorys.Remove(toPut);
                db.SaveChanges();
                toPut.order=order;
                db.categorys.Add(toPut);
                foreach(CategoryF category in movedCategorys){
                    db.Add(category);
                }
                db.SaveChanges();
                return Ok(toPut);
            }
        }
    }
    [HttpPut("swapOrders")]
    public ActionResult SwapOrders(string firstName, string secondName){
        CategoryF? first = db.categorys.FirstOrDefault(t=>t.name == firstName);
        if (first==null){return NotFound($"Category: {firstName}");}
        CategoryF? second = db.categorys.FirstOrDefault(t=>t.name == secondName);
        if (second==null){return NotFound($"Category: {secondName}");}
        db.RemoveRange(first, second);
        db.SaveChanges();
        // long firstOrder = first.order;
        // long secondOrder = second.order;
        // first.order = secondOrder;
        // second.order = firstOrder;
        first.order+=second.order;
        second.order=first.order-second.order;
        first.order-=second.order;
        db.AddRange(first, second);
        db.SaveChanges();
        return Created("",new{first=first,second=second});
    }
    [HttpPut("editName")]
    public ActionResult EditCategoryName(string from, string to){
        CategoryF? search = db.categorys.FirstOrDefault(t=>t.name==from);
        if (search == null){
            return NotFound(from);
        }
        search.name = to;
        db.SaveChanges();
        return Accepted($"Category: {to}");
        // return Modifyed(to);
    }
    // [HttpDelete("remove")]
    
    // public ActionResult RemoveCategory(string name){
    //     CategoryF? category = db.categorys.FirstOrDefault(t=>t.name == name);
    //     if (category is null){return NotFound($"Category: {name}");}
    //     else{
    //         category.removed = true;
    //         db.SaveChanges();
    //         return Created("", category);
    //     }
    // }
    // [HttpPut("restore")]
    // public ActionResult RestoreCategory(string name){
    //     CategoryF? category = db.categorys.FirstOrDefault(t=>t.name==name);
    //     if (category is null){return NotFound($"Category: {name}");}
    //     else{
    //         category.removed=false;
    //         db.SaveChanges();
    //         return Created("", category);
    //     }
    // }
}