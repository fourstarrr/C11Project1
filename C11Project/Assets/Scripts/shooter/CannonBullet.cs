using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{


    public Transform cannonDirection;
    private float speed=2;


    void Update()
    {
        Move();
        DestroyBullet();
    }
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, cannonDirection.position, speed * Time.deltaTime);
    }

    private void DestroyBullet()
    {
        if (Vector2.Distance(transform.position, cannonDirection.position) <= 0.01f)
        {
            Destroy(this.gameObject,0.2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            Debug.Log("player slows down");
        }
    }
}
