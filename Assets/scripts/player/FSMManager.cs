﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//열거형  enum
public enum PlayerState
{
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK
}







public class FSMManager : MonoBehaviour {

    public PlayerState currentState;
    public PlayerState startState;
    public Transform marker;



    Dictionary<PlayerState, PlayerFSMState> states
        = new Dictionary<PlayerState, PlayerFSMState>();

    private void Awake()
    {
        marker =GameObject.FindGameObjectWithTag("Marker").transform;

        states.Add( PlayerState.IDLE,GetComponent<PlayerIDLE>());
        states.Add( PlayerState.RUN, GetComponent<PlayerRUN>());
        states.Add( PlayerState.CHASE, GetComponent<PlayerCHASE>());
        states.Add( PlayerState.ATTACK,GetComponent<PlayerATTACK>());

        
    }

    public void SetState(PlayerState newState)
    {
        foreach(PlayerFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }

        states[newState].enabled = true;
    }
    // Use this for initialization
    void Start () {
        SetState(startState);

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r,out hit,1000f))
            {
                //Debug.Log(hit.point);
                marker.position = hit.point;

                SetState(PlayerState.RUN);

                
            }


            //Debug.Log("Click" + Input.mousePosition);
        }
	}
}
