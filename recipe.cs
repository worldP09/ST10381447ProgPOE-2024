using System;
using System.Collections.Generic;

// Represents a single recipe with ingredients and preparation steps.
class Recipe
{
    // Stores the ingredients with their quantities and units.
    private List<(string Name, double Quantity, string Unit)> ingredients;
    // Stores the preparation steps.
    private List<string> steps;
    // Keeps the original quantities of ingredients for resetting purposes.
    private Dictionary<string, double> originalQuantities;

    // Constructor to initialize the lists and dictionary.
    public Recipe()
    {
        ingredients = new List<(string, double, string)>();
        steps = new List<string>();
        originalQuantities = new Dictionary<string, double>();
    }

    // Adds an ingredient to the recipe.
    public void AddIngredient(string name, double quantity, string unit)
    {
        ingredients.Add((name, quantity, unit));
        // Store the original quantity for resetting later.
        if (!originalQuantities.ContainsKey(name))
        {
            originalQuantities[name] = quantity;
        }
    }

    // Adds a preparation step to the recipe.
    public void AddStep(string step)
    {
        steps.Add(step);
    }

    // Scales the quantities of the ingredients by the given factor.
    public void ScaleQuantities(double factor)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            var (name, quantity, unit) = ingredients[i];
            ingredients[i] = (name, originalQuantities[name] * factor, unit);
        }
    }

    // Resets the quantities of the ingredients to their original values.
    public void ResetQuantities()
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            var (name, _, unit) = ingredients[i];
            ingredients[i] = (name, originalQuantities[name], unit);
        }
    }

    // Clears all the data to start a new recipe.
    public void ClearRecipe()
    {
        ingredients.Clear();
        steps.Clear();
        originalQuantities.Clear();
    }

    // Displays the recipe details to the console.
    public void DisplayRecipe()
    {
        Console.WriteLine("Recipe Details:");
        Console.WriteLine("Ingredients:");
        foreach (var (name, quantity, unit) in ingredients)
        {
            Console.WriteLine($"- {quantity} {unit} of {name}");
        }
        Console.WriteLine("\nSteps:");
        for (int i = 0; i < steps.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {steps[i]}");
        }
    }
}

// The main program class.
class Program
{
    // The entry point of the program.
    static void Main(string[] args)
    {
        Recipe recipe = new Recipe();

        // User input for ingredients.
        Console.WriteLine("Enter the number of ingredients:");
        int numIngredients = int.Parse(Console.ReadLine());
        for (int i = 0; i < numIngredients; i++)
        {
            Console.WriteLine($"Enter ingredient {i + 1} name:");
            string name = Console.ReadLine();
            Console.WriteLine($"Enter ingredient {i + 1} quantity:");
            double quantity = double.Parse(Console.ReadLine());
            Console.WriteLine($"Enter ingredient {i + 1} unit:");
            string unit = Console.ReadLine();

            recipe.AddIngredient(name, quantity, unit);
        }

        // User input for steps.
        Console.WriteLine("Enter the number of steps:");
        int numSteps = int.Parse(Console.ReadLine());
        for (int i = 0; i < numSteps; i++)
        {
            Console.WriteLine($"Enter step {i + 1}:");
            string step = Console.ReadLine();
            recipe.AddStep(step);
        }

        // Scaling the recipe.
        Console.WriteLine("Enter the scaling factor (0.5 for half, 2 for double, 3 for triple):");
        double factor = double.Parse(Console.ReadLine());
        recipe.ScaleQuantities(factor);

        // Display the scaled recipe.
        recipe.DisplayRecipe();

        // Resetting the recipe quantities to original.
        Console.WriteLine("Would you like to reset the quantities to original? (yes/no)");
        string resetChoice = Console.ReadLine().ToLower();
        if (resetChoice == "yes")
        {
            recipe.ResetQuantities();
            recipe.DisplayRecipe();
        }

        // Clearing the recipe data.
        Console.WriteLine("Would you like to clear the recipe to start a new one? (yes/no)");
        string clearChoice = Console.ReadLine().ToLower();
        if (clearChoice == "yes")
        {
            recipe.ClearRecipe();
        }

        
    }
}
