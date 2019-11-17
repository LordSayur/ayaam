using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class AyamController : MonoBehaviour
{
    // config variables
    [SerializeField] private int randomDirectionRange = 2;
    [SerializeField] private int moveSteps = 5;
    [SerializeField] private float curvature = 1f;

    // reference variables
    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent { get; set; }
    private ThirdPersonCharacter character { get; set; }
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private BoxCollider boxCollider;

    // state variables
    private bool isAyamMoving = false;
    private bool isPlayerInRange = false;
    private Vector3 move;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        boxCollider = GetComponentInChildren<BoxCollider>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }


    private void Update()
    {

        if (isPlayerInRange)
        {
            if (target != null)
            {
                move = transform.position + (Vector3.Normalize(transform.position - target.position) * moveSteps);
                MoveAyam(move);
            }
        }
        else
        {
            if (!isAyamMoving)
            {
                StartCoroutine(MoveAyamToRandomLocation());
            }
            else
            {
                // move *= Mathf.Sin(1f + Time.deltaTime);
                // move += (transform.right * Mathf.Sin(curvature * Time.deltaTime));
                MoveAyam(move);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Grabber")
        {
            transform.Translate(Vector3.zero);
            agent.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player")
        {       
            isPlayerInRange = true;
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }

    private void MoveAyam(Vector3 destinationLocation)
    {
        Debug.DrawLine(transform.position, destinationLocation, Color.red);
        Debug.DrawLine(transform.position, agent.desiredVelocity, Color.green);
        agent.SetDestination(destinationLocation);
        character.Move(agent.desiredVelocity, false, false);
    }

    IEnumerator MoveAyamToRandomLocation()
    {
        isAyamMoving = true;
        Vector3 randomPosition = new Vector3(Random.Range(-randomDirectionRange, randomDirectionRange),
                                                            transform.position.y, 
                                                            Random.Range(-randomDirectionRange, randomDirectionRange));
        move = transform.position + (Vector3.Normalize(randomPosition - transform.position) * moveSteps);
        yield return new WaitForSeconds(Random.Range(3,6));
        isAyamMoving = false;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void DeactivateAyam(){
        rb.isKinematic = true;
        capsuleCollider.enabled = false;
        boxCollider.enabled = false;
        agent.enabled = false;
    }
}
