using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Meta.Scripts;

namespace Meta.DroneInvader.Scripts.FSM
{
    public class SimpleStateMachine : MonoBehaviour
    {
        public enum DroneState
        {
            Idle, Patrol, Chase, Combat
        }

        public DroneState currentState = DroneState.Idle;
        public Transform firePos;
        public Transform player;
        public List<Transform> patrolPoints;
        public Transform currentTarget;

        [Header("Properties")]
        public Bullet bulletPrefab;
        public float viewAngle = 60f;
        public float detectDistance = 15f;
        public float combatDistance = 8f;
        
        public float fireRate = 1f;
        public float bulletSpeed = 10f;
        public int bulletDamage = 10;
        public int bulletCount = 1;
        public float bulletSpread = 5f;

        private float _nextPatrol;
        private float _nextFire;
        private NavMeshAgent _agent;
        private Entity _entity;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _entity = GetComponent<Entity>();
        }

        void Update()
        {
            switch (currentState)
            {
                case DroneState.Idle:
                    Idle();
                    break;
                case DroneState.Patrol:
                    Patrol();
                    break;
                case DroneState.Chase:
                    Chase();
                    break;
                case DroneState.Combat:
                    Combat();
                    break;
            }
        }

        bool IsPlayerVisible()
        {
            if (Vector3.Distance(player.position, transform.position) < detectDistance
                && Physics.Raycast(transform.position + Vector3.up, player.position - transform.position,
                    out var hit, detectDistance))
            {
                float angleToPlayer = Vector3.Angle(transform.forward, player.position - transform.position); // 내적과 아크코사인으로도 구할 수 있음
                
                return (angleToPlayer <= viewAngle * 0.5f) && (hit.transform == player.transform);
            }

            return false;
        }

        void Idle()
        {
            if (IsPlayerVisible())
            {
                currentTarget = player;
                currentState = DroneState.Chase;
            }
            
            _agent.destination = transform.position;
            
            if (Time.time >= _nextPatrol)
            {
                _nextPatrol = Time.time + Random.Range(10f, 20f);
                currentTarget = patrolPoints[Random.Range(0, patrolPoints.Count)];
                currentState = DroneState.Patrol;
            }
        }

        void Patrol()
        {
            if (IsPlayerVisible())
            {
                currentTarget = player;
                currentState = DroneState.Chase;
            }

            _agent.destination = currentTarget.position;

            if (Vector3.Distance(transform.position, currentTarget.position) <= _agent.stoppingDistance + 1f)
            {
                _nextPatrol = Time.time + Random.Range(3f, 6f);
                currentState = DroneState.Idle;
            }
        }

        void Chase()
        {
            _agent.destination = currentTarget.position;

            if (Vector3.Distance(transform.position, currentTarget.position) <= combatDistance)
                currentState = DroneState.Combat;
        }

        void Combat()
        {
            if(Vector3.Distance(transform.position, currentTarget.position) > combatDistance)
                currentState = DroneState.Chase;
            
            transform.localRotation = Quaternion.LookRotation(currentTarget.position - transform.position);
            
            if (Time.time >= _nextFire)
            {
                _nextFire = Time.time + fireRate;
                for (int i = 0; i < bulletCount; i++)
                {
                    Bullet bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
                    bullet.damage = bulletDamage;
                    bullet.speed = bulletSpeed;
                    bullet.parent = _entity;
                    bullet.transform.Rotate(0f, Random.Range(-bulletSpread, bulletSpread), 0f);
                }
            }
        }
    }
}