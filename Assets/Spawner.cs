using Unity.Entities;

/*
 * REFERENCE:
 * https://youtu.be/zzN8TQTT2qA
 */

public struct Spawner : IComponentData
{
    public Entity Prefab;
    public int Erows;
    public int Ecols;
}
