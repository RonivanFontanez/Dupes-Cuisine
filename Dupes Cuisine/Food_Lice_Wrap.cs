using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_Lice_Wrap : IEntityConfig
    {
        public const string Id = "LiceWrap";

        public static string Name = UI.FormatAsLink("Lice Wrap", Id.ToUpper());

        public static string Description = $"A dubious snack made by wrapping fresh {UI.FormatAsLink("Meal Lice", BasicPlantFoodConfig.ID)} with {UI.FormatAsLink("Warm Flat Bread", Food_FlatBread.Id)}. The warm flavor from the bread mitigates regretable texture of filling.";

        public static string RecipeDescription = $"Bake a {UI.FormatAsLink("Lice Wrap", Food_Lice_Wrap.Id)}";

        public static ComplexRecipe Recipe;


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
                anim: Assets.GetAnim("food_lice_wrap_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.8f,
                height: 0.4f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 2400000f,
                quality: TUNING.FOOD.FOOD_QUALITY_MEDIOCRE,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 2400f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            ComplexRecipe.RecipeElement[] elementArray1 = new ComplexRecipe.RecipeElement[] { new ComplexRecipe.RecipeElement(Food_FlatBread.Id, 1f), new ComplexRecipe.RecipeElement("BasicPlantFood", 1f) };
            ComplexRecipe.RecipeElement[] elementArray2 = new ComplexRecipe.RecipeElement[] { new ComplexRecipe.RecipeElement("LiceWrap", 1f) };
            ComplexRecipe liceWrapRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", elementArray1, elementArray2), elementArray1, elementArray2);
            liceWrapRecipe.time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME;
            liceWrapRecipe.description = Food_Lice_Wrap.RecipeDescription;
            liceWrapRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            List<Tag> list5 = new List<Tag>();
            list5.Add("CookingStation");
            liceWrapRecipe.fabricators = list5;
            Food_Lice_Wrap.Recipe = liceWrapRecipe;

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }

    }
}
