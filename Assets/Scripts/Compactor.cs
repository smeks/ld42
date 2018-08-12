using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compactor : MonoBehaviour
{

    public Rigidbody NorthWall;
    public Rigidbody SouthWall;

    public void Tick()
    {
        NorthWall.velocity = new Vector3(0, 0, -0.05f);
        SouthWall.velocity = new Vector3(0, 0, 0.05f);
    }
}
