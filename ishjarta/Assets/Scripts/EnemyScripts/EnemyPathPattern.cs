using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathPattern : MonoBehaviour
{
    [SerializeField] AIPath aiPath;

    private Vector3 targetPosition;
    private Vector3 thisPosition;

    bool moveRight = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetPosition == null || aiPath.destination != targetPosition)
            targetPosition = aiPath.destination;
        thisPosition = this.transform.position;

        MoveInCircle();
    }


    void MoveInZikZak()
    {
        aiPath.slowWhenNotFacingTarget = true;

        Vector2 lookdir = targetPosition - thisPosition;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x);
        if (angle < 0)
            angle = (float)(Math.PI - Math.Abs(angle) + Math.PI);

        angle *= Mathf.Rad2Deg;
        //Debug.Log("Winkel: " + angle);

        float difference = 0.5f;
        float x = 1, y = 1;

        if (angle >= 45 && angle < 135)
        {
            //difference += Mathf.Abs(thisPosition.y - targetPosition.y)/2;
            if (thisPosition.x <= targetPosition.x - difference)
            {
                moveRight = true;
                x = 1;
                y = 0;
            }
            else if (thisPosition.x >= targetPosition.x + difference)
            {
                moveRight = false;
                x = -1;
                y = 0;
            }
        }
        else if (angle >= 135 && angle < 225)
        {
            //difference += Mathf.Abs(thisPosition.x - targetPosition.x)/2;
            if (thisPosition.y <= targetPosition.y - difference)
            {
                moveRight = true;
                x = 0;
                y = 1;
            }
            else if (thisPosition.y >= targetPosition.y + difference)
            {
                moveRight = false;
                x = 0;
                y = -1;
            }
        }
        else if (angle >= 225 && angle < 315)
        {
            //difference += Mathf.Abs(thisPosition.y - targetPosition.y)/2;
            if (thisPosition.x >= targetPosition.x + difference)
            {
                moveRight = true;
                x = -1;
                y = 0;
            }
            else if (thisPosition.x <= targetPosition.x - difference)
            {
                moveRight = false;
                x = 1;
                y = 0;
            }
        }
        else if (angle >= 315 && angle <= 359 || angle >= 0 && angle < 45)
        {
            //difference += Mathf.Abs(thisPosition.x - targetPosition.x)/2;
            if (thisPosition.y >= targetPosition.y + difference)
            {
                moveRight = true;
                x = 0;
                y = -1;
            }
            else if (thisPosition.y <= targetPosition.y - difference)
            {
                moveRight = false;
                x = 0;
                y = 1;
            }
        }

        float attackAngle;
        if (moveRight)
            attackAngle = (angle + 90);
        else
            attackAngle = (angle - 90);

        if (attackAngle >= 360)
            attackAngle -= 360;
        if (attackAngle < 0)
            attackAngle += 360;

        //if (attackAngle == 0)
        //{
        //    x *= +1;
        //    y *= 0;
        //}
        //else if (attackAngle > 0 && attackAngle < 90)
        //{
        //    x *= +1;
        //    y *= +1;
        //}
        //else if (attackAngle == 90)
        //{
        //    x *= 0;
        //    y *= +1;
        //}
        //else if (attackAngle > 90 && attackAngle < 180)
        //{
        //    x *= -1;
        //    y *= +1;
        //}
        //else if (attackAngle == 180)
        //{
        //    x *= -1;
        //    y *= 0;
        //}
        //else if (attackAngle > 180 && attackAngle < 270)
        //{
        //    x *= -1;
        //    y *= -1;
        //}
        //else if (attackAngle == 270)
        //{
        //    x *= 0;
        //    y *= -1;
        //}
        //else if (attackAngle > 270 && attackAngle < 360)
        //{
        //    x *= +1;
        //    y *= -1;
        //}
        Vector2 v = Entity.RotateVector2(new Vector2(Time.fixedDeltaTime * 0.4f * x, Time.fixedDeltaTime * 0.4f * y), attackAngle * Mathf.Deg2Rad);
        Debug.Log("x: " + v.x + " | y: " + v.y);
        aiPath.Move(v);
        //aiPath.FinalizeMovement(new Vector2(0.01f * x, 0.01f * y), Quaternion.identity);
    }


    void MoveInCircle()
    {
        aiPath.slowWhenNotFacingTarget = true;

        Vector2 lookdir = targetPosition - thisPosition;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x);
        if (angle < 0)
            angle = (float)(Math.PI - Math.Abs(angle) + Math.PI);

        angle *= Mathf.Rad2Deg;
        //Debug.Log("Winkel: " + angle);

        float difference = 0;
        float x = 1, y = 1;

        if (angle >= 45 && angle < 135)
        {
            difference += Mathf.Abs(thisPosition.y - targetPosition.y);
            if (thisPosition.x <= targetPosition.x - difference)
            {
                moveRight = true;
                x = 1;
                y = 0;
            }
            else if (thisPosition.x >= targetPosition.x + difference)
            {
                moveRight = false;
                x = -1;
                y = 0;
            }
        }
        else if (angle >= 135 && angle < 225)
        {
            difference += Mathf.Abs(thisPosition.x - targetPosition.x);
            if (thisPosition.y <= targetPosition.y - difference)
            {
                moveRight = true;
                x = 0;
                y = 1;
            }
            else if (thisPosition.y >= targetPosition.y + difference)
            {
                moveRight = false;
                x = 0;
                y = -1;
            }
        }
        else if (angle >= 225 && angle < 315)
        {
            difference += Mathf.Abs(thisPosition.y - targetPosition.y);
            if (thisPosition.x >= targetPosition.x + difference)
            {
                moveRight = true;
                x = -1;
                y = 0;
            }
            else if (thisPosition.x <= targetPosition.x - difference)
            {
                moveRight = false;
                x = 1;
                y = 0;
            }
        }
        else if (angle >= 315 && angle <= 359 || angle >= 0 && angle < 45)
        {
            difference += Mathf.Abs(thisPosition.x - targetPosition.x);
            if (thisPosition.y >= targetPosition.y + difference)
            {
                moveRight = true;
                x = 0;
                y = -1;
            }
            else if (thisPosition.y <= targetPosition.y - difference)
            {
                moveRight = false;
                x = 0;
                y = 1;
            }
        }

        float attackAngle;
        if (moveRight)
            attackAngle = (angle + 135);
        else
            attackAngle = (angle - 135);

        if (attackAngle >= 360)
            attackAngle -= 360;
        if (attackAngle < 0)
            attackAngle += 360;

        Vector2 v = Entity.RotateVector2(new Vector2(Time.fixedDeltaTime * x, -Time.fixedDeltaTime / 8 * y), attackAngle * Mathf.Deg2Rad);
        Debug.Log("x: " + v.x + " | y: " + v.y);
        aiPath.Move(v);
    }

    
}

//float attackAngle;
//if (moveRight)
//    attackAngle = (angle + 90);
//else
//    attackAngle = (angle - 90);

//if (attackAngle >= 360)
//    attackAngle -= 360;
//if (attackAngle < 0)
//    attackAngle += 360;

//float x = 1, y = 1;

//if (attackAngle == 0)
//{
//    x = +1;
//    y = 0;
//}
//else if (attackAngle > 0 && attackAngle < 90)
//{
//    x = +1;
//    y = +1;
//}
//else if (attackAngle == 90)
//{
//    x = 0;
//    y = +1;
//}
//else if (attackAngle > 90 && attackAngle < 180)
//{
//    x = -1;
//    y = +1;
//}
//else if (attackAngle == 180)
//{
//    x = -1;
//    y = 0;
//}
//else if (attackAngle > 180 && attackAngle < 270)
//{
//    x = -1;
//    y = -1;
//}
//else if (attackAngle == 270)
//{
//    x = 0;
//    y = -1;
//}
//else if (attackAngle > 270 && attackAngle < 360)
//{
//    x = +1;
//    y = -1;
//}