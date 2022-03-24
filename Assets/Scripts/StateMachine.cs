using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Attack,
        Defend,
        Confused
    }

    [SerializeField] private Text _stateText;
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
            case State.Confused:
                StartCoroutine(ConfusedState());
                break;
        }
    }

    private IEnumerator AttackState()
    {
        while (currentState == State.Attack)
        {
            UpdateStateText(currentState.ToString());
            aIMovement.AIMoveTowards(aIMovement.player.transform);
            if (!aIMovement.PlayerInRange())
            {
                currentState = State.Confused;
            }
            yield return null;
        }
        NextState();
    }

    private IEnumerator DefendState()
    {
        while (currentState == State.Defend)
        {
            UpdateStateText(currentState.ToString());
            aIMovement.AIMoveTowards(aIMovement.waypoints[aIMovement.waypointIndex].transform);
            if (aIMovement.PlayerInRange())
            {
                currentState = State.Attack;
            }
            yield return null;
        }
        NextState();
    }

    private IEnumerator ConfusedState()
    {
        while(currentState == State.Confused)
        {
            UpdateStateText(currentState.ToString());
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

    private void UpdateStateText(string state)
    {
        _stateText.text = state;
    }
}
