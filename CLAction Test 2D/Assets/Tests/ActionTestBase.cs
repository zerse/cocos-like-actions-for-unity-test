using NUnit.Framework;
using UnityEngine;

namespace CocosLikeActions.Test {
  public abstract class ActionTestBase {
    private class DummyMonoBehaviour : MonoBehaviour { }

    protected GameObject GameObject { get; private set; }
    protected MonoBehaviour MonoBehaviour { get; private set; }

    [SetUp]
    public virtual void SetUp() {
      GameObject = new GameObject();
      MonoBehaviour = GameObject.AddComponent<DummyMonoBehaviour>();
    }
  }
}
