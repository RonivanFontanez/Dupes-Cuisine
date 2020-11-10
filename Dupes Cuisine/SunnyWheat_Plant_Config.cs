using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class SunnyWheat_Plant_Config : IEntityConfig
    {
        public const string Id = "SunnyWheat";
        public const string SeedId = "SunnyWheat_Bulb";

        public static string Name = UI.FormatAsLink("Sunny Wheat", Id.ToUpper());
        public static string SeedName = UI.FormatAsLink("Sunny Wheat Bulb", Id.ToUpper());

        public static string Description = $"{Name} produces {UI.FormatAsLink("Sunny Wheat Grain", SunnyWheat_Grain_Config.Id)}, a warm grain that can be processed in to food.";
        public static string SeedDescription = $"An outgrowth bulb from a {UI.FormatAsLink("Sunny Wheat", SunnyWheat_Plant_Config.Id)}. Planting this bulb is more reliable for growing a new wheat plant than using the actual grain.";
        public static string DomesticatedDescription = $"This wheat as engineered to survive well in harsh, dry environments, with little water avaliable. This domesticated variety require little {UI.FormatAsLink("Water", "WATER")}. Also require {UI.FormatAsLink("Sand", "SAND")} as fertilizer.";

        public const float DefaultTemperature = 321.15f;          //-> 48°C
        public const float TemperatureLethalLow = 273.15f;        //-> 0°C  <- crop dies
        public const float TemperatureWarningLow = 313.15f;       //-> 40°C <- crop stops growing
        public const float TemperatureWarningHigh = 343.15f;      //-> 70°C <- crop stops growing
        public const float TemperatureLethalHigh = 348.15f;       //-> 75°C <- crop dies

        public const float IrrigationRate_A = 0.015f;
        public const float IrrigationRate_B = 0.04f;

        public ComplexRecipe Recipe;

        public GameObject CreatePrefab()
        {
            var placedEntity = EntityTemplates.CreatePlacedEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                anim: Assets.GetAnim("sunnyWheat_plant_kanim"),
                initialAnim: "idle_empty",
                sceneLayer: Grid.SceneLayer.BuildingFront,
                width: 1,
                height: 1,
                decor: TUNING.DECOR.BONUS.TIER1,
                defaultTemperature: DefaultTemperature
                );

            EntityTemplates.ExtendEntityToBasicPlant(
                template: placedEntity,
                temperature_lethal_low: TemperatureLethalLow,
                temperature_warning_low: TemperatureWarningLow,
                temperature_warning_high: TemperatureWarningHigh,
                temperature_lethal_high: TemperatureLethalHigh,
                safe_elements: new[] { SimHashes.Oxygen, SimHashes.CarbonDioxide, SimHashes.ContaminatedOxygen, SimHashes.ChlorineGas },
                pressure_sensitive: true,
                pressure_lethal_low: 0.0f,
                pressure_warning_low: 0.15f,
                crop_id: SunnyWheat_Grain_Config.Id);

            EntityTemplates.ExtendPlantToIrrigated(
                template: placedEntity,
                info: new PlantElementAbsorber.ConsumeInfo()
                {
                    tag = GameTags.Water,
                    massConsumptionRate = IrrigationRate_A
                });

            PlantElementAbsorber.ConsumeInfo info = new PlantElementAbsorber.ConsumeInfo
            {
                tag = SimHashes.Sand.CreateTag(),
                massConsumptionRate = IrrigationRate_B
            };
            PlantElementAbsorber.ConsumeInfo[] fertilizers = new PlantElementAbsorber.ConsumeInfo[] { info };
            EntityTemplates.ExtendPlantToFertilizable(template: placedEntity, fertilizers: fertilizers);

            placedEntity.AddOrGet<StandardCropPlant>();

            var seed = EntityTemplates.CreateAndRegisterSeedForPlant(
                plant: placedEntity,
                productionType: SeedProducer.ProductionType.Harvest,
                id: SeedId,
                name: SeedName,
                desc: SeedDescription,
                anim: Assets.GetAnim("sunnyWheat_bulb_kanim"),
                initialAnim: "object",
                numberOfSeeds: 1,
                additionalTags: new List<Tag>() { GameTags.CropSeed },
                planterDirection: SingleEntityReceptacle.ReceptacleDirection.Top,
                replantGroundTag: new Tag(),
                sortOrder: 2,
                domesticatedDescription: DomesticatedDescription,
                collisionShape: EntityTemplates.CollisionShape.CIRCLE,
                width: 0.2f,
                height: 0.2f);

            EntityTemplates.CreateAndRegisterPreviewForPlant(
                seed: seed,
                id: $"{Id}_preview",
                anim: Assets.GetAnim("sunnyWheat_plant_kanim"),
                initialAnim: "place",
                width: 1,
                height: 1);

            //===> SEED RECIPE <=======================================================================
            var input = new[] { new ComplexRecipe.RecipeElement(LeafyPlantConfig.SEED_ID, 5f) };
            var output = new[] { new ComplexRecipe.RecipeElement(SeedId, 1f) };

            var recipeId = ComplexRecipeManager.MakeRecipeID(
                fabricator: KilnConfig.ID,
                inputs: input,
                outputs: output);

            Recipe = new ComplexRecipe(recipeId, input, output)
            {
                time = TUNING.FOOD.RECIPES.SMALL_COOK_TIME,
                description = "Chemistry works in mysterious ways.",
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { KilnConfig.ID },
                sortOrder = 2,
                requiredTech = null
            };
            //=========================================================================================

            SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", TUNING.NOISE_POLLUTION.CREATURES.TIER1);
            return placedEntity;
        }

        public void OnPrefabInit(GameObject inst) { }

        public void OnSpawn(GameObject inst) { }
    }
}
