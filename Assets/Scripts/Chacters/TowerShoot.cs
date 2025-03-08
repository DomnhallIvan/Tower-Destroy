using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : TowerData
{   
    private List<GameObject> _enemiesInRange=new List<GameObject>();
    private GameObject currentTarget;
    private ObjectPool bulletPool;


    private void Awake()
    {
        bulletPool = FindObjectOfType<ObjectPool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
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
        //Filters out null enemies
        _enemiesInRange.RemoveAll(enemy => enemy == null); // Remove destroyed enemies

        //Checks if the current target is null or no longer valid
        if (currentTarget != null && _enemiesInRange.Contains(currentTarget))
        {
            return;
        }

        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in _enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        currentTarget = closestEnemy;

    }

    private void LateUpdate()
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
            
            timeLastSpeedChange += Time.deltaTime;
            if (timeLastSpeedChange >= _fireRate)
            {
                if (Vector3.Angle(directionToTarget, _gun.transform.forward) < _angleTurningAccuracy)
                {
                    Fire(_firePoint.position, _firePoint.forward);
                    timeLastSpeedChange = 0f;
                }
                else
                {
                    // Reset timer slightly so tower doesn't get "stuck" waiting for perfect alignment
                    timeLastSpeedChange = Mathf.Min(timeLastSpeedChange, _fireRate - 0.1f);
                }
            }

        }
    }

    private void Fire(Vector3 firePointPosition, Vector3 fireDirection)
    {
        GameObject fire = bulletPool.GetFromPool();
        fire.transform.position = firePointPosition;
        fire.transform.rotation = Quaternion.identity;

        Rigidbody bulletRB = fire.GetComponent<Rigidbody>();
        if (bulletRB != null)
        {
            //Fix to reset angularVelocity and set the ForceMode.Impulse
            bulletRB.velocity = Vector3.zero;
            bulletRB.angularVelocity = Vector3.zero;
            bulletRB.AddForce(fireDirection * _bulletForce, ForceMode.Impulse);
            fire.GetComponent<BallProjectile>().ReturnDamage(_damage);
        }

    }
}
