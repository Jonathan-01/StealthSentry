using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StealthSentry.Enemies
{
    using Player;

    public enum AIState
    {
        Idle,
        Patrol,
        Wait,
        Alert,
        Chase
    }

    public class EnemyController : MonoBehaviour
    {
        [Tooltip("Starting state for the AI")]
        [SerializeField] AIState currentState = AIState.Idle;

        [Header("Movement")]
        [Tooltip("Speed the AI should move at while patrolling")]
        [SerializeField] float walkSpeed = 2.0f;
        [Tooltip("Speed the AI should move at while in pursuit")]
        [SerializeField] float runSpeed = 4.0f;
        [Tooltip("Average time to wait between checkpoints when patrolling")]
        [SerializeField] float TimeToWait = 5.0f;
        [Tooltip("The desired amount of offset from the wait time")]
        [SerializeField] float TimeRandomness = 0.5f;
        [Tooltip("List of checkpoints to patrol")]
        [SerializeField] GameObject[] patrolPoints;

        [Header("Senses")]
        [Tooltip("Distance the AI should be able to hear the player from")]
        [SerializeField] float hearDistance = 8.0f;
        [Tooltip("Distance at which an AI can see a player")]
        [SerializeField] float sightDistance = 12.0f;
        [Range(0.0f, 360.0f)]
        [Tooltip("Angle of the AIs vision")]
        [SerializeField] float sightAngle = 120.0f;

        // Local
        private Animator anim;                      // Animator component. Not attached by default but if given one it has some preset values to update

        private float stateTimer = 0.0f;            // Timer for how loing the AI has been in a set state
        private int currentCheckpoint = 0;          // Current patrolPoint the AI is trying to reach
        private NavMeshAgent agent;                 // Agent used by the AI to navigate the navmesh
        private float nextWaitTime = 0.0f;          // The amount of time to wait at the next destination

        private PlayerController player;            // A refernece to the player used to check various data
        private Vector3 InvestigatePos;             // A point of interest the AI will investigate

        private Ray ray;                            // Ray used for seeing the player
        private RaycastHit hitResult;               // Result from the ray

        // Start is called before the first frame update
        void Start()
        {
            player = FindFirstObjectByType<PlayerController>();
            agent = GetComponent<NavMeshAgent>();
            anim = transform.GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            stateTimer += Time.deltaTime;

            switch (currentState)
            {
                case AIState.Idle:
                    // Actions
                    ChangeState(AIState.Patrol);

                    // Logic

                    break;

                case AIState.Patrol:
                    // Actions
                    MoveToNextCheckpoint();

                    // Logic
                    if (SeenPlayer())
                    {
                        ChangeState(AIState.Alert);
                    }
                    else if (HeardPlayer())
                    {
                        ChangeState(AIState.Alert);
                    }
                    else if (IsDestinationReached())
                    {
                        UpdateCheckpoint();
                        ChangeState(AIState.Wait);
                    }
                    break;

                case AIState.Wait:
                    // Logic
                    if (SeenPlayer())
                    {
                        ChangeState(AIState.Chase);
                    }
                    else if (HeardPlayer())
                    {
                        ChangeState(AIState.Alert);
                    }
                    else if (TimeOut())
                    {
                        ChangeState(AIState.Patrol);
                        nextWaitTime = 0.0f;
                    }
                    break;

                case AIState.Alert:
                    // Actions
                    MoveToInvestigate();

                    // Logic
                    if (IsDestinationReached())
                    {
                        ChangeState(AIState.Wait);
                    }
                    else if (SeenPlayer())
                    {
                        ChangeState(AIState.Chase);
                    }
                    else if (HeardPlayer())
                    {
                        ChangeState(AIState.Alert);
                    }
                    break;

                case AIState.Chase:
                    // Actions
                    MoveToInvestigate();

                    // Logic
                    if (IsDestinationReached())
                    {
                        ChangeState(AIState.Wait);
                    }
                    else if (SeenPlayer())
                    {
                        ChangeState(AIState.Chase);
                    }
                    else if (HeardPlayer())
                    {
                        ChangeState(AIState.Chase);
                    }
                    break;

                default:
                    break;
            }
        }

        void UpdateAnimator()
        {
            // Switch statement to handle agent speed and animator
            switch (currentState)
            {
                case AIState.Idle:
                    agent.speed = 0.0f;
                    agent.isStopped = true;
                    if (anim)
                        anim.SetBool("IsMoving", false);
                    break;
                case AIState.Patrol:
                    agent.speed = walkSpeed;
                    if (anim)
                        anim.SetBool("IsMoving", true);
                    break;
                case AIState.Wait:
                    agent.speed = 0.0f;
                    agent.isStopped = true;
                    if (anim)
                        anim.SetBool("IsMoving", false);
                    break;
                case AIState.Alert:
                    agent.speed = walkSpeed;
                    if (anim)
                        anim.SetBool("IsMoving", true);
                    break;
                case AIState.Chase:
                    agent.speed = runSpeed;
                    if (anim)
                        anim.SetBool("IsMoving", true);
                    break;
            }
        }

        void ChangeState(AIState newState)
        {
            // Set the new state and reset the timer
            currentState = newState;
            stateTimer = 0;

            UpdateAnimator();
        }

        #region Actions
        void MoveToNextCheckpoint()
        {
            // Move to the set checkpoint
            agent.destination = patrolPoints[currentCheckpoint].transform.position;
            agent.isStopped = false;
        }

        void MoveToInvestigate()
        {
            // Move to the point of interest
            agent.destination = InvestigatePos;
            agent.isStopped = false;
        }

        public void AlertEnemy(Vector3 pos)
        {
            if (currentState == AIState.Chase) return;

            // Change state and update the position to investigate
            ChangeState(AIState.Alert);
            InvestigatePos = pos;
        }

        #endregion
        #region Logic
        void UpdateCheckpoint()
        {
            // Increment the checkpoint and reset if the checkpoint at the end
            currentCheckpoint++;
            if (currentCheckpoint >= patrolPoints.Length)
                currentCheckpoint = 0;
        }

        bool IsDestinationReached()
        {
            return agent.remainingDistance < agent.stoppingDistance && !agent.pathPending;
        }

        bool TimeOut()
        {
            if (nextWaitTime == 0.0f)
            {
                nextWaitTime = Random.  Range(TimeToWait - TimeRandomness, TimeToWait + TimeRandomness);
            }
            return stateTimer > nextWaitTime;
        }

        bool HeardPlayer()
        {
            // Calculate the distance
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Is the player heard?
            bool result = !player.IsInStealth && player.IsMoving && distance < hearDistance;

            // If yes then update the point of interest
            if (result)
            {
                InvestigatePos = player.transform.position;
            }

            return result;
        }

        bool SeenPlayer()
        {
            bool result = false;

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // If player is within range
            if (distanceToPlayer < sightDistance)
            {
                // Find the direction to the player
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

                // If within the correct angle
                if (angleToPlayer < sightAngle / 2)
                {
                    Vector3 startPos;

                    // If the player is crouched then cast the ray from lower to allow for cover
                    if (player.IsInStealth)
                    {
                        startPos = transform.position + Vector3.up * 0.7f;
                        Debug.DrawLine(transform.position + Vector3.up * 0.7f, player.transform.position);
                    }
                    else
                    {
                        startPos = transform.position + Vector3.up * 1.4f;
                        Debug.DrawLine(transform.position + Vector3.up * 1.4f, player.transform.position);
                    }

                    ray = new Ray(startPos, directionToPlayer);
                    if (Physics.Raycast(ray, out hitResult))
                    {
                        // If the ray hits the player then update the position to investigate
                        if (hitResult.transform.gameObject.tag == "Player")
                        {
                            result = true;
                            InvestigatePos = player.transform.position;
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        private void OnDrawGizmosSelected()
        {
            // Gizmo for hearing area
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hearDistance);

            // Gizmo for vision cone (forward)
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward * sightDistance));

            // Gizmo for vision cone (right side)
            Quaternion Rotation1 = Quaternion.AngleAxis(sightAngle / 2, Vector3.up);
            Vector3 VisionConeRender1 = Rotation1 * transform.forward;
            Gizmos.DrawLine(transform.position, transform.position + (VisionConeRender1 * sightDistance));

            // Gizmo for vision cone (left side)
            Quaternion Rotation2 = Quaternion.AngleAxis(-sightAngle / 2, Vector3.up);
            Vector3 VisionConeRender2 = Rotation2 * transform.forward;
            Gizmos.DrawLine(transform.position, transform.position + (VisionConeRender2 * sightDistance));

            // Gizmo for line to start of patrol route
            if (patrolPoints.Length > 0)
            {
                Gizmos.color = Color.grey;
                if (patrolPoints[0] != null)
                {
                    Gizmos.DrawLine(transform.position, patrolPoints[0].gameObject.transform.position);
                }

                // Gizmo to draw the patrol path
                Gizmos.color = Color.magenta;
                for (int i = 0; i < patrolPoints.Length - 1; i++)
                {
                    if (patrolPoints[i] != null && patrolPoints[i+1] != null)
                        Gizmos.DrawLine(patrolPoints[i].gameObject.transform.position, patrolPoints[i+1].gameObject.transform.position);                
                }
                if (patrolPoints[0] != null && patrolPoints[patrolPoints.Length - 1] != null)
                    Gizmos.DrawLine(patrolPoints[patrolPoints.Length - 1].gameObject.transform.position, patrolPoints[0].gameObject.transform.position);
            }
        }
    }
}
