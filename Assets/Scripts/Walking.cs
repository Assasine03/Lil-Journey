using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour, Controls.IKeyActionActions
{
    private Vector2 _movementInput;
    [SerializeField]
    float speed = 0.1f;
   
    
    [SerializeField]
    Animator myAnimator;

    [SerializeField]
    float acceleration = 10f;
    [SerializeField]
    float antiAcceleration = 15f;
    float curSpeed = 0;



    public void OnKeyboard(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    




    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Works");

    }

    // Update is called once per frame
    void Update()
    {
        if (_movementInput.x == 0 & _movementInput.y == 0)
        {
            myAnimator.SetBool("walking", false); //start idle animation
            
            if (curSpeed > 0)
            {
                curSpeed -= antiAcceleration * Time.deltaTime;
            }
            else
            {
                curSpeed = 0;
            }
        }
        else
        {
            
            if (curSpeed < speed)
            {
                curSpeed += acceleration * Time.deltaTime;
            }
            myAnimator.SetBool("walking", true);
        }
        
        Vector2 playerPosition = transform.position;
        if (_movementInput.x > 0 | (myAnimator.GetBool("walking") == false & myAnimator.GetBool("right") == true))
        {
            myAnimator.SetBool("right", true);
            myAnimator.SetBool("left", false);
            myAnimator.SetBool("front", false);
            myAnimator.SetBool("back", false);
            playerPosition.x += Time.deltaTime * curSpeed;
            
        }
        if (_movementInput.x < 0 | (myAnimator.GetBool("walking") == false & myAnimator.GetBool("left") == true))
        {
            myAnimator.SetBool("left", true);
            myAnimator.SetBool("right", false);
            myAnimator.SetBool("front", false);
            myAnimator.SetBool("back", false);
            
            playerPosition.x -= Time.deltaTime * curSpeed;
            
        }
        if (_movementInput.y < 0 | (myAnimator.GetBool("walking") == false & myAnimator.GetBool("front") == true)) {
            myAnimator.SetBool("front", true);
            myAnimator.SetBool("left", false);
            myAnimator.SetBool("right", false);
            myAnimator.SetBool("back", false);
            
            playerPosition.y -= Time.deltaTime * curSpeed;
            
        }
        if (_movementInput.y > 0 | (myAnimator.GetBool("walking") == false & myAnimator.GetBool("back") == true))
        {
            myAnimator.SetBool("back", true);
            myAnimator.SetBool("left", false);
            myAnimator.SetBool("front", false);
            myAnimator.SetBool("right", false);
            
            playerPosition.y += Time.deltaTime * curSpeed;
            
        }
        transform.position = playerPosition;
    }
}
