using System.Text.Json.Serialization;
using NCTR.M.A05.Manager;

internal class Program
{
    private static void Main(string[] args)
    {
        RecipeManager recipeManager = new RecipeManager();
        while (true){
            showMenu();
            string choice = Console.ReadLine();
            if (choice == null && choice == ""){
                System.Console.WriteLine("Invalid input. Please try again!");
                continue;
            }
            switch (choice){
                case "1":
                    System.Console.WriteLine("Enter recipe id: ");
                    int id;
                    while (true){
                        if(int.TryParse(Console.ReadLine(), out id)){
                            break;
                        }
                        System.Console.WriteLine("Invalid choice");
                    }
                    System.Console.WriteLine("Enter recipe name: ");
                    string name;
                    while(true){
                        name = Console.ReadLine();
                        if(name != null && name != ""){
                            break;
                        }
                    }
                    System.Console.WriteLine("Enter recipe description");
                    string description = Console.ReadLine();
                    System.Console.WriteLine("Enter recipe ingredients");
                    string ingredients = Console.ReadLine();
                    System.Console.WriteLine("Enter recipe category");
                    string category = Console.ReadLine();
                    recipeManager.AddRecipe(id, name,description,ingredients,category);
                    break;
                case "2":
                    System.Console.WriteLine("========== All Recipes ==========");
                    System.Console.WriteLine(string.Format("{0,-5} {1,-15} {2,-15} {3,-20} {4, -15}","Id", "Name", "Description","Ingredients", "Category"));
                    recipeManager.GetAllRecipes().ForEach(System.Console.WriteLine);
                    break;
                case "3":
                    System.Console.WriteLine("========== Search Recipes ==========");
                    System.Console.WriteLine("Enter keyword");
                    string keyword = Console.ReadLine();
                    System.Console.WriteLine(string.Format("{0,-5} {1,-15} {2,-15} {3,-20} {4, -15}","Id", "Name", "Description","Ingredients", "Category"));
                    recipeManager.SearchRecipes(keyword).ForEach(System.Console.WriteLine);
                    break;
                case "4":
                    showMenuForImportFile();
                    string choice2 = Console.ReadLine();
                    switch (choice2){
                        case "1":
                            string fileImport;
                            while(true){
                                System.Console.WriteLine("Enter filename");
                                fileImport = Console.ReadLine();
                                if(fileImport.EndsWith(".json")){
                                    break;
                                }
                            }
                            recipeManager.ImportRecipes("JSON",fileImport);
                            break;
                        case "2":
                            string fileImport2;
                            while(true){
                                System.Console.WriteLine("Enter filename");
                                fileImport2 = Console.ReadLine();
                                if(fileImport2.EndsWith(".xlsx")){
                                    break;
                                }
                            }
                            recipeManager.ImportRecipes("Excel",fileImport2);
                            break;
                        case "3":
                            break;
                        default:
                            System.Console.WriteLine("Invalid choice. Please try again");
                            break;
                    }
                    break;
                case "5":
                    showMenuForExportFile();
                    string choice3 = Console.ReadLine();
                    switch (choice3){
                        case "1":
                            string fileExport;
                            while(true){
                                System.Console.WriteLine("Enter filename");
                                fileExport = Console.ReadLine();
                                if(fileExport.EndsWith(".json")){
                                    break;
                                }
                            }
                        
                            recipeManager.ExportRecipes("JSON",fileExport);
                            break;
                        case "2":
                            string fileExport2;
                            while(true){
                                System.Console.WriteLine("Enter filename");
                                fileExport2 = Console.ReadLine();
                                if(fileExport2.EndsWith(".xlsx")){
                                    break;
                                }
                            }
                            recipeManager.ExportRecipes("Excel",fileExport2);
                            break;
                        case "3":
                            System.Console.WriteLine("Come back to main menu");
                            break;
                        default:
                            System.Console.WriteLine("Invalid choice. Please try again");
                            break;
                    }
                    break;
                case "6":
                    return;
                default:
                    System.Console.WriteLine("Invalid choice. Please try again");
                    break;

            }
        }
    }

    private static void showMenuForExportFile(){
        System.Console.WriteLine("========== Export Recipes ==========");
        System.Console.WriteLine("1. Export to JSON");
        System.Console.WriteLine("2. Export to Excel");
        System.Console.WriteLine("3. Main menu");
        System.Console.WriteLine("Enter your choice");
    }
    private static void showMenuForImportFile(){
        System.Console.WriteLine("========== Import Recipes ==========");
        System.Console.WriteLine("1. Import from JSON");
        System.Console.WriteLine("2. Import from Excel");
        System.Console.WriteLine("3. Main menu");
        System.Console.WriteLine("Enter your choice");
    }

    private static void showMenu(){
        System.Console.WriteLine("========== Recipe Manager ==========");
        System.Console.WriteLine("1. Add a recipe");
        System.Console.WriteLine("2. View all recipes");
        System.Console.WriteLine("3. Search recipes");
        System.Console.WriteLine("4. Import recipes");
        System.Console.WriteLine("5. Export recipes");
        System.Console.WriteLine("6. Exit");
        System.Console.WriteLine("Enter your choice");
    }
}