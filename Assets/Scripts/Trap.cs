using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Edge m_ActualEdge;
    public bool m_IsPlaced = false;

    public AudioClip m_Sound;

    public void PlaceTrap(Edge edge)
    {
        transform.position = edge.transform.position;
        transform.rotation = Quaternion.LookRotation(edge.m_Left.transform.position - transform.position);
        m_ActualEdge = edge;
        edge.m_Trap = this;
        m_IsPlaced = true;
        foreach(Edge e in FindObjectsOfType<Edge>())
        {
            //e.gameObject.GetComponent<BoxCollider>().isTrigger = true;                              
            e.gameObject.GetComponent<BoxCollider>().enabled = false;
            e.DisablePortalContruct();

        }

    }

    public void PlayerPassThrought()
    {
        Debug.Log("ALARM");
        GameplayManager.m_Instance.m_AudioSource.PlayOneShot(m_Sound);
    }

    
}
