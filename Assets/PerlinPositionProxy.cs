using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

/*
 * REFERENCE:
 * https://youtu.be/zzN8TQTT2qA
 */

[RequiresEntityConversion]
public class PerlinPositionProxy : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var data = new PerlinPosition { };      //create a new component
        dstManager.AddComponentData(entity, data);      //adding the component to the entity
    }
}
