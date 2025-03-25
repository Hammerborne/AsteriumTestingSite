using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.VFX;

public class ClickExplotion : MonoBehaviour
{
    public VisualEffect LocalExplotionEffect;
    private GraphicsBuffer LocalExplotionBuffer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LocalExplotionBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, 1000,
   Marshal.SizeOf(typeof(VFXExplosionRequest)));
    }

    // Update is called once per frame
    void Update()
    {
        // Imported
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero); // Plano en Y = 0

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 clickPosition = ray.GetPoint(distance);
                TestVFXExplotion.Instance.Explotion(clickPosition, 0.25f);
            }
        }

        // Local 
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero); // Plano en Y = 0

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 clickPosition = ray.GetPoint(distance);
                TestVFXExplotion.Instance.Explotion2(LocalExplotionEffect, clickPosition, 0.25f,ref LocalExplotionBuffer);
                Debug.Log(clickPosition);
            }
        }
    }
}
