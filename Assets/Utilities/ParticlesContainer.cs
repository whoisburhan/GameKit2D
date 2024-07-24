using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticlesContainer : Singleton<ParticlesContainer>
{
    [SerializeField] private GameObject[] poofEffects;
    [SerializeField] private GameObject[] noCountEffects;

    public void SpawnEffect(Transform spawnPoint, ParticlesType type, Vector3 errorOffset, int index = int.MaxValue) 
    {
        int randomIndex;
        switch (type)
        {
            case ParticlesType.None:
                break;
            case ParticlesType.PoofEffect:
                randomIndex = index < poofEffects.Length ? index : Random.Range(0, poofEffects.Length);
                ObjectPoolManager.SpawnObject(poofEffects[randomIndex], spawnPoint.position - errorOffset, poofEffects[randomIndex].transform.rotation, PoolType.Particles);
                break;
            case ParticlesType.NoCountEffect:
                randomIndex = index < noCountEffects.Length ? index : Random.Range(0, noCountEffects.Length);
                ObjectPoolManager.SpawnObject(noCountEffects[randomIndex], spawnPoint.position - errorOffset, noCountEffects[randomIndex].transform.rotation, PoolType.Particles);
                break;

        }

    }
}

public enum ParticlesType 
{
    None, PoofEffect, NoCountEffect
}


//case ParticlesType.BloodEffect:
//    ObjectPoolManager.SpawnObject(bloodEffect, spawnPoint.position - errorOffset, bloodEffect.transform.rotation, PoolType.Particles);
//    break;
//case ParticlesType.BaseHit:
//    randomIndex = Random.Range(0, baseHitEffects.Length);
//    ObjectPoolManager.SpawnObject(baseHitEffects[randomIndex], spawnPoint.position - errorOffset, baseHitEffects[randomIndex].transform.rotation, PoolType.Particles);
//    break;
//case ParticlesType.SpawnEffect:
//    randomIndex = Random.Range(0, spawnEffects.Length);
//    Vector3 tempPos = spawnPoint.position - errorOffset;
//    tempPos.z = 0;
//    ObjectPoolManager.SpawnObject(spawnEffects[randomIndex], tempPos, spawnEffects[randomIndex].transform.rotation, PoolType.Particles);
//    break;