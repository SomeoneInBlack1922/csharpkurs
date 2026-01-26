using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


[PrimaryKey(nameof(name))]
public class Category{
    public Category(){}
    public Category(CategoryR category){
        this.name = category.name;
        this.typeAm = category.typeAm;
        this.removedTypeAm = category.removedTypeAm;
        this.order = category.order;
    }
    [Required]
    public string name {get; set;}="Undefined";
    // internal long id{get;set;}
    public long typeAm{get;set;}
    internal long removedTypeAm{get;set;}
    internal long order{get;set;}
    // [DefaultValue("no")]
    internal List<Type>? typelist{get; set;}
    // public bool IsRemoved(){return removed;}
    // public bool IsNotRemoved(){return !removed;}
}
[PrimaryKey(nameof(name))]
public class Type{
    public Type(){}
    public Type(TypeR? type){
        if (type != null){
            this.order = type.order;
            this.categoryName = type.categoryName;
            this.name = type.name;
            this.titleAm = type.titleAm;
        }
    }
    // [NotMapped]
    // public bool category {get {return categoryNav is null ? true : false;} private set{}}
    internal Category? categoryNav{get;set;}
    internal long order{get;set;}
    internal string? categoryName{get; set;}
    [Required]
    public string name {get; set;}="Undefined";
    public long titleAm {get;set;}
    // [DefaultValue("no")]
    // public string TagFname {get;set;}
    internal List<Title>? titlelist{get; set;}
    internal List<Tag>? taglist{get; set;}
}
[PrimaryKey(nameof(id))]
public class Title{
    public Title(){}
    public Title(TitleR title){
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
    // [DefaultValue("no")
    internal Type? type{get; set;}
    [Required]
    internal Description? description {get; set;}=null!;
}
[PrimaryKey(nameof(titleId))]
public class Description{
    public Description(){}
    public Description(DescriptionR description){
        this.titleId = description.titleId;
        this.desk = description.desk;
        this.photoAm = description.photoAm;
        this.title = new(description.title);
    }
    internal long titleId{get; set;}
    [Required]
    public string desk {get; set;}="Undefined";
    [Required]
    public long photoAm{get; set;}
    // [DefaultValue(false)]
    // internal bool removed{get;set;}
    [Required]
    internal Title title{get; set;}=null!;
    internal List<AssignedTag>? assignedlist{get; set;}
}
[PrimaryKey(nameof(name))]
public class Tag{
    public Tag(){}
    public Tag(TagR tag){
        this.name = tag.name;
    }
    [Required]
    public string name {get; set;}="Undefined";
    internal List<AssignedTag>? assignedList{get;set;}
    public Type? typeNav{get; set;}
    // public List<TypeTagJoin> typeJoinlist {get; set;}
}
[PrimaryKey(nameof(id))]
public class AssignedTag{
    public AssignedTag(){}
    public AssignedTag(AssignedTagR assignedTag){
        this.id = assignedTag.id;
        this.tagName = assignedTag.tagName;
        this.description = new(assignedTag.description);
    }
    internal long id {get; set;}
    public string tagName{get;set;} = null!;
    // [ForeignKey(nameof(Tag))]
    // public string tag{get;set;}
    [Required]
    public string value{get;set;} = "Undefined";
    [Required]
    // [Required]
    public Description description{get;set;} = null!;
    [Required]
    public Tag tagNav{get;set;} = null!;
}
public class TypeTagJoin{
    public Type type{get; set;} =null!;
    public Tag tag {get; set;} =null!;
}
