using UnityEngine;
using System.Collections;

public struct RGBLED {
  public readonly string name;
  public readonly Color color;
  public readonly int brightness;
  public readonly string connectionState;

  public RGBLED(string name, Color color, int brightness, string connectionState) {
    this.name = name;
    this.color = color;
    this.brightness = brightness;
    this.connectionState = connectionState;
  }
}
