using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int healthPoints = 20;
    public int maxHealthPoints = 20;
    public bool isDead;
    public Sprite happyFace;
    public Sprite sadFace;

    private void Start()
    {
        healthPoints = maxHealthPoints;
    }

}
