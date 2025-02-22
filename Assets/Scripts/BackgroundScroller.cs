using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public GameObject[] backgrounds; // Les 2 backgrounds
    public float scrollSpeed = 2f;
    private float backgroundWidth;

    private void Start()
    {
        // Vérifier si Unity a bien chargé les backgrounds depuis l'Inspector
        Debug.Log($"Start : Backgrounds détectés : {backgrounds?.Length ?? 0}");

        if (backgrounds == null || backgrounds.Length < 2)
        {
            Debug.LogError($"Erreur : Assigne au moins 2 backgrounds dans l'Inspector ! Actuellement : {backgrounds?.Length ?? 0}");
            return;
        }

        // Vérifier que chaque background a bien un SpriteRenderer
        foreach (GameObject bg in backgrounds)
        {
            if (bg.GetComponent<SpriteRenderer>() == null)
            {
                Debug.LogError($"Erreur : L'objet {bg.name} n'a pas de SpriteRenderer !");
                return;
            }
        }

        // Récupérer la largeur du premier background
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    



    private void Update()
    {
        foreach (GameObject bg in backgrounds)
        {
            bg.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

            // Si un background sort de l'écran, on le téléporte à droite
            if (bg.transform.position.x <= -backgroundWidth)
            {
                float newX = bg.transform.position.x + backgroundWidth * 2;
                bg.transform.position = new Vector3(newX, bg.transform.position.y, bg.transform.position.z);
            }
        }
    }
}