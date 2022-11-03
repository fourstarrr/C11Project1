using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public float batSpeed;
    public float runRange;
    private Transform playerTransform;
    private Rigidbody2D batRigidbody;
 
    // Start is called before the first frame update
    void Start()
    {
        batRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        judgeRange();
    }

    private void judgeRange()
    {
        int number = 1;
        if (Vector2.Distance(playerTransform.position, transform.position) <= runRange && number == 1)
        {

            moveLeft();
            number = 0;
        }
        else if (number == 0)
        {
            moveLeft();
        }
    }

    private void moveLeft()
    {
        //ËÙÂÊ
        batRigidbody.velocity = Vector2.left * (Enemy.instance.speed + batSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player gets hurt");
        }
    }
}
