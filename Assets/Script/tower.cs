using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    public float radius;
    public float angle;
    public GameObject Player;

    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask ObstacleMask;

    public bool CanSeePlayer;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FOVroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);

        while (true)
        {
            yield return wait;
            FovCheckForPlayer();
        }
    }

    private void FovCheckForPlayer()
    {
        Collider[] CheckPlayer = Physics.OverlapSphere(transform.position, radius, TargetMask);

        if (CheckPlayer.Length != 0)
        {
            Transform Target = CheckPlayer[0].transform;
            Vector3 directionToTarget = (Target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) > angle / 2)
            {
                float DistanceToTarget = Vector3.Distance(transform.position, Target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, DistanceToTarget, ObstacleMask))
                {
                    CanSeePlayer = true;
                }
                else
                    CanSeePlayer = false;
            }
            else
                CanSeePlayer = false;
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;
    }
}
