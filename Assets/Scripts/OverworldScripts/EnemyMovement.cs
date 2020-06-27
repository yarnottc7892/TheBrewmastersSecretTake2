using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    //Reference to the Rigidbody2D
    private Rigidbody2D rb;

    //constant float to store the length of the movement timer
    private const float TIMER = 3.0f;

    //float to keep track of the movement timer
    private float movementTimer;

    //float to keep track of the rest timer
    private float restTimer;

    //Normal Vector that define the direction of the velocity
    private Vector2 walkDirection;

    //Scaler to adjust the speed of this object
    [SerializeField] private float speed;

    //Int to keep track of how many time the enemy changes directions before it rests.
    private int restCounter = 0;

    //Enum to define state to define different behavior for the enemy
    public enum WalkState
    {
        PATROL,
        PURSUE,
        REST
    };

    //Variable that stores the current state of the enemy
    private WalkState enemyState;

    //Reference to the child object transform with the vision cone.
    private Transform visionObject;

    //Reference to the player
    private GameObject player;

    //Enemy_Base scriptable object
    public Enemy_Base enemyType;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyState = WalkState.PATROL;
        visionObject = transform.GetChild(0);
        changeDirection();
        movementTimer = TIMER;
        restTimer = TIMER;
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemyState)
        {
            case WalkState.PATROL: 
                if (movementTimer > 0)
                {
                    rb.velocity = walkDirection * speed;
                    movementTimer -= Time.deltaTime;
                }
                else
                {
                    movementTimer = TIMER;
                    changeDirection();
                }
                break;
            case WalkState.REST:
                if (restTimer > 0)
                {
                    restTimer -= Time.deltaTime;
                }
                else
                {
                    restTimer = TIMER;
                    restCounter = 0;
                    enemyState = WalkState.PATROL;
                }
                break;
            case WalkState.PURSUE:
                pursuePlayer();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //May not be necessary. Might be better to assign on Start.
            player = col.gameObject;

            enemyState = WalkState.PURSUE;
            
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Player"))
        {
            restCounter = 0;
            restTimer = TIMER;
            movementTimer = TIMER;

            int decide = Random.Range(0,3);

            enemyState = decide == 3 ? WalkState.REST : WalkState.PATROL;

            //May not be necessary. Might be better to assign on Start.
            player = null;

            changeDirection();
        }
    }

    private void changeDirection()
    {
        walkDirection = Random.insideUnitCircle.normalized;
        float angle = Mathf.Atan2(walkDirection.y, walkDirection.x);
        visionObject.localPosition = new Vector3(.1f * Mathf.Cos(angle), .1f * Mathf.Sin(angle), 0);
        visionObject.rotation = Quaternion.Euler(0,0, angle * Mathf.Rad2Deg);

        restCounter++;
        if (restCounter == 4)
        {
            rb.velocity = Vector2.zero;
            enemyState = WalkState.REST;
        }

    }

    private void pursuePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x);
        visionObject.localPosition = new Vector3(.1f * Mathf.Cos(angle), .1f * Mathf.Sin(angle), 0);
        visionObject.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

        rb.velocity = direction * speed * 4;
    }
}
