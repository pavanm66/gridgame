using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public OnBoolChange onBool;
    //public EnemyScoutAI enemyAI;
    public bool setter = false;
    public bool Setter
    {
        get
        {
            return setter;
        }
        set
        {
            setter = value;
            if (value)
            {
                //enemyAI.IsPatrollingAI = true;
                onBool.Invoke(true);
            }
            else
            {
                //enemyAI.IsPatrollingAI = false;
                onBool.Invoke(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Setter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Setter = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero,Vector3.one);
    }
}
