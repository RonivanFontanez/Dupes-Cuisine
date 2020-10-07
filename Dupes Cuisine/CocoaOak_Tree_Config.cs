using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;

namespace Dupes_Cuisine
{
    public class CocoaOak_Tree_Config : IEntityConfig
    {
        public const string Id = "KakawaTree";
        public const string SeedId = "KakawaSeed";

        public static string Name = UI.FormatAsLink("Kakawa Tree", Id.ToUpper());
        public static string SeedName = UI.FormatAsLink("Kakawa Seed", Id.ToUpper());

        public static string Description = $"The lush {Name} produces a rock hard {UI.FormatAsLink("Acorn", CocoaOak_Acorn_Config.Id)}.";
        public static string SeedDescription = $"The seed of a {Name}. Requires a long time to grow, but yeld lots of edible nuts.";
        public static string DomesticatedDescription = $"A cultivable {UI.FormatAsLink("Plant", "PLANTS")} survives well in harsh environment. It thrives only with proper illumination, and needs copious amounts of {UI.FormatAsLink("Water", "WATER")}, and {UI.FormatAsLink("Dirt", "DIRT")} as fertilizer.";

        public const float DefaultTemperature = 305.15f;       // -> 32°C
        public const float TemperatureLethalLow = 273.15f;     // -> 0°C
        public const float TemperatureWarningLow = 283.15f;    // -> 10°C <-- stop growing
        public const float TemperatureWarningHigh = 313.15f;   // -> 40°C <--
        public const float TemperatureLethalHigh = 321.15f;    // -> 48°C

        public const float IrrigationRate_A = 0.04f;
        public const float IrrigationRate_B = 0.02f;

        public ComplexRecipe Recipe;

        public GameObject CreatePrefab()
        {
            var placedEntity = EntityTemplates.CreatePlacedEntity(
                id: Id,
                name: Name,
                desc: Description,
                mass: 1f,
                anim: Assets.GetAnim("cocoaOak_plant_kanim"),
                initialAnim: "idle_empty",
                sceneLayer: Grid.SceneLayer.BuildingFront,
                width: 3,
                height: 3,
                decor: TUNING.DECOR.BONUS.TIER1,
                defaultTemperature: DefaultTemperature
                );

            EntityTemplates.ExtendEntityToBasicPlant(
                template: placedEntity,
                temperature_lethal_low: TemperatureLethalLow,
                temperature_warning_low: TemperatureWarningLow,
                temperature_warning_high: TemperatureWarningHigh,
                temperature_lethal_high: TemperatureLethalHigh,
                safe_elements: new[] { SimHashes.Oxygen, SimHashes.CarbonDioxide, SimHashes.ContaminatedOxygen },
                pressure_sensitive: true,
                pressure_lethal_low: 0.0f,
                pressure_warning_low: 0.15f,
                crop_id: CocoaOak_Acorn_Config.Id);

            EntityTemplates.ExtendPlantToIrrigated(
                template: placedEntity,
                info: new PlantElementAbsorber.ConsumeInfo()
                {
                    tag = GameTags.Water,
                    massConsumptionRate = IrrigationRate_A
                });

            PlantElementAbsorber.ConsumeInfo info = new PlantElementAbsorber.ConsumeInfo
            {
                tag = SimHashes.Dirt.CreateTag(),
                massConsumptionRate = IrrigationRate_B
            };
            PlantElementAbsorber.ConsumeInfo[] fertilizers = new PlantElementAbsorber.ConsumeInfo[] { info };
            EntityTemplates.ExtendPlantToFertilizable(template: placedEntity, fertilizers: fertilizers);

            placedEntity.AddOrGet<IlluminationVulnerable>().SetPrefersDarkness(false);
            placedEntity.AddOrGet<StandardCropPlant>();

            var seed = EntityTemplates.CreateAndRegisterSeedForPlant(
                plant: placedEntity,
                productionType: SeedProducer.ProductionType.Harvest,
                id: SeedId,
                name: SeedName,
                desc: SeedDescription,
                anim: Assets.GetAnim("cocoaOak_seed_kanim"),
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
                anim: Assets.GetAnim("cocoaOak_plant_kanim"),
                initialAnim: "place",
                width: 3,
                height: 3);

            //===> SEED RECIPE <=======================================================================
            var input = new[] { new ComplexRecipe.RecipeElement(ForestTreeConfig.SEED_ID, 1f) };
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
