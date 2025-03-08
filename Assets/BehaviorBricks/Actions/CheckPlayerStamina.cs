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
    [Condition("MyActions/CheckPlayerStamina")]
    [Help("check the other guys action")]
    public class CheckPlayerStamina : GOCondition
    {
        // Define the input parameter
        [InParam("stamina")]
        public float stamina;

        /// <summary>
        /// Check the other player's combatIntention, if it's the same as input here, returns true
        public override bool Check()
        {
            return gameObject.GetComponent<PlayerBehavior>().currentStamina >= stamina;
        }
    }
}