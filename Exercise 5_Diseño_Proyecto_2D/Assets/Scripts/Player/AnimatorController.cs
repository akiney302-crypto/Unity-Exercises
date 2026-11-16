using UnityEngine;

[RequireComponent(typeof(Animator))]
    
public class AnimatorController : MonoBehaviour

{
    #region Fields
    [SerializeField] private Wings wings;
    private Animator anim;
    #endregion


    #region Unity Callbacks
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (wings.IsFlying)
        {
            SetFlying(true);
        }
        else
        {
            SetFlying(false);
        }
    }
    #endregion


    #region Public Methods
    public void SetFlying(bool isFlying)
    {
        anim.SetBool("IsFlying", isFlying);
    }
    #endregion
}
