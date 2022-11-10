using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeceleteEffect : MonoBehaviour
{

    private PlayerController player;
    private float primarySpeed;
    private float primaryJumpForce;

    [Header("玩家速度减少值")]
    public float deceleration;
    [Header("玩家跳跃高度减少值")]
    public float reduceHight;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
        primarySpeed = player.speed;
        primaryJumpForce = player.jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changePlayer()
    {


        if (player.speed==primarySpeed-deceleration)
        {
            Debug.Log("玩家再次碰到了球球！");
            recoverStatus();
            player.speed -= deceleration;
            player.jumpForce -= reduceHight;
            Invoke("recoverStatus", 2.0f);
        }
        else
        {
            Debug.Log("玩家减速了！");
            player.speed -= deceleration;
            player.jumpForce -= reduceHight;
            Invoke("recoverStatus", 2.0f);
        }

    }

    private void recoverStatus()
    {
        player.speed = primarySpeed;
        player.jumpForce = primaryJumpForce;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DeceletedEffect")
        {
            Debug.Log("玩家碰到了球球，要被减速了！");
            changePlayer();
        }
      
    }
}
