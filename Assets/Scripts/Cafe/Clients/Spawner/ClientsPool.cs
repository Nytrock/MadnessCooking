using UnityEngine;

public class ClientsPool : Pool<Client> {
    [SerializeField] private Client _clientPrefab;

    public override Client GetObject() {
        if (_pool.Count == 0)
            return Instantiate(_clientPrefab, _container);
        else
            return _pool.Dequeue();
    }

    public override void PutObject(Client client) {
        _pool.Enqueue(client);
        ChangeClientState(client, false);
    }

    public Client GetClient() {
        Client client = GetObject();
        ChangeClientState(client, transform);
        return client;
    }

    public GroupClient GetGroupClient() {
        GroupClient client = GetObject().GetComponent<GroupClient>();
        ChangeClientState(client, true);
        return client;
    }

    private void ChangeClientState(Client client, bool newState) {
        // client.GetComponent<Client>().enabled = false;
        // client.GetComponent<GroupClient>().enabled = false;
        client.gameObject.SetActive(newState);
    }
}
