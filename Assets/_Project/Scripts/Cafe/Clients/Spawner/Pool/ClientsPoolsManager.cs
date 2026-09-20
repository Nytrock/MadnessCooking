using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class ClientsPoolsManager : MonoBehaviour, IUpgradeable<CafeUpgradeData> {
        [SerializeField] private ClientsPool[] _pools;
        [SerializeField] private BaseUpgrade _eatTimeShowUpgrade;

        private CafeUpgradeData _upgradeData;

        public Client GetClient(ClientData clientData) {
            if (clientData.Gender != ClientGender.None)
                return GetClientByGender(clientData.Gender);

            if (clientData.Type == ClientType.Grayman)
                return GetClientByGender(ClientGender.Male);

            ClientsPool randomPool = _pools.GetRandom();
            return randomPool.GetObject();
        }

        private Client GetClientByGender(ClientGender gender) {
            foreach (var pool in _pools) {
                if (pool.ClientsGender == gender) {
                    return pool.GetObject();
                }
            }

            return null;
        }

        public void PutObject(Client client) {
            foreach (var pool in _pools) {
                if (client.Gender == pool.ClientsGender) {
                    pool.PutObject(client);
                    return;
                }
            }

            throw new NullReferenceException($"There's no client pool for gender {client.Gender}");
        }

        public void CheckAddedUpgrade(BaseUpgrade upgrade) {
            if (upgrade == _eatTimeShowUpgrade)
                _upgradeData.ChangeEatTimeShow(true);
        }

        public void BindUpgrade(CafeUpgradeData upgradeData) {
            _upgradeData = upgradeData;
            foreach (var pool in _pools)
                pool.BindUpgrade(upgradeData);
        }
    }
}
