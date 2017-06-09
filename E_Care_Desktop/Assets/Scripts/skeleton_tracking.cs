using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class skeleton_tracking : MonoBehaviour {

    // ui
    public UnityEngine.UI.Text head;
    public UnityEngine.UI.Text shoulder_left;
    public UnityEngine.UI.Text arm_left;
    public UnityEngine.UI.Text shoulder_right;
    public UnityEngine.UI.Text arm_right;
    public UnityEngine.UI.Text shoulder;
    public UnityEngine.UI.Text body;
    public UnityEngine.UI.Text buttom;
    public UnityEngine.UI.Text upper_hip_left;
    public UnityEngine.UI.Text lower_hip_left;
    public UnityEngine.UI.Text leg_left;
    public UnityEngine.UI.Text foot_left;
    public UnityEngine.UI.Text upper_hip_right;
    public UnityEngine.UI.Text lower_hip_right;
    public UnityEngine.UI.Text leg_right;
    public UnityEngine.UI.Text foot_right;

    /*
     * Skeleton index 25 joints
     * Index 0 - 24
     */
    private KinectInterop.JointType u_joint_spine_base = KinectInterop.JointType.SpineBase;
    private KinectInterop.JointType u_joint_spine_mid = KinectInterop.JointType.SpineMid;
    private KinectInterop.JointType u_joint_neck = KinectInterop.JointType.Neck;
    private KinectInterop.JointType u_joint_head = KinectInterop.JointType.Head;
    private KinectInterop.JointType u_joint_shoulder_left = KinectInterop.JointType.ShoulderLeft;
    private KinectInterop.JointType u_joint_elbow_left = KinectInterop.JointType.ElbowLeft;
    private KinectInterop.JointType u_joint_wrist_left = KinectInterop.JointType.WristLeft;
    private KinectInterop.JointType u_joint_hand_left = KinectInterop.JointType.HandLeft;
    private KinectInterop.JointType u_joint_shoulder_right = KinectInterop.JointType.ShoulderRight;
    private KinectInterop.JointType u_joint_elbow_right = KinectInterop.JointType.ElbowRight;
    private KinectInterop.JointType u_joint_wrist_right = KinectInterop.JointType.WristRight;
    private KinectInterop.JointType u_joint_hand_right = KinectInterop.JointType.HandRight;
    private KinectInterop.JointType u_joint_hip_left = KinectInterop.JointType.HipLeft;
    private KinectInterop.JointType u_joint_kneel_left = KinectInterop.JointType.KneeLeft;
    private KinectInterop.JointType u_joint_ankle_left = KinectInterop.JointType.AnkleLeft;
    private KinectInterop.JointType u_joint_foot_left = KinectInterop.JointType.FootLeft;
    private KinectInterop.JointType u_joint_hip_right = KinectInterop.JointType.HipRight;
    private KinectInterop.JointType u_joint_kneel_right = KinectInterop.JointType.KneeRight;
    private KinectInterop.JointType u_joint_ankle_right = KinectInterop.JointType.AnkleRight;
    private KinectInterop.JointType u_joint_foot_right = KinectInterop.JointType.FootRight;
    private KinectInterop.JointType u_joint_spine_shoulder = KinectInterop.JointType.SpineShoulder;
    private KinectInterop.JointType u_joint_hand_tip_left = KinectInterop.JointType.HandTipLeft;
    private KinectInterop.JointType u_joint_thump_left = KinectInterop.JointType.ThumbLeft;
    private KinectInterop.JointType u_joint_hand_tip_right = KinectInterop.JointType.HandTipRight;
    private KinectInterop.JointType u_joint_thump_right = KinectInterop.JointType.ThumbRight;

    private skeleton_joint user_skeleton = new skeleton_joint();

    // KinectManager instance
    private KinectManager kinect_manager;
    private bool is_kinect_initialized;

    /*
     * User tracked id
     * Value : 0 mean none traked user
     */
    private long user_id;

    // Use this for initialization
	void Start () {
        // Get KinectManager instance
        kinect_manager = KinectManager.Instance;

        // Set user tracked id to 0
        user_id = 0;

        if(kinect_manager && kinect_manager.IsInitialized())
        {
            is_kinect_initialized = true;
        }
        else
        {
            is_kinect_initialized = false;
        }
    }

    // Awake use for initialization when application start
    void Awake() {
        // Set application frame rate to 30 frames per second
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_kinect_initialized)
        {
            // Get primary user id
            user_id = kinect_manager.GetPrimaryUserID();
            // Check primary user tracked
            if (user_id != 0)
            {
                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_spine_base))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_spine_base);
                    user_skeleton.joint_collection.joint_spine_base = joint_position;
                    Debug.Log("u_joint_spine_base position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_spine_mid))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_spine_mid);
                    user_skeleton.joint_collection.joint_spine_mid = joint_position;
                    Debug.Log("u_joint_spine_mid position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_neck))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_neck);
                    user_skeleton.joint_collection.joint_neck = joint_position;
                    Debug.Log("u_joint_neck position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_head))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_head);
                    user_skeleton.joint_collection.joint_head = joint_position;
                    Debug.Log("u_joint_head position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_shoulder_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_shoulder_left);
                    user_skeleton.joint_collection.joint_shoulder_left = joint_position;
                    Debug.Log("u_joint_shoulder_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_elbow_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_elbow_left);
                    user_skeleton.joint_collection.joint_elbow_left = joint_position;
                    Debug.Log("u_joint_elbow_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_wrist_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_wrist_left);
                    user_skeleton.joint_collection.joint_wrist_left = joint_position;
                    Debug.Log("u_joint_wrist_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_hand_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_hand_left);
                    user_skeleton.joint_collection.joint_hand_left = joint_position;
                    Debug.Log("u_joint_hand_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_shoulder_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_shoulder_right);
                    user_skeleton.joint_collection.joint_shoulder_right = joint_position;
                    Debug.Log("u_joint_shoulder_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_elbow_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_elbow_right);
                    user_skeleton.joint_collection.joint_elbow_right = joint_position;
                    Debug.Log("u_joint_elbow_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_wrist_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_wrist_right);
                    user_skeleton.joint_collection.joint_wrist_right = joint_position;
                    Debug.Log("u_joint_wrist_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_hand_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_hand_right);
                    user_skeleton.joint_collection.joint_head = joint_position;
                    Debug.Log("u_joint_hand_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_hip_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_hip_left);
                    user_skeleton.joint_collection.joint_hip_left = joint_position;
                    Debug.Log("u_joint_hip_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_kneel_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_kneel_left);
                    user_skeleton.joint_collection.joint_kneel_left = joint_position;
                    Debug.Log("u_joint_kneel_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_ankle_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_ankle_left);
                    user_skeleton.joint_collection.joint_ankle_left = joint_position;
                    Debug.Log("u_joint_ankle_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_foot_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_foot_left);
                    user_skeleton.joint_collection.joint_foot_left = joint_position;
                    Debug.Log("u_joint_foot_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_hip_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_hip_right);
                    user_skeleton.joint_collection.joint_hip_right = joint_position;
                    Debug.Log("u_joint_hip_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_kneel_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_kneel_right);
                    user_skeleton.joint_collection.joint_kneel_right = joint_position;
                    Debug.Log("u_joint_kneel_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_ankle_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_ankle_right);
                    user_skeleton.joint_collection.joint_ankle_right = joint_position;
                    Debug.Log("u_joint_ankle_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_foot_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_foot_right);
                    user_skeleton.joint_collection.joint_foot_right = joint_position;
                    Debug.Log("u_joint_foot_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_spine_shoulder))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_spine_shoulder);
                    user_skeleton.joint_collection.joint_spine_shoulder = joint_position;
                    Debug.Log("u_joint_spine_shoulder position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_hand_tip_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_hand_tip_left);
                    user_skeleton.joint_collection.joint_hand_tip_left = joint_position;
                    Debug.Log("u_joint_hand_tip_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_thump_left))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_thump_left);
                    user_skeleton.joint_collection.joint_thump_left = joint_position;
                    Debug.Log("u_joint_thump_left position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_hand_tip_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_hand_tip_right);
                    user_skeleton.joint_collection.joint_hand_tip_right = joint_position;
                    Debug.Log("u_joint_hand_tip_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                if (kinect_manager.IsJointTracked(user_id, (int)u_joint_thump_right))
                {
                    Vector3 joint_position = kinect_manager.GetJointPosition(user_id, (int)u_joint_thump_right);
                    user_skeleton.joint_collection.joint_thump_right = joint_position;
                    Debug.Log("u_joint_thump_right position: " + joint_position.x + "," + joint_position.y + "," + joint_position.z);
                }

                user_skeleton.update_bone_collection();
                user_skeleton.update_angle_calculate();

            }
        }
    }

}
