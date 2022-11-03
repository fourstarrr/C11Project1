using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.Play("BubbleEnlager");
    }

    // Update is called once per frame
    void Update()
    {
        bubbleMove();
    }
    void bubbleMove()
    {
        transform.Translate(Vector2.right * TimeGel.instance.bubbleSpeed * Time.deltaTime);
    }
    /// <summary>
    /// 超出摄像机范围销毁物体的事件
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    /// <summary>
    /// 减速效果具体实现
    /// 需要跟雪菲沟通tag还是layer
    /// 类别名（enermyTag）
    /// 需要敌人脚本名字(enemy)和速度名(speed)
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == Cards.instance.enemyTag)
        {
            Enemy.instance.speed *= TimeGel.instance.decelerationRatio;
            //collision.GetComponent<Animator>().speed *= TimeGel.instance.decelerationRatio;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Cards.instance.enemyTag)
        {
            Enemy.instance.speed /= TimeGel.instance.decelerationRatio;
            
            //collision.GetComponent<Animator>().speed /= TimeGel.instance.decelerationRatio;
        }
    }
}
