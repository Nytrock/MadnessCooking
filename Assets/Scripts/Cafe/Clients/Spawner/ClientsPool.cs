using UnityEngine;

public class ClientsPool : Pool<Client> {
    [SerializeField] private Client _clientPrefab;

    public override Client GetObject() {
        Client client;
        if (_pool.Count == 0)
            client = Instantiate(_clientPrefab, _container);
        else
            client = _pool.Dequeue();
        client.gameObject.SetActive(true);
        client.ChangeEnable(false);
        return client;
    }

    public override void PutObject(Client client) {
        _pool.Enqueue(client);
        client.gameObject.SetActive(false);
    }
}
