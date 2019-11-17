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
#if MOBILE_INPUT
        score.SetText("Ayam: " + scoreCount.ToString());
#else
        score.SetText(player.name + ": " + scoreCount.ToString());
#endif
        chickens = GameObject.FindGameObjectsWithTag("Chicken").Length;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Chicken") 
        {
            scoreCount++;
#if MOBILE_INPUT
            score.SetText("Ayam: " + scoreCount.ToString());
#else
        score.SetText(player.name + ": " + scoreCount.ToString());
#endif

            AyamController ayam = other.GetComponentInParent<AyamController>();
            ayam.DeactivateAyam();
            ayam.enabled = false;
            
            other.transform.parent.SetParent(gameObject.transform);
            other.transform.parent.transform.localPosition = Vector3.zero;
#if MOBILE_INPUT
            if (scoreCount > chickens - 1)
            {
                winText.enabled = true;
                winText.SetText("Abis Ayam!");
                winText.color = Color.red;

                player.GetComponent<AwangBro>().Dance();
            }
#else
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
#endif
        }
    }
}
