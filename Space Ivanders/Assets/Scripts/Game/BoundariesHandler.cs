using UnityEngine;

public class BoundariesHandler
{
    private readonly BoxCollider2D _leftBoundary, _rightBoundary;
    private readonly Camera _cam;

    public BoundariesHandler(BoxCollider2D leftBoundary, BoxCollider2D rightBoundary, Camera cam){
        _leftBoundary = leftBoundary;
        _rightBoundary = rightBoundary;
        _cam = cam;
    }

    public void SetBoundary(){
        var screenBounds = _cam.ViewportToWorldPoint(new Vector3(1, 1, _cam.transform.position.z));
        
        var leftCenter = -screenBounds.x - _leftBoundary.size.x / 2;
        var rightCenter = screenBounds.x + _rightBoundary.size.x / 2;
        
        _leftBoundary.transform.position = new Vector3(leftCenter, 0, 0);
        _rightBoundary.transform.position = new Vector3(rightCenter, 0, 0);
    }
}