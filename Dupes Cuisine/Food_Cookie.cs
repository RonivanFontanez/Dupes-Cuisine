using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_Cookie : IEntityConfig
    {
        public const string Id = "KakawaCookie";

        public static string Name = UI.FormatAsLink("Frost Cookie", Id.ToUpper());

        public static string Description = $"A buttery cookie with a subtle bittersweet, cold flavor.";

        public static string RecipeDescription = $"Bake a {UI.FormatAsLink("Frost Cookie", Food_Cookie.Id)}.";

        public ComplexRecipe Recipe;

        public GameObject CreatePrefab()
        {
            var looseEntity = EntityTemplates.CreateLooseEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                unitMass: false,
                anim: Assets.GetAnim("food_cookie_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.7f,
                height: 0.5f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 5000000f,
                quality: TUNING.FOOD.FOOD_QUALITY_AMAZING,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 3200f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            var input = new[] { new ComplexRecipe.RecipeElement(ColdWheatConfig.SEED_ID, 4f), new ComplexRecipe.RecipeElement(Food_KakawaBar.Id, 1f), new ComplexRecipe.RecipeElement(Food_KakawaButter.Id, 1f) };
            var output = new[] { new ComplexRecipe.RecipeElement(Id, 1f) };
            /* KCAL CALCULATION
             Sleet Wheat Grains (400 kcal/unit) = 1600 + 500
             Kakawa Bar (2000 kcal/unit) = 2000
             Kakawa Butter (900 kcal/unit) = 900
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
                sortOrder = 3,
                requiredTech = null
            };

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }
    }
}
