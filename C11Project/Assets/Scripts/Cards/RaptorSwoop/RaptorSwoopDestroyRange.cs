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
    // && RaptorSwoop.instance.isUsingRaptorSwoop ������ﶶ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���ʱ������һ����Χ�ڵĵ��˺�������ƻ����ƻ���ƽ̨
        if (RaptorSwoop.instance.destroyTagList.Contains(collision.tag) && RaptorSwoop.instance.isDestroyEnemyAndTrap && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            Debug.Log("��������һ����Χ�ڵĵ��˺�������ƻ����ƻ���ƽ̨");
            Destroy(collision.gameObject);
        }
        //�����ܵ�
        if (RaptorSwoop.instance.passingPlaneTagList.Contains(collision.tag) && !RaptorSwoop.instance.isPastOnePlane && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            Debug.Log("���ڸı��ܵ�");
            Cards.instance.player.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //���ʱ������һ����Χ�ڵĵ��˺�������ƻ����ƻ���ƽ̨
        if (RaptorSwoop.instance.destroyTagList.Contains(collision.tag) && RaptorSwoop.instance.isDestroyEnemyAndTrap && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            Debug.Log("��������һ����Χ�ڵĵ��˺�������ƻ����ƻ���ƽ̨");
            Destroy(collision.gameObject);
        }
        //�����ܵ�
        if (RaptorSwoop.instance.passingPlaneTagList.Contains(collision.tag) && !RaptorSwoop.instance.isPastOnePlane && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            
            Debug.Log("���ڸı��ܵ�");
            Cards.instance.player.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�ܵ�������
        if (RaptorSwoop.instance.passingPlaneTagList.Contains(collision.tag) && !RaptorSwoop.instance.isPastOnePlane && RaptorSwoop.instance.isUsingRaptorSwoop)
        {
            RaptorSwoop.instance.isPastOnePlane = true;
            Debug.Log("�ܵ�������");
            Cards.instance.player.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    /// <summary>
    /// ��ʼ����������ΧΪ��ҽŵ�һ������
    /// </summary>
    public void SetInitPosition()
    {
        transform.SetParent(Cards.instance.player.transform);
        
        /*transform.position = new Vector3(
            Cards.instance.player.transform.position.x,
            Cards.instance.player.transform.position.y - Cards.instance.player.transform.GetComponent<Collider2D>().bounds.size.y / 2,
            Cards.instance.player.transform.position.z);*/  //�����ڽ���
        transform.position = Cards.instance.player.transform.position;
    }
}
