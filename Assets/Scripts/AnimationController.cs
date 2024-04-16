using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Animator doorAnimator;
    [SerializeField] GameObject doorText;
    private CharacterController characterController;
    private Animator animator;
    private bool nearDoor;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        float magnitude = Mathf.Clamp01(moveDirection.magnitude) * speed;
        moveDirection.Normalize();

        characterController.SimpleMove(moveDirection * magnitude);

        if(moveDirection!= Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); 
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("IsWalking", true);
            speed = 0.75f;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("IsWalking", false);
            speed = 5;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("IsDancing", true);
        }
        if(Input.GetKeyUp(KeyCode.K))
        {
            animator.SetBool("IsDancing", false);
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            animator.SetBool("IsAttacking", true);
        }
        if(Input.GetKeyUp(KeyCode.G))
        {
            animator.SetBool("IsAttacking", false);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }
    

    public void OnTriggerEnter(Collider other)
    {
        nearDoor = true;
        doorText.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        nearDoor = false;
        doorText.SetActive(false);
    }

    public void OpenDoor()
    {
        if(nearDoor == true)
        {
            doorAnimator.SetBool("doorOpened", true);
            doorText.SetActive(false);
        }
        if(nearDoor == false)
        {
            Debug.Log("Get Closer to the Door!");
        }
    }
}
