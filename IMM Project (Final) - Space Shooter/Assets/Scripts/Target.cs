using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target
{
    public enum Type
    {
        Physical,
        Path,
        General
    }

    public enum Priority
    {
        Primary, 
        Secondary,
        none
    }


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
        name = source.name;
        position = (Vector3.MoveTowards(source.transform.position, target.transform.position, stepDistance));
        direction = ((target.transform.position - source.transform.position).normalized);
        rotation = (Quaternion.LookRotation(direction));
        distance = Vector3.Distance(target.transform.position, source.transform.position);
        turn = Quaternion.RotateTowards(source.transform.rotation, rotation, 0.5f);
    }

}
