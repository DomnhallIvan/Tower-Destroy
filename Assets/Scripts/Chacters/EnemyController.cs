 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : EnemyData
{   
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private List<Transform> wayPoint;
    [SerializeField] private int currentwayPointIndex=0;    

    void Start()
    { 
       
        agent = GetComponent<NavMeshAgent>();
       _levelManager=FindObjectOfType<LevelManager>();

        healthBar.maxValue = _maxHealth;
        healthBar.value = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wayPointSet) return;
        if(!agent.pathPending&&agent.remainingDistance<=agentStoppingDistance)
        {            
                if(currentwayPointIndex==wayPoint.Count)
                {
                _levelManager.EnemyDestroyed();
                GameManager.instance.OnScoreZoneReached(_damage);
                Destroy(this.gameObject);
                Destroy(healthBar.gameObject);
            }
            else
            {                    
                    agent.SetDestination(wayPoint[currentwayPointIndex].position);
                    currentwayPointIndex++;
            }                                            
        }
    }

    public void SetDestination(List<Transform> wayPoint)
    {
        this.wayPoint = wayPoint;
        wayPointSet = true;
    }

    public void Hit(int damage)
    {
        if (healthBar)
        {
            healthBar.value -= damage;
            if (healthBar.value <= 0)
            {
                Destroy(healthBar.gameObject);
                Destroy(this.gameObject);
                _levelManager.EnemyDestroyed();
            }
        }
    }

    public void KillEnemy()
    {
        Destroy(this.gameObject);
        Destroy(healthBar.gameObject);
    }


}
