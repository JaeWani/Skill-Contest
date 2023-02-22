using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
    }
    public void EffectDestroy() 
    {
        gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
