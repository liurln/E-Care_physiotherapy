using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class socket_get_heart_rate : MonoBehaviour {
    public UnityEngine.UI.Text heart_rate_text;
    
    public UnityEngine.UI.Extensions.UILineRenderer line_renderer;

    [SerializeField] float graph_width = 360;
    [SerializeField] float graph_height = 250;

    private float prefix_width;
    private float prefix_height;

    public GameObject socket_controller;
    private SocketIOComponent socket;
    // Use this for initialization
    void Start () {
        if (socket_controller)
        {
            socket = socket_controller.GetComponent<SocketIOComponent>();
            socket.On("heartrate", socket_heart_rate);
        }

        prefix_width = graph_width / 70;
        prefix_height = graph_height / 125;

    }

    public void socket_heart_rate(SocketIOEvent e)
    {
        heart_rate_text.text = e.data["value"].ToString();
        add_point_new(e.data["value"].ToString());
        // Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data["value"]);
    }



    private void add_point_new(string data)
    {
        List<Vector2> point_list = new List<Vector2>(line_renderer.Points);
        if (point_list.Count > 50)
        {
            point_list.Clear();
        }
        Vector2 point = new Vector2((point_list.Count + 1) * prefix_width, (float.Parse(data) - 50) * prefix_height);
        point_list.Add(point);
        line_renderer.Points = point_list.ToArray();
    }
}
