using Player;
using UnityEngine;

namespace Doors
{
    public class DoubleDoor : MonoBehaviour
    {
        [SerializeField] private float _valueMovement = 1.2f;
        [SerializeField] private Protagonist _protagonist;
        [SerializeField] private GameObject _leftDoor;
        [SerializeField] private GameObject _rightDoor;

        Vector3 _positionCloseLeftDoor;
        Vector3 _positionCloseRightDoor;
        Vector3 _positionOpenLeftDoor;
        Vector3 _positionOpenRightDoor;


        private void Start()
        {
            _positionCloseLeftDoor = _leftDoor.transform.localPosition;
            _positionCloseRightDoor = _rightDoor.transform.localPosition;

            _positionOpenLeftDoor = new Vector3(_positionCloseLeftDoor.x, _positionCloseLeftDoor.y,
                _positionCloseLeftDoor.z + _valueMovement);
            _positionOpenRightDoor = new Vector3(_positionCloseRightDoor.x, _positionCloseRightDoor.y,
                _positionCloseRightDoor.z - _valueMovement);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && _protagonist.IsHaveBlueCard == true)
            {
                Debug.Log("Open door");

                OpenDoor();
            }
            else
                Debug.Log("Not opened");
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player" && _protagonist.IsHaveBlueCard == true)
            {
                Debug.Log("Close door");

                CloseDoor();
            }
        }


        private void OpenDoor()
        {
            _leftDoor.transform.localPosition = _positionOpenLeftDoor;
            _rightDoor.transform.localPosition = _positionOpenRightDoor;
        }

        private void CloseDoor()
        {
            _leftDoor.transform.localPosition = _positionCloseLeftDoor;
            _rightDoor.transform.localPosition = _positionCloseRightDoor;
        }
    }
}