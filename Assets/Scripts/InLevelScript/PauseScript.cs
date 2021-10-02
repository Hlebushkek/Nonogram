using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public void Leave()
    {
        SceneManager.LoadScene(0);
    }
}