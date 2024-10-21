using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedMovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Vector2> route = new List<Vector2>() { new(0f, 0f), new(0f, 0f) };
    [SerializeField] private float MovingSpeed=2f;
    [SerializeField] private float MoveDelay=2f;
    private int _index;
    private bool _canMove = true;
    void Start()
    {
        if (route.Count >=2)
        {
            _index = 0;
            StartCoroutine(MoveCoroutine());
        }
        else
        {
            Debug.Log("Platform route missing");
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            other.gameObject.transform.SetParent(gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            other.gameObject.transform.SetParent(null);
    }

    private IEnumerator MoveCoroutine()
    {
        while(_canMove){
            Vector2 next = _index < route.Count - 1 ? route[_index + 1] : route[0];
            yield return new WaitForSeconds(MoveDelay);
            while (Vector2.Distance(transform.position,next)>0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, next, MovingSpeed * Time.deltaTime);
                yield return null;
            }
            _index++;
            if (_index >= route.Count)
            {
                _index = 0;
            }
        }
    }

    public void ToggleMove()
    {
        _canMove = !_canMove;
    }
}
