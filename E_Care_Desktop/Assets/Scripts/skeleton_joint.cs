using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_joint {

    public struct joint_collection_struct
    {
        public Vector3 joint_spine_base;
        public Vector3 joint_spine_mid;
        public Vector3 joint_neck;
        public Vector3 joint_head;
        public Vector3 joint_shoulder_left;
        public Vector3 joint_elbow_left;
        public Vector3 joint_wrist_left;
        public Vector3 joint_hand_left;
        public Vector3 joint_shoulder_right;
        public Vector3 joint_elbow_right;
        public Vector3 joint_wrist_right;
        public Vector3 joint_hand_right;
        public Vector3 joint_hip_left;
        public Vector3 joint_kneel_left;
        public Vector3 joint_ankle_left;
        public Vector3 joint_foot_left;
        public Vector3 joint_hip_right;
        public Vector3 joint_kneel_right;
        public Vector3 joint_ankle_right;
        public Vector3 joint_foot_right;
        public Vector3 joint_spine_shoulder;
        public Vector3 joint_hand_tip_left;
        public Vector3 joint_thump_left;
        public Vector3 joint_hand_tip_right;
        public Vector3 joint_thump_right;
    };

    public enum joint_vector : int
    {
        // spine shoulder, neck, head
        head = 1,
        // spine shoulder, shoulder left, elbow left
        shoulder_left = 2,
        // shoulder left, elbow left, wrist left
        arm_left = 3,
        // spine shoulder, shoulder right, elbow right
        shoulder_right = 4,
        // shoulder right, elbow right, wrist right
        arm_right = 5,
        // shoulder left, spine shoulder, shoulder right
        shoulder = 6,
        // spine shoulder, spine mid, spine base
        body = 7,
        // hip left, spine base, hip right
        buttom = 8,
        // spine mid, spine base, hip left
        upper_hip_left = 9,
        // spine base, hip left, kneel left
        lower_hip_left = 10,
        // hip left, kneel left, ankle left
        leg_left = 11,
        // kneel left, ankle left, foot left
        foot_left = 12,
        // spine mid, spine base, hip right
        upper_hip_right = 13,
        // spine base, hip right, kneel right
        lower_hip_right = 14,
        // hip right, kneel right, ankle right
        leg_right = 15,
        // kneel right, ankle right, foot right
        foot_right = 16
    };

    public struct bone
    {
        public Vector3 upper_joint;
        public Vector3 mid_joint;
        public Vector3 lower_joint;
    };

    public struct angle_collection_struct
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

    public joint_collection_struct joint_collection;
    public bone[] bone_collection = new bone[16];
    public angle_collection_struct angle_collection;

    /*
     * Fucntion : update_bone_collection
     * Input    : joint_collection joint_collection
     *            bone[] bone_collection
     * update user joint position with joint collection
     */
    public void update_bone_collection()
    {
        bone_collection[0].upper_joint = joint_collection.joint_spine_shoulder;
        bone_collection[0].mid_joint   = joint_collection.joint_neck;
        bone_collection[0].lower_joint = joint_collection.joint_head;

        bone_collection[1].upper_joint = joint_collection.joint_spine_shoulder;
        bone_collection[1].mid_joint   = joint_collection.joint_shoulder_left;
        bone_collection[1].lower_joint = joint_collection.joint_elbow_left;

        bone_collection[2].upper_joint = joint_collection.joint_shoulder_left;
        bone_collection[2].mid_joint   = joint_collection.joint_elbow_left;
        bone_collection[2].lower_joint = joint_collection.joint_wrist_left;

        bone_collection[3].upper_joint = joint_collection.joint_spine_shoulder;
        bone_collection[3].mid_joint   = joint_collection.joint_shoulder_right;
        bone_collection[3].lower_joint = joint_collection.joint_elbow_right;

        bone_collection[4].upper_joint = joint_collection.joint_shoulder_right;
        bone_collection[4].mid_joint   = joint_collection.joint_elbow_right;
        bone_collection[4].lower_joint = joint_collection.joint_wrist_right;

        bone_collection[5].upper_joint = joint_collection.joint_shoulder_left;
        bone_collection[5].mid_joint   = joint_collection.joint_spine_shoulder;
        bone_collection[5].lower_joint = joint_collection.joint_shoulder_right;

        bone_collection[6].upper_joint = joint_collection.joint_spine_shoulder;
        bone_collection[6].mid_joint   = joint_collection.joint_spine_mid;
        bone_collection[6].lower_joint = joint_collection.joint_spine_base;

        bone_collection[7].upper_joint = joint_collection.joint_hip_left;
        bone_collection[7].mid_joint   = joint_collection.joint_spine_base;
        bone_collection[7].lower_joint = joint_collection.joint_hip_right;

        bone_collection[8].upper_joint = joint_collection.joint_spine_mid;
        bone_collection[8].mid_joint   = joint_collection.joint_spine_base;
        bone_collection[8].lower_joint = joint_collection.joint_hip_left;

        bone_collection[9].upper_joint = joint_collection.joint_spine_base;
        bone_collection[9].mid_joint   = joint_collection.joint_hip_left;
        bone_collection[9].lower_joint = joint_collection.joint_kneel_left;

        bone_collection[10].upper_joint = joint_collection.joint_hip_left;
        bone_collection[10].mid_joint   = joint_collection.joint_kneel_left;
        bone_collection[10].lower_joint = joint_collection.joint_ankle_left;

        bone_collection[11].upper_joint = joint_collection.joint_kneel_left;
        bone_collection[11].mid_joint   = joint_collection.joint_ankle_left;
        bone_collection[11].lower_joint = joint_collection.joint_foot_left;

        bone_collection[12].upper_joint = joint_collection.joint_spine_mid;
        bone_collection[12].mid_joint   = joint_collection.joint_spine_base;
        bone_collection[12].lower_joint = joint_collection.joint_hip_right;

        bone_collection[13].upper_joint = joint_collection.joint_spine_base;
        bone_collection[13].mid_joint   = joint_collection.joint_hip_right;
        bone_collection[13].lower_joint = joint_collection.joint_kneel_right;
        
        bone_collection[14].upper_joint = joint_collection.joint_hip_right;
        bone_collection[14].mid_joint   = joint_collection.joint_kneel_right;
        bone_collection[14].lower_joint = joint_collection.joint_ankle_right;
        
        bone_collection[15].upper_joint = joint_collection.joint_kneel_right;
        bone_collection[15].mid_joint   = joint_collection.joint_ankle_right;
        bone_collection[15].lower_joint = joint_collection.joint_foot_right;
    }


    /*
     * Fucntion : update_angle_calculate
     * Input    : angle_collection angle_collection
     *            bone[] bone_collection
     * calculate angle of bone vector in skeleton body
     */
    public void update_angle_calculate()
    {
        float[] tmp_angle = new float[16];
        for (int index = 0; index < 16; index++)
        {
            bone bone_vector = bone_collection[index];
            tmp_angle[index] = angle_calculate(bone_vector);
        }
        angle_collection.head               = tmp_angle[0];
        angle_collection.shoulder_left      = tmp_angle[1];
        angle_collection.arm_left           = tmp_angle[2];
        angle_collection.shoulder_right     = tmp_angle[3];
        angle_collection.arm_right          = tmp_angle[4];
        angle_collection.shoulder           = tmp_angle[5];
        angle_collection.body               = tmp_angle[6];
        angle_collection.buttom             = tmp_angle[7];
        angle_collection.upper_hip_left     = tmp_angle[8];
        angle_collection.lower_hip_left     = tmp_angle[9];
        angle_collection.leg_left           = tmp_angle[10];
        angle_collection.foot_left          = tmp_angle[11];
        angle_collection.upper_hip_right    = tmp_angle[12];
        angle_collection.lower_hip_right    = tmp_angle[13];
        angle_collection.leg_right          = tmp_angle[14];
        angle_collection.foot_right         = tmp_angle[15];
    }


    /*
     * Function angle_calculate
     * Input  : Vector3 upper - upper joint
     *          Vector3 mid   - mid joint
     *          Vector3 lower - lower joint
     * Output : float joint angle
     * Return value of joint's angle
     */
    public float angle_calculate(bone bone_part)
    {
        Vector3 vector_U = bone_part.upper_joint - bone_part.mid_joint;
        Vector3 vector_V = bone_part.lower_joint - bone_part.mid_joint;
        vector_U.Normalize();
        vector_V.Normalize();
        // Find cosine of angle between 2 vectors
        // float dot_product = Vector3.Dot(vector_U, vector_V);
        // return Mathf.Acos(dot_product) / Mathf.PI * 180;
        return Vector3.Angle(vector_U, vector_V);
    }

}
