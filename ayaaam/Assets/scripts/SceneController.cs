using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int MapIndex;

    private void Start()
    {
        MapIndex = GetComponent<MapController>().currentMapIndex + 1;
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadSelectedMap()
    {
        SceneManager.LoadScene(GetComponent<MapController>().currentMapIndex + 1);
    }
}
