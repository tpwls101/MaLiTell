using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource bgmSource; // ��� ������ ����� AudioSource
    public Slider volumeSlider; // ������ ������ �����̴�

    void Start()
    {
        // �ʱ� ���� ����
        if (bgmSource != null && volumeSlider != null)
        {
            volumeSlider.value = bgmSource.volume;
        }
    }

    // �����̴��� ���� ����� �� ȣ��Ǵ� �Լ�
    public void OnVolumeChanged()
    {
        if (bgmSource != null && volumeSlider != null)
        {
            // �����̴��� ������ ������ ����
            bgmSource.volume = volumeSlider.value;
        }
    }
}
