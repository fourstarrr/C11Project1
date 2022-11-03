using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEffect : MonoBehaviour
{
    public static GeneralEffect instance;

    GameObject[] allEnermy;
    [Header("有速度的物品标签"), Tooltip("Enemy的tag名")]
    public string enermyTag = "Enemy";

    [Header("子弹时间参数"), Tooltip("子弹时间减速效果,值越大减速效果越明显"), Range(0.1f, 1)]
    public float decelerationRatio;
    [Tooltip("子弹时间长度,值越大减速效果越明显"), Range(0.1f, 5)]
    public float bulletTime;

    
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;

        FindAllObjectWithSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FindAllObjectWithSpeed()
    {
        if (enermyTag == "") enermyTag = "enemy";
        allEnermy = GameObject.FindGameObjectsWithTag(enermyTag);
    }
    /// <summary>
    /// 使用通用效果子弹时间，让所有物体减速一段时间
    /// 目前已经添加的物体：player，enermy
    /// </summary>
    public void UseGeneralEffect()
    {
        ResetJumpTime();

        StartCoroutine(DecelerationBubbleSpeed());
        StartCoroutine(PlayerBulletTime());
        foreach (GameObject enemy in allEnermy)
        {
            //Debug.Log(enemy.name);
            StartCoroutine(EnemyBulletTime(enemy));
        }
    }
    /// <summary>
    /// 重置跳跃次数
    /// 需要跟xingo沟通
    /// </summary>
    void ResetJumpTime()
    {
        
    }
    /// <summary>
    /// 时间胶囊的减速泡的子弹时间
    /// </summary>
    /// <returns></returns>
    IEnumerator DecelerationBubbleSpeed()
    {
        TimeGel.instance.bubbleSpeed *= decelerationRatio;
        yield return new WaitForSeconds(bulletTime);
        TimeGel.instance.bubbleSpeed /= decelerationRatio;
    }
    /// <summary>
    /// 敌人的子弹时间协程
    /// 组件名需要与雪飞沟通
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    IEnumerator EnemyBulletTime(GameObject enemy)
    {
        if(!enemy.GetComponent<Enemy>()) yield break;
        enemy.GetComponent<Enemy>().speed *= decelerationRatio;
        yield return new WaitForSeconds(bulletTime);
        enemy.GetComponent<Enemy>().speed /= decelerationRatio;
    }
    /// <summary>
    /// 玩家的子弹时间协程
    /// 组件名需要与xingo沟通
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerBulletTime()
    {
        if (!Cards.instance.player.GetComponent<PlayerController>()) yield break;
        Cards.instance.player.GetComponent<PlayerController>().speed *= decelerationRatio;
        yield return new WaitForSeconds(bulletTime);
        Cards.instance.player.GetComponent<PlayerController>().speed /= decelerationRatio;
    }
}
