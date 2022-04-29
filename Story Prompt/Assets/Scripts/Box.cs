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
    GameObject circle;
    bool finished = false;

    public Color colorOfTheBox;
    public Color colorOfTheCircle;
    SceneManagement sceneManager;

    public AudioSource crateReachDestination; 
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagement>();
        moveDistance = GetComponent<BoxCollider2D>().bounds.size.x;
        finalPosition.x = transform.position.x + pushesHorizontaly * moveDistance;
        finalPosition.y = transform.position.y + pushesVerticaly * moveDistance;
        colorOfTheBox = GetComponent<SpriteRenderer>().color;
        circle =  Instantiate(destinyDetermination, finalPosition, Quaternion.identity);
        circle.transform.GetComponent<SpriteRenderer>().color = colorOfTheBox;
        //circle.transform.GetComponent<SpriteRenderer>().color.a = 1.0f;
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
            if ((Vector2)transform.position == finalPosition)
            {
                finished = true;
                circle.SetActive(false);
                sceneManager.thingsDone += 1;
                crateReachDestination.Play();
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

}
