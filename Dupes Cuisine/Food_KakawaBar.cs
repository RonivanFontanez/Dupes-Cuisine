using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_KakawaBar : IEntityConfig
    {
        public const string Id = "KakawaBar";

        public static string Name = UI.FormatAsLink("Kakawa Bar", Id.ToUpper());

        public static string Description = $"{UI.FormatAsLink("Roasted Kakawa", Food_RoastedKakawa.Id)} compressed to a dense, buttery mass. Further cooking remove most of Kakawa bitterness, rendering this bar incredible tasty.";

        public static string RecipeDescription = $"Compress a {UI.FormatAsLink("kakawa Bar", Food_KakawaBar.Id)}";

        public ComplexRecipe Recipe;


        public static GameObject CreateFabricationVisualizer(GameObject result)
        {
            KBatchedAnimController component = result.GetComponent<KBatchedAnimController>();
            GameObject target = new GameObject();
            target.name = result.name + "Visualizer";
            target.SetActive(false);
            target.transform.SetLocalPosition(Vector3.zero);
            KBatchedAnimController local1 = target.AddComponent<KBatchedAnimController>();
            local1.AnimFiles = component.AnimFiles;
            local1.initialAnim = "fabricating";
            local1.isMovable = true;
            KBatchedAnimTracker local2 = target.AddComponent<KBatchedAnimTracker>();
            local2.symbol = new HashedString("meter_ration");
            local2.offset = Vector3.zero;
            local2.skipInitialDisable = true;
            UnityEngine.Object.DontDestroyOnLoad(target);
            return target;
        }

        public GameObject CreatePrefab()
        {

            var looseEntity = EntityTemplates.CreateLooseEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                unitMass: false,
                anim: Assets.GetAnim("food_kakawa_bar_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.8f,
                height: 0.4f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 2000000f,
                quality: TUNING.FOOD.FOOD_QUALITY_GOOD,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 2400f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            var input = new[] { new ComplexRecipe.RecipeElement(Food_RoastedKakawa.Id, 2f), new ComplexRecipe.RecipeElement(Food_KakawaButter.Id, 1f) };
            var output = new[] { new ComplexRecipe.RecipeElement(Id, 1f) };
            /* KCAL CALCULATION
             Roasted Kakawa (300 kcal/unit) = 600 + 500
             Kakawa Butter (900 kcal/unit) = 900
            */

            var recipeId = ComplexRecipeManager.MakeRecipeID(
                fabricator: MicrobeMusherConfig.ID,
                inputs: input,
                outputs: output);

            Recipe = new ComplexRecipe(recipeId, input, output)
            {
                time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME,
                description = RecipeDescription,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { MicrobeMusherConfig.ID },
                sortOrder = 2,
                requiredTech = null
            };
            ComplexRecipeManager.Get().GetRecipe(Recipe.id).FabricationVisualizer = CreateFabricationVisualizer(foodEntity);

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }

    }
}
