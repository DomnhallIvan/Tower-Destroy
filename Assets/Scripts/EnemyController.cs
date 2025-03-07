 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{   
    [SerializeField] private LevelManager _levelManager;

    public List<Transform> wayPoint;
    public int currentwayPointIndex=0;

    private float agentStoppingDistance = 0.7f;

    private bool wayPointSet = false;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    { 
       
        agent = GetComponent<NavMeshAgent>();
       _levelManager=FindObjectOfType<LevelManager>();
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
                Destroy(this.gameObject, 0.1f);
                    
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
}
