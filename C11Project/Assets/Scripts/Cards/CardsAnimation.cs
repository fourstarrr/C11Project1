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
    [Tooltip("�����ּ��")]
    public float deltaTimeInit = 0.4f;
    [Tooltip("���������л���������")]
    public string turnLeftName;
    [Tooltip("���������л���������")]
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
    /// �����л�����
    /// TODO���и�ż����bug��3���������1֡����˸
    /// ԭ�򣺶���������Ϻ󷵻ص�һ֡Ȼ����ִ�еĶ����¼�
    /// ��ʱ����������ӿ춯�������ٶ�
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
