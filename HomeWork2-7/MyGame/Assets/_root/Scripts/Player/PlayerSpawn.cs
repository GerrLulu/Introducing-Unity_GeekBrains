using System;
using UnityEngine;

namespace Player
{
    public class PlayerSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        public static Action PlayerSpawned;

        private void Awake()
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
            PlayerSpawned.Invoke();
        }
    }
}