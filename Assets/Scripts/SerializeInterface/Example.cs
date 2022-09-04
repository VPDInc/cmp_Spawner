using System;
using UnityEngine;
using SerializeInterface.Runtime;

using Object = UnityEngine.Object;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.0.0
namespace SerializeInterface
{
    public class Example : MonoBehaviour
    {
        [Header("Commands")]
        [SerializeReference, Selector] private ICommand _command;
        [SerializeReference, Selector] private ICommand[] _commands;

        private void Start()
        {
            _command.Execute();

            foreach (var command in _commands)
                command.Execute();
        }
    }

    public interface ICommand
    {
        public void Execute();
    }

    [Serializable]
    public class DebugCommand : ICommand
    {
        [SerializeField] private string _message;

        public void Execute() => Debug.Log(_message);
    }

    [Serializable]
    public class InstantiateCommand : ICommand
    {
        [SerializeField] private GameObject _prefab;
        
        public void Execute()
        {
            Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        }
    }

    [Serializable]
    [PopupMenu("Message")]
    public struct Message1 : ICommand
    {
        public void Execute()
        {
            Debug.Log("This is message 1");
        }
    }
    
    [Serializable]
    [PopupMenu("Messages/Message2")]
    public struct Message2 : ICommand
    {
        public void Execute()
        {
            Debug.Log("This is message 2");
        }
    }
    
    [Serializable]
    [PopupMenu("Messages/Message3")]
    public struct Message3 : ICommand
    {
        public void Execute()
        {
            Debug.Log("This is message 3");
        }
    }
}
