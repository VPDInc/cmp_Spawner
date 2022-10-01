using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General
{
    public class SystemFunctions : MonoBehaviour
    {
        public static void Quit() => Application.Quit();

        public static void OpenURl(string url) => Application.OpenURL(url);
    }
}
