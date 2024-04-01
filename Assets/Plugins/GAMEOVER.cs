using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieGameOver : MonoBehaviour
{
    public int muerte = 0; // Variable de muerte

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (other.CompareTag("bala"))
        {
            muerte = 1; // Cambiar la variable de muerte a 1
        }
    }
}
