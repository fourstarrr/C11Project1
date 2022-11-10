using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeceleteEffect : MonoBehaviour
{

    private PlayerController player;
    private float primarySpeed;
    private float primaryJumpForce;

    [Header("����ٶȼ���ֵ")]
    public float deceleration;
    [Header("�����Ծ�߶ȼ���ֵ")]
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
            Debug.Log("����ٴ�����������");
            recoverStatus();
            player.speed -= deceleration;
            player.jumpForce -= reduceHight;
            Invoke("recoverStatus", 2.0f);
        }
        else
        {
            Debug.Log("��Ҽ����ˣ�");
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
            Debug.Log("�������������Ҫ�������ˣ�");
            changePlayer();
        }
      
    }
}
