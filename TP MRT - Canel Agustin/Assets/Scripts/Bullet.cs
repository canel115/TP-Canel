using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    public int damage = 50; // Daño que inflige la bala
    public Transform target; // El objetivo (enemigo) al que se dirigirá la bala

    // Método para asignar el objetivo a la bala
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        // Si no hay objetivo, destruir la bala
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Calcular la dirección hacia el objetivo
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Si la bala está lo suficientemente cerca del enemigo, se considera que impactó
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget(); // Llamar a la función de impacto
            return;
        }

        // Mover la bala hacia el enemigo
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    // Cuando la bala alcanza el objetivo
    void HitTarget()
    {
        // Aplicar daño al enemigo
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // El enemigo recibe daño
        }

        Destroy(gameObject); // Destruir la bala después del impacto
    }
}
