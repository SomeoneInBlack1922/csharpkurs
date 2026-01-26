using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("Type")]
public class TypeController : ControllerBase{
    public FullLibrary db{get;set;}
    public TypeController(FullLibrary db){
        this.db = db;
    }
    [HttpPost("new")]
    public ActionResult NewType(string name, string? CategoryName){
        CategoryF? search = null;
        if (CategoryName != null){
            search = db.categorys.FirstOrDefault(t=>t.name==CategoryName);
            if (search == null){
                return NotFound($"Caregory: {CategoryName}");
            }
            else{
                search.typeAm+=1;
            }
        }
        TypeF born = new(){name=name, categoryNav=search, order=db.types.Count()};
        db.types.Add(born);
        db.SaveChanges();
        return Created("", born);
    }
    [HttpGet("fetch")]
    public ActionResult FetchTypeByName(string typeName){
        TypeF? search = db.types.FirstOrDefault(t=>t.name == typeName);
        return search is null? NotFound($"Type: {typeName}") : Ok(search);
    }
    [HttpGet("get")]
    public ActionResult GetTypes(int amount, int skip = 0){
        if (amount<=0 || skip<0){
            return BadRequest();
        }
        var types = db.types.OrderBy(t=>t.order);
        return types.Count() == 0 ? NoContent() : Ok(types);
    }
    [HttpGet("getFromCategory")]
    public ActionResult GetTypesFromCategory(int amount, string? CategoryName, int skip = 0){
        if (amount<=0 || skip<0){
            return BadRequest();
        }
        if (CategoryName is null){
            var types = db.types.Where(t=>t.categoryNav == null).OrderBy(t=>t.order);
            return types.Count() == 0 ? NoContent() : Ok(types);
        }
        else{
            CategoryF? search = db.categorys.FirstOrDefault(t=>t.name == CategoryName);
            if (search is null){
                return NotFound($"Category: {CategoryName}");
            }
            else{
                var types = db.types.Where(t=>t.categoryNav == search).OrderBy(t=>t.order);
                return types.Count() == 0 ? NoContent() : Ok(types);
            }
        }
    }
    [HttpPut("linkCategory")]
    public ActionResult LinkTypeToCategory(string typeName, string categoryName){
        CategoryF? category = db.categorys.FirstOrDefault(t=>t.name==categoryName);
        if (category is null){return NotFound($"Category: {categoryName}");}
        else{
            TypeF? type = db.types.FirstOrDefault(t=>t.name==typeName);
            if(type is null){return NotFound($"Type: {typeName}");}
            else{
                if (type.categoryNav is not null){
                    CategoryF toChange = db.categorys.First(t=>t.name == type.categoryName);
                    toChange.typeAm -=1;
                }
                type.categoryNav=category;
                category.typeAm +=1;
                db.SaveChanges();
                return Created("",type);
            }
        }
    }
    [HttpPut("editOrder")]
    public ActionResult EditOrder(string name, long order){
        if (order<0 || order >db.categorys.Count()-1){return BadRequest("order");}
        TypeF? toPut = db.types.FirstOrDefault(t=>t.name==name);
        if (toPut==null){return NotFound($"Category: {name}");}
        else{
            if (toPut.order == order){return NoContent();}
            else{
                List<TypeF> movedTypes = new();
                if (toPut.order > order){
                    foreach(var elem in db.types.Where(t=>t.order<toPut.order && t.order >= order)){
                        elem.order+=1;
                        movedTypes.Add(elem);
                        db.types.Remove(elem);
                    }
                }
                else{
                    foreach(var elem in db.types.Where(t=>t.order>toPut.order && t.order <= order)){
                        elem.order-=1;
                        movedTypes.Add(elem);
                        db.types.Remove(elem);
                    }
                }
                db.types.Remove(toPut);
                db.SaveChanges();
                toPut.order=order;
                db.types.Add(toPut);
                foreach(TypeF type in movedTypes){
                    db.Add(type);
                }
                db.SaveChanges();
                return Ok(toPut);
            }
        }
    }
    [HttpPut("swapOrders")]
    public ActionResult SwapOrders(string firstName, string secondName){
        TypeF? first = db.types.FirstOrDefault(t=>t.name == firstName);
        if (first==null){return NotFound($"Type: {firstName}");}
        TypeF? second = db.types.FirstOrDefault(t=>t.name == secondName);
        if (second==null){return NotFound($"Type: {secondName}");}
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
    // [HttpDelete("remove")]
    // public ActionResult RemoveType(string typeName){
    //     TypeF? type = db.types.FirstOrDefault(t=>t.name==typeName);
    //     if (type == null){
    //         return NotFound();
    //     }
    //     else{
    //         CategoryF? category = db.categorys.FirstOrDefault(t=>t.name == type.categoryName);
    //         if (category is not null){category.typeAm-=1; category.removedTypeAm+=1;}
    //         type.removed = true;
    //         db.SaveChanges();
    //         return Created("",type);
    //     }
    // }
    // [HttpPut("restore")]
    // public ActionResult RestoreType(string typeName){
    //     TypeF? type = db.types.FirstOrDefault(t=>t.name==typeName);
    //     if (type == null){
    //         return NotFound();
    //     }
    //     else{
    //         CategoryF? category = db.categorys.FirstOrDefault(t=>t.name == type.categoryName);
    //         if (category is not null){category.typeAm+=1; category.removedTypeAm-=1;}
    //         type.removed = false;
    //         db.SaveChanges();
    //         return Created("",type);
    //     }
    // }
    // [HttpDelete("delete")]
    // public ActionResult DeleteType(string typeName){
    //     TypeF? type = db.types.FirstOrDefault(t=>t.name==typeName);
    //     if (type == null){
    //         return NotFound();
    //     }
    //     else{
    //         CategoryF? category = db.categorys.FirstOrDefault(t=>t.name == type.categoryName);
    //         if (category is not null){
    //             if (type.removed){
    //                 category.removedTypeAm-=1;
    //             }
    //             else{
    //                 category.typeAm-=1;
    //             }
    //         }
    //         db.types.Remove(type);
    //         db.SaveChanges();
    //         return Created("",type);
    //     }
    // }
}