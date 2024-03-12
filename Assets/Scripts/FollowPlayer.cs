using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private float _yOffset;
    
    private PlayerInput _playerInput;

    private Quaternion temp;

    private Vector3 temp2;

    [SerializeField] private float _camRotationMult;

    private float _yRotationStorage;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = _player.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + new Vector3(0, _yOffset, 0);
        
        temp2 = _playerInput.actions["Look"].ReadValue<Vector2>().normalized;
        temp2 *= _camRotationMult;
        
        temp.eulerAngles = new Vector3(temp2.y, temp2.x, 0);
                transform.Rotate(temp.x, 0, 0, Space.Self);
                transform.Rotate(0, temp.y, 0, Space.World);
    }
}
