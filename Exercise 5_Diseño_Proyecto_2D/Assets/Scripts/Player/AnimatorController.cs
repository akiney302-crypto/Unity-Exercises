using UnityEngine;

[RequireComponent(typeof(Animator))]
    
public class AnimatorController : MonoBehaviour

{
    #region Fields
    [SerializeField] private Wings _wings;
    private Animator _anim;
    #endregion


    #region Unity Callbacks
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        _anim.SetBool("IsFlying", _wings.IsFlying);
    }
    #endregion


    #region Public Methods
    
    #endregion
}
