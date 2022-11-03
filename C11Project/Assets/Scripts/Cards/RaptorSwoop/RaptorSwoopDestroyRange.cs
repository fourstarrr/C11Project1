using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorSwoopDestroyRange : MonoBehaviour
{
    public static RaptorSwoopDestroyRange instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
            Destroy(instance);
        instance = this;
    }
    // && RaptorSwoop.instance.isUsingRaptorSwoop 解决人物抖动问题
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //落地时能消灭一定范围内的敌人和陷阱或破坏可破坏的平台
        if (RaptorSwoop.instance.destroyTagList.Contains(collision.tag) && RaptorSwoop.instance.isDestroyEnemyAndTrap && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            Debug.Log("正在消灭一定范围内的敌人和陷阱或破坏可破坏的平台");
            Destroy(collision.gameObject);
        }
        //更换跑道
        if (RaptorSwoop.instance.passingPlaneTagList.Contains(collision.tag) && !RaptorSwoop.instance.isPastOnePlane && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            Debug.Log("正在改变跑道");
            Cards.instance.player.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //落地时能消灭一定范围内的敌人和陷阱或破坏可破坏的平台
        if (RaptorSwoop.instance.destroyTagList.Contains(collision.tag) && RaptorSwoop.instance.isDestroyEnemyAndTrap && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            Debug.Log("正在消灭一定范围内的敌人和陷阱或破坏可破坏的平台");
            Destroy(collision.gameObject);
        }
        //更换跑道
        if (RaptorSwoop.instance.passingPlaneTagList.Contains(collision.tag) && !RaptorSwoop.instance.isPastOnePlane && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            
            Debug.Log("正在改变跑道");
            Cards.instance.player.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //跑道变更完毕
        if (RaptorSwoop.instance.passingPlaneTagList.Contains(collision.tag) && !RaptorSwoop.instance.isPastOnePlane && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            RaptorSwoop.instance.isPastOnePlane = true;
            Debug.Log("跑道变更完毕");
            Cards.instance.player.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    /// <summary>
    /// 初始化设置消灭范围为玩家脚底一块区域
    /// </summary>
    public void SetInitPosition()
    {
        transform.SetParent(Cards.instance.player.transform);
        
        /*transform.position = new Vector3(
            Cards.instance.player.transform.position.x,
            Cards.instance.player.transform.position.y - Cards.instance.player.transform.GetComponent<Collider2D>().bounds.size.y / 2,
            Cards.instance.player.transform.position.z);*/  //生成于脚下
        transform.position = Cards.instance.player.transform.position;
    }
}
