using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScoutAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool isStopped;
    [SerializeField] Vector3 currentDesPoint;
    //[SerializeField] Turret turret;
    [SerializeField] bool isPlayerFound = false;
    [SerializeField] bool isPatrollingAI = false;
    public Transform wayPoints;
    public Vector3[] destinationPoints;

    public bool IsPatrollingAI
    {
        get
        {
            return isPatrollingAI;
        }
        set
        {
            isPatrollingAI = value;
            if (value)
            {
                if (wayPoints != null)
                {
                    SetDestination();
                }
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }
    void Start()
    {
        //turret = GetComponent<Turret>();


        agent = GetComponent<NavMeshAgent>();

        //IsPatrollingAI = wayPoints != null;
        if (wayPoints != null)
        {
            destinationPoints = new Vector3[wayPoints.childCount];
            for (int i = 0; i < wayPoints.childCount; i++)
            {
                destinationPoints[i] = wayPoints.GetChild(i).position;
            }
        }
    }

    void Update()
    {
        if (!IsPatrollingAI)
        {
            return;
        }
        if (!isPlayerFound)
        {
            CheckEnemyReach();
        }
        else
        {
            agent.isStopped = true;
        }
    }
    public void SetDestination()
    {
        agent.isStopped = false;
        isStopped = false;
        int desPoint = Random.Range(0, destinationPoints.Length);
        currentDesPoint = destinationPoints[desPoint];
        agent.SetDestination(currentDesPoint);
    }
    void CheckEnemyReach()
    {
        if (isStopped)
        {
            return;
        }
        if (Vector3.Distance(transform.position, currentDesPoint) <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            isStopped = true;

            float waitTime = Random.Range(1f, 5f);
            Invoke("SetDestination",waitTime);
        }
    }
    
    public void PlayerFound(bool value)
    {
        isPlayerFound = value;
        if (!isPlayerFound)
        {
            IsPatrollingAI = true;
            SetDestination();
        }
        else
        {
            IsPatrollingAI = false;
        }
    }
}
