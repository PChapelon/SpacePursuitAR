using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{

    public Node m_Right;
    public Node m_Left;

    public Trap m_Trap;

    public GameObject m_SpaceShip;

    public GameObject m_PortalContruct;

    // Start is called before the first frame update
    void Awake()
    {
        m_Left = null;
        m_Right = null;
        m_Trap = null;
    }

    public bool IsTrapOn()
    {
        if(m_Trap != null)
            return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AnimationTransition(Vector3 begin, Vector3 end, float timer, GameObject spaceship)
    {
        spaceship.transform.position = begin;
        
        spaceship.transform.rotation = Quaternion.LookRotation(end - begin);
        float animation = 0.0f;
        while(timer > animation)
        {
            spaceship.transform.position = Vector3.Lerp(begin, end, animation / timer);
            animation += Time.deltaTime;
            yield return null;
        }
        Debug.Log("end animation");
        spaceship.transform.position = end;
        
        
        yield return null;

    }

    public IEnumerator AnimationTransition(Vector3 begin, Vector3 end, GameObject spaceship)
    {
        spaceship.transform.position = begin;
        float duration = Vector3.Distance(GetComponent<LineRenderer>().GetPosition(0), GetComponent<LineRenderer>().GetPosition(1)) * 0.5f;
        
        spaceship.transform.rotation = Quaternion.LookRotation(end - begin);
        float animation = 0.0f;
        while(duration > animation)
        {
            spaceship.transform.position = Vector3.Lerp(begin, end, animation / duration);
            animation += Time.deltaTime;
            yield return null;
        }
        Debug.Log("end animation");
        spaceship.transform.position = end;
        
        //m_SpaceShip.gameObject.SetActive(false);
        
        yield return null;

    }

    public void DisablePortalContruct()
    {
        m_PortalContruct.SetActive(false);
    }

    public void EnablePortalContruct()
    {
        m_PortalContruct.SetActive(true);
    }

}

