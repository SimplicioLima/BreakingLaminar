using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision 
{
    Vector3 point;
    Vector3 centerRay;
    Vector3 rightRay;
    Vector3 leftRay;
}







public interface ICollisionDetector
{
    Colision GetCollision(Vector3 origin, Vector3 centerRay, Vector3 rightRay, Vector3 leftRay);
}
