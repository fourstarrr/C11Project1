using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform CannonDirection;
    public GameObject bullet;
    private float time=0;

    public float bulletShotFrequency;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Bullet();

        time = time + Time.deltaTime;
        if (time >= bulletShotFrequency)
        {
            Bullet();
            Debug.Log("bullet");
            time = 0;
        }
    }

    public void Bullet()
    {
        var clone = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        clone.gameObject.GetComponent<CannonBullet>().cannonDirection = CannonDirection;
    }
}
