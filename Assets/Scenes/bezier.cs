using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bezier : MonoBehaviour
{
   [SerializeField]Transform[] Point;
    [Range(0,1)]
    public float value = 0;
    private void Update()
    {
        Vector2 ab = Vector2.Lerp(Point[0].transform.position, Point[1].transform.position, value);
        Vector2 bc = Vector2.Lerp(Point[1].transform.position, Point[2].transform.position, value);
        Vector2 cd = Vector2.Lerp(Point[2].transform.position, Point[3].transform.position, value);

        Vector2 abbc = Vector2.Lerp(ab, bc, value);
        Vector2 bccd = Vector2.Lerp(bc, cd, value);

        Vector2 Pos = Vector2.Lerp(abbc,bccd,value);

        gameObject.transform.position = Pos;
        //value = Mathf.MoveTowards(value, 1, Time.deltaTime);
    }
}
