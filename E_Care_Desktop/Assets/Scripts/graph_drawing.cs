using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graph_drawing : MonoBehaviour {

    public UnityEngine.UI.Extensions.UILineRenderer line_renderer;
    


    private void add_point_new(string data)
    {
        List<Vector2> point_list = new List<Vector2>(line_renderer.Points);
        if(point_list.Count > 60)
        {
            List<Vector2> point_list_new = new List<Vector2>(60);
            point_list.RemoveAt(0);
            foreach ( Vector2 point in point_list)
            {
                point_list_new.Add(new Vector2((point.x - 20), point.y));
            }
            point_list_new.Add(new Vector2(1200, float.Parse(data)));
            line_renderer.Points = point_list_new.ToArray();
        }
        else
        {
            Vector2 point = new Vector2((point_list.Count + 1) * 20 , float.Parse(data));
            point_list.Add(point);
            line_renderer.Points = point_list.ToArray();
        }
    }
}

