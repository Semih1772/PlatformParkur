using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;
    public GameObject winPanel; // Tebrik paneli

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Engellere çarpma veya haritadan düşme kontrolü
        if (other.CompareTag("Obstacle"))
        {
            Respawn();
        }
        // Bitiş noktasına ulaşma
        else if (other.CompareTag("Finish"))
        {
            CompleteLevel();
        }
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint.position;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Karakterin hızını sıfırla
        }
    }

    void CompleteLevel()
    {
        // Tebrikler panelini aç
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        
        // Karakterin hareketini durdur
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // "Yeniden Başlat" butonuna bağlanacak fonksiyon
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // "Sonraki Bölüm" butonuna bağlanacak fonksiyon
    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0); // İlk bölüme döner
        }
    }
}