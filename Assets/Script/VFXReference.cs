using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class VFXReferences : Singleton<VFXReferences>
{
    // Para hitSparks
    public Dictionary<FixedString128Bytes, VisualEffect> HitSparksVFXs = new Dictionary<FixedString128Bytes, VisualEffect>();
    public Dictionary<FixedString128Bytes, GraphicsBuffer> HitSparksRequestsBuffers = new Dictionary<FixedString128Bytes, GraphicsBuffer>();

    // Para explosiones
    public Dictionary<FixedString128Bytes, VisualEffect> ExplosionsVFXs = new Dictionary<FixedString128Bytes, VisualEffect>();
    public Dictionary<FixedString128Bytes, GraphicsBuffer> ExplosionsRequestsBuffers = new Dictionary<FixedString128Bytes, GraphicsBuffer>();

    public Dictionary<FixedString128Bytes, VisualEffectAsset> Graphs = new Dictionary<FixedString128Bytes, VisualEffectAsset>();
    // Thrusters (se mantiene igual)
    public VisualEffect ThrustersGraph;
    public GraphicsBuffer ThrusterRequestsBuffer;
    public GraphicsBuffer ThrusterDatasBuffer;


    public void LoadGraphs(List<VisualEffectAsset> graphs)
    {
        Graphs.Clear();
        ExplosionsVFXs.Clear();
        ExplosionsRequestsBuffers.Clear();
        HitSparksVFXs.Clear();
        HitSparksRequestsBuffers.Clear(); 

        foreach (var graph in graphs)
        {
            Graphs[graph.name] = graph;
        }
        graphs.Clear();
    }
}

public class TestVFXExplotion : Singleton<TestVFXExplotion>
{
    public void Explotion(Vector3 Position, float Scale)
    {
        // Obtener buffer y VFX Graph para la clave "Lazer"
        var graphicsBuffer = VFXReferences.Instance.ExplosionsRequestsBuffers["ExplosionsGraph"];
        var vfxGraph = VFXReferences.Instance.ExplosionsVFXs["ExplosionsGraph"];

        // IDs de las propiedades en el VFX Graph
        int _spawnBatchId = Shader.PropertyToID("SpawnBatch");
        int _requestsCountId = Shader.PropertyToID("SpawnRequestsCount");
        int _requestsBufferId = Shader.PropertyToID("SpawnRequestsBuffer");

        // Asegurar la asignación del buffer al VFX Graph
        vfxGraph.SetGraphicsBuffer(_requestsBufferId, graphicsBuffer);

        // Crear la solicitud con la posición y escala deseada
        int requestCount = 1;
        var request = new VFXExplosionRequest()
        {
            Position = Position,
            Scale = Scale,
        };

        // Para depuración: mostrar valores enviados
        //Debug.Log($"Enviando solicitud de explosión: Posición: {request.Position}, Escala: {request.Scale}");

        // Enviar datos al GraphicsBuffer
        var array = new[] { request };
        graphicsBuffer.SetData(array, 0, 0, requestCount);

        // Establecer el contador de solicitudes en el VFX Graph
        vfxGraph.SetInt(_requestsCountId, requestCount);

        // Enviar el evento para que el VFX Graph procese la solicitud
        vfxGraph.SendEvent(_spawnBatchId);

        VFXReferences.Instance.ExplosionsRequestsBuffers["ExplosionsGraph"] = graphicsBuffer;
    }

    public void Explotion2(VisualEffect effect, Vector3 Position, float Scale, ref GraphicsBuffer buffer)
    {
        // Obtener buffer y VFX Graph
        var graphicsBuffer = buffer;
        var vfxGraph = effect;

        // IDs de las propiedades en el VFX Graph
        int _spawnBatchId = Shader.PropertyToID("SpawnBatch");
        int _requestsCountId = Shader.PropertyToID("SpawnRequestsCount");
        int _requestsBufferId = Shader.PropertyToID("SpawnRequestsBuffer");

        // Asegurar la asignación del buffer al VFX Graph
        vfxGraph.SetGraphicsBuffer(_requestsBufferId, graphicsBuffer);

        // Crear la solicitud con la posición y escala deseada
        int requestCount = 1;
        var request = new VFXExplosionRequest()
        {
            Position = Position,
            Scale = Scale,
        };

        // Para depuración: mostrar valores enviados
        //Debug.Log($"Enviando solicitud de explosión: Posición: {request.Position}, Escala: {request.Scale}");

        // Enviar datos al GraphicsBuffer
        var array = new[] { request };
        graphicsBuffer.SetData(array, 0, 0, requestCount);

        // Establecer el contador de solicitudes en el VFX Graph
        vfxGraph.SetInt(_requestsCountId, requestCount);

        // Enviar el evento para que el VFX Graph procese la solicitud
        vfxGraph.SendEvent(_spawnBatchId);

        VFXReferences.Instance.ExplosionsRequestsBuffers[effect.visualEffectAsset.name] = buffer;
    }

}

[VFXType(VFXTypeAttribute.Usage.GraphicsBuffer)]
public struct VFXExplosionRequest
{
    public Vector3 Position;
    public float Scale;
}