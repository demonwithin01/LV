using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicEnemy : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    [SerializeField]
    private float _movementSpeed = 1f;

    [SerializeField]
    private float _maxDistance = 24f;

    [SerializeField]
    private int _bulletLimit = 3;

    [SerializeField]
    private GameObject _bulletPrefab;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    private List<EnemyBullet> _bulletPool;

    private int _bulletPoolIndex = 0;

    private Path _flightPath;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        this._bulletPool = new List<EnemyBullet>();
        this._flightPath = new Path();

        for ( int i = 0 ; i < this._bulletLimit ; i++ )
        {
            GameObject enemyBullet = Instantiate( this._bulletPrefab );
            EnemyBullet enemyBulletScript = enemyBullet.GetComponent<EnemyBullet>();
            enemyBulletScript.Initialise( this );

            this._bulletPool.Add( enemyBulletScript );
        }

        this._flightPath.Initialise( this );
    }

    // Update is called once per frame
    void Update()
    {

        this._flightPath.Update();
    }

    void OnTriggerEnter( Collider collider )
    {
        if ( collider.gameObject.layer == Layer.PlayerBullet )
        {
            collider.gameObject.SetActive( false );

            Destroy();
        }

    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    public void Fire()
    {
        this._bulletPool[ this._bulletPoolIndex ].Fire( base.transform.position, Vector3.back );

        this._bulletPoolIndex = ( ++this._bulletPoolIndex ) % this._bulletPool.Count;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    private void Destroy()
    {
        base.gameObject.SetActive( false );
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    public float MovementSpeed
    {
        get
        {
            return this._movementSpeed;
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

}
