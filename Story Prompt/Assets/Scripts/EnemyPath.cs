using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public Transform[] pathNodes;

    public float hp = 1;
    public float speed = 3;
    int targetNode = 1;

   

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pathNodes[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != pathNodes[targetNode].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, pathNodes[targetNode].position, speed * Time.deltaTime);
        }
        else {
            if (targetNode != pathNodes.Length - 1) {
                targetNode++;
            }
        }



    }
}
