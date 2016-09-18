using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class RGBLEDPresenter : MonoBehaviour {

  [SerializeField] private Light connectionLight;
  [SerializeField] private GameObject mainLED;
  [SerializeField] private Button updateModelButton;

  private RGBLEDViewModel viewModel;

  void Awake() {
    // Setup a new model (this would come from DB or external service in reality)
    RGBLED model = new RGBLED("SAM RGB LED", Color.magenta, 0, "disconnected");
    viewModel = new RGBLEDViewModel(model);
  }

  void Start() {
    // React to model changes
    viewModel.ConnectionState
      .Where(s => s == "disconnected")
      .Select(_ => Color.red)
      .Subscribe(c => connectionLight.color = c)
      .AddTo(this);

    viewModel.ConnectionState
      .Where(s => s == "connected")
      .Select(_ => Color.green)
      .Subscribe(c => connectionLight.color = c)
      .AddTo(this);

    viewModel.Color
      .Subscribe(c => mainLED.GetComponent<Renderer>().material.color = c)
      .AddTo(this);

    // Listen for UI interactions
    updateModelButton.OnClickAsObservable()
      .Subscribe(UpdateModel)
      .AddTo(this);
  }


  private void UpdateModel(Unit _) {
    var randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    viewModel.Model = new RGBLED("SAM RGB LED", randomColor, 0, "connected");
  }
}
