﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterAnimation : MonoBehaviour {

	private bool _readyToFade;
	private float _startTime;
	private float _fadeDuration = 2f;
    private Crosshair _crosshair;

    private SpriteRenderer _spriteRenderer;

	void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update () {
		FadeSplatter ();
	}

	public void StartFade(Color crosshairColor, Crosshair crosshair)
	{
		_readyToFade = true;
		_startTime = Time.time;
		_spriteRenderer.color = crosshairColor;
        _crosshair = crosshair;
    }

	private void FadeSplatter()
	{
		if(!_readyToFade)
			return;


		float currentTime = Time.time;
		if(currentTime <= _startTime + _fadeDuration)
		{
			float ratio = (currentTime - _startTime) / _fadeDuration;
			byte alpha = (byte)(255f - ratio * 255);
			_spriteRenderer.color = new Color(_spriteRenderer.color.r, 
				_spriteRenderer.color.g,
				_spriteRenderer.color.b,
				alpha);
		}
		else
		{
			_spriteRenderer.color = new Color(_spriteRenderer.color.r, 
				_spriteRenderer.color.g,
				_spriteRenderer.color.b,
				255f);
			_readyToFade = false;

            _spriteRenderer.enabled = false;
            _crosshair.ReturnToPool(this.gameObject);
        }
	}
}
