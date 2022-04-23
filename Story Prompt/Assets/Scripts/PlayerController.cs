using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Player Variables
    [SerializeField]
    float playerSpeed = 5f;
    #endregion

    #region Shooting Variables
    [SerializeField]
    float shootingDelay = 0.4f;
    float shootingTimer = 0;
    public Vector3 offsetVector = new Vector3(0, 0.1f, 0);
    #endregion


    #region Player Components
    [HideInInspector]
    public Animator playerAnim;
    #endregion

    #region Other Class Components
    [SerializeField]
    Projectile projectile;
    public Vector3 projectileMoveVector;
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

        projectileMoveVector= Vector3.zero;
        playerAnim.SetFloat("Shoot", projectileMoveVector.sqrMagnitude);
        if (moveVector == Vector3.zero) PlayerShootInput();
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
        projectileMoveVector.x = Input.GetAxisRaw("Horizontal");
        projectileMoveVector.y = Input.GetAxisRaw("Vertical");

        playerAnim.SetFloat("ShootX", projectileMoveVector.x);
        playerAnim.SetFloat("ShootY", projectileMoveVector.y);
        playerAnim.SetFloat("Shoot", projectileMoveVector.sqrMagnitude);

        if (projectileMoveVector.sqrMagnitude > 0)
        {
            if (Time.time - shootingTimer > shootingDelay)
            {
                shootingTimer = Time.time;
                projectile.moveVector = projectileMoveVector.normalized;

                if (projectileMoveVector.x == 0 && projectileMoveVector.y == 1) Instantiate(projectile.gameObject, transform.position - offsetVector, Quaternion.Euler(0, 0, 0));
                else if (projectileMoveVector.x == 1 && projectileMoveVector.y == 0) Instantiate(projectile.gameObject, transform.position - offsetVector, Quaternion.Euler(0, 0, -90));
                else if (projectileMoveVector.x == 1 && projectileMoveVector.y == 1) Instantiate(projectile.gameObject, transform.position - offsetVector, Quaternion.Euler(0, 0, -45));
                else if (projectileMoveVector.x == 0 && projectileMoveVector.y == -1) Instantiate(projectile.gameObject, transform.position - offsetVector, Quaternion.Euler(0, 0, 180));
                else if (projectileMoveVector.x == -1 && projectileMoveVector.y == 0) Instantiate(projectile.gameObject, transform.position - offsetVector, Quaternion.Euler(0, 0, 90));
                else if (projectileMoveVector.x == -1 && projectileMoveVector.y == -1) Instantiate(projectile.gameObject, transform.position - offsetVector, Quaternion.Euler(0, 0, 135));
                else if (projectileMoveVector.x == -1 && projectileMoveVector.y == 1) Instantiate(projectile.gameObject, transform.position - offsetVector, Quaternion.Euler(0, 0, 45));
                else if (projectileMoveVector.x == 1 && projectileMoveVector.y == -1) Instantiate(projectile.gameObject, transform.position - offsetVector, Quaternion.Euler(0, 0, -135));
            }
        }
        
    }

}
