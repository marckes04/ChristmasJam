using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithDetection : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float detectionDistance = 5f;
    [SerializeField] private float chaseSpeed = 5f;

    [Header("Snowball")]
    [SerializeField] private GameObject snowballPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootCooldown = 2f;
    [SerializeField] private float shootSpeed = 3f;

    private bool movingRight = true;
    private bool playerDetected = false;
    private float shootCooldownTimer = 0f;

    private Animator myAnim;



    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        myAnim.SetBool("Walk", false);
    }

    private void Update()
    {
        if (!playerDetected)
        {
            Patrol();
            myAnim.SetBool("Walk", true);
        }
        else
        {
            ChasePlayer();
            AttackPlayer();
        }

        DetectPlayer();
    }

    private void Patrol()
    {
        // Move between left and right points
        if (movingRight)
        {
            enemy.Translate(Vector3.right * patrolSpeed * Time.deltaTime);
        }
        else
        {
            enemy.Translate(Vector3.left * patrolSpeed * Time.deltaTime);
        }

        // Check if the enemy reached the patrol points
        if (movingRight && enemy.position.x >= rightPoint.position.x)
        {
            movingRight = false;
            FlipEnemy(-8);
        }
        else if (!movingRight && enemy.position.x <= leftPoint.position.x)
        {
            movingRight = true;
            FlipEnemy(8);

        }
    }


    private void FlipEnemy(int scale)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = scale;
        transform.localScale = tempScale;
    }


    private void ChasePlayer()
    {
        // Move towards the player at an increased speed
        float speed = playerDetected ? chaseSpeed : patrolSpeed;

        if (enemy.position.x < leftPoint.position.x || enemy.position.x > rightPoint.position.x)
        {
            movingRight = !movingRight;
        }

        int direction = movingRight ? 1 : -1;
        enemy.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }

    private void DetectPlayer()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(enemy.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        // Check if the player is within the detection distance
        playerDetected = distanceToPlayer <= detectionDistance;
    }

    private void AttackPlayer()
    {
        if (shootCooldownTimer <= 0)
        {
            // Play the attack animation and shoot snowball
            myAnim.Play("Attack");
            StartCoroutine("WaitToShoot");

            // Reset the cooldown timer
            shootCooldownTimer = shootCooldown;
        }
        else
        {
            // Reduce the cooldown timer
            shootCooldownTimer -= Time.deltaTime;
        }
    }

    private void ShootSnowball()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = (GameObject.FindGameObjectWithTag("Player").transform.position - enemy.position).normalized;

        // Instantiate a snowball and shoot it in the direction of the player
        GameObject snowball = Instantiate(snowballPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D snowballRb = snowball.GetComponent<Rigidbody2D>();
        snowballRb.velocity = directionToPlayer * patrolSpeed * shootSpeed; // Adjust speed as needed
    }

    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(0.5f);

        ShootSnowball();
    }

}
