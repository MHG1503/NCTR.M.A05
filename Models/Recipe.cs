using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NCTR.M.A05.Models;

public class Recipe
{
    private string _Name;

    [Required]
    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    
    [Required]
    private int _Id;
    public int Id
    {
        get { return _Id; }
        set {_Id = value; }
    }
    
    private string _Description;
    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }
    
    private List<string> _Ingredients;
    public List<string> Ingredients
    {
        get { return _Ingredients; }
        set { _Ingredients = value; }
    }
    
    private string _Category;
    public string Category
    {
        get { return _Category; }
        set { _Category = value;}
    }
    
    public Recipe(){}

    public Recipe(int id, string name, string? description, string? category) :  this(id, name, description, new List<string>(), category){}
    

    public Recipe(int id, string name, string? description, List<string>? ingredients, string? category)
    {
        _Id = id;
        _Name = name;
        _Description = description;
        Ingredients = ingredients;
        Category = category;
    }

    public override string ToString(){
        return string.Format("{0,-5} {1,-15} {2,-15} {3,-20} {4, -15}", 
        _Id, _Name, _Description ?? "",String.Join(",",Ingredients.ToArray()) , Category ?? "");
    }

    public string IngredientToString(){
        return String.Join(",",Ingredients.ToArray());
    }

}
