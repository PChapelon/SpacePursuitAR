using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGenerator : MonoBehaviour
{
    [Header("Random Parameters")]
    public Vector3 m_MinVector;
    public Vector3 m_MaxVector;

    [Header("Graph Parameters")]
    public int m_NumberNodes = 5;
    public int m_MinimalEdgesPerNode;
    public int m_MaximalEdgesPerNode;
    public float m_Probability = 0.3f;
    public float m_DistanceConnection = 5.0f;


    public GameObject m_NodePrefab;

    public GameObject m_EdgePrefab;


    public int[,] m_MatrixAdj;

    public List<Node> m_ListNodes;
    public void GenerateRandomMatrice(float prob)
    {
        for (int i = 0; i < m_NumberNodes; i++)
            for (int j = 0; j < m_NumberNodes; j++)
            {
                float probability = Random.Range(0.0f, 1.0f);
                int numberAffectation = 0;
                if (numberAffectation > m_MaximalEdgesPerNode)
                {
                    break;
                }
                //if(Vector3.Distance(GetNodeFromIndex(i).transform.position, GetNodeFromIndex(j).transform.position) < Vector3.Distance(m_MinVector, m_MaxVector) * (1.5f / 5.0f) && GetNumberEdgesFrom(j) < m_MaximalEdgesPerNode)
                if (Vector2.Distance(new Vector2(GetNodeFromIndex(i).transform.position.x, GetNodeFromIndex(i).transform.position.z), new Vector2(GetNodeFromIndex(j).transform.position.x, GetNodeFromIndex(j).transform.position.z)) < Vector2.Distance(new Vector2(m_MinVector.x, m_MinVector.z), new Vector2(m_MaxVector.x, m_MaxVector.z)) * (1.5f / 5.0f) && GetNumberEdgesFrom(j) < m_MaximalEdgesPerNode)
                {
                    m_MatrixAdj[i, j] = 1;
                    m_MatrixAdj[j, i] = 1;
                    numberAffectation++;
                }
            }

        if (m_ListNodes.Count > 1)
        {
            for (int i = 0; i < m_NumberNodes; i++)
            {
                int k = 0;
                for (int j = 0; j < m_NumberNodes; j++)
                {
                    k += m_MatrixAdj[i, j];
                }

                if (k == 0)
                {
                    Node nearestNode = GetNearestNodeFrom(GetNodeFromIndex(i));
                    Debug.Log("alone  :  " + GetNodeFromIndex(i).m_Index + "      nearest " + nearestNode.m_Index);
                    m_MatrixAdj[i, nearestNode.m_Index] = 1;
                    m_MatrixAdj[nearestNode.m_Index, i] = 1;
                }
            }

        }
    }


    public int GetNumberEdgesFrom(int indexNode)
    {
        int affectations = 0;
        for (int i = 0; i < m_NumberNodes; i++)
        {
            if (m_MatrixAdj[i, indexNode] != 0)
            {
                affectations++;
            }
        }
        return affectations;
    }

    public Node GetNodeFromIndex(int index)
    {
        return m_ListNodes[index];
    }

    public Node GetNearestNodeFrom(Node n)
    {
        float minDistance = Mathf.Infinity;
        int indexNode = -1;
        if (m_ListNodes.Count > 1)
        {
            foreach (Node node in m_ListNodes)
            {
                if (Vector3.Distance(node.transform.position, n.transform.position) < minDistance && node != n)
                {
                    minDistance = Vector3.Distance(node.transform.position, n.transform.position);
                    indexNode = node.m_Index;
                }
            }
            return GetNodeFromIndex(indexNode);

        }
        else
        {
            return null;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        m_DistanceConnection = (Mathf.Abs(m_MinVector.x) + Mathf.Abs(m_MaxVector.x)) / 2.0f;

        m_ListNodes = new List<Node>();
        m_MatrixAdj = new int[m_NumberNodes, m_NumberNodes];

        for (int i = 0; i < m_NumberNodes; i++)
            for (int j = 0; j < m_NumberNodes; j++)
                m_MatrixAdj[i, j] = 0;

        for (int i = 0; i < m_NumberNodes; i++)
        {
            bool foundPlace = true;
            int maxNumberTry = 5;
            int iteration = 0;
            Vector3 temporaryPosition;
            while (foundPlace)
            {
                temporaryPosition = Tools.Random(m_MinVector, m_MaxVector);
                if (iteration < maxNumberTry)
                    foreach (Node node in m_ListNodes)
                    {
                        if (!Tools.IsInsideCircle(temporaryPosition, node.transform.position, node.m_DistanceNoSpawn))
                        {
                            foundPlace = false;
                        }
                        else
                        {
                            foundPlace = true;
                            break;
                        }
                    }
                else
                    foundPlace = false;
                iteration++;
            }
            GameObject go = Instantiate(m_NodePrefab, Tools.Random(m_MinVector, m_MaxVector), Quaternion.identity);
            go.transform.parent = transform;
            go.GetComponent<Node>().m_Index = i;

            m_ListNodes.Add(go.GetComponent<Node>());
        }

        GenerateRandomMatrice(m_Probability);


        foreach (Node node in m_ListNodes)
        {

            for (int j = 0; j < m_NumberNodes; j++)
            {
                node.m_NumberEdges += m_MatrixAdj[node.m_Index, j];
            }
        }

        for (int i = 0; i < m_NumberNodes; i++)
            for (int j = 0; j < m_NumberNodes; j++)
                if (i != j && j >= i && m_MatrixAdj[i, j] == 1)
                {
                    GameObject go = Instantiate(m_EdgePrefab, (GetNodeFromIndex(i).transform.position + GetNodeFromIndex(j).transform.position) / 2, Quaternion.identity);
                    go.transform.parent = transform;
                    go.GetComponent<Edge>().m_Left = GetNodeFromIndex(i);
                    go.GetComponent<Edge>().m_Right = GetNodeFromIndex(j);

                    go.GetComponent<LineRenderer>().SetPosition(0, go.GetComponent<Edge>().m_Left.transform.position);
                    go.GetComponent<LineRenderer>().SetPosition(1, go.GetComponent<Edge>().m_Right.transform.position);

                    go.GetComponent<Edge>().m_Left.m_Edges.Add(go.GetComponent<Edge>());
                    go.GetComponent<Edge>().m_Right.m_Edges.Add(go.GetComponent<Edge>());
                    
                    go.transform.rotation = Quaternion.LookRotation(go.GetComponent<Edge>().m_Left.transform.position - go.transform.position);
                    
                    //go.transform.RotateAround(new Vector3(0,1,0), 90.0f);
                    //m_Graph.GetNodeFromIndex(i).m_Edges.Add(go.GetComponent<Edge>());
                    //m_Graph.GetNodeFromIndex(j).m_Edges.Add(go.GetComponent<Edge>());

                }



        

        CheckNodes();
        //DisplayMatrice();
    }

    private void DisplayMatrice()
    {
        string str = "";

        for (int i = 0; i < m_NumberNodes; i++)
        {
            str = "";
            for (int j = 0; j < m_NumberNodes; j++)
            {
                str += m_MatrixAdj[i, j] + " ";
            }
            Debug.Log(str);
        }
    }

    public void CheckNodes()
    {
        for(int i = 0; i < m_ListNodes.Count; i ++)
        {
            if(m_ListNodes[i].m_Edges.Count == 1)
            {
                List<Node> nearestNodes = GetNearestNodes(1, m_ListNodes[i]);

                foreach(Node n in nearestNodes)
                {
                    GameObject go = Instantiate(m_EdgePrefab, (m_ListNodes[i].transform.position + n.transform.position) / 2, Quaternion.identity);
                    go.transform.parent = transform;
                    go.GetComponent<Edge>().m_Left = m_ListNodes[i];
                    go.GetComponent<Edge>().m_Right = n;

                    go.GetComponent<LineRenderer>().SetPosition(0, go.GetComponent<Edge>().m_Left.transform.position);
                    go.GetComponent<LineRenderer>().SetPosition(1, go.GetComponent<Edge>().m_Right.transform.position);

                    go.GetComponent<Edge>().m_Left.m_Edges.Add(go.GetComponent<Edge>());
                    go.GetComponent<Edge>().m_Right.m_Edges.Add(go.GetComponent<Edge>());
                }
                Debug.Log("node one linked " +  m_ListNodes[i].m_Index);
                
            }
            else if(m_ListNodes[i].m_Edges.Count == 0)
            {
                List<Node> nearestNodes = GetNearestNodes(2, m_ListNodes[i]);

                foreach(Node n in nearestNodes)
                {
                    GameObject go = Instantiate(m_EdgePrefab, (m_ListNodes[i].transform.position + n.transform.position) / 2, Quaternion.identity);
                    go.transform.parent = transform;
                    go.GetComponent<Edge>().m_Left = m_ListNodes[i];
                    go.GetComponent<Edge>().m_Right = n;

                    go.GetComponent<LineRenderer>().SetPosition(0, go.GetComponent<Edge>().m_Left.transform.position);
                    go.GetComponent<LineRenderer>().SetPosition(1, go.GetComponent<Edge>().m_Right.transform.position);

                    go.GetComponent<Edge>().m_Left.m_Edges.Add(go.GetComponent<Edge>());
                    go.GetComponent<Edge>().m_Right.m_Edges.Add(go.GetComponent<Edge>());
                }
                Debug.Log("node alone " +  m_ListNodes[i].m_Index);

            }
        }
    }
    

    List<Node> GetNearestNodes(int numberNodes, Node node)
    {
        List<Node> nodes = new List<Node>(m_ListNodes); 
        nodes.Remove(node);
        List<Node> result = new List<Node>();

        for(int i = 0 ; i < numberNodes; i ++)
        {
            float distance = Mathf.Infinity;
            int indexNode = -1;
            for(int j = 0; j < nodes.Count; j++)
            {
                if(!nodes[j].IsDirectlyConnectedTo(node))
                {
                    if(distance > Vector3.Distance(nodes[j].transform.position,node.transform.position))
                    {
                        distance = Vector3.Distance(nodes[j].transform.position,node.transform.position);
                        indexNode = j;
                    }
                }
            }
            result.Add(nodes[indexNode]);
            nodes.Remove(nodes[indexNode]);
        }
        return result;
    }

}



