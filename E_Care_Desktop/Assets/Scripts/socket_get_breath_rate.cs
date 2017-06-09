using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class socket_get_breath_rate : MonoBehaviour {
    public UnityEngine.UI.Text breath_rate_text;
    public UnityEngine.UI.Text connection_status;


    public UnityEngine.UI.Extensions.UILineRenderer line_renderer;


    [SerializeField] float graph_width = 360;
    [SerializeField] float graph_height = 250;

    private float prefix_width;
    private float prefix_height;

    public GameObject socket_controller;
    private SocketIOComponent socket;

    private string data = "ee";
    private string tmp_data = "";
    private bool is_connected = false;
    private int counter = 0;
    // Use this for initialization
    void Start()
    {
        if (socket_controller)
        {
            socket = socket_controller.GetComponent<SocketIOComponent>();
            socket.On("breartrate", socket_breath_rate);
            StartCoroutine("timer_counter");
        }

        prefix_width = graph_width / 50;
        prefix_height = graph_height / 34;
    }


    private void socket_breath_rate(SocketIOEvent e)
    {
        if (is_connected)
        {
            tmp_data = e.data["value"].ToString();
            breath_rate_text.text = tmp_data;
            connection_status.text = "";
            add_point_new(e.data["value"].ToString());
        }
        else
        {
            //connection_status.text = "CONNECTION LOST!!!";
        }
        Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
    }



    private void add_point_new(string data)
    {
        List<Vector2> point_list = new List<Vector2>(line_renderer.Points);
        if (point_list.Count > 50)
        {
            point_list.Clear();
        }
        Vector2 point = new Vector2((point_list.Count + 1) * prefix_width, (float.Parse(data) - 10) * prefix_height);
        point_list.Add(point);
        line_renderer.Points = point_list.ToArray();
    }

    private IEnumerator timer_counter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (data.Equals(tmp_data) == false)
            {
                data = tmp_data;
                counter = 0;
            }
            else
            {
                counter++;
            }
            if (counter > 60)
            {
                is_connected = false;
            }
            else
            {
                is_connected = true;
            }
        }
    }
    
}
