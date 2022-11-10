using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public static Jetpack instance;

    [Tooltip("地面层级")]
    public LayerMask layerGroundMask;   //需要跟雪菲沟通

    [Header("状态参数"), Tooltip("正在滑翔")]
    public bool isGliding;
    [Tooltip("正在使用喷气背包")]
    public bool isUsingJetpack;

    [Header("喷气背包参数"), Tooltip("向上位移距离"), Range(1, 50)]
    public float upDistance;
    [Tooltip("喷气背包速度"), Range(1, 10)]
    public float jetpackSpeed;
    [Tooltip("离地解除滑翔距离"), Range(1, 50)]
    public float reachGroundDistance;
    [Tooltip("空中向下控制")]
    public KeyCode controllDownGlidingKeycode;
    [Tooltip("空中向下控制速度"), Range(50, 2000)]
    public float downGlidingSpeed;
    [Tooltip("是否已重置喷气背包参数")]
    public bool isResetJetpackParam;

    [Header("Debug参数")]
    [SerializeField, Tooltip("玩家起始位置")] float originY;
    [SerializeField, Tooltip("喷气背包最高位置")] float targetY;
    [SerializeField, Tooltip("玩家当前位置")] float curPlayerY;
    [SerializeField, Tooltip("玩家已上升时间")] float upTime;
    [SerializeField, Tooltip("到达地面射线显示控制开关")] bool switchRayReachGround;
    [SerializeField, Tooltip("到达地面解除滑翔时冻结")] bool switchReachGroundFrozen;
    [SerializeField, Tooltip("Debug文本显示")] bool switchDebugText;


    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        DebugController();
        //使用喷气背包开始向上位移
        if (isUsingJetpack)
        {
            isResetJetpackParam = false;
            //喷气背包上升阶段
            if (!isGliding)
            {
                //开始计时
                UpTimer();
                //正在使用喷气背包上升
                UpwardDisplacement();
                Debug.Log("正在使用喷气背包上升");
                //到达指定位移距离
                if (IsReachTagetDistance())
                {
                    Cards.instance.player.GetComponent<Collider2D>().isTrigger = false;
                    Debug.Log("已到达指定高度");
                    isGliding = true;
                }
                //已上升足够时间
                if (IsReachJetpackTime())
                {
                    Cards.instance.player.GetComponent<Collider2D>().isTrigger = false;
                    Debug.Log("已到达指定高度");
                    isGliding = true;
                }
            }
            //开始滑翔
            else
            {
                Cards.instance.player.GetComponent<SpriteRenderer>().color = Color.red;
                //即将到达地面解除滑翔状态
                if (IsComingToGround())
                {
                    isUsingJetpack = false;
                    Debug.Log("解除滑翔");
                    ResetJetpackParam();
                }
                //空中向下控制
                ControllDownGliding();
            }
        }
        else if (!isResetJetpackParam)
        {
            ResetJetpackParam();
        }
    }
    void ResetJetpackParam()
    {
        isGliding = false;
        upTime = 0;
        Cards.instance.player.GetComponent<SpriteRenderer>().color = Color.white;
        isResetJetpackParam = true;
    }

    /// <summary>
    /// Debug控制器
    /// </summary>
    void DebugController()
    {
        if (switchRayReachGround)
        {
            ShowReachGroundRay();
        }
        if (switchReachGroundFrozen && isGliding && IsComingToGround())
        {
            FreezePlayer();
        }

        if (switchDebugText)
        {
            Debug.unityLogger.filterLogType = LogType.Log; //显示所有
        }
        else
        {
            Debug.unityLogger.filterLogType = LogType.Error; //只显示Error + Exception
        }
    }
    /// <summary>
    /// 使用喷气背包
    /// </summary>
    public void UseJetpack()
    {
        isUsingJetpack = true;
        originY = Cards.instance.player.transform.position.y;
        targetY = originY + upDistance;
        Cards.instance.player.GetComponent<Collider2D>().isTrigger = true;
    }
    /// <summary>
    /// 向上位移一段距离
    /// </summary>
    void UpwardDisplacement()
    {
        Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.up * jetpackSpeed;
    }
    /// <summary>
    /// 是否飞到指定高度
    /// </summary>
    /// <returns></returns>
    bool IsReachTagetDistance()
    {
        curPlayerY = Cards.instance.player.transform.position.y;
        return curPlayerY >= targetY;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool IsReachJetpackTime()
    {
        float Time = upDistance / jetpackSpeed;
        return Time < upTime;
    }
    void UpTimer()
    {
        upTime += Time.deltaTime;
    }
    /// <summary>
    /// 是否到达离地解除滑翔距离
    /// </summary>
    bool IsComingToGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(Cards.instance.player.transform.position, Vector2.down, reachGroundDistance, layerGroundMask);

        return raycastHit2D;
    }
    /// <summary>
    /// 空中按向下键加速下落
    /// </summary>
    void ControllDownGliding()
    {
        PlayerController.instance.downSpeedUp();
        //if (Input.GetKey(controllDownGlidingKeycode))
        //{
        //    Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Cards.instance.player.GetComponent<Rigidbody2D>().velocity + Vector2.down * downGlidingSpeed * Time.deltaTime;
        //}
    }
    /// <summary>
    /// 显示离地解除滑翔射线
    /// </summary>
    void ShowReachGroundRay()
    {
        Debug.DrawRay(Cards.instance.player.transform.position, Vector2.down * reachGroundDistance, Color.red);
    }

    /// <summary>
    /// 冻结玩家位置
    /// </summary>
    void FreezePlayer()
    {
        Cards.instance.player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }

}
