using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_FlatBread : IEntityConfig
    {
        public const string Id = "FlatBread";

        public static string Name = UI.FormatAsLink("Warm Flat Bread", Id.ToUpper());

        public static string Description = $"A simple flat bread baked from {UI.FormatAsLink("Sunny Wheat Grain", SunnyWheat_Grain_Config.Id)}. Each bite leaves a mild warmth sensation in one's mouth, even when the bread itself is cold.";

        public static string RecipeDescription = $"Bake a {UI.FormatAsLink("Warm Flat Bread", Food_FlatBread.Id)}.";

        public ComplexRecipe Recipe;

        public GameObject CreatePrefab()
        {
            var looseEntity = EntityTemplates.CreateLooseEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                unitMass: false,
                anim: Assets.GetAnim("food_flat_bread_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.8f,
                height: 0.4f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 900000f,
                quality: TUNING.FOOD.FOOD_QUALITY_TERRIBLE,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 2400f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            var input = new[] { new ComplexRecipe.RecipeElement(SunnyWheat_Grain_Config.Id, 3f) };
            var output = new[] { new ComplexRecipe.RecipeElement(Id, 1f) };
            /*KCAL CALCULATION 
             Sunny Wheat Grain (200 kcal/unit) = 600
             Cooking Process = 400
             */

            var recipeId = ComplexRecipeManager.MakeRecipeID(
                fabricator: CookingStationConfig.ID,
                inputs: input,
                outputs: output);

            Recipe = new ComplexRecipe(recipeId, input, output)
            {
                time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME,
                description = RecipeDescription,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { CookingStationConfig.ID },
                sortOrder = 3,
                requiredTech = null
            };

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }

    }
}
