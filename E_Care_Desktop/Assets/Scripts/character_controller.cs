using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class character_controller : MonoBehaviour {

    // Animation object
    public GameObject character_object;
    public GameObject character_object_side;
    private Animation character_animation;
    private Animation character_animation2;
    private skeleton_joint character_skeleton = new skeleton_joint();
    [SerializeField] GameObject joint_spine_base;
    [SerializeField] GameObject joint_spine_mid;
    [SerializeField] GameObject joint_neck;
    [SerializeField] GameObject joint_head;
    [SerializeField] GameObject joint_shoulder_left;
    [SerializeField] GameObject joint_elbow_left;
    [SerializeField] GameObject joint_wrist_left;
    [SerializeField] GameObject joint_hand_left;
    [SerializeField] GameObject joint_shoulder_right;
    [SerializeField] GameObject joint_elbow_right;
    [SerializeField] GameObject joint_wrist_right;
    [SerializeField] GameObject joint_hand_right;
    [SerializeField] GameObject joint_hip_left;
    [SerializeField] GameObject joint_kneel_left;
    [SerializeField] GameObject joint_ankle_left;
    [SerializeField] GameObject joint_foot_left;
    [SerializeField] GameObject joint_hip_right;
    [SerializeField] GameObject joint_kneel_right;
    [SerializeField] GameObject joint_ankle_right;
    [SerializeField] GameObject joint_foot_right;
    [SerializeField] GameObject joint_spine_shoulder;
    [SerializeField] GameObject joint_hand_tip_left;
    [SerializeField] GameObject joint_thump_left;
    [SerializeField] GameObject joint_hand_tip_right;
    [SerializeField] GameObject joint_thump_right;


    [SerializeField] UnityEngine.UI.Button start_button;
    [SerializeField] UnityEngine.UI.Button stop_button;
    [SerializeField] UnityEngine.UI.Button example_button;
    [SerializeField] UnityEngine.UI.Text hint_text;
    [SerializeField] UnityEngine.UI.Text animation_timer;
    [SerializeField] UnityEngine.UI.Text heart_rate_text;
    [SerializeField] UnityEngine.UI.Text breath_rate_text;


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

    private struct collective_collection
    {
        public float head;
        public float shoulder_left;
        public float arm_left;
        public float shoulder_right;
        public float arm_right;
        public float shoulder;
        public float body;
        public float buttom;
        public float upper_hip_left;
        public float lower_hip_left;
        public float leg_left;
        public float foot_left;
        public float upper_hip_right;
        public float lower_hip_right;
        public float leg_right;
        public float foot_right;
    };

    collective_collection compare_coll = new collective_collection();

    // KinectManager instance
    private KinectManager kinect_manager;
    private bool is_kinect_initialized;
    /*
     * User tracked id
     * Value : 0 mean none traked user
     */
    private long user_id;

    private string saveFilePath = "data.csv";

    int hidden_layer = 8;
    int character_layer = 9;

    float animation_length;
    float frame_length;
    int frame = 0;

    string state = "STOP";

    float acc = 0;

	// Use this for initialization
	void Start () {
        // Get KinectManager instance
        kinect_manager = KinectManager.Instance;

        // Set user tracked id to 0
        user_id = 0;

        if (kinect_manager && kinect_manager.IsInitialized())
        {
            is_kinect_initialized = true;
        }
        else
        {
            is_kinect_initialized = false;
        }

        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }


        character_animation = character_object.GetComponent<Animation>();
        character_animation["speedbag"].speed = 0.75f;
        frame_length = character_animation["speedbag"].length;
        Debug.Log(character_animation["speedbag"].normalizedTime % 1);

        character_animation2 = character_object_side.GetComponent<Animation>();
        character_animation2["speedbag"].speed = 0.75f;

        animation_length = character_animation["speedbag"].length;
        //character_animation.Play();
        //show();
        hide();
    }
	
	// Update is called once per frame
	void Update () {
        // play();

        if (is_kinect_initialized)
        {
            // Get primary user id
            user_id = kinect_manager.GetPrimaryUserID();
            // create the file, if needed
            if (!File.Exists(saveFilePath))
            {
                using (StreamWriter writer = File.CreateText(saveFilePath))
                {
                    // csv file header
                    string sLine = "uid,frame,head,shoulder_left,arm_left,shoulder_right,arm_right,shoulder,body,buttom,upper_hip_left,lower_hip_left,leg_left,foot_left,upper_hip_right,lower_hip_right,leg_right,foot_right,heart_rate,breath_rate";
                    writer.WriteLine(sLine);
                }
            }
            if (user_id != 0 && state.Equals("PLAY"))
            {
                show();
                if (character_animation.isPlaying == false)
                {
                    character_animation.Play();
                    character_animation2.Play();
                }
                if (character_animation.isPlaying)
                {
                    if(character_animation2["speedbag"].time + 0.2 > animation_length)
                    {
                        state = "STOP";
                        if (character_animation.isPlaying)
                        {
                            character_animation.Stop();
                            character_animation2.Stop();
                        }
                        Debug.Log("Finish");
                        acc = acc / frame;
                        animation_timer.text = "Finished      ACCURACY: " + acc.ToString();
                        hide();
                    }
                    else
                    {
                        animation_timer.text = "Time lapse: " + character_animation2["speedbag"].time.ToString() + "/" + animation_length.ToString();
                        update_character();
                        update_skeleton();
                        compare_all();
                        acc += compare_coll.arm_left;
                        frame += 1;

                        using (StreamWriter writer = File.AppendText(saveFilePath))
                        {
                            string sLine = string.Format("{0},{1},{2:F2},{3:F2},{4:F2},{5:F2},{6:F2},{7:F2},{8:F2},{9:F2},{10:F2},{11:F2},{12:F2},{13:F2},{14:F2},{15:F2},{16:F2},{17:F2},{18},{19}", "1111", frame.ToString(), compare_coll.head, compare_coll.shoulder_left, compare_coll.arm_left, compare_coll.shoulder_right, compare_coll.arm_right, compare_coll.shoulder, compare_coll.body, compare_coll.buttom, compare_coll.upper_hip_left, compare_coll.lower_hip_left, compare_coll.leg_left, compare_coll.foot_left, compare_coll.upper_hip_right, compare_coll.lower_hip_right, compare_coll.leg_right, compare_coll.foot_right, heart_rate_text.text, breath_rate_text.text);
                            writer.WriteLine(sLine);
                            Debug.Log(sLine);
                        }
                    }
                    
                }
            }
        }
        update_hint();
    }

    private void compare_all()
    {
        compare_coll.head = compare(character_skeleton.angle_collection.head, user_skeleton.angle_collection.head);
        compare_coll.shoulder_left = compare(character_skeleton.angle_collection.shoulder_left, user_skeleton.angle_collection.shoulder_left);
        compare_coll.arm_left = compare(character_skeleton.angle_collection.arm_left, user_skeleton.angle_collection.arm_left);
        compare_coll.shoulder_right = compare(character_skeleton.angle_collection.shoulder_right, user_skeleton.angle_collection.shoulder_right);
        compare_coll.arm_right = compare(character_skeleton.angle_collection.arm_right, user_skeleton.angle_collection.arm_right);
        compare_coll.shoulder = compare(character_skeleton.angle_collection.shoulder, user_skeleton.angle_collection.shoulder);
        compare_coll.body = compare(character_skeleton.angle_collection.body, user_skeleton.angle_collection.body);
        compare_coll.buttom = compare(character_skeleton.angle_collection.buttom, user_skeleton.angle_collection.buttom);
        compare_coll.upper_hip_left = compare(character_skeleton.angle_collection.upper_hip_left, user_skeleton.angle_collection.upper_hip_left);
        compare_coll.lower_hip_left = compare(character_skeleton.angle_collection.lower_hip_left, user_skeleton.angle_collection.lower_hip_left);
        compare_coll.leg_left = compare(character_skeleton.angle_collection.leg_left, user_skeleton.angle_collection.leg_left);
        compare_coll.foot_left = compare(character_skeleton.angle_collection.foot_left, user_skeleton.angle_collection.foot_left);
        compare_coll.upper_hip_right = compare(character_skeleton.angle_collection.upper_hip_right, user_skeleton.angle_collection.upper_hip_right);
        compare_coll.lower_hip_right = compare(character_skeleton.angle_collection.lower_hip_right, user_skeleton.angle_collection.lower_hip_right);
        compare_coll.leg_right = compare(character_skeleton.angle_collection.leg_right, user_skeleton.angle_collection.leg_right);
        compare_coll.foot_right = compare(character_skeleton.angle_collection.foot_right, user_skeleton.angle_collection.foot_right);
    }

    private float compare(float a1, float a2)
    {
        float dif = System.Math.Abs(a1 - a2);
        float ratio = dif / a1;
        float per = 1 - ratio;
        per = per * 100;
        return per;
    }

    private void update_character()
    {
        character_skeleton.joint_collection.joint_spine_base = joint_spine_base.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_spine_mid = joint_spine_mid.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_neck = joint_neck.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_head = joint_head.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_shoulder_left = joint_shoulder_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_elbow_left = joint_elbow_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_wrist_left = joint_wrist_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_hand_left = joint_hand_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_shoulder_right = joint_shoulder_right.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_elbow_right = joint_elbow_right.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_wrist_right = joint_wrist_right.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_head = joint_hand_right.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_hip_left = joint_hip_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_kneel_left = joint_kneel_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_ankle_left = joint_ankle_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_foot_left = joint_foot_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_hip_right = joint_hip_right.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_kneel_right = joint_kneel_right.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_ankle_right = joint_ankle_right.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_foot_right = joint_foot_right.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_spine_shoulder = joint_spine_shoulder.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_hand_tip_left = joint_hand_tip_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_thump_left = joint_thump_left.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_hand_tip_right = joint_hand_tip_right.GetComponent<Transform>().position;
        character_skeleton.joint_collection.joint_thump_right = joint_thump_right.GetComponent<Transform>().position;

        character_skeleton.update_bone_collection();
        character_skeleton.update_angle_calculate();
    }

    private void update_skeleton()
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

    public void play()
    {
        show();
        state = "PLAY";
        frame = 0;
        acc = 0;
        animation_timer.text = "START TRACKING";

    }

    public void stop()
    {
        state = "STOP";
        hide();
        if (character_animation.isPlaying)
        {
            character_animation.Stop();
            character_animation2.Stop();
        }
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }

    public void example()
    {
        show();
        state = "EXAMPLE";
        if (character_animation.isPlaying == false)
        {
            character_animation.Play();
            character_animation2.Play();
        }
    }

    public void hide()
    {
        change_layer.change_layer_all(character_object.transform, hidden_layer);
        change_layer.change_layer_all(character_object_side.transform, hidden_layer);
    }

    public void show()
    {
        change_layer.change_layer_all(character_object.transform, character_layer);
        change_layer.change_layer_all(character_object_side.transform, character_layer);
    }

    private void update_hint()
    {
        if(user_id != 0)
        {
            hint_text.text = "User tracked";
        }
        else
        {
            hint_text.text = "Rise your rise hand to start tracking.";
        }
    }
}
