using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_layer{

	public static void change_layer_all(Transform root_object ,int layer_index)
    {
        root_object.gameObject.layer = layer_index;
        foreach(Transform child_object in root_object)
        {
            change_layer_all(child_object, layer_index);
        }
    }
}
