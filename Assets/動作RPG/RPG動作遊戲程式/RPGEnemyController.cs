using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RPGbearfall
{

    public enum RPGEnemyStates {GUARD, PATROL, CHASE,DEAD };
    public class RPGEnemyController : MonoBehaviour
    {
        public int atk;


        public RPGEnemyCharacter rpgEnemyCharacter;

        public bool isGuard;

        private NavMeshAgent agent;
        [Header("敵人角色動畫控制器")]
        public Animator anim;

        public RPGEnemyStates rPGEnemyStates;
        public Transform targetPlayer;
        public RPGGameManager rPGGameManager;

        private float speed;

        public float sightRadius;

        private Quaternion guardRotation;


        [Header("Patrol State")]
        public float patrolRange;

        public Vector3 wayPoint;

        private Vector3 guardPos;
        [Header("搜索到玩家進行攻擊")]
        public GameObject attakTarget;

        [Header("攻擊距離")]
        public float attackRange;

        [Header("攻擊冷卻時間")]
        public float atkCD;

        [Header("玩家重攻擊冷卻")]
        public Slider slider;
        public int sliderValue;

        [Header("敵人碰撞器")]
        public Collider enemyCollider;
        public float lookAtTime;
        private float remainLookAtTime;

        public float lastAttackTime;

        public bool isWalk;

        public bool isChase;

        public bool isFollow;

        public bool isDead;

        public EnemyCounter enemyCounter;

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
            targetPlayer = rPGGameManager.nowRPGCharacter.transform;

            if (rpgEnemyCharacter.nowHP == -1)
            {
                isDead = true;
                enemyCollider.enabled = false;
                slider.value -= sliderValue;
                rpgEnemyCharacter.nowHP -= 1;
            }
            SwitchState();
            SwitchAnimation();
            RotateEnemy();
            lastAttackTime -= Time.deltaTime;
        }


        public void SwitchAnimation()
        {
            anim.SetBool("Walk", isWalk);
            anim.SetBool("Chase", isChase);
            anim.SetBool("Follow", isFollow);
            anim.SetBool("Death", isDead);
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
                //print("找到玩家");
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
                        agent.isStopped = false;
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

                    if (TargetInAttackRange())
                    {
                        isFollow = false;
                        agent.isStopped = true;

                        if (lastAttackTime < 0)
                        {
                            lastAttackTime = atkCD;


                            Attack();
                        }

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

        public void Attack()
        {
            anim.SetTrigger("Attack");
            
        }

        public void RotateEnemy()
        {

            if (attakTarget != null && attakTarget.transform.position.x > gameObject.transform.position.x)
            {
                gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (attakTarget != null && attakTarget.transform.position.x < gameObject.transform.position.x)
            {
                gameObject.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 180, 0);
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

        public bool TargetInAttackRange()
        {
            if (attakTarget != null)
            {
                return Vector3.Distance(attakTarget.transform.position, transform.position) <= attackRange;
            }
            else return false;
        }
        /*
        public bool TargetInSkillRange()
        {

        }
        */

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
