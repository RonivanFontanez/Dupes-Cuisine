using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{ 

        public class Creamtop_Config : IEntityConfig
        {
            public const string Id = "CreamtopMushroom";
            public const string SeedId = "CreamtopSpore";

            public static string Name = UI.FormatAsLink("Creamtop Mushroom", Id.ToUpper());
            public static string SeedName = UI.FormatAsLink("Creamtop Spore", Id.ToUpper());

            public static string Description = $"A soft white mushroom with a creamie texture on its cap. Despite growing in filthy dark spots, it is edible.";
            public static string SeedDescription = $"A small spore from the {Name}. Plant it in a dark place and its surely to flourish.";
            public static string DomesticatedDescription = $"This {UI.FormatAsLink("Mushroom", "PLANTS")} is suitable for cultivation. To thrive, it requires a dark spot and {UI.FormatAsLink("Polluted Dirty", "TOXICSAND")} as fertilizer.";

            public const float DefaultTemperature = 298.15f;      //25°C
            public const float TemperatureLethalLow = 277.15f;    //5°C
            public const float TemperatureWarningLow = 283.15f;   //10°C <-- stop growing
            public const float TemperatureWarningHigh = 309.15f;  //36°C <--
            public const float TemperatureLethalHigh = 313.15f;   //40°C

            public const float IrrigationRate = 0.03333334f;

            public ComplexRecipe Recipe;

            public GameObject CreatePrefab()
            {
                var placedEntity = EntityTemplates.CreatePlacedEntity(
                    id: Id,
                    name: Name,
                    desc: Description,
                    mass: 1f,
                    anim: Assets.GetAnim("creamtop_plant_kanim"),
                    initialAnim: "idle_empty",
                    sceneLayer: Grid.SceneLayer.BuildingFront,
                    width: 1,
                    height: 2,
                    decor: TUNING.DECOR.BONUS.TIER1,
                    defaultTemperature: DefaultTemperature
                    );

                EntityTemplates.ExtendEntityToBasicPlant(
                    template: placedEntity,
                    temperature_lethal_low: TemperatureLethalLow,
                    temperature_warning_low: TemperatureWarningLow,
                    temperature_warning_high: TemperatureWarningHigh,
                    temperature_lethal_high: TemperatureLethalHigh,
                    safe_elements: new[] { SimHashes.Oxygen, SimHashes.CarbonDioxide },
                    pressure_sensitive: true,
                    pressure_lethal_low: 0.0f,
                    pressure_warning_low: 0.15f,
                    crop_id: Creamtop_Cap_Config.Id);

          
            PlantElementAbsorber.ConsumeInfo info = new PlantElementAbsorber.ConsumeInfo
            {
                tag = SimHashes.DirtyWater.CreateTag(),
                massConsumptionRate = IrrigationRate
            };
            PlantElementAbsorber.ConsumeInfo[] fertilizers = new PlantElementAbsorber.ConsumeInfo[] { info };
            EntityTemplates.ExtendPlantToFertilizable(template: placedEntity, fertilizers: fertilizers );

            placedEntity.AddOrGet<IlluminationVulnerable>().SetPrefersDarkness(true);
            placedEntity.AddOrGet<StandardCropPlant>();

                var seed = EntityTemplates.CreateAndRegisterSeedForPlant(
                    plant: placedEntity,
                    productionType: SeedProducer.ProductionType.Harvest,
                    id: SeedId,
                    name: SeedName,
                    desc: SeedDescription,
                    anim: Assets.GetAnim("creamtop_spore_kanim"),
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
                    anim: Assets.GetAnim("creamtop_plant_kanim"),
                    initialAnim: "place",
                    width: 1,
                    height: 1);

            //===> SEED RECIPE <=======================================================================
            var input = new[] { new ComplexRecipe.RecipeElement(MushroomPlantConfig.SEED_ID, 1f) };
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
