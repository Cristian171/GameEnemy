using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullaPingPong : AIProfile
{
    private int currentWaypoint = 0;
    private int direction = 1; // 1 para avanzar, -1 para retroceder
    private float moveSpeed = 2f; // Velocidad de movimiento
    private float detectionInterval = 2f; // Intervalo de detección en segundos
    private float nextDetectionTime;
    private Transform player; // Referencia al transform del jugador

    protected override float viewDistance { get => 10f; }
    protected override float waitForTurn { get => 5f; }
    protected override List<Vector3> waypoints => new List<Vector3>
    {
       new Vector3(35f, 0f, -2f),   // Punto 1
        new Vector3(30f, 0f, -3f),   // Punto 2
        new Vector3(20f, 0f, -4f),   // Punto 3
        new Vector3(10f, 0f, -5f)    // Punto 4 
    };
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Asume que el jugador tiene el tag "Player".
        nextDetectionTime = Time.time + detectionInterval;
    }
    private void Update()
    {
        ExecuteProfile();
    }

    public override void DetectPlayer()
    {
        if (Time.time >= nextDetectionTime)
        {
            if (IsPlayerInSight())
            {
                OnPlayerDetected();
            }
            nextDetectionTime = Time.time + detectionInterval;
        }
    }

    public override void ExecuteProfile()
    {
        MoveToWaypoint();
        DetectPlayer();
    }
    private bool IsPlayerInSight()
    {
        if (player == null)
            return false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= viewDistance)
        {
            return true;
        }

        return false;
    }

    private void MoveToWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            currentWaypoint += direction;

            if (currentWaypoint >= waypoints.Count || currentWaypoint < 0)
            {
                direction *= -1; // Cambiar la dirección al llegar al final o al principio
                currentWaypoint += direction;
            }
        }
    }
    public override void OnPlayerDetected()
    {
        base.OnPlayerDetected();
    }
}