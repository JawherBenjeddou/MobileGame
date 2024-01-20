using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  

    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();

        // Check if the button is assigned
        if (m_LeftButton != null && m_RightButton != null)
        {
            // Add listeners to the button's click events
            m_LeftButton.onClick.AddListener(OnLeftButtonClick);
            m_RightButton.onClick.AddListener(OnRightButtonClick);
        }
        else
        {
            Debug.LogError("Button is not assigned in the Inspector!");
        }
    }

    // Initialize necessary components
    void InitializeComponents()
    {
        m_rb = GetComponent<Rigidbody>();
        if (m_rb == null)
        {
            Debug.Log("RigidBody does not exist");
        }
    }

    // Function invoked when Left button is clicked
    void OnLeftButtonClick()
    {
        Debug.Log("Left Button Clicked!");
        MoveToRandomPosition();
    }

    // Function invoked when Right button is clicked
    void OnRightButtonClick()
    {
        Debug.Log("Right Button Clicked!");
        MoveToRandomPosition();
    }

    // This function smoothly moves the player to a random position
    void MoveToRandomPosition()
    {
        if (m_LanePositions.Length > 0)
        {
            int randomindex;

            do
            {
                // Randomly select an index from the array
                randomindex = UnityEngine.Random.Range(0, m_LanePositions.Length);
            }
            while (randomindex == lastrandomindex);

            lastrandomindex = randomindex;

            // Get the chosen transform position
            Transform chosenPosition = m_LanePositions[randomindex];

            // Start the coroutine for smooth movement
            StartCoroutine(MoveToPositionCoroutine(chosenPosition.position));
        }
        else
        {
            Debug.LogWarning("No target positions assigned.");
        }
    }

    // Coroutine for smooth movement
    IEnumerator MoveToPositionCoroutine(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float totalTime = 0.354f; // Adjust this to control the time it takes to reach the target

        Vector3 startingPosition = transform.position;

        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the player is exactly at the target position when the coroutine ends
        transform.position = targetPosition;
    }

    //Variables
    [SerializeField] private Button m_LeftButton;
    [SerializeField] private Button m_RightButton;
    [SerializeField] private Transform[] m_LanePositions;
    private int lastrandomindex = -1;
    private Rigidbody m_rb;
}
