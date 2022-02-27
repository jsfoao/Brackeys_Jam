using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UISetText : MonoBehaviour
{
    private TextMeshProUGUI _texRef;

    private void Awake()
    {
        _texRef = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _texRef.text = GameManager.Instance.scoreManager.score.ToString();
    }
}
