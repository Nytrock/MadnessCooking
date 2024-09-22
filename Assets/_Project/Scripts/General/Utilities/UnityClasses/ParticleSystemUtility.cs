using UnityEngine;

public static class ParticleSystemUtility {
    public static void ChangeState(this ParticleSystem particleSystem, bool newValue) {
        if (newValue)
            particleSystem.Play();
        else
            particleSystem.Stop();
    }

    public static void SetSimulationSpeed(this ParticleSystem particleSystem, float simulationSpeed) {
        ParticleSystem.MainModule main = particleSystem.main;
        main.simulationSpeed = simulationSpeed;
    }

    public static void SetColor(this ParticleSystem particleSystem, Color color) {
        ParticleSystem.MainModule main = particleSystem.main;
        main.startColor = color;
    }
}
