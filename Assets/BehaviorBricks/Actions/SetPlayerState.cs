using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Actions
{

    /// <summary>
    /// Implementation of the wait action using busy-waiting (spinning).
    /// </summary>
    [Action("MyActions/SetPlayerState")]
    [Help("Action that success after a period of time.")]
    public class SetPlayerState : BasePrimitiveAction
    {
        // Define the input parameter
        [InParam("thisPlayer")]
        public PlayerBehavior thisPlayer;

        // Define the input parameter
        [InParam("stateName")]
        public string stateName;

        // Define the input parameter
        [InParam("duration")]
        public float duration;

        private float elapsedTime;

        /// <summary>Initialization Method of WaitForSeconds.</summary>
        /// <remarks>Initializes the elapsed time to 0.</remarks>
        public override void OnStart()
        {
            elapsedTime = 0;

            if (thisPlayer.stateMachine.currentState.GetType() == typeof(HitBodyState) ||
                thisPlayer.stateMachine.currentState.GetType() == typeof(HitHeadState) ||
                thisPlayer.stateMachine.currentState.GetType() == typeof(WinState) ||
                thisPlayer.stateMachine.currentState.GetType() == typeof(LoseState))
            {
                return;
            }
            else
            {
                switch (stateName)
                {
                    case "dodge":
                        thisPlayer.stateMachine.SetNextState(new DodgeState());
                        break;
                    case "block":
                        thisPlayer.stateMachine.SetNextState(new BlockState());
                        break;
                    case "left straight":
                        thisPlayer.stateMachine.SetNextState(new LeftStraightState());
                        break;
                    case "left hook":
                        thisPlayer.stateMachine.SetNextState(new LeftHookState());
                        break;
                    case "left body":
                        thisPlayer.stateMachine.SetNextState(new LeftBodyState());
                        break;
                    case "right straight":
                        thisPlayer.stateMachine.SetNextState(new RightStraightState());
                        break;
                    case "right hook":
                        thisPlayer.stateMachine.SetNextState(new RightHookState());
                        break;
                    case "right body":
                        thisPlayer.stateMachine.SetNextState(new RightBodyState());
                        break;
                    case "move forward":
                        thisPlayer.stateMachine.SetNextState(new MoveForwardState());
                        break;
                    case "move back":
                        thisPlayer.stateMachine.SetNextState(new MoveBackwardState());
                        break;
                    default:
                        thisPlayer.stateMachine.SetNextStateToMain();
                        break;
                }
            }

        }

        /// <summary>Method of Update of WaitForSeconds.</summary>
        /// <remarks>Increase the elapsed time and check if you have exceeded the waiting time has ended.</remarks>
        public override TaskStatus OnUpdate()
        {
            //back to idle when time out
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration)
            {
                thisPlayer.stateMachine.SetNextStateToMain();

                return TaskStatus.COMPLETED;
            }
                
            return TaskStatus.RUNNING;
        }
    }
}