using UnityEngine;
using System.Collections;

public class cameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            // Gradually reduce the magnitude over time
            magnitude = Mathf.Lerp(magnitude, 0, elapsed / duration);

            yield return null; // Wait until next frame
        }

        transform.localPosition = originalPosition; // Reset position
    }
}
