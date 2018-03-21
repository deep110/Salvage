using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour {

    public Color starColor;
    public int starsMax = 600;
    public float starSize = 0.35f;
    public float starDistance = 60f;
    public float starClipDistance = 15f;

    private Transform _transform;
    private ParticleSystem.Particle[] points;
    private float starDistanceSqr;
    private float starClipDistanceSqr;

    void Start() {
        _transform = GetComponent<Transform>();

        starDistanceSqr = starDistance * starDistance;
        starClipDistanceSqr = starClipDistance * starClipDistance;

		createStars();
    }

    private void createStars() {
        points = new ParticleSystem.Particle[starsMax];

        for (int i = 0; i < starsMax; i++) {
            points[i].position = Random.insideUnitCircle * starDistance;
            points[i].startColor = new Color(starColor.r, starColor.g, starColor.b, starColor.a);
            points[i].startSize = starSize;
        }
    }

    void Update() {

        for (int i = 0; i < starsMax; i++) {
			float pointDis = (points[i].position - _transform.position).sqrMagnitude;

			if (pointDis > starDistanceSqr) {
				points[i].position = Random.insideUnitCircle.normalized * starDistance + (Vector2)_transform.position;
			}

			if (pointDis <= starClipDistanceSqr) {
				float percentage = pointDis / starClipDistanceSqr;
				points[i].startColor = new Color(1,1,1,percentage);
				points[i].startSize = starSize * percentage;
			}
        }
		GetComponent<ParticleSystem>().SetParticles(points, starsMax);
    }
}
