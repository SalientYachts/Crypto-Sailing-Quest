using UnityEngine;
//using Invector.vCharacterController;
//using Invector.vCamera;
using System.Collections;

public class Navigate : MonoBehaviour
{
    public bool canReleaseHelm = true;

    // Speed
    public float boatMaxSpeed = 10f;
    public AnimationCurve mastBoostCurve;
    public AnimationCurve boatBoostCurve;
    public float boatMaxBackSpeed = 5f;
    public float boatAcceleration = 5f;

    // Boat Rotation
    public float boatTurnSpeed = 0.4f;
    public float shipWheelRotationSpeed = 200f;
    public float rudderRotationSpeed = 50f;
    public float rudderMaxAngle = 70f;

    // Masts Rotation
    public float mastRotationSpeed = 30f;
    public float mastMaxAngle = 75f;

    // Heel
    public float heelMaxForce = 14f;
    public AnimationCurve heelBoostCurve;
    public float heelMaxAngle = 30f;
    public float heelAcceleration = 5f;

    // Clothes accelerations
    public float windInSailsForce = 15f;
    public float windInSailsUpForce = 0.5f;
    public float windInBoutsForce = 2f;
    public float windInFlag = 25f;

    // AutoStop Boat
    public int numberOfSecondsBeforeStop = 4;

      public GameObject player;
      public GameObject buttonDown;
      public GameObject colliders;
      public GameObject matchTarget;
      public Rigidbody rigidBody;
      public GameObject windSystem;
      //public vThirdPersonCamera tpCamera;
      public GameObject hull;
      public GameObject hull_Bouts01;
      public GameObject hull_Bouts02;
      public GameObject shipWheel;
      public GameObject rudder;
      public GameObject mast01;
      public GameObject mast02;
      public GameObject mast01_Sail01;
      public GameObject mast01_Sail02;
      public GameObject mast01_SailUp;
      public GameObject mast01_Bouts01;
      public GameObject mast01_Bouts02;
      public GameObject mast02_Sail01;
      public GameObject mast02_Sail02;
      public GameObject mast02_SailUp;
      public GameObject mast02_Bouts01;
      public GameObject mast02_Bouts02;
      public GameObject front_Sail01;
      public GameObject front_Sail02;
      public GameObject front_SailUp;
      public GameObject flag;
      public GameObject fxWaterTrail;
      public GameObject attachements;

    public GameObject[] ignoreRaycastInNavigation;

    // DEBUG

    public bool isNavigating = false;
    public bool sailsRigging = false;

      public float windAngle;
      public Vector3 windVector;

      public float mastEulerAngle;
      public float mastAngle;
      public float rudderAngle;

      public float mastMinusWind;
      public float boatMinusWind;

    public float boostMultiplier;
    public float boatSpeed;

    public float heelMultiplier;
      public float boatHeel;

   private void Start()
    {
        {/*    player = GameObject.FindGameObjectWithTag("Player");
        buttonDown = gameObject.transform.GetChild(7).gameObject;
        colliders = gameObject.transform.GetChild(6).gameObject;
        matchTarget = gameObject.transform.GetChild(7).GetChild(0).GetChild(1).gameObject;
        rigidBody = gameObject.GetComponent<Rigidbody>();
        windSystem = GameObject.Find("WindSystem");
        tpCamera = FindObjectOfType<vThirdPersonCamera>();

        hull = gameObject.transform.GetChild(0).gameObject;
        hull_Bouts01 = gameObject.transform.GetChild(1).GetChild(0).gameObject;
        hull_Bouts02 = gameObject.transform.GetChild(1).GetChild(1).gameObject;
        shipWheel = gameObject.transform.GetChild(1).GetChild(3).gameObject;
        rudder = gameObject.transform.GetChild(1).GetChild(4).gameObject;
        mast01 = gameObject.transform.GetChild(2).gameObject;
        mast02 = gameObject.transform.GetChild(3).gameObject;
        mast01_Sail01 = gameObject.transform.GetChild(2).GetChild(0).gameObject;
        mast01_Sail02 = gameObject.transform.GetChild(2).GetChild(1).gameObject;
        mast01_SailUp = gameObject.transform.GetChild(2).GetChild(2).gameObject;
        mast01_Bouts01 = gameObject.transform.GetChild(2).GetChild(5).gameObject;
        mast01_Bouts02 = gameObject.transform.GetChild(2).GetChild(6).gameObject;
        mast02_Sail01 = gameObject.transform.GetChild(3).GetChild(0).gameObject;
        mast02_Sail02 = gameObject.transform.GetChild(3).GetChild(1).gameObject;
        mast02_SailUp = gameObject.transform.GetChild(3).GetChild(2).gameObject;
        mast02_Bouts01 = gameObject.transform.GetChild(2).GetChild(5).gameObject;
        mast02_Bouts02 = gameObject.transform.GetChild(2).GetChild(6).gameObject;
        front_Sail01 = gameObject.transform.GetChild(4).GetChild(0).gameObject;
        front_Sail02 = gameObject.transform.GetChild(4).GetChild(1).gameObject;
        front_SailUp = gameObject.transform.GetChild(4).GetChild(2).gameObject;
        flag = transform.GetChild(9).gameObject;
        fxWaterTrail = gameObject.transform.GetChild(10).GetChild(0).gameObject;
        fxWaterTrail = gameObject.transform.GetChild(10).GetChild(0).gameObject;
        attachements = transform.GetChild(8).gameObject;*/
        }
    }
    
    private void Update()
    {
        mastEulerAngle = transform.GetChild(1).gameObject.transform.eulerAngles.y;
        windAngle = windSystem.GetComponent<WindSystem>().windAngle;
        windVector = windSystem.GetComponent<WindSystem>().windVector;

        if (isNavigating)
        {
            player.transform.position = matchTarget.transform.position; // Stay in place

            #region End navigation / Input F
            if (Input.GetKeyDown(KeyCode.F) && (canReleaseHelm || !sailsRigging))
            {
                EndNavigation();
            }
            #endregion

            #region Down sails / Input Z
            if (Input.GetKeyDown(KeyCode.Z))
            {
                sailsRigging = true;
                ChangeSailsPosition();
            }
            #endregion

            #region Up sails / Input X
            if (Input.GetKeyDown(KeyCode.X) && sailsRigging)
            {
                sailsRigging = false;
                ChangeSailsPosition();
            }
            #endregion

            #region Turn mast left / Input Q
            if (Input.GetKey(KeyCode.Q))
            {
                mastAngle += mastRotationSpeed * Time.deltaTime;
                mastAngle = Mathf.Clamp(mastAngle, -mastMaxAngle, mastMaxAngle);
                mast01.transform.localEulerAngles = new Vector3(0f, mastAngle, 0f);
                mast02.transform.localEulerAngles = new Vector3(0f, mastAngle, 0f);
            }
            #endregion

            #region Turn mast right / Input E
            if (Input.GetKey(KeyCode.E))
            {
                mastAngle -= mastRotationSpeed * Time.deltaTime;
                mastAngle = Mathf.Clamp(mastAngle, -mastMaxAngle, mastMaxAngle);
                mast01.transform.localEulerAngles = new Vector3(0f, mastAngle, 0f);
                mast02.transform.localEulerAngles = new Vector3(0f, mastAngle, 0f);
            }
            #endregion

            #region Turn left (ship wheel + rudder) / Input A
            if (Input.GetKey(KeyCode.A))
            {
                rudderAngle += rudderRotationSpeed * Time.deltaTime;
                rudderAngle = Mathf.Clamp(rudderAngle, -rudderMaxAngle, rudderMaxAngle);
                if (rudderAngle > -rudderMaxAngle && rudderAngle < rudderMaxAngle)
                {
                    rudder.transform.Rotate(Vector3.up, rudderRotationSpeed * Time.deltaTime); // Turn rudder
                    shipWheel.transform.Rotate(Vector3.forward, -shipWheelRotationSpeed * Time.deltaTime); // Turn ship wheel
                }
            }
            #endregion

            #region Turn right (ship wheel + rudder) / Input D
            if (Input.GetKey(KeyCode.D))
            {
                rudderAngle -= rudderRotationSpeed * Time.deltaTime;
                rudderAngle = Mathf.Clamp(rudderAngle, -rudderMaxAngle, rudderMaxAngle);
                if (rudderAngle > -rudderMaxAngle && rudderAngle < rudderMaxAngle)
                {
                    rudder.transform.Rotate(Vector3.up, -rudderRotationSpeed * Time.deltaTime); // Turn rudder
                    shipWheel.transform.Rotate(Vector3.forward, shipWheelRotationSpeed * Time.deltaTime); // Turn ship wheel
                }
            }
            #endregion
        }
    }

    private void FixedUpdate()
    {
        ApplyWindInClothes();

        #region Turn the flag

        flag.transform.eulerAngles = new Vector3(0f, windAngle - 180f, 0f); // Turn the flag

        float flagZ;
        if (heelMultiplier > 0f) flagZ = -gameObject.transform.localEulerAngles.z;
        else flagZ = gameObject.transform.localEulerAngles.z;
        flag.transform.GetChild(0).gameObject.transform.localRotation = Quaternion.Euler(new Vector3(flagZ, 0, 0));

        #endregion

        #region mastMinusWind & boatMinusWind

        mastMinusWind = mastEulerAngle - windAngle;
        boatMinusWind = transform.eulerAngles.y - windAngle;

        if (mastMinusWind < 0f)
        {
            mastMinusWind = 360 + mastMinusWind;
        }
        if (boatMinusWind < 0f)
        {
            boatMinusWind = 360 + boatMinusWind;
        }
        #endregion

        #region boostMultiplier & boatSpeed
        
        // Calculate boostMultiplier
        boostMultiplier = mastBoostCurve.Evaluate(mastMinusWind) * boatBoostCurve.Evaluate(boatMinusWind);
        if (boostMultiplier < 1f) boostMultiplier = 1f;

        // Calculate boatSpeed
        if (sailsRigging)
        {
            if (boatSpeed < boatMaxSpeed * 10000 * boostMultiplier)
            {
                boatSpeed += boatAcceleration * 10000 * Time.deltaTime;
            }
            else if (boatSpeed > boatMaxSpeed * 10000 * boostMultiplier)
            {
                boatSpeed -= boatAcceleration * 10000 * Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.S) && isNavigating) // isNavigating
            {
                boatSpeed -= boatAcceleration * 10000 * Time.deltaTime * 2;
                boatSpeed = Mathf.Clamp(boatSpeed, -boatMaxBackSpeed * 10000, boatMaxSpeed * 10000 * boostMultiplier);
            }
            else if (boatSpeed > 0f)
            {
                boatSpeed -= boatAcceleration * 10000 * Time.deltaTime * 2; // Deccelerate faster than accelerate
                boatSpeed = Mathf.Clamp(boatSpeed, 0f, boatMaxSpeed * 10000 * boostMultiplier);
            }
            else if (boatSpeed < 0f)
            {
                boatSpeed += boatAcceleration * 10000 * Time.deltaTime * 2; // Deccelerate faster than accelerate
                boatSpeed = Mathf.Clamp(boatSpeed, -boatMaxBackSpeed * 10000, 0f);
            }
        }

        // Apply bootSpeed
        if (boatSpeed != 0f)
        {
            rigidBody.AddRelativeForce(Vector3.forward * boatSpeed);
        }
        #endregion

        #region heelMultiplier & boatHeel
        
        // Calculate heelMultiplier
        heelMultiplier = heelBoostCurve.Evaluate(boatMinusWind);

        // Calculate boatHeel
        if (sailsRigging)
        {
            if (boatHeel <= heelMaxForce * 10000 * heelMultiplier)
            {
                boatHeel += heelAcceleration * 10000 * Time.deltaTime;
            }
            if (boatHeel > heelMaxForce * 10000 * heelMultiplier)
            {
                boatHeel -= heelAcceleration * 10000 * Time.deltaTime;
            }
        }
        else
        {
            if (boatHeel > 0f)
            {
                boatHeel -= heelAcceleration * 10000 * Time.deltaTime * 1.5f; // Deccelerate faster than accelerate
                boatHeel = Mathf.Clamp(boatHeel, 0f, heelMaxForce * 10000 * heelMultiplier);
            }
            else if (boatHeel < 0f)
            {
                boatHeel += heelAcceleration * 10000 * Time.deltaTime * 1.5f; // Deccelerate faster than accelerate
                boatHeel = Mathf.Clamp(boatHeel, heelMaxForce * 10000 * heelMultiplier, 0f);
            }
        }

        // Apply boatHeel
        float eulerAngleZ = this.transform.rotation.eulerAngles.z;
        if (boatHeel != 0f && (eulerAngleZ > 0f && eulerAngleZ < heelMaxAngle) || (eulerAngleZ > 360 - heelMaxAngle && eulerAngleZ < 360f))
        {
            rigidBody.AddTorque(transform.forward * boatHeel);
        }
        #endregion

        #region Turn (considering rudderAngle)
        if ((boatSpeed > 0f || Input.GetKey(KeyCode.S)) && (rudderAngle < -10f || rudderAngle > 10f)) // To facilitate the non rotation (go forward)
        {
            if (boatSpeed > 0f)
            {
                transform.RotateAround(gameObject.transform.position, Vector3.up, (-boatTurnSpeed * rudderAngle / rudderMaxAngle) * Mathf.Clamp01(boatSpeed / (boatMaxSpeed * 10000)));
            }
            else
            {
                transform.RotateAround(gameObject.transform.position, Vector3.up, 0.5f * (boatTurnSpeed * rudderAngle / rudderMaxAngle) * Mathf.Clamp01(-boatSpeed / (boatMaxBackSpeed * 10000))); // 0.5f to be slower when going back
            }
        }
        #endregion

        #region FX
 
        if (boatSpeed > 100000f) fxWaterTrail.SetActive(true);
        else fxWaterTrail.SetActive(false);

        #endregion

        #region AutoStopBoat when Captain is out
        {/*
        if (!player.GetComponent<vThirdPersonController>().isOnABoat && sailsRigging)
        {
            StartCoroutine(AutoStopBoat(numberOfSecondsBeforeStop));
        }
            */}
        #endregion
    }

    public void StartNavigation()
    {     
        for (int i = 0; i < ignoreRaycastInNavigation.Length; i++)
        {
            SetLayerRecursively(ignoreRaycastInNavigation[i], 2);
        }

        //player.GetComponent<vThirdPersonController>().isCrouching = false;
        //player.GetComponent<vThirdPersonController>().isNavigating = true;
        isNavigating = true;
        //tpCamera.SetMainTarget(this.transform);

        buttonDown.transform.GetChild(0).gameObject.SetActive(false);

        //player.GetComponent<vThirdPersonInput>().SetLockBasicInput(true);
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().mass = 0f;
    }

    public void EndNavigation()
    {
        for (int i = 0; i < ignoreRaycastInNavigation.Length; i++)
        {
            SetLayerRecursively(ignoreRaycastInNavigation[i], 0);
        }

       // player.GetComponent<vThirdPersonController>().isNavigating = false;
        isNavigating = false;
        //tpCamera.SetMainTarget(player.transform);

        buttonDown.transform.GetChild(0).gameObject.SetActive(true);

      //  player.GetComponent<vThirdPersonInput>().SetLockBasicInput(false);
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().mass = 50f;
    }

    public void ChangeSailsPosition()
    {
        if (sailsRigging) // Deploy sails
        {
            mast01_Sail01.SetActive(true);
            mast01_Sail02.SetActive(true);
            mast01_SailUp.SetActive(false); 

            mast02_Sail01.SetActive(true);
            mast02_Sail02.SetActive(true);
            mast02_SailUp.SetActive(false);

            front_Sail01.SetActive(true);
            front_Sail02.SetActive(true);
            front_SailUp.SetActive(false);
        }
        else // Undeploy sails
        {
            mast01_Sail01.SetActive(false);
            mast01_Sail02.SetActive(false);
            mast01_SailUp.SetActive(true);

            mast02_Sail01.SetActive(false);
            mast02_Sail02.SetActive(false);
            mast02_SailUp.SetActive(true);

            front_Sail01.SetActive(false);
            front_Sail02.SetActive(false);
            front_SailUp.SetActive(true);
        }
    }

    public void ApplyWindInClothes()
    {  
        mast01_Sail01.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsForce * windVector.x, 0, windInSailsForce* windVector.z);
        mast01_Sail02.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsForce * windVector.x, 0, windInSailsForce* windVector.z);
        mast01_SailUp.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsUpForce * windVector.x, 9.5f, windInSailsUpForce * windVector.z);
        mast01_Bouts01.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsUpForce * windVector.x, 9.5f, windInSailsUpForce * windVector.z);
        mast01_Bouts02.GetComponent<Cloth>().externalAcceleration = new Vector3(windInBoutsForce * windVector.x, 0, windInBoutsForce * windVector.z);

        mast02_Sail01.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsForce * windVector.x, 0, windInSailsForce * windVector.z);
        mast02_Sail02.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsForce * windVector.x, 0, windInSailsForce * windVector.z);
        mast02_SailUp.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsUpForce * windVector.x, 9.5f, windInSailsUpForce * windVector.z);
        mast02_Bouts01.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsUpForce * windVector.x, 9.5f, windInSailsUpForce * windVector.z);
        mast02_Bouts02.GetComponent<Cloth>().externalAcceleration = new Vector3(windInBoutsForce * windVector.x, 0, windInBoutsForce * windVector.z);

        hull_Bouts01.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsUpForce * windVector.x, 9.5f, windInSailsUpForce * windVector.z);
        hull_Bouts02.GetComponent<Cloth>().externalAcceleration = new Vector3(windInBoutsForce * windVector.x, 0, windInBoutsForce * windVector.z);

        front_Sail01.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsForce * windVector.x, 0, windInSailsForce * windVector.z);
        front_Sail02.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsForce * windVector.x, 0, windInSailsForce * windVector.z);
        front_SailUp.GetComponent<Cloth>().externalAcceleration = new Vector3(windInSailsUpForce * windVector.x, 9.5f, windInSailsUpForce * windVector.z);

        flag.transform.GetChild(0).GetComponent<Cloth>().externalAcceleration = new Vector3(windInFlag * windVector.x, 9.5f, windInFlag * windVector.z);
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    IEnumerator AutoStopBoat(int numberOfSeconds)
    {
        yield return new WaitForSeconds(numberOfSeconds);
        sailsRigging = false;
        ChangeSailsPosition();
    }
}
