using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// RN not really in use, will get it fixed later
/// </summary>

public abstract class Command
{
    public abstract void Execute(float i);
}

public abstract class MoveCommand
{
    public abstract void Execute();
}

public class PerformJump : Command
{
    public override void Execute(float i)
    {
        //rb.AddForce(new Vector3(0,i,0), ForceMode.VelocityChange);
    }
}
