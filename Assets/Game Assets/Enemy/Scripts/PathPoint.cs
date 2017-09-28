using UnityEngine;
using System.Collections;

public class PathPoint
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members
        
    /// <summary>
    /// Holds the end location of the path point.
    /// </summary>
    private Vector3 _endLocation;

    private bool _hasFirePoint = false;
    
    private float _fireTime = 0.5f;

    /// <summary>
    /// Holds the amount of time the unit should wait before leaving the end location for the next point.
    /// </summary>
    private float _waitTime = 0f;
    
    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    public PathPoint( float x, float y, float z )
    {
        this._endLocation = new Vector3( x, y, z );
    }

    public PathPoint( float x, float y, float z, float fireTime )
    {
        this._endLocation = new Vector3( x, y, z );
        this._fireTime = fireTime;
        this._hasFirePoint = true;
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

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    public bool HasFired { get; internal set; }

    /// <summary>
    /// Gets the end location of the unit for this path point.
    /// </summary>
    public Vector3 EndLocation { get { return this._endLocation; } }

    public bool HasFirePoint { get { return this._hasFirePoint; } }

    public float FireTime { get { return this._fireTime; } }

    /// <summary>
    /// Gets the amount of time the unit should wait before moving on to the next point.
    /// </summary>
    public float WaitTime { get { return this._waitTime; } }
    
    #endregion

    /* --------------------------------------------------------------------- */

}
