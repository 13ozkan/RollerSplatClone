using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private GameManager gameManager;
    public Image levelBar;
    public Rigidbody rb;
    private Vector2 _firstPos, _secondPos, _currentPos;
    public float _moveSpeed;
    public float _currentGroundNumbers;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }


    void Update()
    {
        Swipe();

        levelBar.fillAmount = _currentGroundNumbers / gameManager._groundNumbers;

        if (levelBar.fillAmount == 1)
        {
            gameManager.LevelUpdate();
        }
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            _currentPos = new Vector2(
                 _secondPos.x - _firstPos.x,
                 _secondPos.y - _firstPos.y
                );
        }

        _currentPos.Normalize();

        if (_currentPos.y < 0 &&  _currentPos.x >  -0.5f && _currentPos.x < 0.5f)
        {
            rb.velocity = Vector3.back * _moveSpeed;
        }
        else if (_currentPos.y > 0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f)
        {
            rb.velocity = Vector3.forward * _moveSpeed;
        }
        else if(_currentPos.x < 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {
            rb.velocity = Vector3.left * _moveSpeed;
        }
        else if (_currentPos.x > 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {
            rb.velocity = Vector3.right * _moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
        {
            if (other.gameObject.tag == "Ground")
            {
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Constraints();
                _currentGroundNumbers++;
            }
        }

    }

    private void Constraints()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;       
    }
}
