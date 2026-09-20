using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class ClientHolderManagerData {
        [SerializeField, JsonProperty] private List<ClientHolderData> _clientHolders;

        public IEnumerable<ClientHolderData> ClientHolders => _clientHolders;

        public ClientHolderManagerData() {
            _clientHolders = new();
        }

        public void RemoveClientHolderAt(int index) {
            _clientHolders.RemoveAt(index);
        }

        public void AddClientHolder(ClientHolderData newData) {
            _clientHolders.Add(newData);
        }

        public ClientHolderData GetClientHolder(int index) {
            return _clientHolders[index];
        }
    }
}
