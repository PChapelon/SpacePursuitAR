using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{

    public Controls m_Controls;

    public Pawn m_CurrentPlayer;

    private Pawn m_Pirate;
    private Pawn m_BountyHunter;
    private Pawn m_Garde;

    [Header("CanvasTurn")]
    public Text m_PlayerNameTurn;
    public GameObject m_CanvasNextTurn;

    public GameObject m_CanvasPlacement;
    public Text m_PlayerPlacement;

    private float m_HoldingTime;

    public float m_DistanceCamera = 10.0f;

    public static GameplayManager m_Instance;

    private List<Pawn> m_Pawns = new List<Pawn>();
    public Camera m_MainCamera;
    public bool m_ARMode = true;

    public AudioSource m_AudioSource;

    public bool m_PlacementPhase = true;

    private IEnumerator m_HoldCoroutine ;

    public bool m_IsAnimationPlaying;

    void Awake()
    {
        m_Controls = new Controls();

        m_Controls.Default.Click.performed += ctx => MovementPlayer(); 

        m_Controls.NextTurn.SkipTurn.started += ctx => InitNextTurn(); 
        m_Controls.NextTurn.SkipTurn.canceled += ctx => NextTurn(); 

        m_Controls.Init.PlaceCharacter.performed += ctx => StartCoroutine(PlaceCharacter());

        m_Controls.NextTurn.Disable();
        m_Controls.Default.Disable();
        m_Controls.Init.Enable();

        m_AudioSource = GetComponent<AudioSource>();

        m_Instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {

        m_BountyHunter = FindObjectOfType<BountyHunter>();
        m_Pirate = FindObjectOfType<Pirate>();
        m_Garde = FindObjectOfType<Garde>();

        m_Pawns.Add(m_BountyHunter);
        m_Pawns.Add(m_Pirate);
        m_Pawns.Add(m_Garde);


        m_CurrentPlayer = m_BountyHunter;
        if(!m_ARMode)
            m_MainCamera.transform.position = new Vector3(0.0f, 0.0f, -m_DistanceCamera);
        m_HoldCoroutine = HoldingTime();
        DisplayCanvasPlacement();
        GameObject.Find("TextPlayer").GetComponent<Text>().text = m_CurrentPlayer.m_NamePlayer;

    }



    public void MovementPlayer()
    {
        
        if(FindObjectOfType<BountyHunter>().m_IsPlacedOnGraph && FindObjectOfType<Pirate>().m_IsPlacedOnGraph && FindObjectOfType<Garde>().m_IsPlacedOnGraph && !m_IsAnimationPlaying)
            if(m_CurrentPlayer.m_IsPlacedOnGraph)
                StartCoroutine(m_CurrentPlayer.TurnPlayer());
        
    }

    public IEnumerator PlaceCharacter()
    {

        Debug.Log("placement action");
        if(m_CurrentPlayer.Placement())
        {
            m_Controls.Init.Disable();
            yield return new WaitForSeconds(2.0f);
            ChangeCharacter();
            if(m_CurrentPlayer.GetComponent<Criminal>() != null)
            {
                DisplayCanvasPlacement();
            }

            //END OF PLACEMENT PHASE
            if(m_CurrentPlayer == m_BountyHunter)
            {

                m_PlacementPhase = false;
                m_Controls.Init.Disable();
                DisplayCanvasPlayer();
            }
            else
            {
                m_Controls.Init.Enable();

            }

        }
        else
        {
            if(m_CurrentPlayer.GetComponent<Pirate>() != null && !m_CurrentPlayer.GetComponent<Pirate>().m_Trap.m_IsPlaced && m_CurrentPlayer.GetComponent<Pirate>().m_Treasure.m_IsPlaced )
            {

                foreach(Edge e in FindObjectsOfType<Edge>())
                {
                    //e.GetComponent<BoxCollider>().isTrigger = false;
                    e.gameObject.GetComponent<BoxCollider>().enabled = true;
                    
                    e.EnablePortalContruct();

                }
            }
        }
        yield return null;
        
    }

    public void DisplayCanvasPlacement()
    {
        
        m_CanvasPlacement.SetActive(true);
        m_PlayerPlacement.text = "Placer le " + m_CurrentPlayer.m_NamePlayer;

        m_Controls.Init.Disable();
        m_Controls.NextTurn.Enable();

    }

    public void CloseCanvasPlacement()
    {
        m_CanvasPlacement.SetActive(false);
        m_Controls.Init.Enable();
    }

    public void DisplayCanvasPlayer()
    {
        m_Controls.Default.Disable();
        m_PlayerNameTurn.text = "Au tour du " + m_CurrentPlayer.m_NamePlayer;
        
        m_CanvasNextTurn.SetActive( true);
        m_Controls.NextTurn.Enable();
    }

    public void CloseCanvasPlayer()
    {
       
        m_CanvasNextTurn.SetActive(false);
        m_Controls.Default.Enable();
    }

    private void InitNextTurn()
    {
        if(m_HoldingTime <= 0.0f)
            StartCoroutine(m_HoldCoroutine);
    }


    private IEnumerator HoldingTime()
    {
        m_HoldingTime = 0.0f;
        //Debug.Log("holding time");
        while(true)
        {
            m_HoldingTime += Time.deltaTime;
            //Debug.Log("player is holding");

            
            if(m_PlacementPhase)
                m_SliderPlacement.fillAmount = Mathf.Clamp(m_HoldingTime / m_TIMER_MAX_HOLD, 0.0f, 1.0f);
            else
                m_SliderTurn.fillAmount = Mathf.Clamp(m_HoldingTime / m_TIMER_MAX_HOLD, 0.0f, 1.0f);
            yield return new WaitForEndOfFrame();
        }
    }

    public float m_TIMER_MAX_HOLD = 1.0f;
    public Image m_SliderPlacement;
    public Image m_SliderTurn;
    private void NextTurn()
    {
        //Debug.Log("hello");
        StopCoroutine(m_HoldCoroutine);
        if(m_HoldingTime > m_TIMER_MAX_HOLD)
        {

            if(m_PlacementPhase)
                CloseCanvasPlacement();
            else
                CloseCanvasPlayer();


        }
        

        m_HoldingTime = 0.0f;
        m_SliderTurn.fillAmount = 0.0f;
        m_SliderPlacement.fillAmount = 0.0f;
    }

    public void ChangeCharacter()
    {
        int indexNext = -1 ; 
        for(int i = 0; i < m_Pawns.Count; i ++)
        {
            if(m_Pawns[i] == m_CurrentPlayer)
            {
                indexNext = (i + 1) % 3;
                break;
            }
        }
        m_CurrentPlayer = m_Pawns[indexNext];
        if(!m_ARMode)
        {
            m_MainCamera.transform.position = new Vector3(m_CurrentPlayer.transform.position.x,m_CurrentPlayer.transform.position.y, m_CurrentPlayer.transform.position.z -m_DistanceCamera);
        }
        GameObject.Find("TextPlayer").GetComponent<Text>().text = m_CurrentPlayer.m_NamePlayer;
        m_MainCamera.cullingMask = m_CurrentPlayer.m_CameraLayer;
    }

    public void ChangeCharacterButton()
    {
        int indexNext = -1 ; 
        for(int i = 0; i < m_Pawns.Count; i ++)
        {
            if(m_Pawns[i] == m_CurrentPlayer)
            {
                indexNext = (i + 1) % 3;
                break;
            }
        }
        m_CurrentPlayer = m_Pawns[indexNext];
        if(!m_ARMode)
            m_MainCamera.transform.position = new Vector3(m_CurrentPlayer.transform.position.x,m_CurrentPlayer.transform.position.y, m_CurrentPlayer.transform.position.z -m_DistanceCamera);
        m_MainCamera.cullingMask = m_CurrentPlayer.m_CameraLayer;
        if(m_CurrentPlayer == FindObjectOfType<BountyHunter>() || m_CurrentPlayer == FindObjectOfType<Pirate>())
            DisplayCanvasPlayer();
    }


    private void OnEnable()
    {
        m_Controls.Enable();
    }

    private void OnDisable()
    {
        m_Controls.Disable();
    }
}
