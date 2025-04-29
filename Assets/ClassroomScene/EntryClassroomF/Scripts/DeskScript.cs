using UnityEngine;
using UnityEngine.Playables;

public class DeskScript : MonoBehaviour
{

    [SerializeField] PlayableDirector pd; //reference! 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //reference to the playable director
        pd.Play(); //how you start the timeline to play.


    }
}
