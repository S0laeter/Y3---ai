using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework;
using BBUnity.Actions; // BasePrimitiveAction

[Action("MyActions/SetPlayerIntention")]
[Help("what to do now")]
public class SetPlayerIntention : BasePrimitiveAction
{
    // Define the input parameter
    [InParam("thisPlayer")]
    public PlayerBehavior thisPlayer;

    // Define the input parameter
    [InParam("combatIntention")]
    public CombatIntention combatIntention;

    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {
        //do something here...
        thisPlayer.combatIntention = combatIntention;

        // The action is completed. We must inform the execution engine.
        return TaskStatus.COMPLETED;

    } // OnUpdate

}