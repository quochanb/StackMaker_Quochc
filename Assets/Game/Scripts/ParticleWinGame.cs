using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWinGame : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        Player.winGameEvent += OnPlayParticle;
    }

    private void OnDisable()
    {
        Player.winGameEvent -= OnPlayParticle;
    }

    private void OnPlayParticle()
    {
        particle.Play();
    }
}
