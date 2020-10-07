using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_RoastedKakawa : IEntityConfig
    {
        public const string Id = "Roasted_Kakawa";

        public static string Name = UI.FormatAsLink("Roasted Kakawa", Id.ToUpper());

        public static string Description = $"A fully roasted {CocoaOak_Acorn_Config.Name}. The roasting crack open its hard shell reaveling a edible nut, although the eating may be a bitter experience.";

        public static string RecipeDescription = $"Roast a {CocoaOak_Acorn_Config.Name}.";

        public ComplexRecipe Recipe;

        public GameObject CreatePrefab()
        {
            var looseEntity = EntityTemplates.CreateLooseEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                unitMass: false,
                anim: Assets.GetAnim("food_roasted_kakawa_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.8f,
                height: 0.4f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 300000f,
                quality: TUNING.FOOD.FOOD_QUALITY_MEDIOCRE,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 2400f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            var input = new[] { new ComplexRecipe.RecipeElement(CocoaOak_Acorn_Config.Id, 1f) };
            var output = new[] { new ComplexRecipe.RecipeElement(Id, 1f) };

            var recipeId = ComplexRecipeManager.MakeRecipeID(
                fabricator: CookingStationConfig.ID,
                inputs: input,
                outputs: output);

            Recipe = new ComplexRecipe(recipeId, input, output)
            {
                time = TUNING.FOOD.RECIPES.SMALL_COOK_TIME,
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
