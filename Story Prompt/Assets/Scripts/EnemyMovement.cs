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
   

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = pathNodes[0].position;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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

    void approachBuilding() {
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

    void approachGate() {
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
}
