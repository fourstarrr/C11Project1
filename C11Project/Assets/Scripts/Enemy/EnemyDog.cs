using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDog : MonoBehaviour
{
    public float dogSpeed;
    public float runRange;
    private Transform playerTransform;
    private Rigidbody2D dogRigidbody;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        dogRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        judgeRange();
    }

    private void judgeRange()
    {
        int number = 1;
        if(Vector2.Distance(playerTransform.position,transform.position) <= runRange && number == 1)
        {
            anim.SetBool("isRun", true);
            Invoke("moveLeft",1f);
            number = 0;
            Destroy(this.gameObject, 10f);
        }
        else if (number == 0)
        {
            moveLeft();
        }
    }

    private void moveLeft()
    {
        //ËÙÂÊ
        dogRigidbody.velocity = Vector2.left * (Enemy.instance.speed + dogSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player gets hurt");
        }
    }
}
