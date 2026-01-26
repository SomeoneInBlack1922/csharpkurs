using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
[PrimaryKey(nameof(name))]
public class CategoryR{
    public CategoryR(){}
    public CategoryR(Category category){
        this.name = category.name;
        this.typeAm = category.typeAm;
        this.removedTypeAm = category.removedTypeAm;
        this.order = category.order;
    }
    [Required]
    public string name {get; set;}="Undefined";
    public long typeAm{get;set;}
    public long removedTypeAm{get;set;}
    public long order{get;set;}
    internal List<TypeR>? typelist{get; set;}
}
[PrimaryKey(nameof(name))]
public class TypeR{
    public TypeR(){}
    public TypeR(Type? type){
        if (type != null){
            this.order = type.order;
            this.categoryName = type.categoryName;
            this.name = type.name;
            this.titleAm = type.titleAm;
        }
    }
    public long order{get;set;}
    public string? categoryName{get; set;}
    [Required]
    public string name {get; set;}="Undefined";
    public long titleAm {get;set;}
    internal CategoryR? categoryNav{get;set;}
    internal List<TitleR>? titlelist{get; set;}
    internal List<TagR>? taglist{get; set;}
}
[PrimaryKey(nameof(id))]
public class TitleR{
    public TitleR(){}
    public TitleR(Title title){
        this.id = title.id;
        this.name = title.name;
        this.price = title.price;
        this.image = title.image;
        this.date = title.date;
        this.type = title.type == null? null : new(title.type);
        
    }
    public long id {get;set;}
    [Required]
    public string name {get; set;}="Undefined";
    [Required]
    public double price{get; set;}
    public DateTime date {get;set;} = DateTime.Now;
    public string image {get;set;}=null!;
    internal TypeR? type{get; set;}
    internal DescriptionR? description {get; set;}=null!;
}
[PrimaryKey(nameof(titleId))]
public class DescriptionR{
    public DescriptionR(){}
    public DescriptionR(Description description){
        this.titleId = description.titleId;
        this.desk = description.desk;
        this.photoAm = description.photoAm;
        this.title = new(description.title);
    }
    public long titleId{get; set;}
    [Required]
    public string desk {get; set;}="Undefined";
    [Required]
    public long photoAm{get; set;}
    [Required]
    internal TitleR title{get; set;}=null!;
    internal List<AssignedTagR>? assignedlist{get; set;}
}
[PrimaryKey(nameof(name))]
public class TagR{
    public TagR(){}
    public TagR(Tag tag){
        this.name = tag.name;
    }
    [Required]
    public string name {get; set;} = "Undefined";
    internal List<AssignedTagR>? assignedList{get;set;}
    public TypeR? typeNav{get; set;}
}
[PrimaryKey(nameof(id))]
public class AssignedTagR{
    public AssignedTagR(){}
    public AssignedTagR(AssignedTag assignedTag){
        this.id = assignedTag.id;
        this.tagName = assignedTag.tagName;
        this.description = new(assignedTag.description);
    }
    public long id {get; set;}
    public string tagName{get;set;} = null!;
    [Required]
    public string value{get;set;} = "Undefined";
    [Required]
    public DescriptionR description{get;set;}
    [Required]
    public TagR tagNav{get;set;} = null!;
}