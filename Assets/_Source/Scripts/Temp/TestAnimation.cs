using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;



    public void SetAnimation(string id)
    {
        _animator.Play("AA_Club_Dance_Moves_Type" + id); 
    }
}