using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    [SerializeField]
    private float _delay = 0f;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    private BasicEnemy _attachedTo;

    private float _lerpValue = 0f;

    private float _lerpModifier = 1f;

    private Vector3 _lerpStart;
    
    private PathPoint _currentPathPoint;

    private PathPoint _enterScreenPathPoint;

    private int _pathIndex = -1;

    private List<PathPoint> _pathPoints;

    private float _distance = 0f;

    private float _waitTime = 0f;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    public Path()
    {
        this._pathPoints = new List<PathPoint>();
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    // Use this for initialization
    public void Initialise( BasicEnemy attachedTo )
    {
        this._pathPoints = new List<PathPoint>();

        Debug.Log( "Path Start" );

        // DEBUG
        this._enterScreenPathPoint = new PathPoint( 0, 0, 100f );

        //this._pathPoints.Add( new PathPoint( 10f, 0f, 100f, 0.5f ) );
        //this._pathPoints.Add( new PathPoint( 0, 0f, 100f ) );
        //this._pathPoints.Add( new PathPoint( -10f, 0f, 100f, 0.5f ) );

        /** END DEBUG **/


        this._currentPathPoint = this._enterScreenPathPoint;
        this._lerpStart = attachedTo.transform.position;

        this._attachedTo = attachedTo;

        this._distance = Vector3.Distance( this._lerpStart, this._enterScreenPathPoint.EndLocation );
    }

    // Update is called once per frame
    public void Update()
    {
        if ( this._delay > 0f )
        {
            this._delay -= Time.deltaTime;

            return;
        }

        float speedModifier = this._attachedTo.MovementSpeed / this._distance;

        this._lerpValue += ( Time.deltaTime * this._lerpModifier * speedModifier );

        Vector3 position = Vector3.Lerp( this._lerpStart, this._currentPathPoint.EndLocation, this._lerpValue );

        this._attachedTo.transform.position = position;

        if ( this._currentPathPoint.HasFired == false && this._currentPathPoint.HasFirePoint && this._lerpValue >= this._currentPathPoint.FireTime )
        {
            this._currentPathPoint.HasFired = true;

            this._attachedTo.Fire();
        }

        if ( _lerpValue >= 1f )
        {
            if ( _waitTime < this._currentPathPoint.WaitTime )
            {
                this._waitTime += Time.deltaTime;

                return;
            }

            this._pathIndex = ( this._pathIndex + 1 ) % this._pathPoints.Count;
            this._waitTime = 0f;

            PathPoint nextPoint = this._pathPoints[ this._pathIndex ];

            nextPoint.HasFired = false;

            this._lerpStart = this._currentPathPoint.EndLocation;
            this._distance = Vector3.Distance( this._lerpStart, nextPoint.EndLocation );
            this._lerpValue = 0;

            this._currentPathPoint = nextPoint;
        }
    }

    /// <summary>
    /// Adds a new point to the path list.
    /// </summary>
    /// <param name="point">The point to add to the list of points.</param>
    public void Add( PathPoint point )
    {
        if ( this._enterScreenPathPoint == null )
        {
            this._enterScreenPathPoint = point;
            return;
        }

        this._pathPoints.Add( point );
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
