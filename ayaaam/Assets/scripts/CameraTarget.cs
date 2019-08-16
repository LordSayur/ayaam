using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    // state variables
    private Vector3 cameraTargert = Vector3.zero;
    // reference variables
    [SerializeField] private Transform[] players;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraTargert = Vector3.zero;
        for (int i = 0; i < players.Length; i++)
        {
            cameraTargert += players[i].position;
        }
        cameraTargert /= players.Length;

        transform.position = cameraTargert;
    }
}
