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
            GameObject bloodSplat = Instantiate(blood,transform.position,Quaternion.identity);
            bloodSplat.transform.position = new Vector3(bloodSplat.transform.position.x, bloodSplat.transform.position.y, 2f);
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

    public void MoveGhost(Vector3 direction)
    {
        Vector3 currentVelocity = new Vector3(0, 0, 0);
        rb.velocity = (currentVelocity) + (direction * speed);
        if(rb.velocity.x < 0){
            body.transform.localScale = new Vector3(-1,1,1);
        }else if(rb.velocity.x > 0){
            body.transform.localScale = new Vector3(1,1,1);
        }
    }

    public void MoveGhostToward(Vector3 target){
        Vector3 direction = target - transform.position;
        MoveGhost(direction.normalized);
    }

    public void Stop(){
        MoveGhost(Vector3.zero);
    }
}