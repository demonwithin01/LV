using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipControl : MonoBehaviour
{
    /* --------------------------------------------------------------------- */

    #region Editable Fields

    [SerializeField]
    private float _maxDistance = 24f;
    
    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    private PlayerBulletPool _bulletPool;

    private float _movementSpeed = 0.5f;

    private float _timeBetweenShots = 0.2f;

    private float _timeTillNextFire = 0f;
    
    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        this._bulletPool = GameObject.FindObjectOfType<PlayerBulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    #endregion
    
    /* --------------------------------------------------------------------- */

    #region Public Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    private void HandleInput()
    {
        if ( Input.GetKey( KeyCode.DownArrow ) )
        {
            ChangePositionBy( this._movementSpeed );
        }
        else if ( Input.GetKey( KeyCode.UpArrow ) )
        {
            ChangePositionBy( -this._movementSpeed );
        }

        if ( Input.GetKey( KeyCode.Space ) && this._timeTillNextFire <= 0f )
        {
            FireWeapon();
        }

        this._timeTillNextFire -= Time.deltaTime;
    }

    private void ChangePositionBy( float x )
    {
        x = base.transform.position.x + x;
        x = Mathf.Clamp( x, -this._maxDistance, this._maxDistance );

        base.transform.position = new Vector3( x, base.transform.position.y, base.transform.position.z );
    }

    private void FireWeapon()
    {
        this._timeTillNextFire = this._timeBetweenShots;

        Vector3 distance = new Vector3( 2.8f, 0f, 0f );

        LaunchBulletFrom( base.transform.position + distance );
        LaunchBulletFrom( base.transform.position - distance );
    }

    private void LaunchBulletFrom( Vector3 position )
    {
        this._bulletPool.Next.Fire( position, Vector3.forward );
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* --------------------------------------------------------------------- */

}
