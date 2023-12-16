using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPGbearfall
{

    public enum RPGEnemyStates {GUARD, PATROL, CHASE,DEAD };
    public class RPGEnemyController : MonoBehaviour
    {
        public RPGCharacter rPGCharacter;

        public bool isGuard;

        private NavMeshAgent agent;

        private RPGEnemyStates rPGEnemyStates;
        public Transform targetPlayer;
        public RPGGameManager rPGGameManager;

        private float speed;

        public float sightRadius;

        private Quaternion guardRotation;


        [Header("Patrol State")]
        public float patrolRange;

        public Vector3 wayPoint;

        private Vector3 guardPos;

        private GameObject attakTarget;

        public float lookAtTime;
        private float remainLookAtTime;

        public bool isWalk;

        public bool isChase;

        public bool isFollow;

        public bool isDead;

        private void Awake()
        {
            //rPGEnemyState = GetComponent<RPGEnemyState>();
            agent = GetComponent<NavMeshAgent>();
            speed = agent.speed;
            guardPos = transform.position;
            guardRotation = transform.rotation;
            remainLookAtTime = lookAtTime;
        }

        // Start is called before the first frame update
        void Start()
        {
            targetPlayer = rPGGameManager.nowRPGCharacter.transform;
            if (isGuard)
            {
                rPGEnemyStates = RPGEnemyStates.GUARD;
            }
            else
            {
                rPGEnemyStates = RPGEnemyStates.PATROL;
                GetNewWayPoint();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (rPGCharacter.nowHP == 0)
            {
                isDead = true;
            }
            SwitchState();
        }

        void SwitchState()
        {
            if (isDead)
            {
                rPGEnemyStates = RPGEnemyStates.DEAD;
            }


            else if (FoundPlayer())
            {
                rPGEnemyStates = RPGEnemyStates.CHASE;
                print("§ä¨ìª±®a");
            }
            

            switch (rPGEnemyStates)
            {
                case RPGEnemyStates.GUARD:

                    isChase = false;
                    if (transform.position != guardPos)
                    {
                        isWalk = true;
                        agent.isStopped = false;
                        agent.destination = guardPos;

                        if (Vector3.SqrMagnitude(guardPos - transform.position) <= agent.stoppingDistance)
                        {
                            isWalk = false;
                            transform.rotation = guardRotation;
                        }
                    }
                    break;
                case RPGEnemyStates.PATROL:

                    isChase = false;
                    agent.speed = speed * 0.5f;

                    if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
                    {
                        isWalk = false;
                        if (remainLookAtTime > 0)
                        {
                            remainLookAtTime -= Time.deltaTime;
                        }
                        else
                        {
                            GetNewWayPoint();
                        }
                    }
                    else
                    {
                        isWalk = true;
                        agent.destination = wayPoint;
                    }
                    break;

                case RPGEnemyStates.CHASE:

                    isWalk = false;
                    isChase = true;

                    agent.speed = speed;

                    if (!FoundPlayer())
                    {
                        isFollow = false;
                        if (remainLookAtTime > 0)
                        {
                            agent.destination = transform.position;
                            remainLookAtTime -= Time.deltaTime;
                        }
                        else if (isGuard)
                        {
                            rPGEnemyStates = RPGEnemyStates.GUARD;
                        }
                        else
                        {
                            rPGEnemyStates = RPGEnemyStates.PATROL;
                        }
                    }
                    else
                    {
                        isFollow = true;
                        agent.destination = attakTarget.transform.position;
                    }

                    break;
                case RPGEnemyStates.DEAD:

                    agent.enabled = false;

                    Destroy(gameObject, 2f);
                    break;
                    
                default:
                    break;
            }
        }

        bool FoundPlayer()
        {
            var colliders = Physics.OverlapSphere(transform.position, sightRadius);

            foreach (var target in colliders)
            {
                if (target.CompareTag("Player"))
                {
                    attakTarget = target.gameObject;
                    return true;
                }
            }
            attakTarget = null;
            return false;
        }

        

        void GetNewWayPoint()
        {
            remainLookAtTime = lookAtTime;

            float randomX = Random.Range(-patrolRange, patrolRange);
            float randomZ = Random.Range(-patrolRange, patrolRange);

         
            Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);

            NavMeshHit hit;
            wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, sightRadius);
        }
    }
}
