using UnityEngine;

public class door : MonoBehaviour, Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public Animator m_Animator;
    [SerializeField] public bool isOpen;

    void Start()
    {
        if (isOpen) 
        {
            m_Animator.SetBool("isOpen", true);
        }
    }

   
    void Update()
    {
        return;
    }

    public void OnInteract() 
    {
        isOpen = !isOpen;
        m_Animator.SetBool("isOpen", isOpen);
    }
}
