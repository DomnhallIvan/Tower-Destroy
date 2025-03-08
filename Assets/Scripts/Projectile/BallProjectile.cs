using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : ProjectileData
{
    [Header("Referencias")]
    public Rigidbody rb;    

    private void Awake()
    {
        porteriaLayer = LayerMask.NameToLayer("enemy");
        playerLayer = LayerMask.NameToLayer("turret");
        bolaLayer = LayerMask.NameToLayer("endzone");
        //gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        GameManager.instance.onReset += ReturnToPool;
    }

    private void LateUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == porteriaLayer)
        {
            EnemyController enemyref= collision.gameObject.GetComponent<EnemyController>();            
            if (enemyref)
                enemyref.Hit(damage);

            ReturnToPool();
        }
        if (collision.gameObject.layer == playerLayer || collision.gameObject.layer == bolaLayer)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        ObjectPool pool = FindObjectOfType<ObjectPool>();
        if (pool != null)
        {
            pool.ReturnToPool(gameObject);
        }
    }

    //Setear desde TowerShoot cuanto daño hara las pelotas que lance
    public int ReturnDamage(int setdamage)
    {
        damage = setdamage;
        return damage;
    }
}
