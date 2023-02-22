using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("몬스터 종류")]
    [SerializeField] List<GameObject> Monster_Kind = new List<GameObject>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
