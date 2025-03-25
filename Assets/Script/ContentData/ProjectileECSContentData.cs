using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "ProjectileECSData", menuName = "Asterium/ProjectileECSData", order = 11)]
public class ProjectileECSContentData : ScriptableObject
{
    [Tooltip("Important: make sure all ProjectileData have a unique ID")]
    public string ID;
    public GameObject ExplotionVisualEffectObject;


    private static List<ProjectileECSContentData> projectile_list = new List<ProjectileECSContentData>();

    public static void Load(List<ProjectileECSContentData> data)
    {
        projectile_list.Clear();
        projectile_list.AddRange(data);
    }
    public static List<ProjectileECSContentData> GetAll()
    {
        return projectile_list;
    }
    public static ProjectileECSContentData Get(string id)
    {
        foreach (ProjectileECSContentData data in projectile_list)
        {
            if (data.ID == id)
                return data;
        }
        return null;
    }
    /// <summary>
    /// Returns a random template data
    /// </summary>
    public static ProjectileECSContentData GetRandom()
    {
        return projectile_list[Random.Range(0, projectile_list.Count)];
    }
}