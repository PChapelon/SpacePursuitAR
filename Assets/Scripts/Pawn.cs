using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Node m_ActualNode;
    public string m_NamePlayer = "Unknown";

    public bool m_IsPlacedOnGraph = false;

    public float m_SpeedAnimationEdge = 3.0f;

    public GameObject m_Model;

    public LayerMask m_TargetRaycast;
    public LayerMask m_CameraLayer;

    public AudioClip m_ErrorSound;
    
    

    // Start is called before the first frame update
    void Start()
    {

    }


    public virtual Pawn SpawnPlayer()
    {
        Node[] nodes = FindObjectsOfType<Node>();
        int i = Random.Range(0, nodes.Length);
        m_ActualNode = nodes[i];
        transform.position = m_ActualNode.transform.position;
        return this;

    }



    public virtual Node[] AvailableNodesForMovement()
    {
        Node[] arrayNodes = new Node[m_ActualNode.m_Edges.Count + 1];
        for (int i = 0; i < m_ActualNode.m_Edges.Count; i++)
        {
            Edge edge = m_ActualNode.m_Edges[i];
            if (edge.m_Left != m_ActualNode)
            {
                arrayNodes[i] = edge.m_Left;
            }
            else
            {
                arrayNodes[i] = edge.m_Right;
            }
        }
        arrayNodes[m_ActualNode.m_Edges.Count ] = m_ActualNode;
        return arrayNodes;
    }

    public virtual bool ContainsNode(Node n)
    {
        Node[] arrayNodes = AvailableNodesForMovement();
        foreach (Node node in arrayNodes)
        {
            if (node == n)
            {
                return true;
            }
        }
        return false;
    }
/*
    public virtual bool ContainsNodePawnNotPlaced(Node n)
    {
        Node[] arrayNodes = AvailableNodesForMovement();
        foreach (Node node in arrayNodes)
        {
            if (node == n)
            {
                return true;
            }
        }
        return false;
    }*/

    public virtual bool ContainsEdge(Edge e)
    {
        return m_ActualNode.m_Edges.Contains(e);
    }

    public virtual IEnumerator MoveToOtherNode(Node node)
    {
        //m_Model.SetActive(false);
        
        m_Model.transform.parent.GetComponent<Rotatator>().enabled = false;
        m_Model.transform.localRotation = Quaternion.identity;

        yield return StartCoroutine(m_ActualNode.GetEdgeConnectedTo(node).AnimationTransition(m_ActualNode.transform.position, node.transform.position,2.0f, m_Model));
        transform.position = node.transform.position;
        m_ActualNode = node;
        
        m_Model.transform.parent.position = node.transform.position;
        m_Model.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
        m_Model.transform.localRotation = Quaternion.identity;
        m_Model.transform.parent.GetComponent<Rotatator>().enabled = true;


        yield return new WaitForSeconds(0.5f);

    }

    public virtual void PlaceOnNode(Node node)
    {
        transform.position = node.transform.position;
        m_ActualNode = node;
    }

    public virtual IEnumerator TurnPlayer()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,m_TargetRaycast))
            if (hit.transform != null && hit.transform.GetComponent<Node>() != null)
            {
                if(ContainsNode(hit.transform.GetComponent<Node>()))
                {
                    if(m_ActualNode == hit.transform.GetComponent<Node>())
                    {
                        Debug.Log("Same node : search for player");
                        yield return null;
                    }
                    else
                    {
                        yield return MoveToOtherNode(hit.transform.GetComponent<Node>());
                    }
                    GameplayManager.m_Instance.ChangeCharacter();
                    GameplayManager.m_Instance.DisplayCanvasPlayer();
                }
            }
        yield return null;
    }

    public virtual bool Placement()
    {
        Debug.Log("Placement");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            if (hit.transform != null && hit.transform.GetComponent<Node>() != null)
            {
                PlaceOnNode(hit.transform.GetComponent<Node>());
                Debug.Log("Hit node");
                m_IsPlacedOnGraph = true;
                return true;
            }
        return false;
    }
}

