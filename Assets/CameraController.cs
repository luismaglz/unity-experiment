using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform player;

    float positionX = 0f;
    float positionY = 0f;
    

    void Update()
    {
        positionX = player.position.x;
        positionY = player.position.y;
        transform.position = new Vector3(positionX, positionY, transform.position.z);
    }

}
