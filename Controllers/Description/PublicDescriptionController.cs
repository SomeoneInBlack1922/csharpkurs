using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
[ApiController]
[Route("publicDescription")]
public class PublicDescriptionController : ControllerBase {
    public Library db {get;set;}
    public IWebHostEnvironment environment{get;set;}
    public PublicDescriptionController(Library db, IWebHostEnvironment environment) {
        this.db = db;
        this.environment = environment;
    }
    [HttpGet("get")]
    public ActionResult GetDescription(long TitleId) {
        Description? search = db.descriptions.FirstOrDefault(x => x.titleId == TitleId);
        if (search == null) {return NotFound($"Title: {TitleId}");}
        else{
            return Ok(search);
        }
    }
    [HttpPost("newDescription")]
    public ActionResult NewDescription(string titleName, string Description) {
        if (titleName == null || Description == null){return BadRequest();}
        Title? search = db.titles.FirstOrDefault(x => x.name == titleName);
        if (search == null) {return NotFound($"Title: {titleName}");}
        else{
            Description description = new(){desk = Description, photoAm = 0};
            search.description = description;
            db.SaveChanges();
            return Created("",description);
        }
    }
    [HttpPost("setDescription")]
    public ActionResult SetDescription(string titleName, string Description) {
        if (titleName == null || Description == null){return BadRequest();}
        Title? search = db.titles.FirstOrDefault(x => x.name == titleName);
        if (search == null) {return NotFound($"Title: {titleName}");}
        else{
            Description description = search.description;
            description.desk = Description;
            db.SaveChanges();
            return Created("",description);
        }
    }
    [HttpPost("uploadDescriptonImages")]
    public ActionResult UploadDescImages(List<IFormFile> files, [FromForm] string titleName){
        if(files.Count()>4){
            return BadRequest();
        }
        Title? seacrch = db.titles.FirstOrDefault(x => x.name == titleName);
        if(seacrch == null){return NotFound($"Title: {titleName}");}
        else{
            int count = 0;
            foreach (IFormFile file in files){
                count+=1;
                try{
                    var readStreram = file.OpenReadStream();
                    // Image.Identify(readStreram);
                    var image = Image.Load(readStreram);
                    if (image.Height <= 100 || image.Width <= 100){return BadRequest("Dimentions");}
                    image.Mutate(i=>i.Resize(new ResizeOptions (){
                        Size = new Size(1000),
                        Mode = image.Height <= 1000 || image.Width <= 1000? ResizeMode.Pad : ResizeMode.Crop
                    }));
                    var stream = new FileStream(Path.Combine(environment.ContentRootPath, "db", "images", $"{seacrch.id}-d{count}.webp"), FileMode.Create, FileAccess.Write ,FileShare.None);
                    image.SaveAsWebp(stream);
                    // await readStreram.CopyToAsync(stream);
                    readStreram.Close();
                    stream.Close();
                }
                catch (UnknownImageFormatException e){
                    return BadRequest("Invalid image");
                }
            }
            Description? description = db.descriptions.FirstOrDefault(x => x.titleId == seacrch.id);
            if (description == null){
                seacrch.description = new Description { photoAm = count};
            }
            else{
                description.photoAm = files.Count();
            }
            db.SaveChanges();
            return Created($"Images: {files.Count()}", null);
        }
    }
    // [HttpPut("placeDescImage")]
    // public ActionResult PlaceDescImage(IFormFile newImage, int indexToReplace){

    // }
}