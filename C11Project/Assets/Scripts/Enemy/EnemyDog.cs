using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : MonoBehaviour
{

    [Header("移动速度")]
    public float dogSpeed;
   // public float runRange;
   // private Transform playerTransform;
    private Rigidbody2D dogRigidbody;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
        dogRigidbody = gameObject.GetComponentInParent<Rigidbody2D>();
      
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void moveLeft()
    {
        //速率
        dogRigidbody.velocity = Vector2.left * (Enemy.instance.speed + dogSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            anim.SetBool("isRun", true);
            moveLeft();
  
        }
    }
}
