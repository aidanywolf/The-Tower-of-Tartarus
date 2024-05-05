using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackProjectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    private Transform playerTransform; 

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Debug.Log("black proj found player");
            playerTransform = playerObject.transform;
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
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
