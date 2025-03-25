using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UMod;
using UnityEngine;
using UnityEngine.VFX;

public class LoadModManager : MonoBehaviour
{
    const string modName = "CoreMod";
    private ModHost modHost;

    void Start()
    {
        LoadMods();
        ProjectileECSContentData.Load(GetModContent<ProjectileECSContentData>("Data/ProyectilECSDesign", ".asset"));

        foreach (var projectilData in ProjectileECSContentData.GetAll())
        {
            GameObject model = Instantiate(projectilData.ExplotionVisualEffectObject, new Vector3(0,0,0), Quaternion.identity);
            model.name = "VFX Imported";
            var modelVisualEffect = model.GetComponent<VisualEffect>();
            VFXReferences.Instance.ExplosionsVFXs.Add(modelVisualEffect.visualEffectAsset.name, modelVisualEffect);
            VFXReferences.Instance.ExplosionsRequestsBuffers.Add(modelVisualEffect.visualEffectAsset.name, new GraphicsBuffer(GraphicsBuffer.Target.Structured, 1000,
            Marshal.SizeOf(typeof(VFXExplosionRequest))));
        }

    }


    private void LoadMods()
    {
        ModHost modHost = ModHost.CreateNewHost();

#if UNITY_EDITOR
        ModDirectory directory = new ModDirectory(Application.dataPath + "/Mods");
#else
                ModDirectory directory = new ModDirectory(Application.dataPath + "/Mods");
#endif

        Uri modPath = directory.GetModPath(modName);

        Mod.Load(modPath);

    }

    private List<T> GetModContent<T>(string path, string extension) where T : ScriptableObject
    {
        List<T> typeList = new List<T>();
        foreach (ModHost host in ModHost.AllLoadedModHosts)
        {
            if (host.HasAssets == false) continue;
            var hostShipDesignAssets = host.Assets.FindAllInFolderWithExtension(path, extension);
            foreach (var asset in hostShipDesignAssets)
            {
                var scriptableObjetct = asset.Load();
                typeList.Add((T)scriptableObjetct);

            }

        }

        return typeList;
    }
}
