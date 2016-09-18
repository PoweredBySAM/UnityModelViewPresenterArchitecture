using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UniRx;

[TestFixture]
public class RGBLEDViewModelTests {

  private RGBLEDViewModel vm;
  private CompositeDisposable disposables = new CompositeDisposable();

  [SetUp]
  public void Init() {
    var model = new RGBLED("SAM RGB LED", Color.black, 0, "connected");
    vm = new RGBLEDViewModel(model);
  }

  [Test]
  public void TestViewModelUpdatesColorWhenModelChanges() {
    vm.Color.Skip(1)
      .Subscribe(color => Assert.That(color == Color.blue))
      .AddTo(disposables);

    vm.Model = new RGBLED("SAM RGB LED", Color.blue, 0, "connected");
  }

  [Test]
  public void TestViewModelUpdatesConnectionStateWhenModelChanges() {
    vm.ConnectionState.Skip(1)
      .Subscribe(state => Assert.That(state == "disconnected"))
      .AddTo(disposables);

    vm.Model = new RGBLED("SAM RGB LED", Color.blue, 0, "disconnected");
  }

  [TestFixtureTearDown]
  public void FixtureTearDown() {
    disposables.Dispose();
  }
}
