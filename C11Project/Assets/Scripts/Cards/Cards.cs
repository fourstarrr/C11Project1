using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Cards : MonoBehaviour
{
    /// <summary>
    /// ˳���ճ�ʼͼ����ʾ˳����
    /// mid,right,left
    /// </summary>
    public enum CardsType 
    {
        jetpack,//��������
        timeGel,//ʱ�佺��
        raptorSwoop//���ݸ���
    }
    //��ǰѡ�п���
    
    public static Cards instance;

    [Header("��������"), Tooltip("�����е��������")]
    public GameObject player; //����������ԭ����ʹ����Ԥ��������
    [Tooltip("��ұ�ǩ")]
    public string playerTag;
    [Tooltip("���˱�ǩ")]
    public string enemyTag;
    [Tooltip("�����ǩ")]
    public string trapTag;

    [Header("��������"),Tooltip("�����Я������")]
    public int cardMaxNum = 6;
    [Tooltip("��������ʣ������")]
    public int jetpackRemainCardNum;
    [Tooltip("ʱ�佺��ʣ������")]
    public int timeGelRemainCardNum;
    [Tooltip("���ݸ���ʣ������")]
    public int raptorSwoopRemainCardNum;

    [Header("�޿���ʱ�Ĳ�����ʾ"),Tooltip("�޿���UI")]
    public TextMeshProUGUI noCardTip;
    [Tooltip("��������������ʾ")]
    public string jetpackText;
    [Tooltip("ʱ�佺��������ʾ")]
    public string timeGelText;
    [Tooltip("���ݸ���������ʾ")]
    public string raptorSwoopText;

    [Header("��������"), Tooltip("ʹ�ü��ܿ��Ƶİ�������")]
    public KeyCode specialCardKeyCode;
    [Tooltip("ʹ���ӵ�ʱ�俨�Ƶİ�������")]
    public KeyCode generalCardKeyCode;

    [Header("��������"), Tooltip("��������")]
    public string jetpackName;
    [Tooltip("ʱ�佺��")]
    public string timeGelName;
    [Tooltip("���ݸ���")]
    public string raptorSwoopName;
    [Tooltip("��ǰѡ�п���")]
    public CardsType curSelectCard;

    [Header("���Ʋ���"), Tooltip("���ܿ������ӵ�ʱ���ʹ�ü��")]
    public float specialAndGeneralCD;
    [Tooltip("���ܿ���ʹ�ü��")]
    public float specialCardCD;
    [SerializeField, Tooltip("����ʹ����������")]
    bool isUsingJetpack;
    [SerializeField, Tooltip("����ʹ��ʱ�佺��")]
    bool isUsingTimeGel;
    [SerializeField, Tooltip("����ʹ�����ݸ���")]
    bool isUsingRaptorSwoop;
    [SerializeField, Tooltip("����ʹ���ӵ�ʱ��")]
    bool isUsingBulletTime;

    float specialCardTime;
    float specialAndGeneralTime;
    private void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        
        //������������
        LoadGameObject();
    }
    private void Update()
    {
        //����ʣ��������ʾ
        CardNumUIShow();
        //���ƿ���ʣ���������������ֵ
        LimitCardNum();
        //�޿��ƿ���ʱ��UI��ʾ
        NoCardsUITip();
        //��ȡ������Ϣʹ�ÿ���
        GetKeyCodeToUseCard();
        //���¿���Ч������ʹ�����
        UpdateCardsUsingStatus();
    }
    /// <summary>
    /// ���¿���Ч������ʹ�����
    /// </summary>
    void UpdateCardsUsingStatus()
    {
        isUsingJetpack = Jetpack.instance.isUsingJetpack;
        isUsingTimeGel = TimeGel.instance.isUsingTimeGel;
        isUsingRaptorSwoop = RaptorSwoop.instance.isUsingRaptorSwoop;
        isUsingBulletTime = GeneralEffect.instance.isUsingBulletTime;
    }
    /// <summary>
    /// ���һ���µĿ��Ʋ����»�õĿ�����Ϊѡ�еĿ���
    /// </summary>
    /// <param name="cardName">����ӿ�������</param>
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
    /// �¿�����ӵ��л�����
    /// </summary>
    /// <param name="cardName">����ӿ�������</param>
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
    /// ������������
    /// </summary>
    void LoadGameObject()
    {
        if (playerTag == "") playerTag = "Player";
        if (!player) player = GameObject.FindGameObjectWithTag(playerTag);

        if (enemyTag == "") enemyTag = "Enemy";
        if (trapTag == "") trapTag = "Trap";
    }
    /// <summary>
    /// ���ƿ���ʣ���������������ֵ
    /// </summary>
    void LimitCardNum()
    {
        if (jetpackRemainCardNum > cardMaxNum) jetpackRemainCardNum = cardMaxNum;
        if (timeGelRemainCardNum > cardMaxNum) timeGelRemainCardNum = cardMaxNum;
        if (raptorSwoopRemainCardNum > cardMaxNum) raptorSwoopRemainCardNum = cardMaxNum;
    }
    /// <summary>
    /// ����ʣ��������ʾ
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
    /// ��ʣ������Ϊ0�Ŀ��ƿ�����
    /// </summary>
    /// <param name="image">��ǰ����image</param>
    /// <param name="curRemainCardNum">��ǰ��ʣ������</param>
    void CardColorChange(Image image,int curRemainCardNum)
    {
        if (curRemainCardNum <= 0) image.color = Color.gray;
        else image.color = Color.white;
        

    }
    /// <summary>
    /// �޿��ƿ���ʱ��UI��ʾ
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
    /// ��ȡ������Ϣʹ�ÿ���
    /// </summary>
    void GetKeyCodeToUseCard()
    {
        //���ܿ���CD
        if (specialCardTime < specialCardCD)
        {
            specialCardTime += Time.deltaTime;
        }
        //�ӵ�ʱ��ͼ��ܿ��Ƶ�ʹ��CD
        if (specialAndGeneralTime < specialAndGeneralCD)
        {
            specialAndGeneralTime += Time.deltaTime;
        }
        else
        {
            //���⿨��Ч��
            if (Input.GetKeyDown(specialCardKeyCode) && specialCardTime >= specialCardCD)
            {
                //���ÿ���ʹ��CD
                specialAndGeneralTime = 0;
                specialCardTime = 0;
                //���ƴ���߼�
                CardsBreak();
                
                //ʹ����������
                if (curSelectCard == CardsType.jetpack && jetpackRemainCardNum > 0 && Jetpack.instance.isResetJetpackParam)
                {
                    jetpackRemainCardNum--;
                    Jetpack.instance.UseJetpack();
                    Debug.Log("ʹ����������");
                }
                //ʹ��ʱ�佺��
                if (curSelectCard == CardsType.timeGel && timeGelRemainCardNum > 0)
                {
                    timeGelRemainCardNum--;
                    TimeGel.instance.UseTimeGel();
                    Debug.Log("ʹ��ʱ�佺��");
                }
                //ʹ�����ݸ���
                if (curSelectCard == CardsType.raptorSwoop && raptorSwoopRemainCardNum > 0 && RaptorSwoop.instance.isResetRaptorSwoopParam)
                {
                    raptorSwoopRemainCardNum--;
                    RaptorSwoop.instance.UseRaptorSwoop();
                    Debug.Log("ʹ�����ݸ���");
                }
            }
            //�ӵ�ʱ��
            if (Input.GetKeyDown(generalCardKeyCode) && !isUsingBulletTime)
            {
                specialAndGeneralTime = 0;
                GeneralEffect.instance.UseGeneralEffect();
                Debug.Log("ʹ���ӵ�ʱ��");
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
    //���Ƽ�Ĵ���߼�
    public void CardsBreak()
    {
        //���������������ݸ���ļ���
        if(curSelectCard == CardsType.jetpack)
        {
            RaptorSwoop.instance.isUsingRaptorSwoop = false;
        }
        //���ݸ���������������ļ���
        if (curSelectCard == CardsType.raptorSwoop)
        {
            Jetpack.instance.isUsingJetpack = false;
        }
        //����ӵ�ʱ��
        GeneralEffect.instance.isUsingBulletTime = false;
    }
}
