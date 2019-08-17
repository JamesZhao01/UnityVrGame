using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public abstract class InputManagerBase
{
    protected SteamVR_Action_Pose pose = SteamVR_Actions.default_Pose;
    protected SteamVR_Action_Boolean grab = SteamVR_Actions.default_Grab;
    protected SteamVR_Action_Vector2 pos = SteamVR_Actions.default_Trackpad;
    protected SteamVR_Action_Boolean tap = SteamVR_Actions.default_Tap;
    protected SteamVR_Action_Boolean trackpadClick = SteamVR_Actions.default_TrackpadClick;
    protected SteamVR_Action_Vibration haptics = SteamVR_Actions.default_Haptic;

    protected GameObject left = GameObject.Find("[CameraRig]").transform.Find("Controller (left)").gameObject;
    protected GameObject right = GameObject.Find("[CameraRig]").transform.Find("Controller (right)").gameObject;
    public GameObject gameObject;
    public abstract void OnLeftControllerColliderEnter(Collider other);
    public abstract void OnLeftControllerColliderExit(Collider other);
    //public abstract void OnLeftGrab();
    //public abstract void OnLeftTrackpad();
    public abstract void OnRightControllerColliderEnter(Collider other);
    public abstract void OnRightControllerColliderExit(Collider other);
    //public abstract void OnRightGrab();
    //public abstract void OnRightTrackpad();
    public abstract void Update();

    public virtual void Start()
    {

    }

    public virtual void OnDrawGizmos()
    {

    }
}
