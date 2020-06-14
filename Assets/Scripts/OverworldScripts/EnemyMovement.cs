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
                break;
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
}
