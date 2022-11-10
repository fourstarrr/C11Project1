using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Cards : MonoBehaviour
{
    /// <summary>
    /// 顺序按照初始图像显示顺序来
    /// mid,right,left
    /// </summary>
    public enum CardsType 
    {
        jetpack,//喷气背包
        timeGel,//时间胶囊
        raptorSwoop//猛禽俯冲
    }
    //当前选中卡牌
    
    public static Cards instance;

    [Header("引用物体"), Tooltip("场景中的玩家物体")]
    public GameObject player; //坐标错误可能原因是使用了预制体的玩家
    [Tooltip("玩家标签")]
    public string playerTag;
    [Tooltip("敌人标签")]
    public string enemyTag;
    [Tooltip("陷阱标签")]
    public string trapTag;

    [Header("卡牌数量"),Tooltip("最大卡牌携带数量")]
    public int cardMaxNum = 6;
    [Tooltip("喷气背包剩余数量")]
    public int jetpackRemainCardNum;
    [Tooltip("时间胶囊剩余数量")]
    public int timeGelRemainCardNum;
    [Tooltip("猛禽俯冲剩余数量")]
    public int raptorSwoopRemainCardNum;

    [Header("无卡牌时的操作提示"),Tooltip("无卡牌UI")]
    public TextMeshProUGUI noCardTip;
    [Tooltip("喷气背包文字提示")]
    public string jetpackText;
    [Tooltip("时间胶囊文字提示")]
    public string timeGelText;
    [Tooltip("猛禽俯冲文字提示")]
    public string raptorSwoopText;

    [Header("按键设置"), Tooltip("使用技能卡牌的按键设置")]
    public KeyCode specialCardKeyCode;
    [Tooltip("使用子弹时间卡牌的按键设置")]
    public KeyCode generalCardKeyCode;

    [Header("卡牌名称"), Tooltip("喷气背包")]
    public string jetpackName;
    [Tooltip("时间胶囊")]
    public string timeGelName;
    [Tooltip("猛禽俯冲")]
    public string raptorSwoopName;
    [Tooltip("当前选中卡牌")]
    public CardsType curSelectCard;

    [Header("卡牌参数"), Tooltip("技能卡牌与子弹时间的使用间隔")]
    public float specialAndGeneralCD;
    [Tooltip("技能卡牌使用间隔")]
    public float specialCardCD;
    [SerializeField, Tooltip("正在使用喷气背包")]
    bool isUsingJetpack;
    [SerializeField, Tooltip("正在使用时间胶囊")]
    bool isUsingTimeGel;
    [SerializeField, Tooltip("正在使用猛禽俯冲")]
    bool isUsingRaptorSwoop;
    [SerializeField, Tooltip("正在使用子弹时间")]
    bool isUsingBulletTime;

    float specialCardTime;
    float specialAndGeneralTime;
    private void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        
        //加载引用物体
        LoadGameObject();
    }
    private void Update()
    {
        //卡面剩余数量显示
        CardNumUIShow();
        //限制卡牌剩余数量不超过最大值
        LimitCardNum();
        //无卡牌可用时的UI提示
        NoCardsUITip();
        //获取按键信息使用卡牌
        GetKeyCodeToUseCard();
        //更新卡牌效果正在使用情况
        UpdateCardsUsingStatus();
    }
    /// <summary>
    /// 更新卡牌效果正在使用情况
    /// </summary>
    void UpdateCardsUsingStatus()
    {
        isUsingJetpack = Jetpack.instance.isUsingJetpack;
        isUsingTimeGel = TimeGel.instance.isUsingTimeGel;
        isUsingRaptorSwoop = RaptorSwoop.instance.isUsingRaptorSwoop;
        isUsingBulletTime = GeneralEffect.instance.isUsingBulletTime;
    }
    /// <summary>
    /// 添加一张新的卡牌并将新获得的卡牌作为选中的卡牌
    /// </summary>
    /// <param name="cardName">新添加卡牌名称</param>
    public void AddOneCard(string cardName)
    {
        if (cardName == jetpackName && jetpackRemainCardNum < cardMaxNum)
        {
            jetpackRemainCardNum++;
            AddCardChangeAnimation(jetpackName);
        }

        if (cardName == timeGelName && timeGelRemainCardNum < cardMaxNum)
        {
            timeGelRemainCardNum++;
            AddCardChangeAnimation(timeGelName);
        }

        if (cardName == raptorSwoopName && raptorSwoopRemainCardNum < cardMaxNum)
        {
            raptorSwoopRemainCardNum++;
            AddCardChangeAnimation(raptorSwoopName);
        }
    }
    /// <summary>
    /// 新卡牌添加的切换动画
    /// </summary>
    /// <param name="cardName">新添加卡牌名称</param>
    void AddCardChangeAnimation(string cardName)
    {
        if (cardName == jetpackName)
        {
            if (curSelectCard == CardsType.timeGel) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnRightName);
            if (curSelectCard == CardsType.raptorSwoop) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnLeftName);
        }

        if (cardName == timeGelName)
        {
            if (curSelectCard == CardsType.raptorSwoop) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnRightName);
            if (curSelectCard == CardsType.jetpack) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnLeftName);
        }

        if (cardName == raptorSwoopName)
        {
            if (curSelectCard == CardsType.jetpack) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnRightName);
            if (curSelectCard == CardsType.timeGel) CardsAnimation.instance.animator.Play(CardsAnimation.instance.turnLeftName);
        }
    }
    /// <summary>
    /// 加载引用物体
    /// </summary>
    void LoadGameObject()
    {
        if (playerTag == "") playerTag = "Player";
        if (!player) player = GameObject.FindGameObjectWithTag(playerTag);

        if (enemyTag == "") enemyTag = "Enemy";
        if (trapTag == "") trapTag = "Trap";
    }
    /// <summary>
    /// 限制卡牌剩余数量不超过最大值
    /// </summary>
    void LimitCardNum()
    {
        if (jetpackRemainCardNum > cardMaxNum) jetpackRemainCardNum = cardMaxNum;
        if (timeGelRemainCardNum > cardMaxNum) timeGelRemainCardNum = cardMaxNum;
        if (raptorSwoopRemainCardNum > cardMaxNum) raptorSwoopRemainCardNum = cardMaxNum;
    }
    /// <summary>
    /// 卡面剩余数量显示
    /// </summary>
    void CardNumUIShow()
    {
        //Debug.Log(CardsAnimation.instance.images);
        foreach (var image in CardsAnimation.instance.images)
        {
            TextMeshProUGUI childText = image.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if(image.name == jetpackName)
            {
                childText.text = jetpackRemainCardNum.ToString();
                CardColorChange(image, jetpackRemainCardNum);
            }

            if (image.name == timeGelName)
            {
                childText.text = timeGelRemainCardNum.ToString();
                CardColorChange(image, timeGelRemainCardNum);
            }

            if (image.name == raptorSwoopName)
            {
                childText.text = raptorSwoopRemainCardNum.ToString();
                CardColorChange(image, raptorSwoopRemainCardNum);
            }
        }
    }
    /// <summary>
    /// 将剩余数量为0的卡牌卡面变灰
    /// </summary>
    /// <param name="image">当前卡牌image</param>
    /// <param name="curRemainCardNum">当前牌剩余数量</param>
    void CardColorChange(Image image,int curRemainCardNum)
    {
        if (curRemainCardNum <= 0) image.color = Color.gray;
        else image.color = Color.white;
        

    }
    /// <summary>
    /// 无卡牌可用时的UI提示
    /// </summary>
    void NoCardsUITip()
    {
        switch (curSelectCard) 
        {
            case CardsType.jetpack:
                if(jetpackRemainCardNum == 0)
                {
                    noCardTip.text = jetpackText;
                    noCardTip.gameObject.SetActive(true);
                }
                else
                {
                    noCardTip.gameObject.SetActive(false);
                }
                break;
            case CardsType.timeGel:
                if (timeGelRemainCardNum == 0)
                {
                    noCardTip.text = timeGelText;
                    noCardTip.gameObject.SetActive(true);
                }
                else
                {
                    noCardTip.gameObject.SetActive(false);
                }
                break;
            case CardsType.raptorSwoop:
                if (raptorSwoopRemainCardNum == 0)
                {
                    noCardTip.text = raptorSwoopText;
                    noCardTip.gameObject.SetActive(true);
                }
                else
                {
                    noCardTip.gameObject.SetActive(false);
                }
                break;
        }
    }
    /// <summary>
    /// 获取按键信息使用卡牌
    /// </summary>
    void GetKeyCodeToUseCard()
    {
        //技能卡牌CD
        if (specialCardTime < specialCardCD)
        {
            specialCardTime += Time.deltaTime;
        }
        //子弹时间和技能卡牌的使用CD
        if (specialAndGeneralTime < specialAndGeneralCD)
        {
            specialAndGeneralTime += Time.deltaTime;
        }
        else
        {
            //特殊卡牌效果
            if (Input.GetKeyDown(specialCardKeyCode) && specialCardTime >= specialCardCD)
            {
                //重置卡牌使用CD
                specialAndGeneralTime = 0;
                specialCardTime = 0;
                //卡牌打断逻辑
                CardsBreak();
                
                //使用喷气背包
                if (curSelectCard == CardsType.jetpack && jetpackRemainCardNum > 0 && Jetpack.instance.isResetJetpackParam)
                {
                    jetpackRemainCardNum--;
                    Jetpack.instance.UseJetpack();
                    Debug.Log("使用喷气背包");
                }
                //使用时间胶囊
                if (curSelectCard == CardsType.timeGel && timeGelRemainCardNum > 0)
                {
                    timeGelRemainCardNum--;
                    TimeGel.instance.UseTimeGel();
                    Debug.Log("使用时间胶囊");
                }
                //使用猛禽俯冲
                if (curSelectCard == CardsType.raptorSwoop && raptorSwoopRemainCardNum > 0 && RaptorSwoop.instance.isResetRaptorSwoopParam)
                {
                    raptorSwoopRemainCardNum--;
                    RaptorSwoop.instance.UseRaptorSwoop();
                    Debug.Log("使用猛禽俯冲");
                }
            }
            //子弹时间
            if (Input.GetKeyDown(generalCardKeyCode) && !isUsingBulletTime)
            {
                specialAndGeneralTime = 0;
                GeneralEffect.instance.UseGeneralEffect();
                Debug.Log("使用子弹时间");
                if (curSelectCard == CardsType.jetpack && jetpackRemainCardNum > 0)
                {
                    jetpackRemainCardNum--;
                }
                if (curSelectCard == CardsType.timeGel && timeGelRemainCardNum > 0)
                {
                    timeGelRemainCardNum--;
                }
                if (curSelectCard == CardsType.raptorSwoop && raptorSwoopRemainCardNum > 0)
                {
                    raptorSwoopRemainCardNum--;
                }
            }
        }
    }
    //卡牌间的打断逻辑
    public void CardsBreak()
    {
        //喷气背包会打断猛禽俯冲的技能
        if(curSelectCard == CardsType.jetpack)
        {
            RaptorSwoop.instance.isUsingRaptorSwoop = false;
        }
        //猛禽俯冲会打断喷气背包的技能
        if (curSelectCard == CardsType.raptorSwoop)
        {
            Jetpack.instance.isUsingJetpack = false;
        }
        //打断子弹时间
        GeneralEffect.instance.isUsingBulletTime = false;
    }
}
