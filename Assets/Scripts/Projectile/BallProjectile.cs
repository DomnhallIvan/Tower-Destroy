using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour
{
    [Header("Referencias")]
    public Rigidbody rb;
    //private GameManager gameManager;

    [Header("Datos")]
    private int porteriaLayer;
    private int playerLayer;
    private int bolaLayer;
    private Vector3 lastVelocity;
    [SerializeField] private int damage = 10;
    [SerializeField] private int NumOfBounces = 20;
    private int curBounces = 0;

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

            curBounces++;
            if (curBounces <= NumOfBounces) return;
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        ObjectPool pool = FindObjectOfType<ObjectPool>();
        if (pool != null)
        {
            curBounces = 0;
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
