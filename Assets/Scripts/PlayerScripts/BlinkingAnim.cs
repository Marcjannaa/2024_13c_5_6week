using System.Collections;
using UnityEngine;

    public class BlinkingAnim : MonoBehaviour
    {
        private float _interval;
        private float _time = 0f;
        private Sprite _sprite;
    
        private void Start()
        {
            _time = Random.Range(1f, 2f);
            _sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            if (_time < _interval) return;
            StartCoroutine(Blink());
            _time = 0f;
            _interval = Random.Range(2f, 5f);
        }

        private IEnumerator Blink()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = _sprite;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

