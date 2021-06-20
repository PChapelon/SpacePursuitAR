using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager m_Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(m_Instance != null)
        {
            Destroy(this);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    

}
