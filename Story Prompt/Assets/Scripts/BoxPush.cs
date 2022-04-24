using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPush : MonoBehaviour
{
    public KeyCode pushKey;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (Input.GetKey(pushKey)) {
                Debug.Log("HAHAHAHAH");
                transform.parent.gameObject.GetComponent<Box>().moveBox(direction);
                
            }
        }
    }

}
