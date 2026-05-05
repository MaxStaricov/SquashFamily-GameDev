using UnityEngine;
using UnityEngine.UI; // ���������� ��� ������ � ������������ UI.Text
using System.Collections; // ���������� ��� ������������� Coroutines

// ���� �� ����������� TextMeshPro, ���������������� ��������� ������:
using TMPro;

/// <summary>
/// ���������� ��� ����������� � ������� ������ ������� � ����.
/// ��������� ������������� ����� � �������� ����� �����������.
/// </summary>
public class StatusPanelController : MonoBehaviour
{
    [Tooltip("������ �� GameObject ����� ������ ������� (��������, Canvas Group ��� Image ��������).")]
    public GameObject statusPanelGameObject;

    [Tooltip("������ �� ��������� Text (��� TextMeshProUGUI) ������ ����� ������ ��� ����������� ���������.")]
    public TMPro.TextMeshProUGUI statusText; // �������� �� 'public TMPro.TextMeshProUGUI statusText;' ���� ����������� TextMeshPro

    [Tooltip("������������ (� ��������), �� ������� ������ ����� ������.")]
    public float displayDuration = 3f; // ������ ����� ����� 3 ������� �� ���������

    /// <summary>
    /// ����� Awake ���������� ��� �������� �������.
    /// ������������ ��� ������������� � ������� ������ ��� ������ ����.
    /// </summary>
    private void Awake()
    {
        // ���������, ��������� �� ����������� ���������� � ����������.
        if (statusPanelGameObject == null)
        {
            Debug.LogError("StatusPanelController: 'Status Panel GameObject' �� �������� � ����������!", this);
        }
        if (statusText == null)
        {
            Debug.LogError("StatusPanelController: 'Status Text' ��������� �� �������� � ����������!", this);
        }

        // ����������, ��� ������ ���������� ������.
        if (statusPanelGameObject != null)
        {
            statusPanelGameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ���������� ������ ������� � �������� ���������� �� ��������� �����.
    /// </summary>
    /// <param name="message">�����, ������� ����� ��������� �� ������ �������.</param>
    public void ShowStatusPanel(string message)
    {
        // �������� �� null, ����� �������� ������, ���� ���������� �� ���������.
        if (statusPanelGameObject == null || statusText == null)
        {
            Debug.LogError("���������� ���������� ������ �������: ����������� ����������� ������. ��������� ���������� � ����������.", this);
            return;
        }

        // ������������� ����� ��������� �� ������.
        statusText.text = message;

        // ���������� GameObject ������, ����� �� �������.
        statusPanelGameObject.SetActive(true);

        // ��������� �������� ��� ������� ������ ����� �������� �����.
        StartCoroutine(HidePanelAfterDelay(displayDuration));
    }

    /// <summary>
    /// �������� ��� �������� ��������� ������� � ������������ ������� ������.
    /// </summary>
    /// <param name="delay">����� � ��������, ������� ����� ����� ����� �������� ������.</param>
    private IEnumerator HidePanelAfterDelay(float delay)
    {
        // ������� ��������� ���������� ������.
        yield return new WaitForSeconds(delay);

        // �������� ������, ���� ��� ��� ��� ������� � ����������.
        if (statusPanelGameObject != null)
        {
            statusPanelGameObject.SetActive(false);
        }
    }
}