using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(50, 100)]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal"), transform.GetComponent<Rigidbody2D>().velocity.y);
    }
}
