using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHP : MonoBehaviour
{
    [SerializeField] [Range(0, 1000)] public int MobHP;
    float MaxHP;
    [SerializeField]MobKind CurrentMob;
    [SerializeField] Image HPBar;
    [SerializeField] Text HPText;
    enum MobKind 
    { 
    small,
    medium,
    big,
    mediumBoss,
    finalboss
    }
    private void Start()
    {
        MaxHP = MobHP;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            var bulletdmg = collision.gameObject.GetComponent<BulletScript>().BulletDamage;
            Debug.Log(bulletdmg);
            MobHP -= bulletdmg;
            Debug.Log(MobHP);
        }
    }
    void SetHPBar() 
    {
        if (CurrentMob == MobKind.mediumBoss || CurrentMob == MobKind.finalboss) 
        {
            HPBar.fillAmount = MobHP / MaxHP;
            HPText.text = string.Format("HP {0}/" + MaxHP, MobHP);
        }
    }
    private void Update()
    {
        SetHPBar();
    }
}
