﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class Food_Meat_Wrap : IEntityConfig
    {
        public const string Id = "MeatWrap";

        public static string Name = UI.FormatAsLink("Meat Wrap", Id.ToUpper());

        public static string Description = $"A tasty snack made by wrapping grilled {UI.FormatAsLink("Meat", MeatConfig.ID)} with {UI.FormatAsLink("Warm Flat Bread", Food_FlatBread.Id)}. Each bite leaves a mild warm sensation in one's mouth, even when the snack itself is served cold.";

        public static string RecipeDescription = $"Bake a {UI.FormatAsLink("Meat Wrap", Food_Meat_Wrap.Id)}";

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
                anim: Assets.GetAnim("food_meat_wrap_kanim"),
                initialAnim: "object",
                sceneLayer: Grid.SceneLayer.Front,
                collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
                width: 0.8f,
                height: 0.4f,
                isPickupable: true);

            var foodInfo = new EdiblesManager.FoodInfo(
                id: Id,
                caloriesPerUnit: 3000000f,
                quality: TUNING.FOOD.FOOD_QUALITY_GOOD,
                preserveTemperatue: 255.15f,
                rotTemperature: 277.15f,
                spoilTime: 2400f,
                can_rot: true);

            var foodEntity = EntityTemplates.ExtendEntityToFood(
                template: looseEntity,
                foodInfo: foodInfo);

            ComplexRecipe.RecipeElement[] elementArray1 = new ComplexRecipe.RecipeElement[] { new ComplexRecipe.RecipeElement(Food_FlatBread.Id, 1f), new ComplexRecipe.RecipeElement("Meat", 1f) };
            ComplexRecipe.RecipeElement[] elementArray2 = new ComplexRecipe.RecipeElement[] { new ComplexRecipe.RecipeElement("MeatWrap", 1f) };
            ComplexRecipe meatWrapRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", elementArray1, elementArray2), elementArray1, elementArray2);
            meatWrapRecipe.time = TUNING.FOOD.RECIPES.STANDARD_COOK_TIME;
            meatWrapRecipe.description = Food_Meat_Wrap.RecipeDescription;
            meatWrapRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
            List<Tag> list5 = new List<Tag>();
            list5.Add("CookingStation");
            meatWrapRecipe.fabricators = list5;
            Food_Meat_Wrap.Recipe = meatWrapRecipe;

            return foodEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }

    }
}
