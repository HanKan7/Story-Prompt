using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public SpriteRenderer sprite;
    public float projectileSpeed = 5f;
    public float projectileTimer = 2f;
    public Vector3 moveVector;
    private void Start()
    {
        Invoke("DestroyProjectile", projectileTimer);
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

}
