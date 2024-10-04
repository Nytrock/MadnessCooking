using UnityEngine;

public class ClientsPool : Pool<Client> {
    [SerializeField] private Client _clientPrefab;
    private ClientGender _clientsGender;

    public ClientGender ClientsGender => _clientsGender;

    private void Awake() {
        _clientsGender = _clientPrefab.Gender;
    }

    public override Client GetObject() {
        Client client = base.GetObject();
        client.gameObject.SetActive(true);
        client.ChangeEnable(false);
        return client;
    }

    public override void PutObject(Client client) {
        base.PutObject(client);
        client.gameObject.SetActive(false);
    }

    protected override Client CreateObject() {
        return Instantiate(_clientPrefab, _container);
    }
}
