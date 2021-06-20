using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    
    public Node m_ActualNode;

    public bool m_IsPlaced = false;

    public Animation m_CrateAnimator;

    public LayerMask m_MaskEverything;


    public void PlaceTreasure(Node node)
    {
        transform.position = node.transform.position;
        m_ActualNode = node;
        m_IsPlaced = true;
        
    }

    public IEnumerator OpenChestVictory()
    {
        m_CrateAnimator.Play();
        GameplayManager.m_Instance.m_MainCamera.cullingMask = m_MaskEverything;
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("GameOverBountyHunter");
        
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
}
