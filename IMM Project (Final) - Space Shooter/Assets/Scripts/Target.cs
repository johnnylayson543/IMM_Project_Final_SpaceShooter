using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// a class of objects called Target
public class Target
{

    // targets can have different types
    public enum Type
    {
        Physical,
        Path,
        Tactical,
        General
    }

    // targets can have different priorities
    public enum Priority
    {
        Primary, 
        Secondary,
        none
    }

    GameObject sourceObj;     // the source game object to which the target relates
    GameObject targetObj;     // the target game object which the source targets
    GameObject[] targetObjects; // // the target game objects plural that the source targets
    string name;   // name of the target
    Vector3 position;  // its position
    Vector3 direction; // direction from the source
    Quaternion rotation; // rotation of the target expressed as a quaternion
    Quaternion turn;     // the rotation which the source has to make to face the target  
    float distance;      // the distance of the target from the source

    Type type = Type.General;  // the type of target is initially general

    Priority priority = Priority.none;  // initial there is no priority


    // getters and setters

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


    // constructor for the Target object when only one target is considered at a time
    public Target (GameObject source, GameObject target, float stepDistance)
    {
        sourceObj = source; targetObj = target;  // the source and target game objects are set for future reference
        position = (Vector3.MoveTowards(source.transform.position, target.transform.position, stepDistance));  // the position of the target relative to the source is calculated
        direction = ((target.transform.position - source.transform.position).normalized);  // the compass position of the target from the source expressed as a unit vector
        rotation = (Quaternion.LookRotation(direction));  // the direction expressed as a quaternion (a set of rotation parameters and extension of complex numbers) from the source
        distance = Vector3.Distance(target.transform.position, source.transform.position); // the distance from the source to the target
        turn = Quaternion.RotateTowards(source.transform.rotation, rotation, 0.5f); // the change in rotation needed for the source to face the target expressed as quaternion 
    }

    // another constructor for the Target object when many targets are considered as one big target
    public Target(GameObject source, Target[] targets, float stepDistance, bool isRelative = false)
    {
        float distance1;  // the variable that holds the average distance of all the targets
        Vector3 direction1; // the variable that holds the average direction of all the targets
        //Vector3 gravity1;
        sourceObj = source; // the source game object
       
        // Gets the average disance and direction of the other targets of the source's own kind
        distance1 = targets.Average(x => x.distance);    // calculates the average distance of all the targets using C# querying feature and an average function that can be applyed over arrays or lists of objects
        direction1 = new Vector3(
        targets.Average( x => x.direction.x),  // gets the x axis component of the compass direction express as a vector and for every target finds the average
        targets.Average(x => x.direction.y),   // gets the y axis component of the compass direction
        targets.Average(x => x.direction.z)).normalized;  // gets the z axist component and does the same; then normalises the Vector3 to the unit sphere
        
        // using the average direction vector and average distance scalar it finds the average position of
        // all the targets called here the absolute centre of mass (purely geometric, not a force vector)
        Vector3 absoluteCentreOfMass = new Vector3(direction1.x * distance1, direction1.y * distance1, direction1.z * distance1);

        /*gravity1 = new Vector3(
            targets.Average(x => (1 / Mathf.Pow(x.distance, 2)) * Mathf.Cos(Mathf.Atan(x.position.y / x.position.x)) * Mathf.Sin(Mathf.Acos(x.position.z / (1 / Mathf.Pow(x.distance, 2))))       ),
            targets.Average(x => (1 / Mathf.Pow(x.distance, 2)) * Mathf.Sin(Mathf.Atan(x.position.y / x.position.x)) * Mathf.Sin(Mathf.Acos(x.position.z / (1 / Mathf.Pow(x.distance, 2))))       ),
            targets.Average(x => (1 / Mathf.Pow(x.distance, 2))                                                      * Mathf.Cos(Mathf.Acos(x.position.z / (1 / Mathf.Pow(x.distance, 2))))       )  ).normalized;
        */

        // calculates relative navigation parameters turn and distance from target position, target direction and target rotation
        // the absolute centric of mass is used as an average target in the following calculations, many targets considered as one big target
        position = (Vector3.MoveTowards(source.transform.position, absoluteCentreOfMass, stepDistance));
        direction = ((position - source.transform.position).normalized);
        rotation = (Quaternion.LookRotation(direction));
        distance = Vector3.Distance(position, source.transform.position);
        turn = Quaternion.RotateTowards(source.transform.rotation, rotation, 0.5f);
    }

    // gets the x y z dimensions from spherical dimensions specific
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
