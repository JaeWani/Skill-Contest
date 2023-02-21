using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private MeshRenderer _Render;
    private float _offset;
    [Header ("속도")]
    [Range (0,1)][SerializeField]private float _speed;
    void Start()
    {
        _Render = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        _offset += Time.deltaTime * _speed;
        _Render.material.mainTextureOffset = new Vector2(0,_offset);
    }
}
