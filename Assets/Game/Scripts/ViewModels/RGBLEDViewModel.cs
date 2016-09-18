using UnityEngine;
using System.Collections;
using UniRx;

public class RGBLEDViewModel {

  public ReactiveProperty<RGBLED> ModelProperty { get; private set; }

  public RGBLED Model {
    get { return ModelProperty.Value; }
    set { ModelProperty.Value = value; }
  }
    
  public IObservable<Color> Color { get; private set; }
  public IObservable<int> Brightness { get; private set; }
  public IObservable<string> ConnectionState { get; private set; }

  public RGBLEDViewModel(RGBLED model) {
    ModelProperty = new ReactiveProperty<RGBLED>(model);
    Color = ModelProperty.Select(m => m.color);
    Brightness = ModelProperty.Select(m => m.brightness);
    ConnectionState = ModelProperty.Select(m => m.connectionState);
  }
}
