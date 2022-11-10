using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //玩家移动速度
    [Header("玩家移动速度")]
    public float speed;
    [Header("玩家跳跃高度")]
    public float jumpForce;
    [Header("最大跳跃次数")]
    public int maxJumpTimes;
    [Header("手感调试")]
    public float fallAddition;
    public float jumpAddition;
    [Header("地面图层")]
    public LayerMask ground;
    public LayerMask upHill;
    public LayerMask downHill;
    [Header("下降速度")]
    public float downSpeed;

    [Header("debug部分")]
    public bool isOnGround;//在地面
    public bool canJump;//能跳？
    public bool isGliding;//滑翔
    public bool isOnUpHill;//上斜坡
    public bool isOnDownHill;//下斜坡
    public bool isJumping;//跳跃
    public bool canGliding = true;//能飞？

    public static PlayerController instance;

    private int jumpTimes = 0;//跳跃次数
    private float skyTime;

    private CapsuleCollider2D cc;
    private Rigidbody2D rb;

    private Vector2 capsuleColliderSize;//碰撞体大小

    
    private Vector2 slopeNormalPerp;
    void Start()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;

        cc = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        capsuleColliderSize = cc.size;
    }

    
    void Update()
    {
        Jump();
        downSpeedUp();
    }
    private void FixedUpdate()
    {
        SlopeCheck();
        PlayerMove();
        StatusCheck();
        //Gliding();
    }
    public void downSpeedUp()
    {
        if(Input.GetKey(KeyCode.S) && !isOnGround && !isOnDownHill && !isOnUpHill) //cxy
        {
            //canGliding=false;
            rb.velocity = Vector2.down * downSpeed;
            //rb.AddForce(new Vector2(0f, -500f));
        }
        /*if(Input.GetKeyUp(KeyCode.S))
        {
            canGliding = true;
        }*/
    }
    /*void Gliding()
    {
        if(isGliding&&canGliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }*/
    public void Jump()
    {
        if (Input.GetButtonDown("Jump")&&jumpTimes<maxJumpTimes)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpTimes++;
        }
    }
    public void StatusCheck()
    {
        if(isOnGround)
        {
            jumpTimes = 0;
            skyTime = 0;
        }
        else
        {
            skyTime += Time.deltaTime;
        }
        if(skyTime>2f)
        {
            isGliding = true;
        }
    }
    //玩家一直向右移动
    private void PlayerMove()
    {
         rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    private void SlopeCheck()//检测是否在斜坡上
    {
        Vector2 checkPosFront = transform.position- (Vector3)(new Vector2(-transform.localScale.x/2, transform.localScale.y / 2 - 0.05f));
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0f, transform.localScale.y / 2 - 0.05f));
        Vector2 checkPosBack = transform.position - (Vector3)(new Vector2(transform.localScale.x/2, transform.localScale.y / 2 - 0.05f));
        SlopeCheckHorizontal(checkPosFront,checkPosBack);
        SlopeCheckGround(checkPos);
    }
    private void SlopeCheckHorizontal(Vector2 checkPos1,Vector2 chekPos2)//检测是否在坡上
    {
        float slopeCheckHorizontalDistance = transform.localScale.x ;
        
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos1, Vector2.down, slopeCheckHorizontalDistance, upHill);
       
        RaycastHit2D slopeHitBack = Physics2D.Raycast(chekPos2, Vector2.down, slopeCheckHorizontalDistance, downHill);
        
        if (slopeHitFront)
        {
            isOnUpHill = true;
            isOnGround = true;
        }
        else 
        {
            isOnUpHill = false;           
        }
        if(slopeHitBack)
        {
            isOnDownHill = true;
            isOnGround = true;
        }
        else
        {
            isOnDownHill = false;
        }

    }

    private void SlopeCheckGround(Vector2 checkPos)//垂直检测
    {
        float slopeCheckHorizontalDistance = transform.localScale.y ;
        float glidingDistance = transform.localScale.y * 2;
        RaycastHit2D checkGround = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckHorizontalDistance, ground);
        RaycastHit2D checkGliding1 = Physics2D.Raycast(checkPos, Vector2.down, glidingDistance*3, ground);
        RaycastHit2D checkGliding2 = Physics2D.Raycast(checkPos, Vector2.down, glidingDistance*3, upHill);
        RaycastHit2D checkGliding3 = Physics2D.Raycast(checkPos, Vector2.down, glidingDistance*3, downHill);
        if (checkGround)
        {
            isOnGround = true;
        }
        else if(!isOnUpHill&&!isOnDownHill)
        {
            isOnGround = false;
        }
        if(checkGliding1||checkGliding2||checkGliding3)
        {
            isGliding = false;
        }
    }
    void JumpOptimize()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallAddition * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 )
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * jumpAddition * Time.deltaTime;
        }
    }
    private void OnDrawGizmos()
    {
        Vector2 checkPosFront = transform.position - (Vector3)(new Vector2(-transform.localScale.x / 2, transform.localScale.y / 2 - 0.05f));
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0f, transform.localScale.y / 2 - 0.05f));
        Vector2 checkPosBack = transform.position - (Vector3)(new Vector2(transform.localScale.x / 2, transform.localScale.y / 2 - 0.05f));
        float slopeCheckHorizontalDistance = transform.localScale.x ;
        float slopeCheckHorizontalDistance1 = transform.localScale.y ;
        Gizmos.DrawRay(checkPosFront, Vector2.down * slopeCheckHorizontalDistance1);
        Gizmos.DrawRay(checkPosBack, Vector2.down * slopeCheckHorizontalDistance1);
        Gizmos.DrawRay(checkPos, Vector2.down * slopeCheckHorizontalDistance1);
    }

}
