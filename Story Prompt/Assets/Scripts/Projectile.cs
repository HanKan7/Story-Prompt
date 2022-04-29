using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public SpriteRenderer sprite;
    public float projectileSpeed = 5f;
    public float projectileTimer = 2f;
    public Vector3 moveVector;

    public ParticleSystem destroyEffect;
    GameManager gameManager;

    private void Start()
    {
        Invoke("DestroyProjectile", projectileTimer);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        GetComponent<Rigidbody2D>().velocity = moveVector * Time.deltaTime * projectileSpeed * 100;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(destroyEffect, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            gameManager.enemyDestroyed += 1;
        }
    }

}
