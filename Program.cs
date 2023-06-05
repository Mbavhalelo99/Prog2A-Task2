using System;
using System.Collections.Generic;
using System.Numerics;

namespace RecipeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Recipe recipe = new Recipe();
            List<string> recipeNames = new List<string>();
            List<string> ingredientNames = new List<string>();
            List<double> ingredientQuantities = new List<double>();
            List<string> ingredientUnits = new List<string>();
            List<string> steps = new List<string>();
            List<Recipe> recipes = new List<Recipe>();

            Console.Write("Enter the name of Recipe: ");
            recipe.recipeNames = (Console.ReadLine());

            Console.Write("Enter the number of ingredients: ");
            recipe.NumIngredients = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < recipe.NumIngredients; i++)
            {
                Console.Write($"Enter the name of ingredient {i + 1}: ");
                ingredientNames.Add(Console.ReadLine());

                Console.Write($"Enter the quantity of {ingredientNames[i]}: ");
                ingredientQuantities.Add(Convert.ToDouble(Console.ReadLine()));

                Console.Write($"Enter the unit of measurement for {ingredientNames[i]}: ");
                ingredientUnits.Add(Console.ReadLine());
            }

            Console.Write("\nEnter the number of steps: ");
            recipe.NumSteps = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < recipe.NumSteps; i++)
            {
                Console.Write($"Enter Step {i + 1}: ");
                steps.Add(Console.ReadLine());
            }

            recipe.DisplayRecipe();

            Console.Write("\nEnter the scaling factor (0.5, 2, or 3): ");
            double factor = Convert.ToDouble(Console.ReadLine());
            recipe.ScaleRecipe(factor);

            recipe.DisplayRecipe();

            recipe.ResetQuantities();

            recipe.DisplayRecipe();

            recipe.Run();

            while (true)
            {
                Console.Write("Enter recipe name (or 'q' to quit): ");
                string recipeName = Console.ReadLine();
                if (recipeName == "q")
                {
                    break;
                }

                Recipe recipeNameObj = new Recipe(recipeName);
                recipes.Add(recipeNameObj);
            }
        }
    }

    public class Recipe
    {
        private string? recipeName;

        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public int TotalCalories { get; set; }
        public int NumSteps { get; internal set; }
        public int NumIngredients { get; internal set; }

        private List<Recipe> recipes;
        private double factor;
        internal string recipeNames;

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
            recipes = new List<Recipe>();
        }

        public Recipe(string name, List<Ingredient> ingredients, List<string> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
            TotalCalories = CalculateTotalCalories();
        }

        public Recipe(string? recipeName)
        {
            this.recipeName = recipeName;
        }

        public void DisplayRecipe()
        {
            Console.WriteLine("Ingredients:");
            foreach (Ingredient ingredient in Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }
        }

        public void ScaleRecipe(double scalingFactor)
        {
            Console.Write("Enter the name of the recipe to scale: ");
            string recipeName = Console.ReadLine();

            Recipe recipeToScale = recipes.Find(r => r.Name == recipeName);

            if (recipeToScale == null)
            {
                Console.WriteLine("Recipe not found.");
                return;
            }

            recipeToScale.ScaleRecipe(scalingFactor);

            Console.WriteLine($"Scaled recipe: {recipeToScale.Name}");
            recipeToScale.DisplayRecipe();
        }


        public void ResetQuantities()
        {
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
        }

        public void ListRecipes()
        {
            Console.WriteLine("Recipes:");

            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine(recipe.Name);
            }
        }

        public void ClearRecipe()
        {
            Ingredients.Clear();

        }

        public int CalculateTotalCalories()
        {
            int totalCalories = 0;

            foreach (Ingredient ingredient in Ingredients)
            {
                totalCalories += ingredient.Calories;
            }

            return totalCalories;
        }


        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Enter a command:");
                Console.WriteLine("1. Add recipe");
                Console.WriteLine("2. List recipes");
                Console.WriteLine("3. Scale recipe");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddRecipe();
                        break;
                    case 2:
                        ListRecipes();
                        break;
                    case 3:
                        ScaleRecipe();
                        break;
                    default:
                        Console.WriteLine("Invalid command. Please try again.");
                        break;
                }
            }
        }

        private void ScaleRecipe()
        {
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        public void AddRecipe()
        {
            Recipe recipe = new Recipe();

            Console.Write("Enter the recipe name: ");
            recipe.Name = Console.ReadLine();

            while (true)
            {
                Recipe.Ingredient ingredient = new Recipe.Ingredient();

                Console.Write("Enter an ingredient name or \"X\" to finish: ");
                string input = Console.ReadLine();

                if (input == "X")
                {
                    break;
                }

                ingredient.Name = input;

                Console.Write("Enter the quantity: ");
                ingredient.Quantity = double.Parse(Console.ReadLine());

                Console.Write("Enter the unit: ");
                ingredient.Unit = Console.ReadLine();

                Console.Write("Enter the number of calories: ");
                ingredient.Calories = int.Parse(Console.ReadLine());

                if (ingredient.Calories > 300)
                {
                    Console.WriteLine("Warning: The calories for this ingredient exceed 300.");
                }

                Console.Write("Enter the food group: ");
                ingredient.FoodGroup = Console.ReadLine();

                recipe.Ingredients.Add(ingredient);
            }

            while (true)
            {
                Console.Write("Enter a Step or \"X\" to finish: ");
                string input = Console.ReadLine();

                if (input == "X")
                {
                    break;
                }

                recipe.Steps.Add(input);
            }

            recipes.Add(recipe);
        }

        public class Ingredient
        {
            public string Name { get; set; }
            public double Quantity { get; set; }
            public double OriginalQuantity { get; set; }
            public string Unit { get; set; }
            public int Calories { get; set; }
            public string FoodGroup { get; set; }
        }

    }
}



