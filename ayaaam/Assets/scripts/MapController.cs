using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private GameObject[] Maps;
    [HideInInspector] public int currentMapIndex = 0;
    private int previousMapIndex = 0;

    private void Start()
    {
        Maps[currentMapIndex].SetActive(true);
    }

    public void ChangeMap()
    {

        previousMapIndex = currentMapIndex;
        if(Maps.Length == currentMapIndex + 1)
        {
            currentMapIndex = 0;
        }
        else
        {
            currentMapIndex += 1;
        }

        Maps[previousMapIndex].SetActive(false);
        Maps[currentMapIndex].SetActive(true);
    }
}
