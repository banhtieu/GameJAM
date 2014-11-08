using UnityEngine;
using System.Collections;

public class AnimalSpawner : MonoBehaviour {

	// maximum number of animals
	public int maxAnimals = 5;

	public GameObject animalPrefab;
	// Spawn area
	public Rect spawnArea;

	// created animals
	private ArrayList animals = new ArrayList();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (animals.Count < maxAnimals) {
			float cameraHeight = Camera.main.orthographicSize;
			float cameraWidth = Screen.width * cameraHeight / Screen.height;

			Vector2 size = new Vector2(cameraWidth, cameraHeight);

			float randomX = Random.value - 0.5f;
			float randomY = Random.value - 0.5f;

			Vector3 position = new Vector3(size.x * randomX, size.y * randomY, 0.0f);
			GameObject animal = (GameObject) Instantiate(animalPrefab, position, new Quaternion());
			animals.Add(animal);
			animal.GetComponent<AnimalController>().SetSpawner(this);
		}
	}


	// an animal has been fed - remove it
	public void ReportFeed(GameObject animal) {

		animals.Remove(animal);
		GameController.GetInstance().IncreaseScore();
	}
}
