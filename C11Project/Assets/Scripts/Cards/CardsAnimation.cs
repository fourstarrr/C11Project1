using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardsAnimation : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Image[] images;
    [Tooltip("鼠标滚轮间隔")]
    public float deltaTimeInit = 0.4f;
    [Tooltip("卡牌向左切换动画名字")]
    public string turnLeftName;
    [Tooltip("卡牌向右切换动画名字")]
    public string turnRightName;
    float deltaTime;

    public static CardsAnimation instance;
    // Start is called before the first frame update
    void Start()
    {
        images = GetComponentsInChildren<Image>();
        deltaTime = deltaTimeInit;
        animator = GetComponent<Animator>();
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        images = GetComponentsInChildren<Image>();
        AnimatorController();
        UpdateCurSelectCard();
        UpdataImageInfo();
    }
    /// <summary>
    /// 卡牌切换动画
    /// TODO：有个偶发性bug，3副画面会有1帧的闪烁
    /// 原因：动画播放完毕后返回第一帧然后再执行的动画事件
    /// 暂时解决方案：加快动画播放速度
    /// </summary>
    void AnimatorController()
    {
        float mouseValue = Input.GetAxis("Mouse ScrollWheel");
        if(deltaTime > 0)
        {
            deltaTime -= Time.deltaTime;
        }
        else
        {
            if (mouseValue < 0)
            {
                animator.Play(turnRightName);
                deltaTime = deltaTimeInit;
            }
            else if (mouseValue > 0)
            {
                animator.Play(turnLeftName);
                deltaTime = deltaTimeInit;
            }
        }
    }
    void UpdateCurSelectCard()
    {
        Vector3 maxScale = Vector3.zero;
        foreach(var image in images)
        {
            if (image.GetComponent<RectTransform>().localScale.x > maxScale.x)
            {
                maxScale = image.GetComponent<RectTransform>().localScale;
            }
        }
        foreach(var image in images)
        {
            if(image.GetComponent<RectTransform>().localScale == maxScale)
            {
                var cardName = image.GetComponent<Image>().sprite.name;
                if (cardName == Cards.instance.jetpackName)
                {
                    Cards.instance.curSelectCard = Cards.CardsType.jetpack;
                }

                if (cardName == Cards.instance.timeGelName)
                {
                    Cards.instance.curSelectCard = Cards.CardsType.timeGel;
                }

                if (cardName == Cards.instance.raptorSwoopName)
                {
                    Cards.instance.curSelectCard = Cards.CardsType.raptorSwoop;
                }
            }
        }
        //Debug.Log(Cards.curSelectCard);
    }
    void UpdataImageInfo()
    {
        foreach(var image in images)
        {
            image.name = image.sprite.name;
            image.transform.GetChild(0).name = image.sprite.name;
        }
    }
    void ChangeColorLeft()
    {
        var temp = images[0].sprite;
        images[0].sprite = images[1].sprite;
        images[1].sprite = images[2].sprite;
        images[2].sprite = temp;

    }
    void ChangeColorRight()
    {
        var temp = images[2].sprite;
        images[2].sprite = images[1].sprite;
        images[1].sprite = images[0].sprite;
        images[0].sprite = temp;

    }
}
