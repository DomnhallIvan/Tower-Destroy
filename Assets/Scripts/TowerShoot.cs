using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerShoot : MonoBehaviour
{
    [SerializeField] Transform _core;
    [SerializeField] Transform _gun;

    private List<GameObject> _enemiesInRange=new List<GameObject>();

    private GameObject currentTarget;

    [SerializeField] private float _turningSpeed=10;
    [SerializeField] private float _angleTurningAccuracy = 80;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Hello");
            _enemiesInRange.Add(other.gameObject);
            UpdateTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            _enemiesInRange.Remove(other.gameObject);
            currentTarget = null;
            UpdateTarget();
        }
    }

    private void UpdateTarget()
    {
        if (currentTarget != null) {
            return;
        }GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in _enemiesInRange)
        {
            if (enemy == null) { return; }
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy<closestDistance)
                {
                    closestDistance= distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null)
        {
            currentTarget = closestEnemy;
        }
        else
        {
            currentTarget = null;
        }

    }

    private void Update()
    {
        if (currentTarget != null)
        {
            Vector3 aimAt = new Vector3(currentTarget.transform.position.x, _core.transform.position.y, currentTarget.transform.position.z);
            float distToTarget = Vector3.Distance(aimAt, _gun.transform.position);

            Vector3 relativeTargetPosition = _gun.transform.position + (_gun.transform.forward * distToTarget);

            relativeTargetPosition = new Vector3(relativeTargetPosition.x, currentTarget.transform.position.y, relativeTargetPosition.z);

            _gun.transform.rotation = Quaternion.Slerp(_gun.transform.rotation, Quaternion.LookRotation(relativeTargetPosition - _gun.transform.position), Time.deltaTime * _turningSpeed);
            _core.transform.rotation = Quaternion.Slerp(_core.transform.rotation, Quaternion.LookRotation(aimAt - _core.transform.position), Time.deltaTime * _turningSpeed);

            Vector3 directionToTarget=currentTarget.transform.position-_gun.transform.position;

            if (Vector3.Angle(directionToTarget, _gun.transform.forward) < _angleTurningAccuracy){
                Fire();
            }
        }
    }

    private void Fire()
    {
        Debug.Log("FiringAtEnemies");

    }
}
