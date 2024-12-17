using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    //0 is low, 1 is high
    public int hitboxType;

    public float hitboxDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider otherPlayer)
    {
        //if it hits a player, and its not the parenting player
        if (otherPlayer.tag == "Player" && otherPlayer != this.transform.parent.transform.parent.GetComponent<Collider>())
        {
            otherPlayer.GetComponent<PlayerBehavior>().TakeHit(hitboxDamage, hitboxType);
        }

    }

}
