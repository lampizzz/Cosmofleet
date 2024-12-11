using ExitGames.Client.Photon;
using UnityEngine;
using GameClasses;

public class PhotonSetup : MonoBehaviour
{
    public static PhotonSetup Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Регистрируем тип CellState
        PhotonPeer.RegisterType(typeof(CellState), (byte)'C', CellStateSerializer, CellStateDeserializer);
    }

    // Сериализация CellState
    private static byte[] CellStateSerializer(object obj)
    {
        var cellState = (CellState)obj;
        return new byte[] { (byte)cellState };
    }

    // Десериализация CellState
    private static object CellStateDeserializer(byte[] data)
    {
        return (CellState)data[0];
    }
}