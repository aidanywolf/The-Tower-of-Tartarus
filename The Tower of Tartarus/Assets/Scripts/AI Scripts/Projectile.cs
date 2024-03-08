using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {   
        //projectile collides with boulder, projectile destroyed
        if(other.gameObject.tag == "Boulder" || other.gameObject.tag == "DamagingBoulder"){
            Destroy(this.gameObject);
        }
        //projectile collides with player, call losehealth and projectile is destroyed
        else if(other.gameObject.tag == "Player"){
            Player player = other.gameObject.GetComponent<Player>();
            player.LoseHealth();
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "LengthWall" || other.gameObject.tag == "WidthWall"){
            Destroy(this.gameObject);
        }
    }
}

