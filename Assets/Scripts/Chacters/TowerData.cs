using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : MonoBehaviour
{
    [Header("Rotation")]
    public Transform _core;
    public Transform _gun;
    public float _turningSpeed = 10;
    public float _angleTurningAccuracy = 80;

    [Header("Shoot")]
    public float timeLastSpeedChange = 0f;
    public int _damage = 30;
    public float _fireRate = 1.2f;
    public float _bulletForce = 200;
    public Transform _firePoint;
}
