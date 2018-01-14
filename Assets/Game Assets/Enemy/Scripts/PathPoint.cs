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

    /// <summary>
    /// Holds whether or not this path point causes the enemy to fire.
    /// </summary>
    private bool _hasFirePoint = false;

    /// <summary>
    /// Holds the percentage within the lifecyle of travelling to this point that the enemy fires its weapon.
    /// </summary>
    private float _fireTime = 0.5f;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    /// <summary>
    /// Constructor used for aiding in the creation of clones.
    /// </summary>
    private PathPoint()
    {

    }

    /// <summary>
    /// Creates a path point instance without a fire point.
    /// </summary>
    /// <param name="x">The end x co-ordinate.</param>
    /// <param name="y">The end y co-ordinate.</param>
    /// <param name="z">The end z co-ordinate.</param>
    public PathPoint( float x, float y, float z )
    {
        this._endLocation = new Vector3( x, y, z );
    }

    /// <summary>
    /// Creates a path point instance with a fire point.
    /// </summary>
    /// <param name="x">The end x co-ordinate.</param>
    /// <param name="y">The end y co-ordinate.</param>
    /// <param name="z">The end z co-ordinate.</param>
    /// <param name="fireTime">The lerp position within the travel to fire the weapon.</param>
    public PathPoint( float x, float y, float z, float fireTime )
    {
        this._endLocation = new Vector3( x, y, z );
        this._fireTime = fireTime;
        this._hasFirePoint = true;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Creates a clone of the path point.
    /// </summary>
    /// <returns>The clone path point.</returns>
    public PathPoint Clone()
    {
        PathPoint clone = new PathPoint();

        clone._endLocation = this._endLocation;
        clone._fireTime = this._fireTime;
        clone._hasFirePoint = this._hasFirePoint;

        return clone;
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

    public bool HasFired { get; internal set; }

    /// <summary>
    /// Gets the end location of the unit for this path point.
    /// </summary>
    public Vector3 EndLocation { get { return this._endLocation; } }

    /// <summary>
    /// Gets whether or not this path point causes the enemy to fire.
    /// </summary>
    public bool HasFirePoint { get { return this._hasFirePoint; } }

    /// <summary>
    /// Gets the percentage within the lifecyle of travelling to this point that the enemy fires its weapon.
    /// </summary>
    public float FireTime { get { return this._fireTime; } }

    /// <summary>
    /// Gets the amount of time the unit should wait before moving on to the next point.
    /// </summary>
    public float WaitTime { get; set; }

    #endregion

    /* --------------------------------------------------------------------- */

}
