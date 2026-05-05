using UnityEngine;

public class Door : MonoBehaviour,Interactable
{

    public Animator animator;
    private bool isOpen = false;
    public Light fridgeLight;

    public void OnInteract()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            fridgeLight.gameObject.SetActive(true);
            animator.SetBool("isOpen", true);
        }
        else
        {
            fridgeLight.gameObject.SetActive(false);
            animator.SetBool("isOpen", false);
        }
    }

}
