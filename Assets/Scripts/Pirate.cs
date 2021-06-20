using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pirate : Criminal
{

    public Treasure m_Treasure;
    public Trap m_Trap;

    public LayerMask m_RaycastEdge;
   void Start()
    {
        m_NamePlayer = "Policier";
    }

    public override bool Placement()
    {
        if(!m_Treasure.m_IsPlaced)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity,m_TargetRaycast))
                if (hit.transform != null && hit.transform.GetComponent<Node>() != null)
                {
                    
                    m_Treasure.PlaceTreasure(hit.transform.GetComponent<Node>());
                }
            
            return false;
        }
        else if(!m_Trap.m_IsPlaced)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_RaycastEdge))
                if (hit.transform != null && hit.transform.GetComponent<Edge>() != null)
                {
                    m_Trap.PlaceTrap(hit.transform.GetComponent<Edge>());
                }
            return false;
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.transform != null && hit.transform.GetComponent<Node>() != null)
                {
                    if(FindObjectOfType<Treasure>().ContainsNode(hit.transform.GetComponent<Node>() ))
                    {
                        PlaceOnNode(hit.transform.GetComponent<Node>());
                        m_IsPlacedOnGraph = true;
                        return true;
                    }
                    else
                    {
                        GameplayManager.m_Instance.m_AudioSource.PlayOneShot(m_ErrorSound);

                    }
                }
            return false;
        }
    }

    public override IEnumerator TurnPlayer()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_TargetRaycast))
            if (hit.transform != null && hit.transform.GetComponent<Node>() != null)
            {
                if(m_ActualNode == hit.transform.GetComponent<Node>())
                {
                    if(FindObjectOfType<BountyHunter>().m_ActualNode == m_ActualNode)
                        SceneManager.LoadScene("GameOverPirate");
                    GameplayManager.m_Instance.ChangeCharacter();
                    yield return null;
                }
                else
                if(ContainsNode(hit.transform.GetComponent<Node>()))
                {
                    GameplayManager.m_Instance.m_IsAnimationPlaying = true;
                    yield return MoveToOtherNode(hit.transform.GetComponent<Node>());
                    GameplayManager.m_Instance.m_IsAnimationPlaying = false;
                    GameplayManager.m_Instance.ChangeCharacter();

                }
                    
                
            }

        yield return null;
    }


}
