using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("categoryR")]
public class CategoryRemoveController : ControllerBase{
    public Library db {get;set;}
    public FullLibrary fdb {get;set;}
    public CategoryRemoveController (Library db, FullLibrary fdb){
        this.db = db;
        this.fdb = fdb;
    }
    [HttpDelete("remove")]
    
    public ActionResult RemoveCategory(string name){
        Category? category = db.categorys.FirstOrDefault(t=>t.name == name);
        if (category is null){return NotFound($"Category: {name}");}
        else{
            foreach(Type type in db.types.Where(t=>t.categoryNav == category)){
                foreach(Tag tag in db.tags.Where(t=>t.typeNav == type)){
                    foreach(AssignedTag assignedTag in db.assignedTags.Where(t=>t.tagNav == tag)){
                        db.assignedTagsR.Add(new(assignedTag));
                        db.assignedTags.Remove(assignedTag);
                    }
                    db.tagsR.Add(new(tag));
                    db.tags.Remove(tag);
                }
                foreach(Title title in db.titles.Where(t=>t.type == type)){
                    foreach (AssignedTag atag in db.assignedTags.Where(t=>t.description == title.description)){
                        db.assignedTagsR.Add(new(){id = atag.id, description = new(atag.description)});
                        db.assignedTags.Remove(atag);
                    }
                    db.titlesR.Add(new(title));
                    db.titles.Remove(title);
                }
                db.typesR.Add(new(type));
                db.types.Remove(type);
            }
            db.categorys.Remove(category);
            db.categorysR.Add(new(category));
            db.SaveChanges();
            return Created("", category);
        }
    }
    [HttpPut("restore")]
    public ActionResult RestoreCategory(string name){
        CategoryR? category = db.categorysR.FirstOrDefault(t=>t.name == name);
        if (category is null){return NotFound($"Category: {name}");}
        else{
            foreach(TypeR type in db.typesR.Where(t=>t.categoryNav == category)){
                foreach(TitleR title in db.titlesR.Where(t=>t.type == type)){
                    foreach (AssignedTagR atag in db.assignedTagsR.Where(t=>t.description == title.description)){
                        db.assignedTags.Add(new(atag));
                        db.assignedTagsR.Remove(atag);
                    }
                    db.titles.Add(new(title));
                    db.titlesR.Remove(title);
                }
                db.types.Add(new(type));
                db.typesR.Remove(type);
            }
            db.categorys.Add(new(category));
            db.categorysR.Remove(category);
            db.SaveChanges();
            return Created("", category);
        }
    }
    [HttpDelete("delete")]
    public ActionResult DeleteCategory(string name){
        Category? category = db.categorys.FirstOrDefault(t=>t.name == name);
        if (category is null){return NotFound($"Category: {name}");}
        else{
            foreach(Type type in db.types.Where(t=>t.categoryNav == category)){
                foreach(Title title in db.titles.Where(t=>t.type == type)){
                    foreach (AssignedTag atag in db.assignedTags.Where(t=>t.description == title.description)){
                        db.assignedTags.Remove(atag);
                    }
                    db.titles.Remove(title);
                }
                db.types.Remove(type);
            }
            db.categorys.Remove(category);
            db.SaveChanges();
            return Created("", category);
        }
    }
    [HttpDelete("deleteRemoved")]
    public ActionResult DeleteRemovedCategory(string name){
        CategoryR? category = db.categorysR.FirstOrDefault(t=>t.name == name);
        if (category is null){return NotFound($"Category: {name}");}
        else{
            foreach(TypeR type in db.typesR.Where(t=>t.categoryNav == category)){
                foreach(TitleR title in db.titlesR.Where(t=>t.type == type)){
                    foreach (AssignedTagR atag in db.assignedTagsR.Where(t=>t.description == title.description)){
                        db.assignedTagsR.Remove(atag);
                    }
                    db.titlesR.Remove(title);
                }
                db.typesR.Remove(type);
            }
            db.categorysR.Remove(category);
            db.SaveChanges();
            return Created("", category);
        }
    }
}