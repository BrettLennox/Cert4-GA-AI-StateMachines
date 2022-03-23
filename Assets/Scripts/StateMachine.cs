using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Attack,
        Defend,
        Confusion
    }

    public State currentState;
    private AIMovement aIMovement;

    private void Start()
    {
        aIMovement = GetComponent<AIMovement>();
        NextState();
    }

    private void NextState()
    {
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defend:
                StartCoroutine(DefendState());
                break;
            case State.Confusion:
                StartCoroutine(ConfusionState());
                break;
        }
    }

    private IEnumerator AttackState()
    {
        while (currentState == State.Attack)
        {
            aIMovement.AIMoveTowards(aIMovement.player.transform);
            if (!aIMovement.PlayerInRange())
            {
                currentState = State.Confusion;
            }
            yield return null;
        }
        NextState();
    }

    private IEnumerator DefendState()
    {
        while (currentState == State.Defend)
        {
            aIMovement.AIMoveTowards(aIMovement.waypoints[aIMovement.waypointIndex].transform);
            if (aIMovement.PlayerInRange())
            {
                currentState = State.Attack;
            }
            yield return null;
        }
        NextState();
    }

    private IEnumerator ConfusionState()
    {
        while(currentState == State.Confusion)
        {
            aIMovement.DisplayConfusionText();
            aIMovement.AIMoveTowards(transform);
            if (aIMovement.PlayerInRange())
            {
                currentState = State.Attack;
            }
            else
            {
                currentState = State.Defend;
            }
            yield return new WaitForSeconds(1.5f);
        }
        aIMovement.HideConfusionText();
        NextState();
    }
}
