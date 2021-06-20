using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garde : Criminal
{

    public AudioClip m_SoundAlert;
    // Start is called before the first frame update
    void Start()
    {
        m_NamePlayer = "Garde";
    }

    public override bool Placement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            if (hit.transform != null && hit.transform.GetComponent<Node>() != null)
            {

                if(FindObjectOfType<Pirate>().ContainsNode(hit.transform.GetComponent<Node>() ))
                {
                    m_IsPlacedOnGraph = true;
                    PlaceOnNode(hit.transform.GetComponent<Node>());
                    return true;
                }
                else
                {
                    GameplayManager.m_Instance.m_AudioSource.PlayOneShot(m_ErrorSound);
                }
            }
        return false;
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
                    if(m_ActualNode != hit.transform.GetComponent<Node>())
                    {
                        GameplayManager.m_Instance.m_IsAnimationPlaying = true;
                        yield return MoveToOtherNode(hit.transform.GetComponent<Node>());
                        GameplayManager.m_Instance.m_IsAnimationPlaying = false;
                        
                        Node[] nodes = AvailableNodesForMovement();
                        foreach(Node node in nodes)
                        {
                            if(FindObjectOfType<BountyHunter>().m_ActualNode == node)
                            {
                                GameplayManager.m_Instance.m_AudioSource.PlayOneShot(m_SoundAlert);
                            }
                        }
                        GameplayManager.m_Instance.ChangeCharacter();
                        GameplayManager.m_Instance.DisplayCanvasPlayer();
                    }
                }
            }
        yield return null;
    }



    /**public override void TurnPlayer()
     {
         RaycastHit hit;
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if (Physics.Raycast(ray, out hit))
             if (hit.transform != null && hit.transform.GetComponent<Node>() != null)
             {
                 if(ContainsNode(hit.transform.GetComponent<Node>()))
                 {
                     if(m_ActualNode == hit.transform.GetComponent<Node>())
                     {
                         Debug.Log("Same node : search for player");
                     }
                     else
                     {
                         MoveToOtherNode(hit.transform.GetComponent<Node>());
                     }
                     GameplayManager.m_Instance.ChangeCharacter();
                     GameplayManager.m_Instance.DisplayCanvasPlayer();
                     //DisplayCanvasPlayer();
                 }
             }
     }*/
    

   /**public override void TurnPlayer()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            if (hit.transform != null && hit.transform.GetComponent<Node>() != null)
            {
                if(ContainsNode(hit.transform.GetComponent<Node>()))
                {
                    if(m_ActualNode == hit.transform.GetComponent<Node>())
                    {
                        Debug.Log("Same node : search for player");
                    }
                    else
                    {
                        MoveToOtherNode(hit.transform.GetComponent<Node>());
                    }
                    GameplayManager.m_Instance.ChangeCharacter();
                    GameplayManager.m_Instance.DisplayCanvasPlayer();
                    //DisplayCanvasPlayer();
                }
            }
    }*/
}
