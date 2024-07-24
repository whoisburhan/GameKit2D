using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnsParticleToPool : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject, PoolType.Particles);
    }
}
