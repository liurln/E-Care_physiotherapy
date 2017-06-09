using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_tracking : MonoBehaviour {


    //  Character instance
    public GameObject character;
    private Animation character_animation;

    // ui
    //public UnityEngine.UI.Text head;
    //public UnityEngine.UI.Text shoulder_left;
    //public UnityEngine.UI.Text arm_left;
    //public UnityEngine.UI.Text shoulder_right;
    //public UnityEngine.UI.Text arm_right;
    //public UnityEngine.UI.Text shoulder;
    //public UnityEngine.UI.Text body;
    //public UnityEngine.UI.Text buttom;
    //public UnityEngine.UI.Text upper_hip_left;
    //public UnityEngine.UI.Text lower_hip_left;
    //public UnityEngine.UI.Text leg_left;
    //public UnityEngine.UI.Text foot_left;
    //public UnityEngine.UI.Text upper_hip_right;
    //public UnityEngine.UI.Text lower_hip_right;
    //public UnityEngine.UI.Text leg_right;
    //public UnityEngine.UI.Text foot_right;
    /*
     * Skeleton index 25 joints
     * Index 0 - 24
     */
    public GameObject joint_spine_base          ;
    public GameObject joint_spine_mid           ;
    public GameObject joint_neck                ;
    public GameObject joint_head                ;
    public GameObject joint_shoulder_left       ;
    public GameObject joint_elbow_left          ;
    public GameObject joint_wrist_left          ;
    public GameObject joint_hand_left           ;
    public GameObject joint_shoulder_right      ;
    public GameObject joint_elbow_right         ;
    public GameObject joint_wrist_right         ;
    public GameObject joint_hand_right          ;
    public GameObject joint_hip_left            ;
    public GameObject joint_kneel_left          ;
    public GameObject joint_ankle_left          ;
    public GameObject joint_foot_left           ;
    public GameObject joint_hip_right           ;
    public GameObject joint_kneel_right         ;
    public GameObject joint_ankle_right         ;
    public GameObject joint_foot_right          ;
    public GameObject joint_spine_shoulder      ;
    public GameObject joint_hand_tip_left       ;
    public GameObject joint_thump_left          ;
    public GameObject joint_hand_tip_right      ;
    public GameObject joint_thump_right         ;

    private skeleton_joint character_skeleton = new skeleton_joint();

    // Use this for initialization
    void Start()
    {
        // Set application frame rate to 30 frames per second
        Application.targetFrameRate = 60;
        character_animation = character.GetComponent<Animation>();
    }

    // Awake use for initialization when application start
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (character_animation.isPlaying) {
            
            character_skeleton.joint_collection.joint_spine_base        = joint_spine_base.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_spine_mid         = joint_spine_mid.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_neck              = joint_neck.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_head              = joint_head.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_shoulder_left     = joint_shoulder_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_elbow_left        = joint_elbow_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_wrist_left        = joint_wrist_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_hand_left         = joint_hand_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_shoulder_right    = joint_shoulder_right.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_elbow_right       = joint_elbow_right.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_wrist_right       = joint_wrist_right.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_head              = joint_hand_right.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_hip_left          = joint_hip_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_kneel_left        = joint_kneel_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_ankle_left        = joint_ankle_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_foot_left         = joint_foot_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_hip_right         = joint_hip_right.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_kneel_right       = joint_kneel_right.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_ankle_right       = joint_ankle_right.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_foot_right        = joint_foot_right.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_spine_shoulder    = joint_spine_shoulder.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_hand_tip_left     = joint_hand_tip_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_thump_left        = joint_thump_left.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_hand_tip_right    = joint_hand_tip_right.GetComponent<Transform>().position;
            character_skeleton.joint_collection.joint_thump_right       = joint_thump_right.GetComponent<Transform>().position;

            character_skeleton.update_bone_collection();
            character_skeleton.update_angle_calculate();

            //display detail
            //head.text = "" + character_skeleton.angle_collection.head;
            //shoulder_left.text = "" + character_skeleton.angle_collection.shoulder_left;
            //arm_left.text = "" + character_skeleton.angle_collection.arm_left;
            //shoulder_right.text = "" + character_skeleton.angle_collection.shoulder_right;
            //arm_right.text = "" + character_skeleton.angle_collection.arm_right;
            //shoulder.text = "" + character_skeleton.angle_collection.shoulder;
            //body.text = "" + character_skeleton.angle_collection.body;
            //buttom.text = "" + character_skeleton.angle_collection.buttom;
            //upper_hip_left.text = "" + character_skeleton.angle_collection.upper_hip_left;
            //lower_hip_left.text = "" + character_skeleton.angle_collection.lower_hip_left;
            //leg_left.text = "" + character_skeleton.angle_collection.leg_left;
            //foot_left.text = "" + character_skeleton.angle_collection.foot_left;
            //upper_hip_right.text = "" + character_skeleton.angle_collection.upper_hip_right;
            //lower_hip_right.text = "" + character_skeleton.angle_collection.lower_hip_right;
            //leg_right.text = "" + character_skeleton.angle_collection.leg_right;
            //foot_right.text = "" + character_skeleton.angle_collection.foot_right;
        }

    }
}
