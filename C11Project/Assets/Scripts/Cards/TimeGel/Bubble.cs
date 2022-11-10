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
        Destroy(gameObject);
        TimeGel.instance.BubbleStayOnMapList.Remove(gameObject);
    }
    /// <summary>
    /// 减速效果具体实现
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
    /// <summary>
    /// 减速效果重置
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Cards.instance.enemyTag)
        {
            Enemy.instance.speed /= TimeGel.instance.decelerationRatio;
            
            //collision.GetComponent<Animator>().speed /= TimeGel.instance.decelerationRatio;
        }
    }
}
