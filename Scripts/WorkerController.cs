using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorkerController : BaseBehavior {

	// the step prefab
	public GameObject stepPrefab;

	// Walking direction
	public Vector3 walkingDirection = new Vector3(1.0f, 0.0f, 0.0f);

	// speed of this object
	public float speed = 2.0f;

	// feeding speeds	
	public float feedingSpeed = 50.0f;

	// time to spawn a step
	public float delayTime = 0.0001f;

	// Indicator Object
	private GameObject indicator;

	// Game 
	private GameObject character;

	// warning icon
	private GameObject warningIcon;

	public GameObject cryObject;

	// mouse down true
	private bool mouseDown = false;

	// the array list
	private ArrayList steps = new ArrayList();

	// the timer
	private float timer;

	// animal to feed
	private AnimalController feedingAnimal;

	// state of current characters
	private CharacterState state;

	private bool isPaused;

	// character states
	public enum CharacterState {
		WALKING,
		FEEDING,
		DIE
	};

	// Mouse Down = true
	void OnMouseDown() {

		mouseDown = true;
		indicator.renderer.enabled = true;
		timer = 0;

		// reset all steps;
		foreach (GameObject step in steps) {
			DestroyObject(step);
		}

		steps.Clear();
	}

	// on collision enter 2d
	void OnCollisionEnter2D(Collision2D collision) {
		Die();
	}

	void OnTriggerExit2D(Collider2D collider) {
		warningIcon.renderer.enabled = false;
	}

	// Trigger Enter
	void OnTriggerEnter2D(Collider2D collider) {
		if (state == CharacterState.WALKING) {
			// check if collider is the animal
			feedingAnimal = collider.gameObject.GetComponent<AnimalController>();
			if (feedingAnimal != null) {
				state = CharacterState.FEEDING;
				character.GetComponent<Animator>().Play("MCFeed");
			} else {
				warningIcon.renderer.enabled = true;
			}
		}
	}

	// Use this for initialization
	void Start () {
		indicator = transform.Find("Indicator").gameObject;
		character = transform.Find("Character").gameObject;
		warningIcon = transform.Find("WarningIcon").gameObject;
		warningIcon.renderer.enabled = false;
		indicator.renderer.enabled = false;
	}
	
	// Update is called once per frame
	public override void ConditionalUpdate () {

		if (state == CharacterState.WALKING) {
			Walk();
		} else if (state == CharacterState.FEEDING) {
			if (feedingAnimal.Feed(feedingSpeed * Time.deltaTime)) {
				state = CharacterState.WALKING;
				character.GetComponent<Animator>().Play("MCWalk");
			}
		}

		if (mouseDown) {
			if (Input.GetMouseButton(0)) {

				// Instantiate a step
				if (Tick()) {
					Vector3 lastPosition = new Vector3(transform.position.x, transform.position.y, 1.0f);

					if (steps.Count > 0) {
						GameObject lastStep = (GameObject) steps[steps.Count - 1];
						lastPosition = lastStep.transform.position;
					}

					Vector3 position = GetMousePosition();

					Vector3 direction = position - lastPosition;

					Quaternion rotation = new Quaternion(); //DirectionToRotation(direction);

					if (direction.magnitude > 0.5f) {
						GameObject step = (GameObject) Instantiate(stepPrefab, position, rotation);
						step.transform.localScale = DirectionToScale(direction);
						steps.Add(step);
					}
				}
			} else {
				indicator.renderer.enabled = false;
				mouseDown = false;
			}
		}
	}

	// Covert direction to rotation
	Quaternion DirectionToRotation(Vector3 direction) {
		float angle = Mathf.Atan2(direction.x, direction.y);
		Quaternion rotation = new Quaternion();
		rotation.eulerAngles = new Vector3(0, 0, -Mathf.Rad2Deg * angle);
		return rotation;
	}

	// Update timer and return if delayTime is over
	bool Tick() {
		bool result = false;

		timer += Time.deltaTime;

		if (timer > delayTime) {
			result = true;
			timer -= delayTime;
		}

		return result;
	}

	// This animal
	void Walk() {
		bool cancelStep = false;

		if (steps.Count > 0) {
			Vector3 position = transform.position;
			GameObject step = (GameObject) steps[0];
			Vector3 stepPosition = step.transform.position;

			Vector3 direction = stepPosition - position;

			if (direction.magnitude < 0.1f) {
				transform.position = stepPosition;
				DestroyObject(step);
				steps.RemoveAt(0);

				cancelStep = true;
			} else {
				walkingDirection = direction;
			}
		}

		if (!cancelStep) {
			Vector3 movement = walkingDirection.normalized * speed * Time.deltaTime;
			transform.Translate(movement);

			// set rotation
			//character.transform.rotation = DirectionToRotation(walkingDirection);

			character.transform.localScale = DirectionToScale(walkingDirection);
		}
	}

	// Die
	void Die() {
		state = CharacterState.DIE;
		character.GetComponent<Animator>().Play("MCDie");
		GameController.GetInstance().GameOver();
	}

	// Get Mouse Position
	Vector3 GetMousePosition() {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = -Camera.main.transform.position.z;

		return Camera.main.ScreenToWorldPoint(mousePosition);
	}

	Vector3 DirectionToScale(Vector3 direction){
		Vector3 right = new Vector3(1.0f, 0.0f, 0.0f);
		float dot = Vector3.Dot(right, direction);
		dot = dot > 0.0f ? -1.0f : 1.0f;
		return new Vector3(dot, 1.0f, 1.0f);
	}

	//
	public void Update() {

		if (!isPaused) {
			if (GameController.GetInstance().IsPaused()) {
				character.GetComponent<Animator>().Play("MCFeed");
				isPaused = true;
			}
		} else {
			if (!GameController.GetInstance().IsPaused() && state == CharacterState.WALKING) {
				character.GetComponent<Animator>().Play("MCWalk");
				isPaused = false;
			}
		}

		if (!isPaused) {
			ConditionalUpdate();
		}

		if (GameController.GetInstance().IsGameOver() && state == CharacterState.WALKING) {
			if (cryObject) {
				Instantiate(cryObject, transform.position, transform.rotation);
				DestroyObject(gameObject);
			}
		}
	}



}
