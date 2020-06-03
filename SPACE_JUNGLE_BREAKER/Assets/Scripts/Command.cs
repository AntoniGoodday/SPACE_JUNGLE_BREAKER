using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Rigidbody rb, float i);
}

public abstract class MoveCommand
{
    public abstract void Execute();
}

public class PerformJump : Command
{
    public override void Execute(Rigidbody rb, float i)
    {
        rb.AddForce(new Vector3(0,i,0), ForceMode.Impulse);
    }
}
