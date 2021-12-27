using UnityEngine;
public class TouchCamera : MonoBehaviour
{
    public bool canPan = true;
    public bool canScale = true;

    public MouseSettings mouseSettings = new MouseSettings(0, 10, 10);

    public Range angleRange = new Range(0, 90);
    public Range distanceRange = new Range(1, 1000);

    //around center
    public Transform target;

    public float delta = 10;

    private Vector2 oldPos1;
    private Vector2 oldPos2;

    private bool m_isSinleFinger;

    private Vector3 targetPan;
    private Vector3 currentPan;

    private Vector2 targetAngles;
    private Vector2 currentAngles;

    private float targetDistance;
    private float currentDistance;

    private bool getCurrentDA = true;

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    [Space]
    public float minZoom;
    public float maxZoom;
    public Vector3 viewPortA;
    public Vector3 viewPortB;
    public Vector3 viewPortC;
    public Vector3 oldPos;
    public float oldSize;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    public Vector3 direction;
    public float dragSpeed;
    public float dragTiming;
    public float dragTime = 2f;


    //Damper (damping) for move and rotate
    // [Range(0, 10)]
    public float damper = 2;

    void Start()
    {
        GameObject camTargetObj = GameObject.Find("Main Camera Target");
        if (camTargetObj == null)
            camTargetObj = new GameObject("Main Camera Target");
        target = camTargetObj.transform;

        currentAngles = targetAngles = transform.eulerAngles;
        currentPan = targetPan = transform.position;
        currentDistance = targetDistance = Vector3.Distance(transform.position, target.position);

        //viewPortA = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //viewPortB = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        //viewPortC = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        //xMin = xMin + (Mathf.Abs(viewPortA.x) - Mathf.Abs(viewPortC.x));
        //xMax = xMax - (Mathf.Abs(viewPortB.x) - Mathf.Abs(viewPortC.x));
        //yMin = yMin + (Mathf.Abs(viewPortA.y) - Mathf.Abs(viewPortC.y));
        //yMax = yMax - (Mathf.Abs(viewPortB.y) - Mathf.Abs(viewPortC.y));
    }

    void Update()
    {
        DragCamera();

        if (Input.touchCount <= 0)
        {
            getCurrentDA = true;
            return;
        }
        else
        {
            // Make sure to get the latest Camera status
            if (getCurrentDA)
            {
                getCurrentDA = false;
                currentAngles = targetAngles = transform.eulerAngles;
                currentDistance = targetDistance = Vector3.Distance(transform.position, target.position);
                if (Camera.main.orthographic)
                {
                    currentPan = targetPan = transform.position;
                    currentDistance = targetDistance = Camera.main.orthographicSize;
                }
            }
        }

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {

                Touch touch = Input.GetTouch(0);
                //rotate
                if (!Camera.main.orthographic)
                {
                    targetAngles.y += touch.deltaPosition.x * mouseSettings.pointerSensitivity;
                    targetAngles.x -= touch.deltaPosition.y * mouseSettings.pointerSensitivity;

                    targetAngles.x = Mathf.Clamp(targetAngles.x, angleRange.min, angleRange.max);

                    currentAngles = Vector2.Lerp(currentAngles, targetAngles, damper * Time.deltaTime);
                    //rotate of target
                    Quaternion rotation = Quaternion.Euler(currentAngles);
                    Vector3 newPosition = target.position + rotation * Vector3.back * currentDistance;
                    transform.position = newPosition;
                    transform.rotation = rotation;
                }
                else
                {
                    //Pan
                    if (canPan)
                    {
                        targetPan.x -= touch.deltaPosition.x / delta;
                        targetPan.y -= touch.deltaPosition.y / delta;
                        currentPan = Vector3.Lerp(currentPan, targetPan, (damper) * Time.deltaTime);
                        // transform.position = targetPan;
                        transform.position = Vector3.SmoothDamp(transform.position, targetPan, ref velocity, smoothTime);
                        direction = (targetPan - transform.position).normalized;
                    }
                }

            }
            m_isSinleFinger = true;

            dragTiming = dragTime;
            Debug.LogError(m_isSinleFinger);
        }

        //mouse scrollewheel zoom
        if (canScale)
        {
            if (Input.touchCount > 1)
            {
                if (m_isSinleFinger)
                {
                    oldPos1 = Input.GetTouch(0).position;
                    oldPos2 = Input.GetTouch(1).position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
                {

                    var tempPos1 = Input.GetTouch(0).position;
                    var tempPos2 = Input.GetTouch(1).position;

                    float currentTouchDistance = Vector3.Distance(tempPos1, tempPos2);
                    float lastTouchDistance = Vector3.Distance(oldPos1, oldPos2);

                    targetDistance -= (currentTouchDistance - lastTouchDistance) * Time.deltaTime * mouseSettings.wheelSensitivity;


                    targetDistance = Mathf.Clamp(targetDistance, distanceRange.min, distanceRange.max);
                    currentDistance = Mathf.Lerp(currentDistance, targetDistance, damper * Time.deltaTime);

                    if (Camera.main.orthographic)
                        Camera.main.orthographicSize = currentDistance;
                    else
                        transform.position = target.position - transform.forward * currentDistance;


                    oldPos1 = tempPos1;
                    oldPos2 = tempPos2;
                    m_isSinleFinger = false;
                }
            }
        }
        viewPortA = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewPortB = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        if (viewPortA.x < xMin || viewPortA.y < yMin || viewPortB.x > xMax || viewPortB.y > yMax)
        {
            Camera.main.transform.position = oldPos;
            //Camera.main.orthographicSize = oldSize;
            return;
        }
        if (Camera.main.orthographicSize < 1)
        {
            Camera.main.orthographicSize = 1;
            return;
        }
        oldPos = Camera.main.transform.position;
       // oldSize = Camera.main.orthographicSize;

    }

    void DragCamera()
    {
        if (dragTiming > 0 && Input.touchCount == 0)
        {
            transform.position = transform.position + (direction * dragSpeed * Time.deltaTime);
            dragTiming -= Time.deltaTime;
        }
    }

}
public struct MouseSettings
{
    public int mouseButtonID;
    public float pointerSensitivity;
    public float wheelSensitivity;

    public MouseSettings(int mouseButtonID, float pointerSensitivity, float wheelSensitivity)
    {
        this.mouseButtonID = mouseButtonID;
        this.pointerSensitivity = pointerSensitivity;
        this.wheelSensitivity = wheelSensitivity;
    }
}

public struct Range
{
    public float min;
    public float max;

    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}
