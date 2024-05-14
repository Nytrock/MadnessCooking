using System.Collections.Generic;
using UnityEngine;

public class ClientsPool : MonoBehaviour
{
    [SerializeField] private Client _clientPrefab;
    [SerializeField] private GroupClient _grouClientPrefab;

    private Transform _container;
    private Queue<Client> _normalClients;
    private Queue<GroupClient> _groupClients;

    private void Awake()
    {
        _normalClients = new();
        _groupClients = new();
        _container = transform;
    }

    public Client GetClient()
    {
        Client client;
        if (_normalClients.Count == 0) {
            client = Instantiate(_clientPrefab, _container);
        } else {
            client = _normalClients.Dequeue();
        }

        ActivateClient(client);
        return client;
    }

    public GroupClient GetGroupClient()
    {
        GroupClient client;
        if (_groupClients.Count == 0) {
            client = Instantiate(_grouClientPrefab, _container);
        } else {
            client = _groupClients.Dequeue();
        }

        ActivateClient(client);
        return client;
    }

    private void ActivateClient(Client client)
    {
        client.enabled = true;
        client.gameObject.SetActive(true);
    }

    public void PutClient(Client client)
    {
        if (client.ClientData.Count == ClientCount.One)
            _normalClients.Enqueue(client);
        else
            _groupClients.Enqueue(client as GroupClient);
        client.enabled = false;
        client.gameObject.SetActive(false);
    }
}
