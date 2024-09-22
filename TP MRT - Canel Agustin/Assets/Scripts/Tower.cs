using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 5f; // Rango de ataque de la torre
    public float attackRate = 1f; // Tiempo entre ataques
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // Punto desde el cual se disparará la bala
    private float attackCountdown = 0f;
    public Transform target; // El enemigo al que atacará

    void Update()
    {
        // Buscar enemigos
        FindTarget();

        // Atacar si hay un objetivo en rango
        if (target != null)
        {
            if (attackCountdown <= 0f)
            {
                Shoot(); // Disparar hacia el enemigo
                attackCountdown = 1f / attackRate; // Resetear el contador para el siguiente disparo
            }

            attackCountdown -= Time.deltaTime;
        }
    }

    // Encontrar el objetivo más cercano dentro del rango de la torre
    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Buscar todos los enemigos
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform; // Establecer el enemigo más cercano como objetivo
        }
        else
        {
            target = null;
        }
    }

    // Método para disparar la bala
    void Shoot()
    {
        // Crear la bala en el punto de disparo de la torre
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        // Asegurarse de que la bala tenga un objetivo
        if (bullet != null)
        {
            bullet.Seek(target); // Enviar la bala hacia el enemigo
        }
    }

    // Mostrar el rango de la torre en la vista del editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
