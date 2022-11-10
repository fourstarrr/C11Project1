using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Ѫ��")]
    public int HP;
    [Header("����")]
    public int score;

   
    [Header("�޵�֡ʱ��")]
    public float invincibleframe; 
    [Header("�˴������޵л���Ԥ����")]
    public GameObject Invincible;

    [Header("״̬������"), Tooltip("�ɷ�������ʧ�Ŀ���")]
    public bool switchDeathDestroy;

    public int nowHealthy;

    private float injurytime;
    private float speed;
    private float noInjurytime;

    private bool isHurt = false;
    private bool isInvincible;
   

    public int Healthy;

    private PlayerController pc;
    void Start()
    {
        pc = GetComponent<PlayerController>();
        Healthy = HP;
        nowHealthy = Healthy;
        injurytime = 10;
        speed = pc.speed;
    }

    // Update is called once per frame
    void Update()
    {
        Dead();

        DamgeTime();
        LifeBack();
    }
    public void GetDamgae()
    {
        Healthy -= 1;
        injurytime = 0;
    }
    private void LifeBack()
    {
        if(Healthy<HP&&noInjurytime>10)
        {
            noInjurytime = 0;
            Healthy++;
            nowHealthy++;
        }

    }
    public void Dead()
    {
        if (nowHealthy > Healthy && Healthy > 0 && injurytime <= invincibleframe)
        {
            Invincible.SetActive(true);
            isHurt = true;
            pc.speed = speed * 0.85f;
            noInjurytime = 0;
        }
        else if (nowHealthy > Healthy && Healthy > 0 && injurytime > invincibleframe)
        {
            nowHealthy = Healthy;
            Invincible.SetActive(false);
            isHurt = false;
            pc.speed = speed;
        }
        if (Healthy <= 0)
        {
           // if (switchDeathDestroy) 
                Destroy(gameObject); //cxy 
        }
    }
    private void DamgeTime()
    {
        injurytime += Time.deltaTime;
        noInjurytime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.tag == "Trap" || other.tag == "Enemy" && !isHurt)
        {

            GetDamgae();

        }
        if (other.tag == "Gold")
        {
            Destroy(other.gameObject);
            score++;
        }
        if (other.tag == "DeadLine")
        {
            if (switchDeathDestroy) Destroy(gameObject);
        }
    }
}
