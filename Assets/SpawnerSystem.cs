using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

/*
 * REFERENCE:
 * https://youtu.be/N7pi-4r5G3I
 */

public class SpawnerSystem : JobComponentSystem
{
    EndSimulationEntityCommandBufferSystem m_EntityCommandBufferSystem;

    protected override void OnCreate()
    {
        m_EntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    struct SpawnJob: IJobProcessComponentDataWithEntity<Spawner, LocalToWorld>      //the job that needs to be run
    {
        public EntityCommandBuffer CommandBuffer;
        public void Execute(Entity entity, int index, [ReadOnly] ref Spawner spawner, [ReadOnly] ref LocalToWorld location)
        {
            for (int x = 0; x < spawner.Erows; x++)
            {
                for (int z = 0; z < spawner.Ecols; z++)
                {
                    var instance = CommandBuffer.Instantiate(spawner.Prefab);
                    var pos = math.transform(location.Value, new float3(x, noise.cnoise(new float2(x, z) * 0.21f), z)); //use ECS library for Perlin Noise
                    CommandBuffer.SetComponent(instance, new Translation { Value = pos });
                }
            }
            CommandBuffer.DestroyEntity(entity);
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)      //Run the job. Similar to the "Update" function in object-based Unity
    {
        var job = new SpawnJob
        {
            CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer()
        }.ScheduleSingle(this, inputDeps);

        m_EntityCommandBufferSystem.AddJobHandleForProducer(job);
        return job;
    }
}
