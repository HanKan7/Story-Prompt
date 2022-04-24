using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    float moveDistance;
    bool moving = false;
    Vector2 destinyPositoin;
    public float speed = 1;

    Vector2 finalPosition;
    public int pushesHorizontaly;
    public int pushesVerticaly;
    public GameObject destinyDetermination;
    bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        moveDistance = GetComponent<SpriteRenderer>().bounds.size.x;
        finalPosition.x = transform.position.x + pushesHorizontaly * moveDistance;
        finalPosition.y = transform.position.y + pushesVerticaly * moveDistance;
        Instantiate(destinyDetermination, finalPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        if (moving && ! finished) {
            boxMovement();
        }
    }

    public void moveBox(Vector2 direction) {
        if (!moving && ! finished) {
            moving = true;
            destinyPositoin = (Vector2)transform.position - moveDistance * direction;
        }
    }

    public void boxMovement() {
        if ((Vector2)transform.position != destinyPositoin)
        {
            transform.position = Vector2.MoveTowards(transform.position, destinyPositoin, speed * Time.deltaTime);
        }
        else { 
            moving = false;
            if ((Vector2)transform.position == finalPosition) {
                finished = true;
            }
        }

    }

}
