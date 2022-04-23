using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public SpriteRenderer sprite;
    public float projectileSpeed = 5f;
    public Vector3 moveVector;

    private void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        transform.position += moveVector * Time.deltaTime * projectileSpeed;
    }

}
