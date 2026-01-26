using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
public class CategoryF : Category{
    [Required]
    public new string name {get; set;}="Undefined";
    public new long typeAm{get;set;}
    public new long removedTypeAm{get;set;}
    public new long order{get;set;}
    internal new List<TypeF>? typelist{get; set;}
}
public class TypeF : Type{
    public new long order{get;set;}
    public new string? categoryName{get; set;}
    [Required]
    public new string name {get; set;}="Undefined";
    public new long titleAm {get;set;}    internal new CategoryF? categoryNav{get;set;}
    internal new List<TitleF>? titlelist{get; set;}
    internal new List<TagF>? taglist{get; set;}
}
public class TitleF : Title{
    public new long id {get;set;}
    [Required]
    public new string name {get; set;}="Undefined";
    [Required]
    public new double price{get; set;}
    public new DateTime date {get;set;} = DateTime.Now;
    public new string image {get;set;}=null!;
    internal new TypeF? type{get; set;}
    internal new DescriptionF? description {get; set;}=null!;
}
public class DescriptionF : Description{
    public new long titleId{get; set;}
    [Required]
    public new string desk {get; set;}="Undefined";
    [Required]
    public new long photoAm{get; set;}
    [Required]
    internal new TitleF title{get; set;}=null!;
    internal new List<AssignedTagF>? assignedlist{get; set;}
}
public class TagF : Tag{
    [Required]
    public new string name {get; set;} = "Undefined";
    internal new List<AssignedTagF>? assignedList{get;set;}
    public new TypeF? typeNav{get; set;}
}
public class AssignedTagF : AssignedTag{
    public new long id {get; set;}
    public new string tagName{get;set;} = null!;
    [Required]
    public new string value{get;set;} = "Undefined";
    [Required]
    public new DescriptionF? description{get;set;}
    [Required]
    public new TagF tagNav{get;set;} = null!;
}
