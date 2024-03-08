using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpie : MonoBehaviour
{
    ProjectileThrower projectileThrower;
    [SerializeField] int launchDelay;
    [SerializeField] GameObject player;
    [SerializeField] public int health;
    public Rigidbody2D rb;
    [SerializeField] public float speed;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float meleeRadius;
    public bool aggroed = false;
    Player playerScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player"); 
        playerScript = player.GetComponent<Player>();
        projectileThrower = GetComponent<ProjectileThrower>();
        StartCoroutine(LaunchProjectileCoroutine());
    }
    //projectile launches one at a a time with delay
    IEnumerator LaunchProjectileCoroutine()
    {
        while (true)
        {
            if(aggroed){
                projectileThrower.Launch(player.transform.position);
                yield return new WaitForSeconds(launchDelay);
            }
            else{
                yield return new WaitForSeconds(1);
            }
        }
    }

    //health tracker
    void Update(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, meleeRadius, playerLayer);
        if(colliders.Length > 0 ){
            playerScript.LoseHealth();
        }
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //enemy collides with boudler, enemy loses health
        if(other.gameObject.tag == "DamagingBoulder"){
            health-=1;
        }
    }

    public void MoveHarpie(Vector3 direction)
    {
        Vector3 currentVelocity = new Vector3(0, 0, 0);
        rb.velocity = (currentVelocity) + (direction * speed);
    }

    public void MoveHarpieToward(Vector3 target){
        Vector3 direction = target - transform.position;
        MoveHarpie(direction.normalized);
    }

    public void Stop(){
        MoveHarpie(Vector3.zero);
    }
}