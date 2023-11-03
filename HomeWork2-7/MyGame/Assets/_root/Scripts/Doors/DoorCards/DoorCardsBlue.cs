using UnityEngine;

namespace Doors
{
    public class DoorCardsBlue : MonoBehaviour
    {
        [SerializeField] private float _rotationalSpeed = 45.0f;


        private void Update()
        {
            transform.Rotate(Vector3.up, _rotationalSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Player")
            {
                gameObject.SetActive(false);
            }
        }
    }
}