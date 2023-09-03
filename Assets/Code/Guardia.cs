using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardia : AIProfile
{
    protected override float viewDistance { get => 10f; }
    protected override float waitForTurn { get => 5f; }

    protected override List<Vector3> waypoints => throw new System.NotImplementedException();
    private float nextRotationTime;
    private Transform player; // Referencia al transform del jugador
    private float rotationInterval = 5f; // Intervalo de tiempo para rotar en segundos


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Asume que el jugador tiene el tag "Player".
    }
    private void Update()
    {
        ExecuteProfile();
        DetectPlayer();
    }
    public override void DetectPlayer()
    {
        if (player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= viewDistance)
        {
            OnPlayerDetected();
        }
    }

    public override void ExecuteProfile()
    {
        if (Time.time >= nextRotationTime)
        {
            RotateGuard();
            nextRotationTime = Time.time + rotationInterval;
        }
    }

    private void RotateGuard()
    {
        float randomAngle = UnityEngine.Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, randomAngle, 0f);
    }

    public override void OnPlayerDetected()
    {
        base.OnPlayerDetected();
    }
}
