using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public Transform[] pathNodes;
    public GameObject path;

    public float hp = 1;
    public float speed = 3;
    int targetNode = 1;

    bool attacking = false;
    public bool returnToGate = false;
    public float timeBetweenAttack = 2;
    float attackTimeCount = 0;

    public int damage = 2;

    public GameObject targetBuilding;
    GameManager gameManager;

    //Anim Variables
    Animator anim;
   

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = pathNodes[0].position;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.dialoguing == false) { 
            if (!attacking)
            {
                approachBuilding();
            }
            else {
                if (!returnToGate)
                {
                    attackBuilding();
                }
                else {
                    approachGate();
                }
                
            }
        }
    }

    void approachBuilding() 
    {
        HandleAnimation();

        if (transform.position != path.transform.GetChild(targetNode).transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, path.transform.GetChild(targetNode).transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (targetNode != path.transform.childCount - 1)
            {
                targetNode++;
            }
            else
            {
                attacking = true;
            }
        }
    }

    void approachGate() 
    {
        HandleAnimation();
        if (transform.position != path.transform.GetChild(targetNode).transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, path.transform.GetChild(targetNode).transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (targetNode != 0)
            {
                targetNode--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void attackBuilding() {
        SetAnimBoolsToFalse();
        anim.SetBool("attack", true);
        if (attackTimeCount < timeBetweenAttack)
        {
            attackTimeCount += Time.deltaTime;
        }
        else {
            if (targetBuilding != null) { 
                targetBuilding.GetComponent<Building>().takeDamage(damage);
                attackTimeCount = 0;
            }

        }
        
    }

    void HandleAnimation()
    {
        if (Mathf.Abs(transform.position.y - path.transform.GetChild(targetNode).transform.position.y) > Mathf.Abs(transform.position.x - path.transform.GetChild(targetNode).transform.position.x))
        {
            //UpDown
            if (transform.position.y > path.transform.GetChild(targetNode).transform.position.y)
            {
                //Down Animation
                SetAnimBoolsToFalse();
                anim.SetBool("moveDown", true);
            }
            else
            {
                SetAnimBoolsToFalse();
                anim.SetBool("moveUp", true);
            }
        }
        else
        {
            if (transform.position.x > path.transform.GetChild(targetNode).transform.position.x)
            {
                //Down Animation
                SetAnimBoolsToFalse();
                anim.SetBool("moveLeft", true);
            }
            else
            {
                SetAnimBoolsToFalse();
                anim.SetBool("moveRight", true);
            }
        }
    }

    void SetAnimBoolsToFalse()
    {
        anim.SetBool("moveRight", false);
        anim.SetBool("moveLeft", false);
        anim.SetBool("moveUp", false);
        anim.SetBool("moveDown", false);
        anim.SetBool("attack", false);
    }
}
