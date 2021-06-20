using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    public int m_NumberEdges;

    public float m_DistanceNoSpawn;
    public int m_Index;

    public List<Edge> m_Edges;

    public GameObject m_ButtonMovement;

    public GameObject m_Models;


    // Start is called before the first frame update
    void Awake()
    {
        m_Edges = new List<Edge>();
    }

    void Start()
    {
        int index = Random.Range(0, m_Models.transform.childCount);
        m_Models.transform.GetChild(index).gameObject.SetActive(true);
    }

    public void EnableButton()
    {
        m_ButtonMovement.SetActive(true);
    }

    public void DisableButton()
    {
        m_ButtonMovement.SetActive(false);
    }

    public void MoveToNode()
    {
        //FindObjectOfType<BountyHunter>().transform.position = transform.position;
    }

    public bool IsDirectlyConnectedTo(Node node)
    {
        foreach(Edge edge in m_Edges)
        {
            if(edge.m_Left == node || edge.m_Right == node)
            {
                return true;
            }
        }
        return false;
    }

    public Edge GetEdgeConnectedTo(Node node)
    {
        foreach(Edge edge in m_Edges)
        {
            if(edge.m_Left == node || edge.m_Right == node)
            {
                return edge;
            }
        }
        return null;
    }

}
