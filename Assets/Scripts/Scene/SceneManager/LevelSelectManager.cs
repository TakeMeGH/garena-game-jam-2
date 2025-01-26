using UnityEngine;

namespace TKM
{
    public class LevelSelectManager : MonoBehaviour
    {
        [SerializeField] InputReader _inputReader;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _inputReader.EnableUIInput();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
