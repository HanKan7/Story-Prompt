using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Player Variables
    [SerializeField]
    float playerSpeed = 5f;
    #endregion


    #region Player Components
    [HideInInspector]
    public Animator playerAnim;
    #endregion
    
    [HideInInspector]
    public Vector3 moveVector;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovementInput()
    {
        //Movement X Axis
        if (Input.GetKey(KeyCode.A)) moveVector.x = -1;
        else if (Input.GetKey(KeyCode.D)) moveVector.x = 1;
        else moveVector.x = 0;

        //Movement Y Axis
        if (Input.GetKey(KeyCode.W)) moveVector.y = 1;
        else if (Input.GetKey(KeyCode.S)) moveVector.y = -1;
        else moveVector.y = 0;

        playerAnim.SetFloat("Horizontal", moveVector.x);
        playerAnim.SetFloat("Vertical", moveVector.y);
        playerAnim.SetFloat("Speed", moveVector.sqrMagnitude);
    }

    void PlayerMovement()
    {
        PlayerMovementInput();
        transform.position += moveVector.normalized * playerSpeed * Time.deltaTime;
    }

    void PlayerShootInput()
    {

    }

}
