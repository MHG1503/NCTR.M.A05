using System;
using NCTR.M.A05.Models;
using System.IO;
using OfficeOpenXml;
namespace NCTR.M.A05.Manager;
using System.Text.Json;
public class RecipeManager
{
    List<Recipe> recipes= new List<Recipe>();

    public void AddRecipe(int id, string name, string description, string ingredientsStr, string category){
        if(recipes.Find(recipe => recipe.Id == id) != null){
            System.Console.WriteLine("Already exist recipe : " + name);
            return;
        }
        List<string> ingredientList = new List<string>();
        if(ingredientsStr != null && ingredientsStr.Length > 0){
            ingredientList = ingredientsStr.Split(",").ToList();
        }
        recipes.Add(new Recipe(id, name,description,ingredientList,category));
    }

    public List<Recipe> GetAllRecipes(){
        return recipes.ToList();
    }

    public List<Recipe> SearchRecipes(string keyword){
        return recipes
        .Where((recipe)=> 
        recipe.Name.Contains(keyword) || 
        recipe.Category.Contains(keyword) || 
        recipe.Description.Contains(keyword) ||
        String.Join(",",recipe.Ingredients.ToArray()).Contains(keyword))
        .ToList();
    }

    public void ExportRecipes(string option, string fileName){
        if(option == "JSON"){
            string result = "";
            foreach(var item in recipes){
                result += toJson(item);
            }
            File.WriteAllText(fileName, result);
        }else{
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Recipes");

                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Description";
                worksheet.Cells[1, 4].Value = "Ingredients";
                worksheet.Cells[1, 5].Value = "Category";

                int curRow = 2;
                for(int i = 0; i < recipes.Count; i++){
                    worksheet.Cells[curRow, 1].Value = recipes[i].Id;
                    worksheet.Cells[curRow, 2].Value = recipes[i].Name;
                    worksheet.Cells[curRow, 3].Value = recipes[i].Description;
                    worksheet.Cells[curRow, 4].Value = String.Join(",",recipes[i].Ingredients.ToArray());
                    worksheet.Cells[curRow, 5].Value = recipes[i].Category;
                    curRow++;
                }    
                
                // Lưu file
                File.WriteAllBytes(fileName, package.GetAsByteArray());

                Console.WriteLine("Đã ghi file Excel thành công!");
            }
        }
    }

    public void ImportRecipes(string option,string fileName){
        if(option == "JSON"){
            if(!File.Exists(fileName)){
                System.Console.WriteLine("File dont exits");
            }else{
                List<Recipe> tempRecipes = JsonSerializer.Deserialize<List<Recipe>>(File.ReadAllText(fileName));
                foreach(Recipe recipe in tempRecipes){
                    AddRecipe(recipe.Id,recipe.Name,recipe.Description,recipe.IngredientToString(),recipe.Category);
                }
                System.Console.WriteLine("Import success");
            }
        }else{
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows; 
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    AddRecipe(
                        Convert.ToInt32(worksheet.Cells[row, 1].Value),
                        (string)worksheet.Cells[row, 2].Value,
                        (string)worksheet.Cells[row, 3].Value,
                        (string)worksheet.Cells[row, 4].Value,
                        (string)worksheet.Cells[row, 5].Value);
                }
            }
            System.Console.WriteLine("Import success");
        }
    }

    private String toJson(Recipe recipe){
        return JsonSerializer.Serialize(recipes, new JsonSerializerOptions { WriteIndented = true });
    }

    private Recipe toRecipe(String recipeJson){
        return JsonSerializer.Deserialize<Recipe>(recipeJson);
    }

}
