using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{

    [Header("移动速度")]
    public float batSpeed;
  
    private Rigidbody2D batRigidbody;
 
    // Start is called before the first frame update
    void Start()
    {
        batRigidbody = gameObject.GetComponentInParent<Rigidbody2D>();
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void moveLeft()
    {
        //速率
        batRigidbody.velocity = Vector2.left * (Enemy.instance.speed + batSpeed);
        //Debug.LogWarning(Enemy.instance.speed + batSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveLeft();
        }
    }
}
