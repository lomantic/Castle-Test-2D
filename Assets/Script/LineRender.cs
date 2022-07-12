using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
  [SerializeField] LineRenderer _lineRenderer;
  [SerializeField] Vector3 playerPosition;
  [SerializeField] Vector3 portalPosition;
  // Start is called before the first frame update
  private void awake()
  {
    _lineRenderer = GetComponent<LineRenderer>();
  }

  // Update is called once per frame
  public void SetUpLine(Vector3 playerPos, Vector3 portalPos)
  {
    _lineRenderer.positionCount = 2;
    playerPosition = playerPos;
    portalPosition = portalPos;
  }

  private void Update()
  {
    _lineRenderer.SetPosition(0, playerPosition);
    _lineRenderer.SetPosition(1, portalPosition);
  }
}
