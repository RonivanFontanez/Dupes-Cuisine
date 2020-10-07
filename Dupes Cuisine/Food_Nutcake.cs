using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_Nutcake : IEntityConfig
    {
        public const string Id = "Nut_cake";

        public static string Name = UI.FormatAsLink("Nutcake", Id.ToUpper());

        public static string Description = $"A saborous {UI.FormatAsLink("Nutcake", Food_Nutcake.Id)} baked from bitter nuts, spieces and wheat. Brings warmth to the heart (and stomach).";

        public static string RecipeDescription = $"Bake a {UI.FormatAsLink("Nutcake", Food_Nutcake.Id)} .";

        public ComplexRecipe Recipe;

        public GameObject CreatePrefab()
        {
            var looseEntity = EntityTemplates.CreateLooseEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                unitMass: false,
                anim: Assets.GetAnim("food_nutcake_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.7f,
                height: 0.5f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 4800000f,
                quality: TUNING.FOOD.FOOD_QUALITY_MORE_WONDERFUL,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 3200f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            var input = new[] { new ComplexRecipe.RecipeElement(SunnyWheat_Grain_Config.Id, 8f), new ComplexRecipe.RecipeElement(Food_RoastedKakawa.Id, 5f), new ComplexRecipe.RecipeElement(Food_KakawaButter.Id, 1f), new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 1f) };
            var output = new[] { new ComplexRecipe.RecipeElement(Id, 1f) };
            /* KCAL CALCULATION
            Sunny Wheat Grain (200 kcal/unit) = 1200 + 400
            Roasted Kakawa (300 kcal/unit) = 1500 + 400
            Kakawa Butter (900 kcal/unit) = 900
            Pincha Peppernut (400 kcal/unit) = 400
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
                sortOrder = 2,
                requiredTech = null
            };

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }
    }
}
