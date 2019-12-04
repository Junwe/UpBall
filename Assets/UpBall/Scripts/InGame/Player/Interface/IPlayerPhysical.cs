using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerPhysical
{
    void SetMouseButton(Vector3 direction, float movePower);
    void SetMouseButtonUp(Vector3 direction, float movePower);
    void Oncollision(Collision2D collision);
    void CheckUnderWall(BALLSTATE _ballState, ref bool _isCheck);

    Rigidbody2D myRigidbody
    {
        get;
    }
}
