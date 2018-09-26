﻿using UnityEngine;
using ShootAR.Enemies;

namespace ShootAR.TestTools
{
	/// <summary>
	/// Test class for replacing enemy classes in tests.
	/// </summary>
	class TestEnemy : Enemy
	{
		public static TestEnemy Create(
			float speed = default(float),
			int damage = default(int),
			int pointsValue = default(int),
			float x = 0, float y = 0, float z = 0,
			GameState gameState = null)
		{
			var o = new GameObject(nameof(TestEnemy)).AddComponent<TestEnemy>();
			o.Speed = speed;
			o.Damage = damage;
			o.PointsValue = pointsValue;
			o.transform.position = new Vector3(x, y, z);
			o.gameState = gameState;
			return o;
		}

		protected override void OnDestroy() { ActiveCount--; }
	}

	/// <summary>
	/// Bullet to test hitting player
	/// </summary>
	internal class TestBullet : MonoBehaviour
	{
		int damage;
		public bool hit;

		public static TestBullet Create(int damage)
		{
			var o = new GameObject("Bullet").AddComponent<TestBullet>();
			o.damage = damage;
			return o;
		}

		private void Start()
		{
			var collider = gameObject.AddComponent<SphereCollider>();
			collider.isTrigger = true;

			gameObject.AddComponent<Rigidbody>();
		}

		private void OnTriggerEnter(Collider other)
		{
			Debug.Log("Bullet hit!");
			other.GetComponent<Player>().GetDamaged(damage);
			hit = true;
		}
	}

	class TestShooter : Pyoopyoo { private new void OnDestroy() { ActiveCount--; } }
	class TestMeleer : Boopboop { private new void OnDestroy() { ActiveCount--; } }

	/// <summary>
	/// Target for testing purposes.
	/// </summary>
	class TestTarget : Enemy
	{
		public bool GotHit { get; private set; }

		public void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<Bullet>() != null) GotHit = true;
		}

		private new void Start()
		{
			gameObject.AddComponent<SphereCollider>();
		}
	}
}
