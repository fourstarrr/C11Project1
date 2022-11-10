using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    public static Jetpack instance;

    [Tooltip("����㼶")]
    public LayerMask layerGroundMask;   //��Ҫ��ѩ�ƹ�ͨ

    [Header("״̬����"), Tooltip("���ڻ���")]
    public bool isGliding;
    [Tooltip("����ʹ����������")]
    public bool isUsingJetpack;

    [Header("������������"), Tooltip("����λ�ƾ���"), Range(1, 50)]
    public float upDistance;
    [Tooltip("���������ٶ�"), Range(1, 10)]
    public float jetpackSpeed;
    [Tooltip("��ؽ���������"), Range(1, 50)]
    public float reachGroundDistance;
    [Tooltip("�������¿���")]
    public KeyCode controllDownGlidingKeycode;
    [Tooltip("�������¿����ٶ�"), Range(50, 2000)]
    public float downGlidingSpeed;
    [Tooltip("�Ƿ�������������������")]
    public bool isResetJetpackParam;

    [Header("Debug����")]
    [SerializeField, Tooltip("�����ʼλ��")] float originY;
    [SerializeField, Tooltip("�����������λ��")] float targetY;
    [SerializeField, Tooltip("��ҵ�ǰλ��")] float curPlayerY;
    [SerializeField, Tooltip("���������ʱ��")] float upTime;
    [SerializeField, Tooltip("�������������ʾ���ƿ���")] bool switchRayReachGround;
    [SerializeField, Tooltip("�������������ʱ����")] bool switchReachGroundFrozen;
    [SerializeField, Tooltip("Debug�ı���ʾ")] bool switchDebugText;


    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        DebugController();
        //ʹ������������ʼ����λ��
        if (isUsingJetpack)
        {
            isResetJetpackParam = false;
            //�������������׶�
            if (!isGliding)
            {
                //��ʼ��ʱ
                UpTimer();
                //����ʹ��������������
                UpwardDisplacement();
                Debug.Log("����ʹ��������������");
                //����ָ��λ�ƾ���
                if (IsReachTagetDistance())
                {
                    Cards.instance.player.GetComponent<Collider2D>().isTrigger = false;
                    Debug.Log("�ѵ���ָ���߶�");
                    isGliding = true;
                }
                //�������㹻ʱ��
                if (IsReachJetpackTime())
                {
                    Cards.instance.player.GetComponent<Collider2D>().isTrigger = false;
                    Debug.Log("�ѵ���ָ���߶�");
                    isGliding = true;
                }
            }
            //��ʼ����
            else
            {
                Cards.instance.player.GetComponent<SpriteRenderer>().color = Color.red;
                //�����������������״̬
                if (IsComingToGround())
                {
                    isUsingJetpack = false;
                    Debug.Log("�������");
                    ResetJetpackParam();
                }
                //�������¿���
                ControllDownGliding();
            }
        }
        else if (!isResetJetpackParam)
        {
            ResetJetpackParam();
        }
    }
    void ResetJetpackParam()
    {
        isGliding = false;
        upTime = 0;
        Cards.instance.player.GetComponent<SpriteRenderer>().color = Color.white;
        isResetJetpackParam = true;
    }

    /// <summary>
    /// Debug������
    /// </summary>
    void DebugController()
    {
        if (switchRayReachGround)
        {
            ShowReachGroundRay();
        }
        if (switchReachGroundFrozen && isGliding && IsComingToGround())
        {
            FreezePlayer();
        }

        if (switchDebugText)
        {
            Debug.unityLogger.filterLogType = LogType.Log; //��ʾ����
        }
        else
        {
            Debug.unityLogger.filterLogType = LogType.Error; //ֻ��ʾError + Exception
        }
    }
    /// <summary>
    /// ʹ����������
    /// </summary>
    public void UseJetpack()
    {
        isUsingJetpack = true;
        originY = Cards.instance.player.transform.position.y;
        targetY = originY + upDistance;
        Cards.instance.player.GetComponent<Collider2D>().isTrigger = true;
    }
    /// <summary>
    /// ����λ��һ�ξ���
    /// </summary>
    void UpwardDisplacement()
    {
        Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.up * jetpackSpeed;
    }
    /// <summary>
    /// �Ƿ�ɵ�ָ���߶�
    /// </summary>
    /// <returns></returns>
    bool IsReachTagetDistance()
    {
        curPlayerY = Cards.instance.player.transform.position.y;
        return curPlayerY >= targetY;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool IsReachJetpackTime()
    {
        float Time = upDistance / jetpackSpeed;
        return Time < upTime;
    }
    void UpTimer()
    {
        upTime += Time.deltaTime;
    }
    /// <summary>
    /// �Ƿ񵽴���ؽ���������
    /// </summary>
    bool IsComingToGround()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(Cards.instance.player.transform.position, Vector2.down, reachGroundDistance, layerGroundMask);

        return raycastHit2D;
    }
    /// <summary>
    /// ���а����¼���������
    /// </summary>
    void ControllDownGliding()
    {
        PlayerController.instance.downSpeedUp();
        //if (Input.GetKey(controllDownGlidingKeycode))
        //{
        //    Cards.instance.player.GetComponent<Rigidbody2D>().velocity = Cards.instance.player.GetComponent<Rigidbody2D>().velocity + Vector2.down * downGlidingSpeed * Time.deltaTime;
        //}
    }
    /// <summary>
    /// ��ʾ��ؽ����������
    /// </summary>
    void ShowReachGroundRay()
    {
        Debug.DrawRay(Cards.instance.player.transform.position, Vector2.down * reachGroundDistance, Color.red);
    }

    /// <summary>
    /// �������λ��
    /// </summary>
    void FreezePlayer()
    {
        Cards.instance.player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
    }

}
