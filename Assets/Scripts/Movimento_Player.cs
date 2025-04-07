using UnityEngine;

public class Movimento_Player : MonoBehaviour
{

    private CharacterController controller;
    private Transform mycamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mycamera = Camera.main.transform;
       

    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movimento = new Vector3 (horizontal, 0, vertical);
        movimento = mycamera.TransformDirection(movimento);
        movimento.y = 0;

        controller.Move(movimento * Time.deltaTime * 3);
        controller.Move(new Vector3(0, -9.81f, 0) * Time.deltaTime);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        } 

    }
}
