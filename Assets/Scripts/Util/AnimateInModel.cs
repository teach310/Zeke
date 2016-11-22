using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Common.Animation
{
	public class AnimateInModel : MonoBehaviour {

		/// <summary>
		/// The start point. localPosition
		/// </summary>
		public Vector3 startPoint;

		public Vector3 EndPoint{ get; private set;}

		void Reset(){
			startPoint = Vector3.right * 300;
		}

		public void Init(){
			EndPoint = this.transform.position;
			this.transform.position = (EndPoint + startPoint);
		}
	}
}

