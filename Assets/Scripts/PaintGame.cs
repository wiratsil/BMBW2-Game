using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintGame : Singleton<PaintGame>
{
    public Color paintColor;
    public bool painting;
    private RaycastHit2D raycastHit2D;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && painting)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            RaycastHit2D hit = new RaycastHit2D();
            foreach (RaycastHit2D ray in hits)
            {
                if (hit.collider == null)
                {
                    hit = ray;
                    continue;
                }
                if (ray.collider.GetComponent<SpriteRenderer>().sortingOrder > hit.collider.GetComponent<SpriteRenderer>().sortingOrder)
                {
                    hit = ray;

                }
                if (ray.collider.transform.position.z > hit.collider.transform.position.z)
                {
                }
            }
            if (hit.collider != null && hit.collider.tag == "Paint")
            {
                hit.collider.GetComponent<SpriteRenderer>().color = paintColor;
            }
        }
    }

    public void SetPaint(bool bo)
    {
        painting = bo;
    }
}
