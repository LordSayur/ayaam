using UnityEngine;

public class GrabberController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Chicken") 
        {
            AyamController ayam = other.GetComponentInParent<AyamController>();
            ayam.DeactivateAyam();
            ayam.enabled = false;
            
            other.transform.parent.SetParent(gameObject.transform);
            other.transform.parent.transform.localPosition = Vector3.zero;
        }
    }
}
