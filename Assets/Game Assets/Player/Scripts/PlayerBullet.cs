using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    private float _maxDistance = 150f;

    private float _force = 40f;

    private Vector3 _fireFrom;

    private Vector3 _direction;

    private ShipControl _ship;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    public void Initialise( ShipControl ship )
    {
        this._ship = ship;
        this.gameObject.SetActive( false );
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( Vector3.Distance( this._fireFrom, base.transform.position ) > this._maxDistance )
        {
            this.gameObject.SetActive( false );
        }
        else
        {
            base.transform.position += this._direction;
        }
    }
    
    #endregion
    
    /* --------------------------------------------------------------------- */

    #region Public Methods

    public void Fire( Vector3 fireFrom, Vector3 direction )
    {
        this._fireFrom = fireFrom;
        this._direction = direction;

        base.transform.position = fireFrom;
        
        base.gameObject.SetActive( true );
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* --------------------------------------------------------------------- */

}
