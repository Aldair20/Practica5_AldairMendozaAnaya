using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Método llamado cuando el objeto entra en colisión con otro objeto
    public void OnCollisionEnter(Collision collision)
    {
        // Verifica si la etiqueta del objeto con el que colisionó es "city"
        if (collision.gameObject.CompareTag("city"))
        {
            // Destruye este objeto (el proyectil)
            Destroy(gameObject);
        }
    }

    // Método llamado cuando el objeto es creado por primera vez
    void Start()
    {
        // No necesitas ningún código aquí para este ejemplo
    }

    // Método llamado en cada fotograma de la ejecución del juego
    void Update()
    {
        // Destruye este objeto (el proyectil) después de 5 segundos
        Destroy(gameObject, 5.0f);
    }
}
