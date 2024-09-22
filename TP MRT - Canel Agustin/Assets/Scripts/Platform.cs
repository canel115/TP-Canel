using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject towerPrefab;  // El prefab de la torre a instanciar
    private GameObject currentTower;  // Referencia a la torre que se instanciará

    // Detectar cuando el jugador entra en el área de la plataforma
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Verificar si el objeto que colisionó es el jugador
        {
            // Instanciar la torre en la posición de la plataforma (centrada)
            if (currentTower == null)  // Si no hay una torre ya creada
            {
                // Usar la posición del collider para centrar la torre
                Vector3 platformCenter = GetComponent<Collider>().bounds.center;
                currentTower = Instantiate(towerPrefab, platformCenter, Quaternion.identity);
            }
        }
    }

    // Detectar cuando el jugador sale del área de la plataforma
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // Verificar si el objeto que colisionó es el jugador
        {
            // Destruir la torre cuando el jugador deja de estar en contacto con la plataforma
            if (currentTower != null)
            {
                Destroy(currentTower);
            }
        }
    }
}
