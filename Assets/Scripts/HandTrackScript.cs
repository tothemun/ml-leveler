using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HandTrackScript : MonoBehaviour {
    #region  Public Variables
    public enum HandPoses { Ok, Finger, Thumb, OpenHandBack, Fist, NoPose };
    public HandPoses pose = HandPoses.NoPose;
    public GameObject LeftIndex, RightIndex;
    #endregion

    #region Private Variables
    private MLHandKeyPose[] _gestures;
    private bool LeftPosing = false;
    private bool RightPosing = false;
    #endregion

    #region Unity Methods
    private void Awake() {
        MLHands.Start();
        _gestures = new MLHandKeyPose[5]; 
        _gestures[0] = MLHandKeyPose.Ok;
        _gestures[1] = MLHandKeyPose.Finger;
        _gestures[2] = MLHandKeyPose.OpenHandBack;
        _gestures[3] = MLHandKeyPose.Fist;
        _gestures[4] = MLHandKeyPose.Thumb;
        MLHands.KeyPoseManager.EnableKeyPoses(_gestures, true, false);
    }
    private void OnDestroy() {
        MLHands.Stop();
    }

    private void Update() {
        if (GetGesture(MLHands.Left, MLHandKeyPose.Thumb)){
            ShowPoints(MLHands.Left, LeftIndex, !LeftPosing);
            LeftPosing = true;
        }else{
            LeftPosing = false;
        }

        if (GetGesture(MLHands.Right, MLHandKeyPose.Thumb)){
            ShowPoints(MLHands.Right, RightIndex, !RightPosing); 
            RightPosing = true;
        }else{
            RightPosing = false;
        }
    }
    #endregion

    #region Private Methods
    private void ShowPoints(MLHand hand, GameObject sphere, bool _starting) {
        Vector3 _pos = hand.Thumb.KeyPoints[0].Position;
        if(_starting)
        {
            sphere.transform.position = _pos;
        }else{
            if(Vector3.Distance(sphere.transform.position,_pos) < 0.5f)
            {
                sphere.transform.position = Vector3.Lerp(sphere.transform.position,_pos,Time.deltaTime / 0.0625f);
            }
        }
    }

    private bool GetGesture(MLHand hand, MLHandKeyPose type) {
        if (hand != null) {            
            if (hand.KeyPose == type) {
                print(hand.KeyPoseConfidence);
                if (hand.KeyPoseConfidence > 0.9f) {                       
                    return true;
                }
            }
        }
        return false;
    }
    #endregion

}