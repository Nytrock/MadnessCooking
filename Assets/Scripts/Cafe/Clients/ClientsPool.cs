using System.Collections.Generic;
using UnityEngine;

public class ClientsPool : MonoBehaviour
{
    [SerializeField] private Client _prefab;
    [SerializeField] private Transform _container;

    private Queue<Client> _pool;

    private void Awake()
    {
        _pool = new Queue<Client>();
        _container = transform;
    }

    public Client GetObject()
    {
        if (_pool.Count == 0) {
            var client = Instantiate(_prefab, _container);
            _pool.Enqueue(client);
        }

        return _pool.Dequeue();
    }

    public void PutObject(Client client)
    {
        _pool.Enqueue(client);
        client.gameObject.SetActive(false);
    }
}
