using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss : MonoBehaviour
{
    [SerializeField] GameObject[] BattleShip;
    [SerializeField] [Range(0,10)]float AttackDelay = 0.1f;
    [SerializeField] GameObject WarningEffect;
    [SerializeField] GameObject Laser;
    private void Start()
    {
        StartCoroutine(Paturn2());
    }
    IEnumerator LaserPaturn() 
    {
        for (int i = 0; i < 6; i++) 
        {
            WarningEffect.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            WarningEffect.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
        Laser.SetActive(true);
        yield return new WaitForSeconds(2);
        Laser.SetActive(false);
    }
    IEnumerator Paturn1() 
    {
        for(int i = 0; i < 5; i++) 
        {
            for (int f = 0; f < 5; f++)
            {
            yield return new WaitForSeconds(AttackDelay);
                for (int g = 0; g < 2; g++)
                {
                    ObjectPooler.SpawnFromPool("ÀûÃÑ¾Ë",BattleShip[g].transform.position);
                    ObjectPooler.SpawnFromPool("ÀûÃÑ¾Ë",BattleShip[g].transform.position,Quaternion.Euler(0,0,10));
                    ObjectPooler.SpawnFromPool("ÀûÃÑ¾Ë",BattleShip[g].transform.position, Quaternion.Euler(0,0,20));
                    ObjectPooler.SpawnFromPool("ÀûÃÑ¾Ë",BattleShip[g].transform.position,Quaternion.Euler(0,0,-10));
                    ObjectPooler.SpawnFromPool("ÀûÃÑ¾Ë",BattleShip[g].transform.position,Quaternion.Euler(0,0,-20));
                }
            }
        }
    }
    IEnumerator Paturn2() 
    {
        StartCoroutine(LaserPaturn());
        for (int i = 0; i < 5; i++)
        {
            for (int f = 0; f < 5; f++)
            {
                yield return new WaitForSeconds(AttackDelay);
                for (int g = 0; g < 2; g++)
                {
                    ObjectPooler.SpawnFromPool("ÀûÃÑ¾Ë", BattleShip[g].transform.position);
                    ObjectPooler.SpawnFromPool("ÀûÃÑ¾Ë", BattleShip[g].transform.position, Quaternion.Euler(0, 0, 5));
                    ObjectPooler.SpawnFromPool("ÀûÃÑ¾Ë", BattleShip[g].transform.position, Quaternion.Euler(0, 0, -5));
                }
            }
        }
    }
}
