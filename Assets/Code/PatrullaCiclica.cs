using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullaCiclica : AIProfile
{
    private int currentWaypoint = 0;
    private float moveSpeed = 2f; // Velocidad de movimiento
    private float detectionInterval = 2f; // Intervalo de detección en segundos
    private float nextDetectionTime;
    private Transform player; // Referencia al transform del jugador

    protected override float viewDistance { get => 10f; }
    protected override float waitForTurn { get => 5f; }
    protected override List<Vector3> waypoints => new List<Vector3>
        {
        new Vector3(1f, 0f, 1f),   // Punto 1
        new Vector3(3f, 0f, 1f),   // Punto 2
        new Vector3(3f, 0f, 3f),   // Punto 3
        new Vector3(1f, 0f, 3f)    // Punto 4 (vuelve al primero)
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
    public override void ExecuteProfile()
    {
        MoveToWaypoint();
        DetectPlayer();
    }

    private void MoveToWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypoint];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
        }
    }

    public override void OnPlayerDetected()
    {
        base.OnPlayerDetected();
    }
}
