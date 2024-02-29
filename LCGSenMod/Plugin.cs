using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.PlayerLoop;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using LCGSenMod.Patches;

namespace LCGSenMod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class GSenModBase : BaseUnityPlugin
    {
        private const string modGUID = "ChaosCrab.LCGSMod";
        private const string modName = "LC GS Mod";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static GSenModBase Instance;

        internal ManualLogSource mls;

        internal static List<AudioClip> SoundFX;
        internal static AssetBundle Bundle;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Mod Awake");

            harmony.PatchAll(typeof(StartOfRoundPatch));

            SoundFX = new List<AudioClip>();
            string FolderLocation = Instance.Info.Location;
            FolderLocation = FolderLocation.TrimEnd("LCGSenMod.dll".ToCharArray());
            Bundle = AssetBundle.LoadFromFile(FolderLocation + "sengancut");

            if(Bundle != null)
            {
                mls.LogInfo("Loaded Asset Bundle");
                SoundFX = Bundle.LoadAllAssets<AudioClip>().ToList();
            }
            else
            {
                mls.LogError("Failed to Load Bundle");
            }
        }
    }
}
