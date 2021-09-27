using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelScript : MonoBehaviour
{
    [SerializeField] private Texture2D TextureInLevel;
    public void LoadLevel()
    {
        TransferDataScript.levelTexture = TextureInLevel;
        SceneManager.LoadScene(1);
    }
}
