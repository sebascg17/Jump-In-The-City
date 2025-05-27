using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public AudioSource backgroundMusic;
    void Start ()
    {
        // Deseleccionar cualquier UI seleccionada al inicio
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public async void ShowGameOver ()
    {
        await Task.Delay(2000); // Milliseconds
        gameOverPanel.SetActive(true);
        // Detiene la música de fondo
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
        Time.timeScale = 0f; // Pausar el juego
    }

    public void RestartGame ()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame ()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
