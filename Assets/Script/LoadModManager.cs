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

    public GameObject BaseVisualEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadMods();
        ProjectileECSContentData.Load(GetModContent<ProjectileECSContentData>("Data/ProyectilECSDesign", ".asset"));
        //VFXReferences.Instance.LoadGraphs(GetVisualEffecAssetFromModContent("Data/Graph", ".vfx"));

        var graphs = new List<VisualEffectAsset>();
        foreach (var item in ProjectileECSContentData.GetAll())
        {
            graphs.Add(item.ExplotionGraphAsset);
        }
        VFXReferences.Instance.LoadGraphs(graphs);

        foreach (var graph in VFXReferences.Instance.Graphs)
        {
            GameObject model = Instantiate(BaseVisualEffect, new Vector3(0,0,0), Quaternion.identity);
            model.name = "VFX Imported";
            model.GetComponent<VisualEffect>().visualEffectAsset = graph.Value;

            VFXReferences.Instance.ExplosionsVFXs.Add(graph.Key, model.GetComponent<VisualEffect>());
            VFXReferences.Instance.ExplosionsRequestsBuffers.Add(graph.Key, new GraphicsBuffer(GraphicsBuffer.Target.Structured, 1000,
            Marshal.SizeOf(typeof(VFXExplosionRequest))));
        }





        // Cargar de los resources un VFX graph












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
