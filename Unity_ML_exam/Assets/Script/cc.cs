using UnityEngine;

public class cc : MonoBehaviour
{
    public static bool complete;
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "不會飛")
        {
            complete = true;
        }
    }
}
