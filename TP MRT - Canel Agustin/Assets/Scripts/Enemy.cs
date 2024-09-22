using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f; // Velocidad del enemigo
    public int health = 100; // Vida del enemigo
    public Transform[] waypoints; // Array de puntos de referencia (waypoints)

    private int waypointIndex = 0; // �ndice del waypoint actual

    void Start()
    {
        // Inicialmente coloca al enemigo en la posici�n del primer waypoint
        transform.position = waypoints[waypointIndex].position;
    }

    void Update()
    {
        Move();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Move()
    {
        // Si el enemigo ha alcanzado el �ltimo waypoint, destr�yelo o haz algo especial
        if (waypointIndex >= waypoints.Length)
        {
            Destroy(gameObject); // O cualquier acci�n que desees hacer al final del camino
            return;
        }

        // Obtener la direcci�n hacia el siguiente waypoint
        Vector3 direction = waypoints[waypointIndex].position - transform.position;

        // Mover al enemigo en esa direcci�n
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Si el enemigo est� lo suficientemente cerca del waypoint, pasa al siguiente
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
        }
    }
}
