using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallData : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject attacker;
    public float attackDamage = 0.0f;
    public bool isAlive = true;
    public float secondsAlive;

    void Update()
    {
        //this handles how long the cannonball can travel before being destroyed
        if (isAlive)
        {
            StartCoroutine(CannonBallAlive());
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {

        Attack attackData = new Attack(attacker, attackDamage);


        collision.gameObject.SendMessage("TakeDamage", attackData, SendMessageOptions.DontRequireReceiver);
        attacker.gameObject.SendMessage("AddScore");
        //Destroy CannonBall
        Destroy(this.gameObject);
    }

    IEnumerator CannonBallAlive()//handles how long cannonball is alive
    {
        yield return new WaitForSeconds(secondsAlive);
        isAlive = false;
    }
}
