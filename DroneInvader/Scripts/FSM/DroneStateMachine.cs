using UnityEngine;

namespace Meta.DroneInvader.Scripts.FSM
{
    public class DroneStateMachine : MonoBehaviour
    {
        public State currentState;
        
        private void Update()
        {
            RunStateMachine();
        }

        private void RunStateMachine()
        {
            State nextState = currentState?.RunCurrentState();

            if (nextState)
            {
                SwitchState(nextState);
            }
        }

        private void SwitchState(State newState)
        {
            if (currentState != newState)
            {
                currentState.OnStateExit();
                newState.OnStateEnter();
            }
            
            currentState = newState;
        }
    }
}