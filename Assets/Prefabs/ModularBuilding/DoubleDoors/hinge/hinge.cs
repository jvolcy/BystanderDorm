using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************************************************
* Overview
* --------
* This class is intended for use with a door and
* frame object.  In other wordds, you must have a
* structure that swings (the door) as well as one
* that does not (the frame, for example).  You can
* create a dummy parent if your door does not have
* a frame.  The stationary frame will house the
* trigger that will automatically open/close the
* door.  This trigger must not swing with the door.
* 
* How To Use
* ----------
* 1) Instantiate the hinge object as a child of the
* frame.  2) Position the hinge at approximately half
* way up the door at the edge where the door's
* hinges would be.  3) The hinge object has a trigger
* collider.  Position this collider so that it
* covers the area on either side of the door where
* the player would trigger it.  Hint: for best
* result, most of the trigger area should be on the
* side the door swing into.  That way, if the door
* hits the player, the player does not get pushed
* outside the trigger zone, causing the door to 
* close before the player can get through.
* 4)The hinge object has a pivot child object.  Set the
* parts of the door that need to swing (the door, the
* door knobs, door plates, etc.) as children of
* the pivot object. 5) Set the door swing parameter
* (LH or RH).
*/
public class hinge : MonoBehaviour
{
    [SerializeField] DoorSwing DoorType;
    [SerializeField] bool AutoDoorOpener = true;
    [SerializeField] Pivot pivot;

    // Start is called before the first frame update
    void Start()
    {
        pivot.DoorType = DoorType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!AutoDoorOpener) return;
        pivot.Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!AutoDoorOpener) return;
        pivot.Close();
    }
}
