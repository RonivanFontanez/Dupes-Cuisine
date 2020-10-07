using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{

    public class Food_Meat_Taco : IEntityConfig
    {
        public const string Id = "MeatTaco";

        public static string Name = UI.FormatAsLink("Meat Taco", Id.ToUpper());

        public static string Description = $"A filling meal made with slowly baked {UI.FormatAsLink("Meat", MeatConfig.ID)} with {UI.FormatAsLink("Omellete", CookedEggConfig.ID)}, and a pinch of {UI.FormatAsLink("Pepper Nut", SpiceNutConfig.ID)}, all served with a {UI.FormatAsLink("Warm Flat Bread", Food_FlatBread.Id)}. It promptly leaves a warm sensation while it goes inside, as well when it leaves.";

        public static string RecipeDescription = $"Bake a {UI.FormatAsLink("Meat Taco", Food_Meat_Taco.Id)}";

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
                anim: Assets.GetAnim("food_meat_taco_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.8f,
                height: 0.4f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 6200000f,
                quality: TUNING.FOOD.FOOD_QUALITY_MORE_WONDERFUL,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 2400f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            ComplexRecipe.RecipeElement[] elementArray1 = new ComplexRecipe.RecipeElement[] { new ComplexRecipe.RecipeElement(Food_FlatBread.Id, 1f), new ComplexRecipe.RecipeElement("Meat", 1f), new ComplexRecipe.RecipeElement("CookedEgg", 1f), new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 1f) };
            ComplexRecipe.RecipeElement[] elementArray2 = new ComplexRecipe.RecipeElement[] { new ComplexRecipe.RecipeElement("MeatTaco", 1f) };
            ComplexRecipe meatTacoRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("GourmetCookingStation", elementArray1, elementArray2), elementArray1, elementArray2);
            meatTacoRecipe.time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME;
            meatTacoRecipe.description = Food_Meat_Taco.RecipeDescription;
            meatTacoRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            List<Tag> list6 = new List<Tag>();
            list6.Add("GourmetCookingStation");
            meatTacoRecipe.fabricators = list6;
            Food_Meat_Taco.Recipe = meatTacoRecipe;

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }

    }
}

