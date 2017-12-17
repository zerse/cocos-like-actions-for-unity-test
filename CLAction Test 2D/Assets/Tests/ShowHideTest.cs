using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace CocosLikeActions.Test.ShowHide {
  public abstract class ShowHideTestBase : ActionTestBase {
    public override void SetUp() {
      base.SetUp();

      // let GameObject has Renderer
      var texture = new Texture2D( 1, 1 );
      var pixel = new Color( 1.0f, 1.0f, 1.0f, 1.0f );
      texture.SetPixel( 0, 0, pixel );
      texture.Apply();

      SpriteRenderer spriteRenderer = GameObject.AddComponent<SpriteRenderer>();

      var rect = new Rect( 0f, 0f, 1f, 1f );
      spriteRenderer.sprite = Sprite.Create( texture, rect, Vector2.zero );
    }
  }
}

// CLHide
//
namespace CocosLikeActions.Test.ShowHide {
  public class HideTest : ShowHideTestBase {
    [UnityTest]
    public IEnumerator StartAsCoroutine_WillSetGameObjectNotVisible() {
      var renderer = GameObject.GetComponent<Renderer>();
      var hide = new CLHide();

      // validate gameObject's renderer is ready
      Assert.NotNull( renderer );
      Assert.True( renderer.enabled );

      // Wait for StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( hide.StartAsCoroutine( GameObject ) );

      // assert hide action is applied
      Assert.False( renderer.enabled );
      Assert.True( hide.IsDone );
    }

    [UnityTest]
    public IEnumerator Clone_WillCreateClonedHide() {
      var renderer = GameObject.GetComponent<Renderer>();
      var source = new CLHide();
      var cloned = source.Clone();

      // assert cloned action is CLHide
      Assert.NotNull( cloned );
      Assert.IsInstanceOf<CLHide>( cloned );

      // validate gameObject's renderer is ready
      Assert.NotNull( renderer );
      Assert.True( renderer.enabled );

      // assert nothing started yet
      Assert.False( source.IsDone );
      Assert.False( cloned.IsDone );

      // Wait for cloned.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( cloned.StartAsCoroutine( GameObject ) );

      // assert cloned action is applied
      Assert.False( renderer.enabled );
      Assert.True( cloned.IsDone );

      // assert source action is not executed
      Assert.False( source.IsDone );
    }

    [UnityTest]
    public IEnumerator Reverse_WillCreateShowAction() {
      var renderer = GameObject.GetComponent<Renderer>();
      var hide = new CLHide();
      var reversed = hide.Reverse();

      // assert reversed action is CLShow
      Assert.NotNull( reversed );
      Assert.IsInstanceOf<CLShow>( reversed );

      // validate gameObject's renderer is ready
      Assert.NotNull( renderer );
      Assert.True( renderer.enabled );

      // assert nothing started yet
      Assert.False( hide.IsDone );
      Assert.False( reversed.IsDone );

      // Wait for hide.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( hide.StartAsCoroutine( GameObject ) );

      // assert hide action is applied
      Assert.False( renderer.enabled );
      Assert.True( hide.IsDone );

      // Wait for reversed.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( reversed.StartAsCoroutine( GameObject ) );

      // assert reversed action is applied
      Assert.True( renderer.enabled );
      Assert.True( reversed.IsDone );
    }
  }
}

// CLShow
//
namespace CocosLikeActions.Test.ShowHide {
  public class ShowTest : ShowHideTestBase {
    public override void SetUp() {
      base.SetUp();

      // set-up GameObject not visible
      var renderer = GameObject.GetComponent<Renderer>();
      renderer.enabled = false;
    }

    [UnityTest]
    public IEnumerator StartAsCoroutine_WillSetGameObjectVisible() {
      var renderer = GameObject.GetComponent<Renderer>();
      var show = new CLShow();

      // validate gameObject's renderer is ready
      Assert.NotNull( renderer );
      Assert.False( renderer.enabled );

      // Wait for StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( show.StartAsCoroutine( GameObject ) );

      // assert show is applied
      Assert.True( renderer.enabled );
      Assert.True( show.IsDone );
    }

    [UnityTest]
    public IEnumerator Clone_WillCreateClonedShow() {
      var renderer = GameObject.GetComponent<Renderer>();
      var source = new CLShow();
      var cloned = source.Clone();

      // assert cloned action is CLShow
      Assert.NotNull( cloned );
      Assert.IsInstanceOf<CLShow>( cloned );

      // validate gameObject's renderer is ready
      Assert.NotNull( renderer );
      Assert.False( renderer.enabled );

      // assert nothing started yet
      Assert.False( source.IsDone );
      Assert.False( cloned.IsDone );

      // Wait for cloned.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( cloned.StartAsCoroutine( GameObject ) );

      // assert cloned action is applied
      Assert.True( renderer.enabled );
      Assert.True( cloned.IsDone );

      // assert source action is not executed
      Assert.False( source.IsDone );
    }

    [UnityTest]
    public IEnumerator Reverse_WillCreateHideAction() {
      var renderer = GameObject.GetComponent<Renderer>();
      var show = new CLShow();
      var reversed = show.Reverse();

      // assert reversed action is CLHide
      Assert.NotNull( reversed );
      Assert.IsInstanceOf<CLHide>( reversed );

      // validate gameObject's renderer is ready
      Assert.NotNull( renderer );
      Assert.False( renderer.enabled );

      // assert nothing started yet
      Assert.False( show.IsDone );
      Assert.False( reversed.IsDone );

      // Wait for show.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( show.StartAsCoroutine( GameObject ) );

      // assert hide action is applied
      Assert.True( renderer.enabled );
      Assert.True( show.IsDone );

      // Wait for reversed.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( reversed.StartAsCoroutine( GameObject ) );

      // assert reversed action is applied
      Assert.False( renderer.enabled );
      Assert.True( reversed.IsDone );
    }
  }
}
