using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public string weaponName; // 무기이름
    public int bulletsPerMag;// 총알이 들어갈 수있는 량
    public int bulletsTotal; // 보유한 총알의 총 갯수
    public int currentBullets; // 현재 장착된 총알 갯수
    public int currentPoints;
    public float damage;  // 내가 입힐 수 있는 데미지
    public float range; // 사거리
    public float fireRate; // 발사 간격(딜레이)
    public float accuracy; // 정확성
    public Vector3 aimPosition; // 조준 지점
    private Vector3 originalPosition; // 원래 지점
    private float originalAccuracy;
 
    private float fireTimer; // 발사 시간
    private bool isReloading; // 로딩 중이라면
    private bool isAiming; // 조준 중이라면

    public Transform shootPoint; // 발사점
    public Transform bulletCasingPoint; // 총알에 붙는 프리펩 할당 지점
    private Animator anim;
    public ParticleSystem muzzleFlash; 
    public Text BulletsText;
    public Text PointsText;

    public GameObject hitSparkPrefab;
    public GameObject hitHolePrefab;
    public GameObject bulletCasing;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    public Transform camRecoil;
    public Vector3 recoilKickback;
    public float recoilAmount;
    private float originalRecoil;
    // Start is called before the first frame update
    void Start()
    {
        currentBullets = bulletsPerMag;
        anim = GetComponent<Animator> ();
        BulletsText.text = currentBullets + " / " + bulletsTotal;
        
        originalPosition = transform.localPosition;
        originalAccuracy = accuracy;
        originalRecoil = recoilAmount;

    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        isReloading = info.IsName("Reload");
        if (GameManager.canPlayerMove)
        {

            if (Input.GetButton("Fire1"))
            {
                if (currentBullets > 0)
                {
                    Fire();
                }
                else
                {
                    DoReload();
                }

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                DoReload();
            }

            if (fireTimer < fireRate)
            {
                fireTimer += Time.deltaTime;
            }
            AimDownSight();
            RecoilBack();

        }
    }
    private void Fire()
    {
        if (fireTimer < fireRate || isReloading)
        
            return;
        
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward + Random.onUnitSphere * accuracy, out hit, range))
        {
            GameObject hitSpark = Instantiate(hitSparkPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            hitSpark.transform.SetParent(hit.transform);
            Destroy(hitSpark, 0.5f);
            GameObject hitHole = Instantiate(hitHolePrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            hitHole.transform.SetParent(hit.transform);
            Destroy(hitHole, 5f);


            HealthManager healthManager = hit.transform.GetComponent<HealthManager>();
            if (healthManager)
            {
                healthManager.ApplyDamage(damage);
            }

            Rigidbody rigidbody = hit.transform.GetComponent<Rigidbody>();
            if(rigidbody)
            {
                rigidbody.AddForceAtPosition (transform.forward*5f*damage, transform.position);
            }
        }
                

            currentBullets--;
            fireTimer = 0.0f;
            anim.CrossFadeInFixedTime("Fire", 0.01f);
            audioSource.PlayOneShot(shootSound);
            muzzleFlash.Play();
            BulletsText.text = currentBullets + "/" + bulletsTotal;
            Recoil();
            BulletEffect();
            
        

    }
    private void DoReload()
    {
        if(!isReloading && currentBullets < bulletsPerMag && bulletsTotal > 0 )
        {
            anim.CrossFadeInFixedTime("Reload", 0.01f);
            audioSource.PlayOneShot(reloadSound);
        }
    }
    public void Reload()
    {
   
        
            int bulletsToReload = bulletsPerMag - currentBullets;

            if (bulletsToReload > bulletsTotal)
            {
                bulletsToReload = bulletsTotal;
            }
            currentBullets += bulletsToReload;
            bulletsTotal -= bulletsToReload;
            BulletsText.text = currentBullets + " / " + bulletsTotal;

        
       


    }

    private void AimDownSight() 
    {
        if(Input.GetButton("Fire2")&& !isReloading)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * 8f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 40f, Time.deltaTime * 8f);
            isAiming = true;
            accuracy = originalRecoil / 2f;
            recoilAmount = originalRecoil / 2f;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * 5f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, Time.deltaTime * 8f);
            isAiming = false;
            accuracy = originalAccuracy;
            recoilAmount = originalRecoil;
        }
    }

    private void Recoil()
    {

        Vector3 recoilVector = new Vector3(Random.Range(-recoilKickback.x, recoilKickback.x), recoilKickback.y, recoilKickback.z);
        Vector3 recoilCamVector = new Vector3(-recoilVector.y * 400f, recoilVector.x * 200f, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + recoilVector, recoilAmount / 2f);
        camRecoil.localRotation = Quaternion.Slerp(camRecoil.localRotation, Quaternion.Euler(camRecoil.localEulerAngles + recoilCamVector), recoilAmount);
        
    }

    private void RecoilBack()
    {
        camRecoil.localRotation = Quaternion.Slerp(camRecoil.localRotation, Quaternion.identity, Time.deltaTime * 2f);
    }
    private void BulletEffect()
    {
        Quaternion randomQuaternion = new Quaternion(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f), 1);
        GameObject casing = Instantiate(bulletCasing, bulletCasingPoint);
        casing.transform.localRotation = randomQuaternion;
        casing.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(Random.Range(50f, 100f), Random.Range(50f, 100f), Random.Range(-30f, 30f)));
        Destroy(casing, 1f);
    }
}
