using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BountyHunter : Pawn
{

    void Start()
    {
        m_NamePlayer = "Voleur";
    }

    public override IEnumerator TurnPlayer()
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
                        if(FindObjectOfType<Treasure>().m_ActualNode == m_ActualNode)
                        {
                            yield return FindObjectOfType<Treasure>().OpenChestVictory();
                        }
                        else
                        {
                            GameplayManager.m_Instance.ChangeCharacter();
                            GameplayManager.m_Instance.DisplayCanvasPlayer();
                        }
                    }
                    else
                    {
                        if(hit.transform.GetComponent<Node>() == FindObjectOfType<Garde>().m_ActualNode)
                        {
                            GameplayManager.m_Instance.m_AudioSource.PlayOneShot(m_ErrorSound);
                            //play sound error
                        }
                        else
                        {
                            Edge edge = m_ActualNode.GetEdgeConnectedTo(hit.transform.GetComponent<Node>());
                            Debug.Log("hit node around");
                            if(edge.IsTrapOn())
                                edge.m_Trap.PlayerPassThrought();
                            GameplayManager.m_Instance.m_IsAnimationPlaying = true;
                            yield return MoveToOtherNode(hit.transform.GetComponent<Node>());
                            GameplayManager.m_Instance.m_IsAnimationPlaying = false;

                            GameplayManager.m_Instance.ChangeCharacter();
                            GameplayManager.m_Instance.DisplayCanvasPlayer();
                        }
                    }
                    
                }
            }

        yield return null;
    }
    
}
