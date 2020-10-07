using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TUNING;
using Harmony;
using STRINGS;
using UnityEngine;
using ProcGen;
using System.Runtime.CompilerServices;

namespace Dupes_Cuisine
{
    public class Plant_Patches
    {

        public const float CyclesForGrowth = 4f;

        [HarmonyPatch(typeof(EntityConfigManager), "LoadGeneratedEntities")]
        public class EntityConfigManager_LoadGeneratedEntities_Patch
        {
            private static void Prefix()
            {
                //===> FOOD STUFF <==================================================================================================

                // Grilled Creamtop
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_GrilledCreamtop.Id.ToUpperInvariant()}.NAME", Food_GrilledCreamtop.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_GrilledCreamtop.Id.ToUpperInvariant()}.DESC", Food_GrilledCreamtop.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_GrilledCreamtop.Id.ToUpperInvariant()}.RECIPEDESC", Food_GrilledCreamtop.RecipeDescription);

                // Roasted Kakawa
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_RoastedKakawa.Id.ToUpperInvariant()}.NAME", Food_RoastedKakawa.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_RoastedKakawa.Id.ToUpperInvariant()}.DESC", Food_RoastedKakawa.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_RoastedKakawa.Id.ToUpperInvariant()}.RECIPEDESC", Food_RoastedKakawa.RecipeDescription);

                //Kakawa Butter
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_KakawaButter.Id.ToUpperInvariant()}.NAME", Food_KakawaButter.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_KakawaButter.Id.ToUpperInvariant()}.DESC", Food_KakawaButter.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_KakawaButter.Id.ToUpperInvariant()}.RECIPEDESC", Food_KakawaButter.RecipeDescription);

                //Kakawa Bar
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_KakawaBar.Id.ToUpperInvariant()}.NAME", Food_KakawaBar.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_KakawaBar.Id.ToUpperInvariant()}.DESC", Food_KakawaBar.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_KakawaBar.Id.ToUpperInvariant()}.RECIPEDESC", Food_KakawaBar.RecipeDescription);

                //Nutcake
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Nutcake.Id.ToUpperInvariant()}.NAME", Food_Nutcake.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Nutcake.Id.ToUpperInvariant()}.DESC", Food_Nutcake.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Nutcake.Id.ToUpperInvariant()}.RECIPEDESC", Food_Nutcake.RecipeDescription);

                //Cookie
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Cookie.Id.ToUpperInvariant()}.NAME", Food_Cookie.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Cookie.Id.ToUpperInvariant()}.DESC", Food_Cookie.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Cookie.Id.ToUpperInvariant()}.RECIPEDESC", Food_Cookie.RecipeDescription);

                //Mushroom Stew
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_MushroomStew.Id.ToUpperInvariant()}.NAME", Food_MushroomStew.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_MushroomStew.Id.ToUpperInvariant()}.DESC", Food_MushroomStew.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_MushroomStew.Id.ToUpperInvariant()}.RECIPEDESC", Food_MushroomStew.RecipeDescription);

                //Warm Flat Bread
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_FlatBread.Id.ToUpperInvariant()}.NAME", Food_FlatBread.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_FlatBread.Id.ToUpperInvariant()}.DESC", Food_FlatBread.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_FlatBread.Id.ToUpperInvariant()}.RECIPEDESC", Food_FlatBread.RecipeDescription);

                //Lice Wrap
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Lice_Wrap.Id.ToUpperInvariant()}.NAME", Food_Lice_Wrap.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Lice_Wrap.Id.ToUpperInvariant()}.DESC", Food_Lice_Wrap.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Lice_Wrap.Id.ToUpperInvariant()}.RECIPEDESC", Food_Lice_Wrap.RecipeDescription);

                //Meat Wrap
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Meat_Wrap.Id.ToUpperInvariant()}.NAME", Food_Meat_Wrap.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Meat_Wrap.Id.ToUpperInvariant()}.DESC", Food_Meat_Wrap.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Meat_Wrap.Id.ToUpperInvariant()}.RECIPEDESC", Food_Meat_Wrap.RecipeDescription);

                //Pacu Wrap
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Fish_Wrap.Id.ToUpperInvariant()}.NAME", Food_Fish_Wrap.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Fish_Wrap.Id.ToUpperInvariant()}.DESC", Food_Fish_Wrap.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Fish_Wrap.Id.ToUpperInvariant()}.RECIPEDESC", Food_Fish_Wrap.RecipeDescription);

                //Meat Taco
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Meat_Taco.Id.ToUpperInvariant()}.NAME", Food_Meat_Taco.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Meat_Taco.Id.ToUpperInvariant()}.DESC", Food_Meat_Taco.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Meat_Taco.Id.ToUpperInvariant()}.RECIPEDESC", Food_Meat_Taco.RecipeDescription);

                //Sea Taco
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Sea_Taco.Id.ToUpperInvariant()}.NAME", Food_Sea_Taco.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Sea_Taco.Id.ToUpperInvariant()}.DESC", Food_Sea_Taco.Description);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Food_Sea_Taco.Id.ToUpperInvariant()}.RECIPEDESC", Food_Sea_Taco.RecipeDescription);


                //===> CREAMTOP MUSHROOM <===========================================> PLANT <==================================

                // Creamtop Cap
                Strings.Add($"STRINGS.ITEMS.FOOD.{Creamtop_Cap_Config.Id.ToUpperInvariant()}.NAME", Creamtop_Cap_Config.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Creamtop_Cap_Config.Id.ToUpperInvariant()}.DESC", Creamtop_Cap_Config.Description);

                // Creamtop Spore
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{Creamtop_Config.SeedId.ToUpperInvariant()}.NAME", Creamtop_Config.SeedName);
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{Creamtop_Config.SeedId.ToUpperInvariant()}.DESC", Creamtop_Config.SeedDescription);

                // Creamtop Mushroom
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Creamtop_Config.Id.ToUpperInvariant()}.NAME", Creamtop_Config.Name);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Creamtop_Config.Id.ToUpperInvariant()}.DESC", Creamtop_Config.Description);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Creamtop_Config.Id.ToUpperInvariant()}.DOMESTICATEDDESC", Creamtop_Config.DomesticatedDescription);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Creamtop_Cap_Config.Id, CyclesForGrowth * 600.0f, 1));

                //===> KAKAWA TREE <=================================================> PLANT <==================================

                // Kakawa Acorn
                Strings.Add($"STRINGS.ITEMS.FOOD.{CocoaOak_Acorn_Config.Id.ToUpperInvariant()}.NAME", CocoaOak_Acorn_Config.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{CocoaOak_Acorn_Config.Id.ToUpperInvariant()}.DESC", CocoaOak_Acorn_Config.Description);

                // Kakawa Seed
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{CocoaOak_Tree_Config.SeedId.ToUpperInvariant()}.NAME", CocoaOak_Tree_Config.SeedName);
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{CocoaOak_Tree_Config.SeedId.ToUpperInvariant()}.DESC", CocoaOak_Tree_Config.SeedDescription);

                // Kakawa Tree
                Strings.Add($"STRINGS.CREATURES.SPECIES.{CocoaOak_Tree_Config.Id.ToUpperInvariant()}.NAME", CocoaOak_Tree_Config.Name);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{CocoaOak_Tree_Config.Id.ToUpperInvariant()}.DESC", CocoaOak_Tree_Config.Description);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{CocoaOak_Tree_Config.Id.ToUpperInvariant()}.DOMESTICATEDDESC", CocoaOak_Tree_Config.DomesticatedDescription);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(CocoaOak_Acorn_Config.Id, CyclesForGrowth * 1400.0f, 12));

                //===> SUNNY WHEAT <================================================> PLANT <==================================

                // Sunny Wheat Grain
                Strings.Add($"STRINGS.ITEMS.FOOD.{SunnyWheat_Grain_Config.Id.ToUpperInvariant()}.NAME", SunnyWheat_Grain_Config.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{SunnyWheat_Grain_Config.Id.ToUpperInvariant()}.DESC", SunnyWheat_Grain_Config.Description);

                // Sunny Wheat Bulb
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{SunnyWheat_Plant_Config.SeedId.ToUpperInvariant()}.NAME", SunnyWheat_Plant_Config.SeedName);
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{SunnyWheat_Plant_Config.SeedId.ToUpperInvariant()}.DESC", SunnyWheat_Plant_Config.SeedDescription);

                // Sunny Wheat Plant
                Strings.Add($"STRINGS.CREATURES.SPECIES.{SunnyWheat_Plant_Config.Id.ToUpperInvariant()}.NAME", SunnyWheat_Plant_Config.Name);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{SunnyWheat_Plant_Config.Id.ToUpperInvariant()}.DESC", SunnyWheat_Plant_Config.Description);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{SunnyWheat_Plant_Config.Id.ToUpperInvariant()}.DOMESTICATEDDESC", SunnyWheat_Plant_Config.DomesticatedDescription);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(SunnyWheat_Grain_Config.Id, CyclesForGrowth * 2700.0f, 18));
            }
        }

        // ===> IMIGRATION PRINTER GIFT <========================================================================================
        [HarmonyPatch(typeof(Immigration))]
        [HarmonyPatch("ConfigureCarePackages")]
        public static class Immigration_ConfigureCarePackages_Patch
        {
            public static void Postfix(ref Immigration __instance)
            {
                var field = Traverse.Create(__instance).Field("carePackages");
                var list = field.GetValue<CarePackageInfo[]>().ToList();

                list.Add(new CarePackageInfo(Creamtop_Config.SeedId, 3f, () => GameClock.Instance.GetCycle() >= 3));
                list.Add(new CarePackageInfo(CocoaOak_Tree_Config.SeedId, 1f, () => GameClock.Instance.GetCycle() >= 3));
                list.Add(new CarePackageInfo(SunnyWheat_Plant_Config.SeedId, 3f, () => GameClock.Instance.GetCycle() >= 3));

                field.SetValue(list.ToArray());
            }
        }



    }
}
