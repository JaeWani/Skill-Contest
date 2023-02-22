using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] [Range (0,100)]public int BulletDamage;
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
        if (transform.position.x > 1.56f)
            gameObject.SetActive(false);
        else if (transform.position.x < -6.87f)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Wall" || other.gameObject.CompareTag("NoDieMonster") || other.gameObject.CompareTag("Monster"))
        {
            EffectManager.Instance.ExplosionEffect(transform.position,transform.localScale);
            gameObject.SetActive(false);
        }
        
    }
    void Move()
    {
    transform.Translate(MoveDirection * Player.Instance.BulletSpeed * Time.deltaTime);
    }
}
