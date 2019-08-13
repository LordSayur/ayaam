using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

        private bool isMoving = false;
        [SerializeField] private int ranNum = 2;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (!isMoving)
            {
                StartCoroutine(MoveAyam());
            }
        }

        private void OnTriggerStay(Collider other) {
            if (other.transform.tag == "Player")
            {
                if (target != null)
                {
                    agent.SetDestination(transform.position + (Vector3.Normalize(transform.position - target.position) * 5f));
                    character.Move(agent.desiredVelocity, false, false);
                }
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        IEnumerator MoveAyam(){
            isMoving = true;
            agent.SetDestination(transform.position + new Vector3(UnityEngine.Random.Range(-ranNum, ranNum), transform.position.y, UnityEngine.Random.Range(-ranNum, ranNum)));
            character.Move(agent.desiredVelocity, false, false);
            yield return new WaitForSeconds(3);
            isMoving = false;
        }
    }
}
