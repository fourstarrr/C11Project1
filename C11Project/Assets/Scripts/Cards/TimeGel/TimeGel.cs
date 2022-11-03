using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGel : MonoBehaviour
{
    [Header("减速气泡"),Tooltip("减速气泡预制体")]
    public GameObject bubblePrefab;
    [Tooltip("减速气泡移速，值越大越快"),Range(1,10)]
    public float bubbleSpeed;
    [Tooltip("减速气泡生成位置(正值x向右，正值y向上)")]
    public Vector3 bubblePos = new Vector3(1,0,0);
    [Tooltip("气泡减速效果,值越大减速效果越明显"),Range(0.1f,1)]
    public float decelerationRatio;

    public static TimeGel instance;

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

    }
    public void UseTimeGel()
    {
        GameObject newBubble = Instantiate(bubblePrefab,Cards.instance.player.transform.localPosition + bubblePos, Quaternion.identity);
    }
}
