using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : MonoBehaviour
{

    public Transform point1, point2, point3, point4,point5,point6,point7;
    private Transform pointTarget;
    public float speed;
    private bool direction = true;
    // Start is called before the first frame update
    void Start()
    {
        pointTarget = point1;
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.position = Vector2.MoveTowards(transform.position, pointTarget.position, speed * Time.deltaTime); 
        if(pointTarget==point4)
        {
            Invoke("moving", 0.15f);
        }
        else
        {
          moving();
        }
        
    }
    private void moving()
    {
        if (direction) { 
        if (Vector2.Distance(transform.position, point1.position) <= 0.01f)
        {
            pointTarget = point2;
            Debug.Log("2");
            
        }
        if (Vector2.Distance(transform.position, point2.position) <= 0.01f)
        {
            pointTarget = point3;
            
            Debug.Log("3");
        }
        if (Vector2.Distance(transform.position, point3.position) <= 0.01f)
        {
            pointTarget = point4;
           
                Debug.Log("4");
        }
        if (Vector2.Distance(transform.position, point4.position) <= 0.01f)
        {
            pointTarget = point5;
            Debug.Log("5");
        }
        if (Vector2.Distance(transform.position, point5.position) <= 0.01f)
        {
            pointTarget = point6;
                Debug.Log("6");
            }
        if (Vector2.Distance(transform.position, point6.position) <= 0.01f)
        {
            pointTarget = point7;
                Debug.Log("7");
                direction = false;
        }   
        }
        else { 
      
        if (Vector2.Distance(transform.position, point7.position) <= 0.01f)
        {
            pointTarget = point6;
        }
        if (Vector2.Distance(transform.position, point6.position) <= 0.01f)
        {
            pointTarget = point5;
        }
        if (Vector2.Distance(transform.position, point5.position) <= 0.01f)
        {
            pointTarget = point4;
        }
        if (Vector2.Distance(transform.position, point4.position) <= 0.01f)
        {
            pointTarget = point3;
        }
        if (Vector2.Distance(transform.position, point3.position) <= 0.01f)
        {
            pointTarget = point2;
        }
        if (Vector2.Distance(transform.position, point2.position) <= 0.01f)
        {
            pointTarget = point1;
                direction = true;
        }
        }
    }
}
