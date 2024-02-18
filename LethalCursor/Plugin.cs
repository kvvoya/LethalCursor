using System.IO;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;


namespace LethalCursor
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private ConfigEntry<string> cursorPath;
        private ConfigEntry<int> hotspotX;
        private ConfigEntry<int> hotspotY;
        private ConfigEntry<bool> disableModEntirely;

        private void Awake()
        {
            cursorPath = Config.Bind("General", "CursorPath", "BepInEx/plugins/LethalCompanyTemplate/Resources/cursor.png", "The path to an image to use as a cursor.");
            hotspotX = Config.Bind("General", "HotSpotX", 0, "The X value of the cursor hotspot");
            hotspotY = Config.Bind("General", "HotSpotY", 0, "The Y value of the cursor hotspot");
            disableModEntirely = Config.Bind("General.Toggles", "DisableMod", false, "Just use the default cursor.");

            if (disableModEntirely.Value)
            {
                Logger.LogWarning("You have disabled the mod in the configuration file. The mod will use the default cursor");
                return;
            }

            Logger.LogInfo("LethalCompanyTemplate has been loaded! The default image is the white pixel, refer to the instructions or to the config for more info!");
            Logger.LogInfo($"Loading the image from {cursorPath.Value}");

            Texture2D cursorTexture = new Texture2D(32, 32);
            byte[] fileData = File.ReadAllBytes(cursorPath.Value);
            cursorTexture.LoadImage(fileData);

            if (cursorTexture != null)
            {
                Logger.LogInfo("Cursor is loaded succesfully!");
                Cursor.SetCursor(cursorTexture, new Vector2(hotspotX.Value, hotspotY.Value), CursorMode.Auto);
            }
            else
            {
                Logger.LogError("Something went wrong while loading the cursor! Using the default cursor instead!");
            }
        }
    }
}