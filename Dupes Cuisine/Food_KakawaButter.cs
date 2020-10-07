using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_KakawaButter : IEntityConfig
    {
        public const string Id = "KakawaButter";

        public static string Name = UI.FormatAsLink("Kakawa Butter", Id.ToUpper());

        public static string Description = $"An oily butter extracted from {CocoaOak_Acorn_Config.Name}. This butter has a tasty aroma, although one must like bitterness to actually eat it in this form.";

        public static string RecipeDescription = $"Extract oil from {CocoaOak_Acorn_Config.Name}.";

        public ComplexRecipe Recipe;

        public GameObject CreatePrefab()
        {
            var looseEntity = EntityTemplates.CreateLooseEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                unitMass: false,
                anim: Assets.GetAnim("food_kakawa_butter_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.8f,
                height: 0.4f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 0f,
                quality: -1,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 2400f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            var input = new[] { new ComplexRecipe.RecipeElement(CocoaOak_Acorn_Config.Id, 3f) };
            var output = new[] { new ComplexRecipe.RecipeElement(Id, 1f) };

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
                sortOrder = 4,
                requiredTech = null
            };

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }

    }
}
