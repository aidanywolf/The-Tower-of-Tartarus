using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    ProjectileThrower projectileThrower;
    [SerializeField] int launchDelay;
    [SerializeField] GameObject player;
    [SerializeField] public int health;
    Rigidbody2D rb;
    [SerializeField] public float speed;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float meleeRadius;
    [SerializeField] GameObject body;
    [SerializeField] GameObject blood;
    [SerializeField] GameObject healthItem;
    public bool aggroed = false;
    Player playerScript;
    [SerializeField] ParticleSystem deathParticles;
    AudioManager audioManager;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player"); 
        playerScript = player.GetComponent<Player>();
        projectileThrower = GetComponent<ProjectileThrower>();
        StartCoroutine(LaunchProjectileCoroutine());
        GameObject audioManagerObj = GameObject.Find("AudioManager");
        audioManager = audioManagerObj.GetComponent<AudioManager>();
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

    //enemy collides with boudler, enemy loses health
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "DamagingBoulder"){
            health-=1;
        }
        if(health <= 0){
            //make blood splat
            GameObject bloodSplat = Instantiate(blood,transform.position,Quaternion.identity);
            bloodSplat.transform.position = new Vector3(bloodSplat.transform.position.x, bloodSplat.transform.position.y, 2f);
            bloodSplat.transform.parent = this.transform.parent;

            //chance drop health item
            int healthChance = Random.Range(0, 12);
            if(healthChance == 0){
                GameObject item = Instantiate(healthItem,transform.position,Quaternion.identity);
               // item.GetComponent<Rigidbody2D>().velocity = boulderVelocity / 2;
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, 1f);
                item.transform.parent = this.transform.parent;
            }

            deathParticles = Instantiate(deathParticles, transform.position, Quaternion.identity);
            deathParticles.transform.parent = this.transform.parent;
            audioManager.PlaySFX(audioManager.enemyDeath);
            Destroy(this.gameObject);
        }
    }

    //ghost faced dir changes based on move dir
    public void MoveGhost(Vector3 direction)
    {
        rb.velocity = (direction * speed);
        if(rb.velocity.x < 0){
            body.transform.localScale = new Vector3(-1,1,1);
        }else if(rb.velocity.x > 0){
            body.transform.localScale = new Vector3(1,1,1);
        }
    }

    //move ghost toward provided target
    public void MoveGhostToward(Vector3 target){
        Vector3 direction = target - transform.position;
        MoveGhost(direction.normalized);
    }

    public void Stop(){
        MoveGhost(Vector3.zero);
    }
}