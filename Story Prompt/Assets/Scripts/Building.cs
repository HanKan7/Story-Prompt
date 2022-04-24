using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public GameObject buildingHP;
    Image HPGage;
    public float Maxhp = 20;
    float hp;
    // Start is called before the first frame update
    void Start()
    {
        HPGage = buildingHP.GetComponent<Image>();
        hp = Maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        HPGage.fillAmount = hp / Maxhp;
    }

    public void takeDamage(int damage) {
        hp -= damage;
        if (hp <= 0) {
            Destroy(gameObject);
        }
    }
}
