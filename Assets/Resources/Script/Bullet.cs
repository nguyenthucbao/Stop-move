using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Character seft;
    void Start()
    {
        Destroy(gameObject, 1f + 0.05f * seft.level);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bot") && other.GetComponent<Character>() != seft)
        {
            seft.GainLevel();
            other.GetComponent<Character>().OnDeath();
            Destroy(gameObject);
        }
    }
}
