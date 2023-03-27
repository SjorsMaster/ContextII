using System.Collections;
using System.Collections.Generic;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class QRCodeScanner : MonoBehaviour {
    public List<InventoryItem> items;

    public GameObject inventorySpot, itemObject;
    int pos;
    public InventorySaver inventorySaver;
    [SerializeField]
    private RawImage _rawImagineBackground;
    [SerializeField]
    private AspectRatioFitter _aspectRatioFitter;
    [SerializeField]
    private TextMeshProUGUI _textOut;
    [SerializeField]
    private RectTransform _scanZone;

    [SerializeField]
    GameObject cameraScreen;

    private bool _isCamAvailable;
    private WebCamTexture _camTexture;

    // Start is called before the first frame update
    void Start() {
        SetUpCamera();
    }

    // Update is called once per frame
    void Update() {
        UpdateCameraRender();
    }

    private void SetUpCamera() {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0) {
            _isCamAvailable = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++) {
            if (devices[i].isFrontFacing == false) {
                _camTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
            }
        }

        _camTexture.Play();
        _rawImagineBackground.texture = _camTexture;
        _isCamAvailable = true;
    }

    private void UpdateCameraRender() {
        if (_isCamAvailable == false) return;
        float ratio = (float)_camTexture.width / (float)_camTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = -_camTexture.videoRotationAngle;
        _rawImagineBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void OnClickScan() {
        Scan();
    }

    private void Scan() {
        try {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_camTexture.GetPixels32(), _camTexture.width, _camTexture.height);
            if (result != null) {
                for(int i = 0; i < items.Count; ) { //This should really be done with a dictionary, but I have no time and i'm really tired. yikes
                    _textOut.text = (items[i].id == result.Text) + " " + i;
                    if (items[i].id == result.Text) { pos = i; break; }
                    else { i++; }
                }
                GameObject tmp = Instantiate(itemObject, inventorySpot.transform);
                tmp.GetComponent<ItemWithStats>().SetStats(items[pos]);
                inventorySaver.SaveItems();
                cameraScreen.SetActive(false);
                _textOut.text = "scan";
            }
            else {
                _textOut.text = "try again";
            }
        }
        catch {
            _textOut.text = "error";
        }
    }
}
