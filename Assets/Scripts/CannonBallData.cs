using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallData : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject attacker;
    public float attackDamage = 0.0f;

    private void OnCollisionEnter(Collision collision)
    {

        Attack attackData = new Attack(attacker, attackDamage);

        collision.gameObject.SendMessage("TakeDamage", attackDamage, SendMessageOptions.DontRequireReceiver);
        //Destroy CannonBall
        Destroy(this.gameObject);
    }
}
