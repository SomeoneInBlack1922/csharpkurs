using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
[ApiController]
[Route("publicTitle")]
public class PublicTitleController : ControllerBase{
    Library db {get;set;}
    ILogger<PublicTitleController> logger{get;set;}
    IWebHostEnvironment environment {get;set;}
    public PublicTitleController(Library db, ILogger<PublicTitleController> logger, IWebHostEnvironment environment) {
        this.db = db;
        this.logger = logger;
        this.environment = environment;
    }
    [HttpGet("search")]
    public ActionResult SearchTitlesInType(int amount, string request, string TypeName, int skip = 0){
        Type? type = db.types.FirstOrDefault(x=>x.name == TypeName);
        if (type == null){return NotFound($"Type: {TypeName}");}
        else{
            var selected = db.titles.Where(x=>
                x.name.ToLower().Contains(request.ToLower()) && x.type == type
            ).OrderBy(x=>x.date);
            var titlesAmount = selected.Count();
            var titles = selected.OrderBy(x=>x.date).Skip(skip).Take(amount);
            return titlesAmount == 0? NoContent() : Ok(new{amount = titlesAmount, titles = titles});
        }
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
        Type? search = db.types.FirstOrDefault(t=>t.name == TypeName);
        if (search is null){
            return NotFound($"Type: {TypeName}");
        }
        else{
            var selected = db.titles.Where(t=>t.type == search);
            var titlesAmount = selected.Count();
            var titles = selected.OrderBy(x=>x.date).Skip(skip).Take(amount);
            return titlesAmount == 0? NoContent() : Ok(new{amount = titlesAmount, titles = titles});
        }
    }
    [HttpPost("new")]
    public ActionResult NewType(string name,string? TypeName, double price = 0){
        Type? search = null;
        if (TypeName != null){
            search = db.types.FirstOrDefault(t=>t.name==TypeName);
            if (search == null){
                return NotFound($"Type: {TypeName}");
            }
            else{
                search.titleAm+=1;
            }
        }
        Title born = new(){name=name, type=search, price = price};
        db.titles.Add(born);
        db.SaveChanges();
        return Created("", born);
    }
    [HttpPost("setPrice")]
    public ActionResult SetPrice(string titleName, double price){
        if (price <= 0){return BadRequest("price");}
        else{
            Title? search = db.titles.FirstOrDefault(t=>t.name == titleName);
            if (search == null){return NotFound($"Title: {titleName}");}
            else{
                search.price = price;
                db.SaveChanges();
                return Created("", search);
            }
        }
    }
    [HttpPost("setName")]
    public ActionResult SetName(string titleName, string newName){
        if (newName == "" || titleName == ""){return BadRequest();}
        else{
            Title? search = db.titles.FirstOrDefault(t=>t.name == titleName);
            if (search == null){return NotFound($"Title: {titleName}");}
            else{
                search.name = newName;
                db.SaveChanges();
                return Created("", search);
            }
        }
    }
    [HttpPost("uploadMainImage")]
    public IActionResult SaveImage (string titleName, IFormFile file){
        if (titleName == "") {return BadRequest();}
        Title? title = db.titles.FirstOrDefault(x => x.name == titleName);
        if (title is null){return NotFound();}
        else{
            try{
                var readStreram = file.OpenReadStream();
                // Image.Identify(readStreram);
                var image = Image.Load(readStreram);
                if (image.Height <= 100 || image.Width <= 100){return BadRequest("Dimentions");}
                image.Mutate(i=>i.Resize(new ResizeOptions (){
                    Size = new Size(1000),
                    Mode = image.Height <= 1000 || image.Width <= 1000? ResizeMode.Pad : ResizeMode.Crop
                    }));
                var stream = new FileStream(Path.Combine(environment.ContentRootPath, "db", "images", title.image), FileMode.Create, FileAccess.Write ,FileShare.None);
                image.SaveAsWebp(stream);
                // await readStreram.CopyToAsync(stream);
                readStreram.Close();
                stream.Close();
                return Created($"Main image: {titleName}", null);
            }
            catch (UnknownImageFormatException e){
                return BadRequest("Invalid image");
            }
        }
    }
}