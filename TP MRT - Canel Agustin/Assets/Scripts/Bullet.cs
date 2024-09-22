using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    public int damage = 50; // Da�o que inflige la bala
    public Transform target; // El objetivo (enemigo) al que se dirigir� la bala

    // M�todo para asignar el objetivo a la bala
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

        // Calcular la direcci�n hacia el objetivo
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Si la bala est� lo suficientemente cerca del enemigo, se considera que impact�
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget(); // Llamar a la funci�n de impacto
            return;
        }

        // Mover la bala hacia el enemigo
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    // Cuando la bala alcanza el objetivo
    void HitTarget()
    {
        // Aplicar da�o al enemigo
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // El enemigo recibe da�o
        }

        Destroy(gameObject); // Destruir la bala despu�s del impacto
    }
}
