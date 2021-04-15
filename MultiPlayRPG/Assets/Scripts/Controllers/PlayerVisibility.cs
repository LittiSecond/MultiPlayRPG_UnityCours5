using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;


namespace MultiPlayRPG
{
    public class PlayerVisibility : NetworkBehaviour
    {
        #region Fields

        [SerializeField] private float _visRange = 10.0f;
        [SerializeField] private float _visUpdateInterval = 1.0f;
        [SerializeField] private LayerMask _visMask;

        private Transform _transform;
        private float _visUpdateTime;

        #endregion


        #region UnityMethods

        public override void OnStartServer()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (isServer)
            {
                if (Time.time - _visUpdateTime > _visUpdateInterval)
                {
                    GetComponent<NetworkIdentity>().RebuildObservers(false);
                    _visUpdateTime = Time.time;
                }
                
            }
        }

        public override bool OnRebuildObservers(HashSet<NetworkConnection> observers, bool initialize)
        {
            Collider[] hits = Physics.OverlapSphere(_transform.position, _visRange, _visMask);
            foreach (Collider hit in hits)
            {
                CharacterOfPlr character = hit.GetComponent<CharacterOfPlr>();
                if (character != null && character.PlayerScriptsConnectorr != null)
                {
                    NetworkIdentity identity = 
                        character.PlayerScriptsConnectorr.GetComponent<NetworkIdentity>();
                    if (identity != null && identity.connectionToClient != null)
                    {
                        observers.Add(identity.connectionToClient);
                    }
                }
            }

            CharacterOfPlr m_character = GetComponent<CharacterOfPlr>();
            if (m_character != null && !observers.Contains(
                m_character.PlayerScriptsConnectorr.Connection))
            {
                observers.Add(m_character.PlayerScriptsConnectorr.Connection);
            }
            return true;
        }

        public override bool OnCheckObserver(NetworkConnection connection)
        {
            CharacterOfPlr character = GetComponent<CharacterOfPlr>();
            if (character != null && connection == character.PlayerScriptsConnectorr.Connection)
            {
                return true;
            }

            PlayerScriptsConnector player = null;
            foreach (UnityEngine.Networking.PlayerController controller in connection.playerControllers)
            {
                if (controller != null)
                {
                    player = controller.gameObject.GetComponent<PlayerScriptsConnector>();
                    if (player != null) break;
                }
            }

            if (player != null && player.Character != null)
            {
                return (player.Character.transform.position - _transform.position).magnitude < _visRange;
            }
            else return false;
        }

        #endregion


        #region Methods

        #endregion
    }
}