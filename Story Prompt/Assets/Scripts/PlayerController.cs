using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 5f;

    Vector3 moveVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerInput()
    {
        //Movement X Axis
        if (Input.GetKey(KeyCode.A)) moveVector.x = -1;
        else if (Input.GetKey(KeyCode.D)) moveVector.x = 1;
        else moveVector.x = 0;

        //Movement Y Axis
        if (Input.GetKey(KeyCode.W)) moveVector.y = 1;
        else if (Input.GetKey(KeyCode.S)) moveVector.y = -1;
        else moveVector.y = 0;
    }

    void PlayerMovement()
    {
        PlayerInput();
        transform.position += moveVector.normalized * playerSpeed * Time.deltaTime;
    }

}
