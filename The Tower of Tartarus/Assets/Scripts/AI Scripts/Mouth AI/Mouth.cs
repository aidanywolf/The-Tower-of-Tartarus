using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    MouthProjectileThrower projectileThrower;
    [SerializeField] int launchDelay;
    [SerializeField] GameObject player;
    [SerializeField] public int health;
    public Rigidbody2D rb;
    [SerializeField] public float speed;
    [SerializeField] GameObject body;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float meleeRadius;
    [SerializeField] GameObject blood;
    [SerializeField] GameObject healthItem;
    public bool aggroed = false;
    Player playerScript;
    [SerializeField] ParticleSystem deathParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player"); 
        playerScript = player.GetComponent<Player>();
        projectileThrower = GetComponent<MouthProjectileThrower>();
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //enemy collides with boudler, enemy loses health
        if(other.gameObject.tag == "DamagingBoulder"){
            health-=1;
        }
        if(health <= 0){
            //make bloodsplat
            GameObject bloodSplat = Instantiate(blood,transform.position,Quaternion.identity);
            bloodSplat.transform.position = new Vector3(bloodSplat.transform.position.x, bloodSplat.transform.position.y, 2f);
            bloodSplat.transform.parent = this.transform.parent;

            //chance drop health item
            int healthChance = Random.Range(0, 12);
            if(healthChance == 0){
                GameObject item = Instantiate(healthItem,transform.position,Quaternion.identity);
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 1f);
                item.transform.parent = this.transform.parent;
            }

            deathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
            deathParticles.transform.parent = this.transform.parent;
            Destroy(this.gameObject);
        }
    }


    // move mouth based on given direction
    public void MoveMouth(Vector3 direction)
    {
        rb.velocity = (direction * speed);
        if(rb.velocity.x < 0){
            body.transform.localScale = new Vector3(-1,1,1);
        }else if(rb.velocity.x > 0){
            body.transform.localScale = new Vector3(1,1,1);
        }
    }

    public void MoveMouthToward(Vector3 target){
        Vector3 direction = target - transform.position;
        MoveMouth(direction.normalized);
    }

    public void Stop(){
        MoveMouth(Vector3.zero);
    }
}