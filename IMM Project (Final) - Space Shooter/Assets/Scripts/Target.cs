using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target
{
    public enum Type
    {
        Physical,
        Path,
        Tactical,
        General
    }

    public enum Priority
    {
        Primary, 
        Secondary,
        none
    }

    GameObject sourceObj;
    GameObject targetObj;
    GameObject[] targetObjects;
    string name;
    Vector3 position;
    Vector3 direction;
    Quaternion rotation;
    Quaternion turn;
    float distance;

    Type type = Type.General;

    Priority priority = Priority.none;

    public Vector3 getPosition()
    {
        return position;
    }

    public Vector3 getDirection()
    {
        return direction;
    }
    public Quaternion getRotation()
    {
        return rotation;
    }

    public Quaternion getTurn()
    {
        return turn;
    }

    public float getDistance()
    {
        return distance;
    }

    public Type getType()
    {
        return type;
    }

    public GameObject getTargetObj()
    {
        return targetObj;
    }

    public GameObject getSourceObj()
    {
        return sourceObj;
    }

    public string getName() { return name; }

    public void setPosition(Vector3 p)
    {
        position = p;
    }

    public void setDirection(Vector3 d)
    {
        direction = d;
    }

    public void setRotation(Quaternion r)
    {
        rotation = r;
    }

    public void setDistance(float d)
    {
        distance = d;
    }

    public void setType(Type t)
    {
        type = t;
    }


    public void setPriority(Priority p)
    {
        priority = p;
    }

    public Target (GameObject source, GameObject target, float stepDistance)
    {
        sourceObj = source; targetObj = target;
        position = (Vector3.MoveTowards(source.transform.position, target.transform.position, stepDistance));
        direction = ((target.transform.position - source.transform.position).normalized);
        rotation = (Quaternion.LookRotation(direction));
        distance = Vector3.Distance(target.transform.position, source.transform.position);
        turn = Quaternion.RotateTowards(source.transform.rotation, rotation, 0.5f);
    }


    public Target(GameObject source, Target[] targets, float stepDistance, bool isRelative = false)
    {
        float distance1;
        Vector3 direction1;
        //Vector3 gravity1;
        sourceObj = source;
       
        distance1 = targets.Average(x => x.distance);
        direction1 = new Vector3(
        targets.Average( x => x.direction.x),
        targets.Average(x => x.direction.y),
        targets.Average(x => x.direction.z)).normalized;
        Vector3 absoluteCentreOfMass = new Vector3(direction1.x * distance1, direction1.y * distance1, direction1.z * distance1);

        /*gravity1 = new Vector3(
            targets.Average(x => (1 / Mathf.Pow(x.distance, 2)) * Mathf.Cos(Mathf.Atan(x.position.y / x.position.x)) * Mathf.Sin(Mathf.Acos(x.position.z / (1 / Mathf.Pow(x.distance, 2))))       ),
            targets.Average(x => (1 / Mathf.Pow(x.distance, 2)) * Mathf.Sin(Mathf.Atan(x.position.y / x.position.x)) * Mathf.Sin(Mathf.Acos(x.position.z / (1 / Mathf.Pow(x.distance, 2))))       ),
            targets.Average(x => (1 / Mathf.Pow(x.distance, 2))                                                      * Mathf.Cos(Mathf.Acos(x.position.z / (1 / Mathf.Pow(x.distance, 2))))       )  ).normalized;
        */

        position = (Vector3.MoveTowards(source.transform.position, absoluteCentreOfMass, stepDistance));
        direction = ((position - source.transform.position).normalized);
        rotation = (Quaternion.LookRotation(direction));
        distance = Vector3.Distance(position, source.transform.position);
        turn = Quaternion.RotateTowards(source.transform.rotation, rotation, 0.5f);
    }

    Vector3 getCartFromSpherical (Vector3 position1)
    {
        float r = Mathf.Pow(Mathf.Pow(position1.x,2)+ Mathf.Pow(position1.y, 2)+ Mathf.Pow(position1.z, 2), 1/2);
        float theta = Mathf.Atan(position1.y / position1.x);
        float phi = Mathf.Acos(position1.z / r);

        Vector3 w = new Vector3(
            r * Mathf.Cos(theta) * Mathf.Sin(phi),
            r * Mathf.Sin(theta) * Mathf.Sin(phi),
            r * Mathf.Cos(phi)
            );

        return w;
    }

}
