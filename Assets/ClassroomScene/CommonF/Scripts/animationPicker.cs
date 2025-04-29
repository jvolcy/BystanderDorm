using UnityEngine;

public class animationPicker : MonoBehaviour
{
    [SerializeField] int NumAnimations = 0; //will get overwritten!!!!!!!!!!!
    //create a reference to a component of type Animator
    Animator animator; //varaible name and then the type
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>(); //get me a refernece to a component of type animator to the current game object
        InvokeRepeating("ChangeAnimationIndex", 0, 5);
        ChangeAnimationIndex();

    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void ChangeAnimationIndex(){
        animator.SetInteger("index", Random.Range(0, NumAnimations));
    }

    public void HandRaiser1(){
        animator.SetTrigger("RaiseHand");
    }
}
