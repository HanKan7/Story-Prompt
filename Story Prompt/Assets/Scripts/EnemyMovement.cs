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
    public float timeBetweenAttack = 2;
    float attackTimeCount = 0;

    public int damage = 2;

    public GameObject targetBuilding;
    
   

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = pathNodes[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            approachBuilding();
        }
        else {
            attackBuilding();
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
