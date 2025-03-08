using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    [Header("Datos")]
    public int porteriaLayer;
    public int playerLayer;
    public int bolaLayer;
    public Vector3 lastVelocity;
    public int damage = 10;
}
