using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        InitializeComponents();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSwipeInput();
    }

    // Initialize necessary components
    void InitializeComponents()
    {
        m_rb = GetComponent<Rigidbody>();
        if (m_rb == null)
        {
            Debug.LogError("Rigidbody not found!");
        }
    }

    // Handle swipe input
    void HandleSwipeInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record the start position of the swipe
                    m_TouchStartPos = touch.position;
                    m_IsSwiping = true;
                    break;

                case TouchPhase.Moved:
                    // Check if the user is still swiping
                    if (m_IsSwiping)
                    {
                        // Calculate the swipe distance
                        float swipeDelta = touch.position.x - m_TouchStartPos.x;

                        // Determine if it's a left or right swipe based on the delta
                        if (Mathf.Abs(swipeDelta) > 50f) // You can adjust this threshold
                        {
                            // Swipe to the left
                            if (swipeDelta < 0)
                            {
                                MoveToRandomPosition();
                                m_IsSwiping = false;
                            }
                            // Swipe to the right
                            else
                            {
                                MoveToRandomPosition();
                                m_IsSwiping = false;
                            }
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    m_IsSwiping = false;
                    break;
            }
        }
    }

    // Function to move the player to a random position
    void MoveToRandomPosition()
    {
        if (m_LanePositions.Length > 0)
        {
            int randomindex;

            do
            {
                // Randomly select an index from the array
                randomindex = UnityEngine.Random.Range(0, m_LanePositions.Length);
            } while (randomindex == lastrandomindex);

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
    // Variables
    [SerializeField] private Transform[] m_LanePositions;
    private int lastrandomindex = -1;
    private Rigidbody m_rb;

    // Swipe detection variables
    private Vector2 m_TouchStartPos;
    private bool m_IsSwiping = false;

}