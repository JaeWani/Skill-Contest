using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void OnDisable()
    {
    ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
    }
    void OnEnable() 
    {

    }
    public Vector3 MoveDirection;
    void Start()
    {
    
    }

    void Update()
    {
        Move();
        if(transform.position.x > 1.56f)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Wall")
            gameObject.SetActive(false);
    }
    void Move()
    {
    transform.Translate(MoveDirection * Player.Instance.BulletSpeed * Time.deltaTime);
    }
}
