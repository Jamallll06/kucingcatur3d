using UnityEngine;
using System.Collections;


public class BossGridMovement : MonoBehaviour
{

    public float moveSpeed = 5f;


    public IEnumerator MoveToTile(Vector3 target)
    {

        while (Vector3.Distance(
            transform.position,
            target) > 0.05f)
        {

            transform.position =
            Vector3.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );


            yield return null;

        }


        transform.position = target;

    }

}