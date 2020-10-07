using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_MushroomStew : IEntityConfig
    {
        public const string Id = "MushroomStew";

        public static string Name = UI.FormatAsLink("Mushroom Stew", Id.ToUpper());

        public static string Description = $"A thick, flavorful soup made by simmering mushroons and spices, placed within a hallowed Frost Bun and baked in a oven." ;

        public static string RecipeDescription = $"Bake a {UI.FormatAsLink("Mushroom Stew", Food_MushroomStew.Id)}.";

        public ComplexRecipe Recipe;

        public GameObject CreatePrefab()
        {
            var looseEntity = EntityTemplates.CreateLooseEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                unitMass: false,
                anim: Assets.GetAnim("food_mushroom_stew_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.7f,
                height: 0.5f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 7000000f,
                quality: TUNING.FOOD.FOOD_QUALITY_MORE_WONDERFUL,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 1200f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            var input = new[] { new ComplexRecipe.RecipeElement(ColdWheatConfig.SEED_ID, 3f), new ComplexRecipe.RecipeElement(Food_GrilledCreamtop.Id, 3f), new ComplexRecipe.RecipeElement(ColdWheatBreadConfig.ID, 1f) };
            var output = new[] { new ComplexRecipe.RecipeElement(Id, 1f) };
            /* KCAL CALCULATION
               Sleet Wheat Grains (400 kcal/unit) = 1200
               Grilled Creamtop (1200 kcal/unit) = 3600
               Pincha Peppernut (400 kcal/unit) = 800
               Frost Bun (1200 kcal/unit) = 1200 + 200
            */

            var recipeId = ComplexRecipeManager.MakeRecipeID(
                fabricator: GourmetCookingStationConfig.ID,
                inputs: input,
                outputs: output);

            Recipe = new ComplexRecipe(recipeId, input, output)
            {
                time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME,
                description = RecipeDescription,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { GourmetCookingStationConfig.ID },
                sortOrder = 4,
                requiredTech = null
            };

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }
    }
}
