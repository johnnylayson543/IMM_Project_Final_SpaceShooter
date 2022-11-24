using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    private bool fireProjectile = false;
    private bool canShoot = true;
    float firingInterval = 1.0f;
    private float firingTimer = 0.0f;
    private GameObject targetObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // add delta time in seconds to the firing timer
        firingTimer += Time.deltaTime;

        // find the parent name
        string parentName = transform.parent.gameObject.name;

        // assign the parent object
        GameObject parentObj = transform.parent.gameObject;

        // assign the target object based on the parent object
        if (parentObj.tag == "Player") {
            targetObj = GameObject.Find("Enemy");
        } else if(parentObj.tag == "Enemy")
        {
            if (GameObject.Find("Player")) { targetObj = GameObject.Find("Player"); }
            else if (GameObject.Find("Earth"))  { targetObj = GameObject.Find("Earth"); }
            else { targetObj = GameObject.Find("Origin"); }
        } else
        { 
        }


        // find the direction of the target from the current spacecraft
        Vector3 targetPosition = (this && targetObj) ? (targetObj.transform.position - transform.position) : new Vector3(0,0,0);

        // conditions under which a spacecraft can use projectiles i.e. fire their weapons
        // -- an enemy spacecraft may open fire if its target is in view and within range
        // -- a player can open fire if they press the spacebar key
        // -- --- but only if the firing interval time is exceeded after the each firing 
        bool fireCondition1 = (parentObj.tag == "Enemy") && (Vector3.Angle(transform.forward, targetPosition.normalized) < 30.0f && Vector3.Distance(transform.position, targetObj.transform.position) < 50.0f);
        bool fireCondition2 = (parentObj.tag == "Player") && (Input.GetKeyDown(KeyCode.Space));
        fireProjectile = (fireCondition1 || fireCondition2) && (canShoot == true);
        if(fireProjectile) { canShoot = false; }

        // Launch a projectile if Space key is pressed down
        if (fireProjectile)
        {

            // Player launches a projectile
            GameObject projectileObj = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectileObj.tag = parentObj.tag + "Projectile";

        }

        // reset the firing timer
        if (firingTimer > firingInterval)
        {
            firingTimer = 0.0f;
            canShoot = true;
        }
    }
}
