using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorSwoop : MonoBehaviour
{
    public static RaptorSwoop instance;
    [Header("引用物体"), Tooltip("自动设置猛禽俯冲的落地消灭范围于玩家脚底")]
    public bool automaticSetPosition;
    [Tooltip("地面层")]
    public LayerMask layerGround;

    [Header("状态参数"), Tooltip("正在使用猛禽俯冲")]
    public bool isUsingRaptorSwoop;
    [Tooltip("人物处于地面")]
    public bool isOnGround;
    [Tooltip("消灭一定范围的敌人和陷阱")]
    public bool isDestroyEnemyAndTrap;
    [Tooltip("是否已经穿过一次平台")]
    public bool isPastOnePlane;
    [Tooltip("正在向前弹射")]
    public bool isEjectionForward;
    [Tooltip("是否已经重置猛禽俯冲参数")]
    public bool isResetRaptorSwoopParam;

    [Header("猛禽俯冲参数"),Tooltip("猛禽俯冲速度"), Range(100, 2000)]
    public float raptorSwoopSpeed;
    [Tooltip("弹射速度"), Range(1,50)]
    public float ejectionSpeed;
    [Tooltip("弹射时间"), Range(0,10)]
    public float ejectionTime;
    [Tooltip("猛禽俯冲时消灭的物品或平台标签列表")]
    public List<string> destroyTagList;
    [Tooltip("猛禽俯冲时可穿越的平台标签列表")]
    public List<string> passingPlaneTagList;

    
    //bool pastDestroyPlane;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsingRaptorSwoop)
        {
            isResetRaptorSwoopParam = false;
            //地面检测
            OnGroundCheck();
            //弹射控制
            if (isEjectionForward)
            {
                EjectionForward();
            }
            //将破环范围置于玩家位置
            if (automaticSetPosition)
            {
                RaptorSwoopDestroyRange.instance.SetInitPosition();
            }
            //破坏完可破坏地面后落于不可破坏地面
            //或更换跑道后破坏完可破坏地面后落于不可破坏地面
            if (isOnGround && isPastOnePlane)
            {
                //短暂停留后向前弹射
                StartEjectionForward();
                Invoke("EndRaptorSwoop", ejectionTime);
            }
            //猛禽俯冲状态：
            //破坏一切可破坏的平台与敌人和陷阱
            else
            {
                //快速垂直下落
                RapidDown();
                //落地时能消灭一定范围内的敌人和陷阱或破坏可破坏的平台
                DestroyEnemyAndTrap();
            }
        }
        //猛禽俯冲结束后重置参数
        else if(!isResetRaptorSwoopParam)
        {
            ResetResetRaptorSwoopParamParam();
        }
    }
    public void UseRaptorSwoop()
    {
        isUsingRaptorSwoop = true;
    }
    /// <summary>
    /// 快速垂直下落
    /// </summary>
    void RapidDown()
    {
        Debug.Log("正在进行猛禽俯冲快速下落");
        Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.down * Time.deltaTime * raptorSwoopSpeed + Physics2D.gravity;
    }
    /// <summary>
    /// 落地时能消灭一定范围内的敌人和陷阱
    /// 或破坏可破坏的平台
    /// </summary>
    void DestroyEnemyAndTrap()
    {
        RaptorSwoopDestroyRange.instance.gameObject.SetActive(true);
        isDestroyEnemyAndTrap = true;
    }
    void EjectionForward()
    {
        Debug.Log("正在前冲");
        //Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.right * Time.deltaTime * ejectionSpeed;
        //Cards.instance.player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Time.deltaTime * ejectionSpeed);
        Cards.instance.player.transform.Translate(Vector2.right * Time.deltaTime * ejectionSpeed);
        //Debug.Log(Cards.instance.player.GetComponent<Rigidbody2D>().velocity);
    }
    /// <summary>
    /// 开始弹射
    /// </summary>
    void StartEjectionForward()
    {
        RaptorSwoopDestroyRange.instance.gameObject.SetActive(false);
        isEjectionForward = true;
    }
    /// <summary>
    /// 结束弹射
    /// </summary>
    void EndRaptorSwoop()
    {
        isUsingRaptorSwoop = false;
        Debug.Log("结束猛禽俯冲");
        return;
    }
    /// <summary>
    /// 重置参数
    /// </summary>
    void ResetResetRaptorSwoopParamParam()
    {
        RaptorSwoopDestroyRange.instance.gameObject.SetActive(false);
        isDestroyEnemyAndTrap = false;
        isEjectionForward = false;
        isPastOnePlane = false;
        isResetRaptorSwoopParam = true;
    }
    void OnGroundCheck()
    {
        isOnGround = Cards.instance.player.GetComponent<Collider2D>().IsTouchingLayers(layerGround);

    }
}
