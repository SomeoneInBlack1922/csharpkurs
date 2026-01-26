// using Microsoft.AspNetCore.Mvc;
// [ApiController]
// [Route("typeR")]
// public class TypeRemoveController : ControllerBase{
//     public Library db {get;set;}
//     public FullLibrary fdb {get;set;}
//     public TypeRemoveController (Library db, FullLibrary fdb){
//         this.db = db;
//         this.fdb = fdb;
//     }
//     [HttpDelete("remove")]
//     public ActionResult RemoveType(string typeName){
//         TypeF? type = fdb.types.FirstOrDefault(t=>t.name==typeName);
//         if (type == null){
//             return NotFound();
//         }
//         else{
//             CategoryF? category = fdb.categorys.FirstOrDefault(t=>t.name == type.categoryName);
//             if (category is not null){category.typeAm-=1; category.removedTypeAm+=1;}
//             type.removed = true;
//             db.SaveChanges();
//             return Created("",type);
//         }
//     }
//     [HttpPut("restore")]
//     public ActionResult RestoreType(string typeName){
//         TypeF? type = db.types.FirstOrDefault(t=>t.name==typeName);
//         if (type == null){
//             return NotFound();
//         }
//         else{
//             CategoryF? category = db.categorys.FirstOrDefault(t=>t.name == type.categoryName);
//             if (category is not null){category.typeAm+=1; category.removedTypeAm-=1;}
//             type.removed = false;
//             db.SaveChanges();
//             return Created("",type);
//         }
//     }
//     [HttpDelete("delete")]
//     public ActionResult DeleteType(string typeName){
//         TypeF? type = db.types.FirstOrDefault(t=>t.name==typeName);
//         if (type == null){
//             return NotFound();
//         }
//         else{
//             CategoryF? category = db.categorys.FirstOrDefault(t=>t.name == type.categoryName);
//             if (category is not null){
//                 if (type.removed){
//                     category.removedTypeAm-=1;
//                 }
//                 else{
//                     category.typeAm-=1;
//                 }
//             }
//             db.types.Remove(type);
//             db.SaveChanges();
//             return Created("",type);
//         }
//     }
// }