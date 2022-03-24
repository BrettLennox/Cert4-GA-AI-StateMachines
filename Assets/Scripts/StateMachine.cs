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

    #region UI Text
    [Header("UI Text")]
    [SerializeField] private Text _stateText;
    [SerializeField] private Text _confusedText;
    #endregion

    public State currentState;
    private AIMovement aIMovement;

    private void Start()
    {
        aIMovement = GetComponent<AIMovement>();
        NextState();
    }

    private void NextState()
    {
        //checks what the current state is set to and then starts that coroutine
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
            //sets the AIMoveTowards target to be the players position
            aIMovement.AIMoveTowards(aIMovement.player.transform);
            //checks if the player has moved out of range
            if (!aIMovement.PlayerInRange())
            {
                //changes the state to be the confused state
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
            //sets the AIMoveTowards target to be the waypoints
            aIMovement.AIMoveTowards(aIMovement.waypoints[aIMovement.waypointIndex].transform);
            if (aIMovement.PlayerInRange())
            {
                //sets the state to be the attack state
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
            DisplayConfusedText();
            aIMovement.AIMoveTowards(transform);
            //checks if player becomes in range
            if (aIMovement.PlayerInRange())
            {
                //if yes, change state to attack state
                currentState = State.Attack;
            }
            else
            {
                //if no, sets state to defend state
                currentState = State.Defend;
            }
            yield return new WaitForSeconds(1.5f);
        }
        HideConfusedText();
        NextState();
    }

    private void UpdateStateText(string state)
    {
        //updates state text to display the current state its in
        _stateText.text = state;
    }
    public void DisplayConfusedText()
    {
        //enabled the confused text gameobject
        _confusedText.gameObject.SetActive(true);
    }

    public void HideConfusedText()
    {
        //disabled the confused text gameobject
        _confusedText.gameObject.SetActive(false);
    }
}
