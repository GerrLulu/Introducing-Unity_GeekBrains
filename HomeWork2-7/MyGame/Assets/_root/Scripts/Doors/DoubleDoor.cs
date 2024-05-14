using Player;
using UnityEngine;

namespace Doors
{
    public class DoubleDoor : MonoBehaviour
    {
        [SerializeField] private Protagonist _protagonist;

        private Animation _anim;


        private void Awake()
        {
            _anim = GetComponent<Animation>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && _protagonist.IsHaveBlueCard == true)
            {
                Debug.Log("Open door");

                OpenDoor();
            }
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
            _anim.Play("DoubleDoorOpen");
        }

        private void CloseDoor()
        {
            _anim.Play("DoubleDoorClose");
        }
    }
}