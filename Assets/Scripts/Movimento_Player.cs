using UnityEngine;

public class Movimento_Player : MonoBehaviour
{
    private Transform myCamera;
    private CharacterController controller;
    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {              
       myCamera = Camera.main.transform;
      controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();



    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertiacal = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0, vertiacal);

        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0;

        controller.Move(movimento * Time.deltaTime * 5);

        controller.Move(new Vector3(0, -9.81f, 0) * Time.deltaTime);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

        animator.SetBool("IsWalking", movimento != Vector3.zero);


    }
}
