using System;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

/*
 * REFERENCE:
 * https://youtu.be/zzN8TQTT2qA
 */

public class SpawnProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject cube;
    public int rows;
    public int cols;

    public void DeclareReferencedPrefabs(List<GameObject> gameObjects)
    {
        gameObjects.Add(cube);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawnerData = new Spawner
        {
            Prefab = conversionSystem.GetPrimaryEntity(cube),
            Erows = rows,
            Ecols = cols
        };
        dstManager.AddComponentData(entity, spawnerData);
    }
}
