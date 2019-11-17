using UnityEngine;
using TMPro;

public class GrabberController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI winText;
    private int scoreCount = 0;
    [SerializeField] private Transform player;
    private int chickens = 0;

    private void Start()
    {
        winText.enabled = false;
        score.SetText(player.name + ": " + scoreCount.ToString());
        chickens = GameObject.FindGameObjectsWithTag("Chicken").Length;
        Debug.Log(chickens);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Chicken") 
        {
            scoreCount++;
            score.SetText(player.name + ": " + scoreCount.ToString());

            AyamController ayam = other.GetComponentInParent<AyamController>();
            ayam.DeactivateAyam();
            ayam.enabled = false;
            
            other.transform.parent.SetParent(gameObject.transform);
            other.transform.parent.transform.localPosition = Vector3.zero;

            if (scoreCount > 2)
            {
                winText.enabled = true;
                winText.SetText(player.name + " Manag!!!");
                if (player.name == "Si Biru")
                {
                    winText.color = Color.blue;
                }
                else
                {
                    winText.color = Color.red;
                }

                player.GetComponent<AwangBro>().Dance();
            }
        }
    }
}
