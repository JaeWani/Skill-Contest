using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    public Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3
        (
        Mathf.Clamp(position.x, -6.87f,1.56f),
        Mathf.Clamp(position.y, -4.4f, 4.4f),
        0
        );
    }
    [Header ("이미지")]
    [SerializeField] Sprite right,left;
    [SerializeField] Image HPBar;
    [SerializeField] Text HPText;
    [Header ("플레이어 스탯")]
    [Range (0,10)] public float Speed;
    [Range (0,10)] public float BulletSpeed;
    [Range (0,10)] public float AttackSpeed;
    [Range(0, 300)] public int PlayerHP = 100;
    [Header ("점멸 스킬 상태")]
    [SerializeField] public bool CanFlash = true;
    [SerializeField] public bool UseFlash = false;
    [SerializeField] [Range (1,10)] public float FlashCool = 5;
    [SerializeField] Text FlashCoolTimeText;
    [Header ("보호막 스킬 상태")]
    [SerializeField] GameObject ShieldObj;
    [SerializeField] public bool CanShield = true;
    [SerializeField] [Range (1,20)]public float ShieldCool = 15;
    [SerializeField] [Range (1,20)]public float ShieldDuration = 5;
    [SerializeField] Text ShieldCoolTimeText;

    public enum BulletKind
    {
        Bullet_1,
        Bullet_2,
        Bullet_3,
        Bullet_4
    }
    [Header ("현재 총알 상태")]
    [SerializeField] BulletKind CurrentBullet = BulletKind.Bullet_1;
    public enum ItemKind
    {
        none,
        Fire3X,
        Fire5X
    }

    [Header ("먹은 아이템 상태")]
    [SerializeField] public ItemKind CurrentItem = ItemKind.none;
    IEnumerator Fire()
    {
        switch(CurrentItem)
        {
            case ItemKind.none:
            ObjectPooler.SpawnFromPool(CurrentBullet.ToString(),transform.position);
            break;
            case ItemKind.Fire3X:
            Fire3XItem();
            break;
            case ItemKind.Fire5X:
            Fire5XItem();
            break;

        }
        yield return new WaitForSeconds(AttackSpeed);
        StartCoroutine(Fire());
    }
    void Fire3XItem()
    {
        ObjectPooler.SpawnFromPool(CurrentBullet.ToString(),transform.position);
        ObjectPooler.SpawnFromPool(CurrentBullet.ToString(),transform.position,Quaternion.Euler(0,0,-5));
        ObjectPooler.SpawnFromPool(CurrentBullet.ToString(),transform.position,Quaternion.Euler(0,0,5));
    }
    void Fire5XItem()
    {
        ObjectPooler.SpawnFromPool(CurrentBullet.ToString(),transform.position);
        ObjectPooler.SpawnFromPool(CurrentBullet.ToString(),transform.position,Quaternion.Euler(0,0,-5));
        ObjectPooler.SpawnFromPool(CurrentBullet.ToString(),transform.position,Quaternion.Euler(0,0,5));
        ObjectPooler.SpawnFromPool(CurrentBullet.ToString(),transform.position,Quaternion.Euler(0,0,-10));
        ObjectPooler.SpawnFromPool(CurrentBullet.ToString(),transform.position,Quaternion.Euler(0,0,10));
    }
    void Move()
    {
        transform.localPosition = ClampPosition(transform.position);
        float veloX = Input.GetAxis("Horizontal");
        float veloY = Input.GetAxis("Vertical");
        if(veloX > 0)
        {
            anim.SetBool("IsRight", true);
            anim.SetBool("IsLeft", false);
        }
        else if(veloX < 0)
        {
            anim.SetBool("IsLeft", true);
            anim.SetBool("IsRight", false);
        }
        else
        {
            anim.SetBool("IsLeft", false);
            anim.SetBool("IsRight", false);
        }
        Rigid.velocity = new Vector2(veloX * Speed,veloY * Speed);
    }
    void SetHPBar() 
    {
        HPBar.fillAmount = PlayerHP / 100f;
        HPText.text = string.Format("HP {0}/100", PlayerHP);
    }
    void Flash() // 점멸 스킬입니다!
    {
        if(CanFlash == true)
        {
            if(Input.GetKeyDown(KeyCode.F) && UseFlash == false)
                UseFlash = true;
            else if(Input.GetKeyDown(KeyCode.F) && UseFlash == true)
                UseFlash = false;
            if(UseFlash == true)
            {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 
				Input.mousePosition.y, -Camera.main.transform.position.z));
                Debug.Log(point);
                if(point.x < 1.56f)
                {
                transform.position = point;
                StartCoroutine(FlashCoolTime());
                }
                else if(point.x > 1.56f)
                {
                    UseFlash = false;
                }
            }
            }
        }
    }
    IEnumerator FlashCoolTime() // 점멸 쿨타임을 돌려주는 코드입니다
    {
        CanFlash = false;
        UseFlash = false;
        yield return new WaitForSeconds(FlashCool);
        CanFlash = true;
    }
    void Shield()
    {
        if(CanShield == true)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(ShieldCoolTime());
                StartCoroutine(ShieldDurationTime());
            }
        }
    }
    IEnumerator ShieldDurationTime()
    {
        ShieldObj.SetActive(true);
        yield return new WaitForSeconds(ShieldDuration);
        ShieldObj.SetActive(false);
    }
    IEnumerator ShieldCoolTime()
    {
        CanShield = false;
        yield return new WaitForSeconds(ShieldCool);
        CanShield = true;
    }
    
    Rigidbody2D Rigid;
    SpriteRenderer spr;
    Animator anim;
    void Start()
    {
        Rigid = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ShieldObj.SetActive(false);
        StartCoroutine(Fire());
    }

    void Update()
    {
        Move();
        Flash();
        Shield();
        CheatKey();
        SetHPBar();
    }
    void CheatKey()
    {
        if (Input.GetKeyDown(KeyCode.F2))
            CurrentItem = ItemKind.Fire3X;
        else if (Input.GetKeyDown(KeyCode.F3))
            CurrentItem = ItemKind.Fire5X;
        else if (Input.GetKeyDown(KeyCode.F1))
            CurrentItem = ItemKind.none;
        else if (Input.GetKeyDown(KeyCode.F4))
            PlayerHP -= 10;
        else if (Input.GetKeyDown(KeyCode.F5))
            PlayerHP += 10;
    }
}
