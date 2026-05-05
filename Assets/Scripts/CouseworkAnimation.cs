using System.Collections;
using TMPro;
using UnityEngine;

public class CouseworkAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    private string fullText = "The internet, once a modest network connecting a handful of research institutions, has evolved into one of humanity�s most transformative inventions. Its origins trace back to the late 1960s with the creation of ARPANET, a project funded by the U.S. Department of Defense to explore methods of communication that could withstand disruptions, such as those caused by war or natural disasters. The development of key protocols like TCP/IP in the 1970s laid the foundation for a globally interconnected system of networks. By the 1980s, academic and commercial entities began joining the web, and the invention of the World Wide Web by Tim Berners-Lee in 1989 revolutionized how information was shared and accessed.\r\n\r\nWhat began as a tool for researchers and government agencies quickly expanded into a global phenomenon that reshaped nearly every aspect of modern life. The internet democratized access to knowledge, enabling anyone with a connection to learn about virtually any subject from anywhere in the world. It transformed communication, making it possible to instantly connect across continents through email, messaging, video calls, and social media platforms. Economies were restructured around digital commerce, giving rise to e-commerce giants and remote work opportunities that blurred the lines between physical borders.\r\n\r\nHowever, the internet's influence is not without complications. Issues such as misinformation, cybercrime, data privacy concerns, and digital divides have emerged alongside its benefits. Social dynamics have shifted dramatically, with online communities playing an increasingly central role in personal identity, political movements, and cultural exchange. Governments and corporations now grapple with the challenges of regulating content, protecting user rights, and ensuring ethical use of artificial intelligence and big data analytics.\r\n\r\nDespite these complexities, the internet remains a cornerstone of innovation and progress. From education and healthcare to entertainment and activism, its reach continues to expand, touching lives in ways previously unimaginable. As we move further into the digital age, understanding ";

    [SerializeField] private float delayBetweenLetters = 0.1f;
    [SerializeField] private float delayAfterComplete = 1.5f;
    private float symbolСost = 1f;
    private bool isRunning = true;

    private void OnEnable()
    {
        if(!StaminaBar.Instance.CheckEmpty() && !SatietyBar.Instance.CheckEmpty())
        {
            // ProgressBar.Instance.UpdateValue(StaminaBar.Instance.StatusBar.Present.value);
            StartCoroutine(ShowTextLoop());
        }
    }

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator ShowTextLoop()
    {
    
        while (isRunning)
        {
            string currentText = "";

            foreach (char c in fullText)
            {
                if (!isRunning) yield break;

                if(!StaminaBar.Instance.CheckUpdate(-symbolСost)) break;

                currentText += c;
                textComponent.text = currentText;

                StaminaBar.Instance.UpdateValue(-symbolСost);
                ProgressBar.Instance.UpdateValue(symbolСost);

                yield return new WaitForSeconds(delayBetweenLetters);
            }

            yield return new WaitForSeconds(delayAfterComplete);

            textComponent.text = "";
        }

        yield break;
    }

}
