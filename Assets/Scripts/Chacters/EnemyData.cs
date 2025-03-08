using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyData : MonoBehaviour
{
    public Slider healthBar;

    public int _maxHealth = 100;
    public int _damage = 1;

    public NavMeshAgent agent;

    public float agentStoppingDistance = 1f;

    public bool wayPointSet = false;
}
