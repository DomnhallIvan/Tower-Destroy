 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{   
    [SerializeField] private LevelManager _levelManager;

    [SerializeField] private List<Transform> wayPoint;
    [SerializeField] private int currentwayPointIndex=0;

    private float agentStoppingDistance = 0.8f;

    private bool wayPointSet = false;

    [SerializeField] Slider _healthBarPrefab;
    Slider healthBar;

    [SerializeField] private int _maxHealth=100;
    [SerializeField] private int _damage = 1;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    { 
       
        agent = GetComponent<NavMeshAgent>();
       _levelManager=FindObjectOfType<LevelManager>();

        healthBar=Instantiate(_healthBarPrefab,this.transform.position,Quaternion.identity);
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar.maxValue = _maxHealth;
        healthBar.value = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wayPointSet) return;
        if (healthBar)
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 0.8f);
        }

        if(!agent.pathPending&&agent.remainingDistance<=agentStoppingDistance)
        {            
                if(currentwayPointIndex==wayPoint.Count)
                {
                _levelManager.EnemyDestroyed();
                GameManager.instance.OnScoreZoneReached(_damage);


               // Destroy(healthBar);
               // Destroy(this.gameObject, 0.1f);
                
                    
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
            }
        }
    }


}
