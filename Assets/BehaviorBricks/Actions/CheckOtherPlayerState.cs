using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework;
using BBUnity.Actions; // BasePrimitiveAction

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a perception condition to check if the objective is close depending on a given distance.
    /// </summary>
    [Condition("MyActions/CheckOtherPlayerState")]
    [Help("check the other guys action")]
    public class CheckOtherPlayerState : GOCondition
    {
        // Define the input parameter
        [InParam("otherPlayer")]
        public GameObject otherPlayer;

        // Define the input parameter
        [InParam("combatIntention")]
        public string stateName;

        /// <summary>
        /// verfwedjwndiqwnodjqwocjfewoijvwuinferf
        public override bool Check()
        {
            return otherPlayer.GetComponent<StateMachine>().currentState.GetType() == typeof(LeftStraightState);
        }
    }

}