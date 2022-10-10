using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;

    public int GetDamage() => damage;

    public void Hit()
    {
        Debug.Log("Destroy enemy game object : " + gameObject.name);
        Destroy(gameObject);
    }
}
