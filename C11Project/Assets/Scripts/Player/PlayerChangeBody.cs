using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeBody : MonoBehaviour
{
    private PlayerController PlayerController;


        private void Start()
    {
        PlayerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(PlayerController.isOnUpHill && !PlayerController.isOnDownHill)
        {
            transform.localEulerAngles = new Vector3(0, 0, 30);
        }
        else if(!PlayerController.isOnUpHill && PlayerController.isOnDownHill)
        {
            transform.localEulerAngles = new Vector3(0, 0, -30);
        }
        else if (!PlayerController.isOnUpHill && !PlayerController.isOnDownHill)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
}
