using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] GameObject Explosion;
    public void ExplosionEffect(Vector3 transform, Vector3 scale)
    {
        var explosion = ObjectPooler.SpawnFromPool("Æø¹ß", transform);
        explosion.transform.localScale = scale;
    }
}
