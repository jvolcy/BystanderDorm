using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampusSceneManager : MonoBehaviour
{

    [SerializeField] CanvasQuad BeginSimulation;
    [SerializeField] EventTrigger MelaninHallTrigger;
    [SerializeField] Transform BellesHallEntry;
    [SerializeField] Transform MelaninHallEntry;
    [SerializeField] Transform MelaninToCampusPlayerPosition;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //put us in front of our room, facing the door
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(MelaninHallEntry);
        }
    }


    public void LoadScene(string sceneName)
    {

        if (sceneName == "MelaninHall")
        {
            Debug.Log("CampusSceneManager:LoadScene()...Visited MelaninHall set to true.");
            GameManager.visitedMelaninHall = true;
        }

        GameManager.LoadScene(sceneName);       //load the requested scene
    }


    private void OnDestroy()
    {
        Debug.Log("CampusSceneManager: Unsubscribing to OnSceneLoaded...");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("CampusSceneManager:OnSceneLoaded()...");

        if (GameManager.visitedMelaninHall == true)
        {
            Debug.Log("CampusSceneManager: Melaning Hall visited.");
            GameManager.CanvasQuadSelect("BeginSimulation");
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(MelaninToCampusPlayerPosition);
            MelaninHallTrigger.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("CampusSceneManager: Melaning Hall NOT visited.");
            GameManager.CanvasQuadSelect("");
            MelaninHallTrigger.gameObject.SetActive(true);
        }
    }
}
