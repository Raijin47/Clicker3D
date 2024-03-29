using UnityEngine;

public class ParticleUIService : MonoBehaviour
{
    [SerializeField] private ParticleSystem _goldParticle;
    [SerializeField] private ParticleSystem _diamondParticle;
    [SerializeField] private ParticleSystem _prestigeParticle;

    [SerializeField] private RectTransform _goldTransform;
    [SerializeField] private RectTransform _diamondTransform;
    [SerializeField] private RectTransform _prestigeTransform;

    public ParticleSystem GoldParticle => _goldParticle;
    public ParticleSystem DiamondParticle => _diamondParticle;
    public ParticleSystem PrestigeParticle => _prestigeParticle;

    public RectTransform GoldTransform => _goldTransform;
    public RectTransform DiamondTransform => _diamondTransform;
    public RectTransform PrestigeTransform => _prestigeTransform;
}