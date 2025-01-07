using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampusSceneManager : MonoBehaviour
{

    [SerializeField] CanvasQuad BeginSimulation;
    [SerializeField] EventTrigger MelaninHallTrigger;

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
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(new Vector3(0f, 0.1f, 5f), new Vector3(0f, 0f, 0f));
        }
    }


    public void LoadScene(string sceneName)
    {

        if (sceneName == "MelaninHall")
        {
            Debug.Log("CampusSceneManager:LoadScene()...Visited MelaninHall set to true.");
            GameManager.instance.visitedMelaninHall = true;
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

        if (GameManager.instance.visitedMelaninHall == true)
        {
            GameManager.CanvasQuadSelect("BeginSimulation");
            MelaninHallTrigger.gameObject.SetActive(false);
        }
        else
        {
            GameManager.CanvasQuadSelect("");
            MelaninHallTrigger.gameObject.SetActive(true);
        }
    }
}
