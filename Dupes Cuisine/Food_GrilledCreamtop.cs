using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_GrilledCreamtop : IEntityConfig
    {
        public const string Id = "Grilled_Creamtop";

        public static string Name = UI.FormatAsLink("Grilled Creamtop", Id.ToUpper());

        public static string Description = $"The grilled fruiting of a {UI.FormatAsLink("Creamtop", Creamtop_Config.Id)}. It has a crispy texture and a soft, mildly sweet, earthy flavor.";

        public static string RecipeDescription = $"Grills a {UI.FormatAsLink("Creamtop Cap", Creamtop_Cap_Config.Id)}.";

        public ComplexRecipe Recipe;

        public GameObject CreatePrefab()
        {
            var looseEntity = EntityTemplates.CreateLooseEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                unitMass: false,
                anim: Assets.GetAnim("food_grilled_creamtop_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.8f,
                height: 0.4f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 1800000f,
                quality: TUNING.FOOD.FOOD_QUALITY_MEDIOCRE,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 2400f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            var input = new[] { new ComplexRecipe.RecipeElement(Creamtop_Cap_Config.Id, 1f) };
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
                sortOrder = 2,
                requiredTech = null
            };

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }

    }
}
