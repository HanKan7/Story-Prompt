using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public GameObject buildingHP;
    public ParticleSystem smoke;
    bool isSmokeInstantiated = false;
    public Vector3 offsetVector;
    Image HPGage;
    public float Maxhp = 20;
    float hp;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        HPGage = buildingHP.GetComponent<Image>();
        hp = Maxhp;
        offsetVector = transform.lossyScale;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HPGage.fillAmount = hp / Maxhp;
    }

    public void takeDamage(int damage) {
        hp -= damage;
        if(hp <= Maxhp/1.2f)
        {
            if (!isSmokeInstantiated)
            {
                isSmokeInstantiated = true;
                GameObject smokeGameObject = Instantiate(smoke.gameObject, transform.position, smoke.transform.rotation);
            }
        }
        if (hp <= 0) {
            Destroy(gameObject);
            gameManager.buildingDestroyed += 1;
        }
    }


}
