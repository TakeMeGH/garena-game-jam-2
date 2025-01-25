using UnityEngine;

namespace TKM
{
    public class Crystal : MonoBehaviour
    {
        private static Crystal _instance;

        public static Crystal Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<Crystal>();
                }
                return _instance;
            }
        }


        public int Health;
        public void Attacked()
        {
            Health--;
            Debug.Log("Health: " + Health);
        }
    }
}
