﻿using UnityEngine;
using System.Text;
using System.Collections;

// Make sure we can access the uMod api.
using UMod;

namespace UMod.Example
{
    /// <summary>
    /// An example script that shows how to list detailed information about all installed mods.
    /// This example makes use of the 'ModDirectory' but there are other methods.
    /// To use this script simply attach it to a game object.
    /// </summary>
    [ExecuteInEditMode]
    public class Ex07_ListModsDetailed : MonoBehaviour
    {
        private ModDirectory modDirectory = null;

        // The path used for the mod directory
        public string modDirectoryPath = "";
        
        private void Start()
        {
            // Check if we are in-editor and not in play mode - if so the component has just been added
            if (Application.isEditor == true && Application.isPlaying == false)
            {
                // Make sure we dont alter the users suggestion
                if (string.IsNullOrEmpty(modDirectoryPath) == true)
                {
                    // Initialize to a default directory
                    modDirectoryPath = Application.persistentDataPath + "/Mods";
                }
                return;
            }

            // Create the mod directory at the path
            modDirectory = new ModDirectory(modDirectoryPath);

            // Setup the mod directory before we can use it
            //ModDirectory.DirectoryLocation = modDirectoryPath;

            // Check if there are any installed mods.
            if(modDirectory.HasMods == true) //ModDirectory.HasMods == true)
            {
                // We can now use the mod directory to list all mods installed.
                // Note that this method will list all valid mods located in the 'modDirectory' path.
                foreach(IModInfo mod in modDirectory.GetMods())// ModDirectory.GetMods())
                {
                    // Build a detailed message for the mod
                    string fullMessage = string.Format("Name = {0}, Version = {1}, Core Version = {2}, Author = {3}, Description = {4}",
                        mod.NameInfo.ModName,
                        mod.NameInfo.ModVersion,
                        mod.ModCoreVersion,
                        mod.ModAuthor,
                        mod.ModDescription);

                    // Print the message to the console
                    ExampleUtil.Log(this, fullMessage);
                }
            }
            else
            {
                // There are no mods installed in 'modDirectory' so just print a message
                ExampleUtil.LogError(this, "There are no mods installed in the mod directory");
            }
        }
    }
}
