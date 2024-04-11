using System;

namespace RecipeApp
{
    // Main Program class responsible for user interaction
    class Program
    {
        static void Main(string[] args)
        {
            RecipeManager recipeManager = new RecipeManager(); // Create an instance of RecipeManager
            bool isRunning = true;

            // Main loop to display menu and process user input
            while (isRunning)
            {
                Console.WriteLine();
                DisplayMenu(); // Display menu options to the user

                // Get user choice and validate input
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 6)
                {
                    Console.WriteLine("Please enter a valid option (1-6).");
                    Console.Write("Enter your choice: ");
                }

                // Process user choice
                switch (choice)
                {
                    case 1:
                        recipeManager.EnterRecipeDetails(); // Call method to enter recipe details
                        break;
                    case 2:
                        recipeManager.DisplayRecipe(); // Call method to display recipe
                        break;
                    case 3:
                        recipeManager.ScaleRecipe(); // Call method to scale recipe
                        break;
                    case 4:
                        recipeManager.ResetQuantities(); // Call method to reset quantities
                        break;
                    case 5:
                        recipeManager.ClearAllDataWithConfirmation(); // Call method to clear all data with confirmation
                        break;
                    case 6:
                        isRunning = false; // Exit the program
                        Console.WriteLine("\u001b[31mExiting...\u001b[0m"); // Red color for exit message
                        break;
                }
            }
        }

        // Method to display menu options
        static void DisplayMenu()
        {
            Console.WriteLine("1. Enter Recipe Details");
            Console.WriteLine("2. Display Recipe");
            Console.WriteLine("3. Scale Recipe");
            Console.WriteLine("4. Reset Quantities");
            Console.WriteLine("5. Clear All Data");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
        }
    }

    // Class responsible for managing recipe data and operations
    class RecipeManager
    {
        private string[] ingredients;
        private double[] quantities;
        private string[] units;
        private string[] steps;
        private double scaleFactor = 1.0;

        // Method to enter recipe details
        public void EnterRecipeDetails()
        {
            ClearAllData(); // Clear existing data before entering new recipe details

            Console.WriteLine();

            // Get the number of ingredients from the user
            Console.Write("Enter number of ingredients: ");
            int numIngredients;
            while (!int.TryParse(Console.ReadLine(), out numIngredients) || numIngredients <= 0)
            {
                Console.WriteLine("Please enter a valid positive integer for the number of ingredients.");
                Console.Write("Enter number of ingredients: ");
            }

            // Allocate memory for arrays based on the number of ingredients
            ingredients = new string[numIngredients];
            quantities = new double[numIngredients];
            units = new string[numIngredients];

            // Get details for each ingredient
            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine();
                Console.Write($"Enter name of ingredient {i + 1}: ");
                ingredients[i] = Console.ReadLine();

                Console.Write($"Enter quantity of {ingredients[i]}: ");
                while (!double.TryParse(Console.ReadLine(), out quantities[i]) || quantities[i] <= 0)
                {
                    Console.WriteLine("Please enter a valid positive number for the quantity.");
                    Console.Write($"Enter quantity of {ingredients[i]}: ");
                }

                Console.Write($"Enter unit of measurement for {ingredients[i]}: ");
                units[i] = Console.ReadLine();
            }

            // Get the number of steps from the user
            Console.WriteLine();
            Console.Write("Enter number of steps: ");
            int numSteps;
            while (!int.TryParse(Console.ReadLine(), out numSteps) || numSteps <= 0)
            {
                Console.WriteLine("Please enter a valid positive integer for the number of steps.");
                Console.Write("Enter number of steps: ");
            }

            // Allocate memory for the steps array based on the number of steps
            steps = new string[numSteps];

            // Get details for each step
            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine();
                Console.Write($"Enter step {i + 1}: ");
                steps[i] = Console.ReadLine();
            }
        }

        // Method to display recipe
        public void DisplayRecipe()
        {
            Console.WriteLine("\nRecipe Details:");
            for (int i = 0; i < ingredients.Length; i++)
            {
                Console.WriteLine($"\u001b[32m{quantities[i] * scaleFactor} {units[i]}\u001b[0m of \u001b[33m{ingredients[i]}\u001b[0m"); // Green color for units, Yellow color for ingredients
            }

            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Length; i++)
            {
                Console.WriteLine($"{i + 1}. \u001b[34m{steps[i]}\u001b[0m"); // Blue color for steps
            }
        }

        // Method to scale recipe
        public void ScaleRecipe()
        {
            Console.Write("Enter scale factor (0.5, 2, or 3): ");
            double factor;
            while (!double.TryParse(Console.ReadLine(), out factor) || (factor != 0.5 && factor != 2 && factor != 3))
            {
                Console.WriteLine("Invalid scale factor. Please enter 0.5, 2, or 3.");
                Console.Write("Enter scale factor: ");
            }

            scaleFactor = factor;
            Console.WriteLine($"Recipe scaled by {scaleFactor}x.");
        }

        // Method to reset quantities
        public void ResetQuantities()
        {
            scaleFactor = 1.0;
            Console.WriteLine("Quantities reset to original values.");
        }

        // Method to clear all data with confirmation
        public void ClearAllDataWithConfirmation()
        {
            Console.WriteLine("Are you sure you want to clear all data? (Y/N)");
            char choice = Console.ReadLine().ToUpper()[0];
            if (choice == 'Y')
            {
                ClearAllData();
                Console.WriteLine("\u001b[31mAll data cleared.\u001b[0m"); // Red color for confirmation message
            }
        }

        // Method to clear all data
        private void ClearAllData()
        {
            ingredients = null;
            quantities = null;
            units = null;
            steps = null;
            scaleFactor = 1.0;
        }
    }
}
