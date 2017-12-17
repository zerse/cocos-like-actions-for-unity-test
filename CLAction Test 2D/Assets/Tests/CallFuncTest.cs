using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

// CLCallFunc
//
namespace CocosLikeActions.Test.CallFunc {
  public class CallFuncTest : ActionTestBase {
    [UnityTest]
    public IEnumerator StartAsCoroutine_WillExecuteCallback() {
      int executionCount = 0;
      Action callback = () => { executionCount += 1; };
      var callFunc = new CLCallFunc( callback );

      // assert nothing started yet
      Assert.Zero( executionCount );
      Assert.False( callFunc.IsDone );

      // Wait for StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( callFunc.StartAsCoroutine( GameObject ) );

      // assert callback is executed only once
      Assert.NotZero( executionCount );
      Assert.AreEqual( 1, executionCount );

      // assert action is done
      Assert.True( callFunc.IsDone );
    }

    [UnityTest]
    public IEnumerator Clone_WillCreateClonedCallFunc() {
      int executionCount = 0;
      Action callback = () => { executionCount += 1; };
      var source = new CLCallFunc( callback );
      var cloned = source.Clone();

      // assert nothing started yet
      Assert.Zero( executionCount );
      Assert.False( source.IsDone );
      Assert.False( cloned.IsDone );

      // Wait for StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( cloned.StartAsCoroutine( GameObject ) );

      // assert cloned callback is executed only once
      Assert.NotZero( executionCount );
      Assert.AreEqual( 1, executionCount );

      // assert source action is not done
      Assert.False( source.IsDone );

      // assert cloned action is done
      Assert.True( cloned.IsDone );
    }

    [UnityTest]
    public IEnumerator Reverse_WillCreateReversedCallFunc() {
      bool executed = false;
      bool reverseExecuted = false;
      Action callback = () => { executed = true; };
      Action reverseCallback = () => { reverseExecuted = true; };
      var source = new CLCallFunc( callback, reverseCallback );
      var reversed = source.Reverse();

      // assert nothing started yet
      Assert.False( executed );
      Assert.False( reverseExecuted );
      Assert.False( source.IsDone );
      Assert.False( reversed.IsDone );

      // Wait for reversed.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( reversed.StartAsCoroutine( GameObject ) );

      // assert callback is not executed
      Assert.False( executed );

      // assert reverseCallback is executed
      Assert.True( reverseExecuted );

      // assert source action is not done
      Assert.False( source.IsDone );

      // assert reversed action is done
      Assert.True( reversed.IsDone );
    }
  }
}

// CLCallFuncN
//
namespace CocosLikeActions.Test.CallFunc {
  public class CallFuncNTest : ActionTestBase {
    [UnityTest]
    public IEnumerator StartAsCoroutine_WillExecuteCallbackWithTargetGameObject() {
      GameObject targetGameObject = null;
      Action<GameObject> callback = (GameObject go) => { targetGameObject = go; };
      var callFuncN = new CLCallFuncN( callback );

      // assert nothing started yet
      Assert.Null( targetGameObject );
      Assert.False( callFuncN.IsDone );

      // Wait for StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( callFuncN.StartAsCoroutine( GameObject ) );

      // assert callback is executed and Target is Correct
      Assert.AreSame( GameObject, targetGameObject );

      // assert action is done
      Assert.True( callFuncN.IsDone );
    }

    [UnityTest]
    public IEnumerator Clone_WillCreateClonedCallFuncN() {
      GameObject targetGameObject = null;
      Action<GameObject> callback = ( GameObject go ) => { targetGameObject = go; };
      var source = new CLCallFuncN( callback );
      var cloned = source.Clone();

      // assert cloned action is CLCallFuncN
      Assert.NotNull( cloned );
      Assert.IsInstanceOf<CLCallFuncN>( cloned );

      // assert nothing started yet
      Assert.Null( targetGameObject );
      Assert.False( source.IsDone );
      Assert.False( cloned.IsDone );

      // Wait for cloned.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( cloned.StartAsCoroutine( GameObject ) );

      // assert cloned action is executed and Target is Correct
      Assert.AreSame( GameObject, targetGameObject );

      // assert source action is not done
      Assert.False( source.IsDone );

      // assert cloned action is done
      Assert.True( cloned.IsDone );
    }

    [UnityTest]
    public IEnumerator Reverse_WillCreateReversedCallFuncN() {
      GameObject targetGameObject = null;
      GameObject reverseGameObject = null;
      Action<GameObject> callback = ( GameObject go ) => { targetGameObject = go; };
      Action<GameObject> reverseCallback = ( GameObject go ) => { reverseGameObject = go; };
      var source = new CLCallFuncN( callback, reverseCallback );
      var reversed = source.Reverse();

      // assert reversed action is CLCallFuncN
      Assert.NotNull( reversed );
      Assert.IsInstanceOf<CLCallFuncN>( reversed );

      // assert nothing started yet
      Assert.Null( targetGameObject );
      Assert.Null( reverseGameObject );
      Assert.False( source.IsDone );
      Assert.False( reversed.IsDone );

      // Wait for reversed.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( reversed.StartAsCoroutine( GameObject ) );

      // assert reversed action is executed and Target is Correct
      Assert.AreSame( GameObject, reverseGameObject );

      // assert callback is not executed
      Assert.Null( targetGameObject );

      // assert source action is not done
      Assert.False( source.IsDone );

      // assert reversed action is done
      Assert.True( reversed.IsDone );
    }
  }
}

// CLCallFuncO
//
namespace CocosLikeActions.Test.CallFunc {
  public class CallFuncOTest : ActionTestBase {
    [UnityTest]
    public IEnumerator StartAsCoroutine_WillExecuteCallbackWithGivenObject() {
      object obj = new object();
      object givenObject = null;
      Action<object> callback = ( object o ) => { givenObject = o; };
      var callFuncO = new CLCallFuncO( callback, obj );

      // assert nothing started yet
      Assert.Null( givenObject );
      Assert.False( callFuncO.IsDone );

      // Wait for StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( callFuncO.StartAsCoroutine( GameObject ) );

      // assert callback is executed and given object is Correct
      Assert.AreSame( obj, givenObject );

      // assert action is done
      Assert.True( callFuncO.IsDone );
    }

    [UnityTest]
    public IEnumerator Clone_WillCreateClonedCallFuncO() {
      object obj = new object();
      object givenObject = null;
      Action<object> callback = ( object o ) => { givenObject = o; };
      var source = new CLCallFuncO( callback, obj );
      var cloned = source.Clone();
      
      // assert cloned action is CLCallFuncO
      Assert.NotNull( cloned );
      Assert.IsInstanceOf<CLCallFuncO>( cloned );

      // assert nothing started yet
      Assert.Null( givenObject );
      Assert.False( source.IsDone );
      Assert.False( cloned.IsDone );
      
      // Wait for cloned.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( cloned.StartAsCoroutine( GameObject ) );

      // assert cloned action is executed and given object is Correct
      Assert.AreSame( obj, givenObject );

      // assert source action is not done
      Assert.False( source.IsDone );

      // assert cloned action is done
      Assert.True( cloned.IsDone );
    }

    [UnityTest]
    public IEnumerator Reverse_WillCreateReversedCallFuncO() {
      object obj = new object();
      object givenObject = null;
      object reverseObject = null;
      Action<object> callback = ( object o ) => { givenObject = o; };
      Action<object> reverseCallback = ( object o ) => { reverseObject = o; };
      var source = new CLCallFuncO( callback, reverseCallback, obj );
      var reversed = source.Reverse();
      
      // assert reversed action is CLCallFuncO
      Assert.NotNull( reversed );
      Assert.IsInstanceOf<CLCallFuncO>( reversed );

      // assert nothing started yet
      Assert.Null( givenObject );
      Assert.Null( reverseObject );
      Assert.False( source.IsDone );
      Assert.False( reversed.IsDone );

      // Wait for reversed.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( reversed.StartAsCoroutine( GameObject ) );

      // assert reversed action is executed and Target is Correct
      Assert.AreSame( obj, reverseObject );

      // assert callback is not executed
      Assert.Null( givenObject );

      // assert source action is not done
      Assert.False( source.IsDone );

      // assert reversed action is done
      Assert.True( reversed.IsDone );
    }
  }
}

// CLCallFuncND
//
namespace CocosLikeActions.Test.CallFunc {
  public class CallFuncNDTest : ActionTestBase {
    [UnityTest]
    public IEnumerator StartAsCoroutine_WillExecuteCallbackWithTargetAndGivenData() {
      GameObject targetGameObject = null;
      int sourceData = 1;
      int givenData = 0;
      Action<GameObject, int> callback = ( GameObject go, int d ) => { targetGameObject = go; givenData = d; };
      var callFuncND = new CLCallFuncND<int>( callback, sourceData );

      // assert nothing started yet
      Assert.Null( targetGameObject );
      Assert.Zero( givenData );
      Assert.False( callFuncND.IsDone );

      // Wait for StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( callFuncND.StartAsCoroutine( GameObject ) );

      // assert callback is executed and Target, Data is Correct
      Assert.AreSame( GameObject, targetGameObject );
      Assert.AreEqual( sourceData, givenData );

      // assert action is done
      Assert.True( callFuncND.IsDone );
    }

    [UnityTest]
    public IEnumerator Clone_WillCreateClonedCallFuncND() {
      GameObject targetGameObject = null;
      object sourceData = new object();
      object givenData = null;
      Action<GameObject, object> callback = ( GameObject go, object d ) => { targetGameObject = go; givenData = d; };
      var source = new CLCallFuncND<object>( callback, sourceData );
      var cloned = source.Clone();
      
      // assert cloned action is CLCallFuncND
      Assert.NotNull( cloned );
      Assert.IsInstanceOf<CLCallFuncND<object>>( cloned );

      // assert nothing started yet
      Assert.Null( targetGameObject );
      Assert.Null( givenData );
      Assert.False( source.IsDone );
      Assert.False( cloned.IsDone );

      // Wait for cloned.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( cloned.StartAsCoroutine( GameObject ) );

      // assert cloned action is executed and Target, Data is Correct
      Assert.AreSame( GameObject, targetGameObject );
      Assert.AreSame( sourceData, givenData );

      // assert source action is not done
      Assert.False( source.IsDone );

      // assert cloned action is done
      Assert.True( cloned.IsDone );
    }

    [UnityTest]
    public IEnumerator Reverse_WillCreateReversedCallFuncND() {
      GameObject targetGameObject = null;
      GameObject reverseGameObject = null;
      object sourceData = new object();
      object givenData = null;
      object reverseData = null;
      Action<GameObject, object> callback = ( GameObject go, object d ) => { targetGameObject = go; givenData = d; };
      Action<GameObject, object> reverseCallback = ( GameObject go, object d ) => { reverseGameObject = go; reverseData = d; };
      var source = new CLCallFuncND<object>( callback, reverseCallback, sourceData );
      var reversed = source.Reverse();
      
      // assert reversed action is CLCallFuncN
      Assert.NotNull( reversed );
      Assert.IsInstanceOf<CLCallFuncND<object>>( reversed );

      // assert nothing started yet
      Assert.Null( targetGameObject );
      Assert.Null( reverseGameObject );
      Assert.Null( givenData );
      Assert.Null( reverseData );
      Assert.False( source.IsDone );
      Assert.False( reversed.IsDone );

      // Wait for reversed.StartAsCoroutine to be finished
      yield return MonoBehaviour.StartCoroutine( reversed.StartAsCoroutine( GameObject ) );

      // assert reversed action is executed and Target, Data is Correct
      Assert.AreSame( GameObject, reverseGameObject );
      Assert.AreSame( sourceData, reverseData );

      // assert callback is not executed
      Assert.Null( targetGameObject );
      Assert.Null( givenData );

      // assert source action is not done
      Assert.False( source.IsDone );

      // assert reversed action is done
      Assert.True( reversed.IsDone );
    }
  }
}
