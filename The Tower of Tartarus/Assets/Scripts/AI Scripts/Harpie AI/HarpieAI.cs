using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding; //import the pathfinding library

public class HarpieAI : MonoBehaviour
{
    public Harpie myHarpie; 
    public Player targetPlayer;

    [Header("Config")]
    public LayerMask walls;
    public LayerMask obstacles;
    public float sightDistance = 5;

    [Header("Pathfinding")]
    Pathfinder<Vector2> pathfinder;
    [SerializeField] float gridSize = 1f;

    //States
    HarpieAIState currentState;
    public HarpieAIPatrolState patrolState{get; private set;}
    public HarpieAIBeginState beginState{get; private set;}
    public HarpieAIAggroState aggroState{get; private set;}


    public void ChangeState(HarpieAIState newState){

        currentState = newState;

        currentState.BeginStateBase();
    }


    void Start()
    {
        beginState = new HarpieAIBeginState(this);
        patrolState = new HarpieAIPatrolState(this);
        aggroState = new HarpieAIAggroState(this);
        currentState = beginState;
        targetPlayer = GameObject.Find("Player").GetComponent<Player>(); 
    
        pathfinder = new Pathfinder<Vector2>(GetDistance,GetNeighbourNodes,1000);
    }


    void FixedUpdate()
    {
        currentState.UpdateStateBase(); //work the current state

    }

    public Player GetTarget(){
        //are we close enough?
        if(Vector3.Distance(myHarpie.transform.position,targetPlayer.transform.position) > sightDistance){
            return null;
        }

        //is vision blocked by a wall?
        RaycastHit2D hit = Physics2D.Linecast(myHarpie.transform.position, targetPlayer.transform.position,walls);
        if(hit.collider != null){
            return null;
        }
        //put this in random?

        return targetPlayer;

    }

    //pathfinding
    public float GetDistance(Vector2 A, Vector2 B)
    {
        return (A - B).sqrMagnitude; //Uses square magnitude to lessen the CPU time.
    }

    Dictionary<Vector2,float> GetNeighbourNodes(Vector2 pos)
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
        for (int i=-1;i<2;i++)
        {
            for (int j=-1;j<2;j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j)*gridSize;
                if (!Physics2D.Linecast(pos,pos+dir, obstacles))
                {
                    neighbours.Add(GetClosestNode( pos + dir), dir.magnitude);
                }
            }

        }
        return neighbours;
    }

    //find the closest spot on the grid to begin our pathfinding adventure
    Vector2 GetClosestNode(Vector2 target){
        return new Vector2(Mathf.Round(target.x/gridSize)*gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    public void GetMoveCommand(Vector2 target, ref List<Vector2> path) //passing path with ref argument so original path is changed
    {
        path.Clear();
        Vector2 closestNode = GetClosestNode(myHarpie.transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
        {
            path.Add(target); //add the final position as our last stop
        }



    }

    //simple wrapper to pathfind to our target
    public void GetTargetMoveCommand(ref List<Vector2> path){
        GetMoveCommand(targetPlayer.transform.position, ref path);
    }
    public void GetRandomMoveCommand(ref List<Vector2> path){
        // get a random number for both x and y directions
        float randomX = Random.Range(-2f, 2f); 
        float randomY = Random.Range(-2f, 2f); 

        // random pos is curr position modified by random offset
        Vector3 randomOffset = new Vector3(randomX, randomY, 0);
        Vector3 randomPos = myHarpie.transform.position + randomOffset;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPos, 1f);
        bool hitWall = false;

        // Check if any collider is a wall or obstacle
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("LengthWall") || collider.CompareTag("WidthWall") || collider.CompareTag("Obstacle") || collider.CompareTag("EnemyWall") || collider.CompareTag("Chest"))
            {
                hitWall = true;
                break;
            }
        }

        // If overlapcircle doesn't hit a wall, get the move command
        if (!hitWall)
        {
            GetMoveCommand(randomPos, ref path);
        }
    }
}
