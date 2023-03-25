using System.Collections;
using System.Collections.Generic;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class QRCodeScanner : MonoBehaviour {
    [SerializeField]
    private RawImage _rawImagineBackground;
    [SerializeField]
    private AspectRatioFitter _aspectRatioFitter;
    [SerializeField]
    private TextMeshProUGUI _textOut;
    [SerializeField]
    private RectTransform _scanZone;

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
                _textOut.text = result.Text;
                //If succesful, remove scanner, add inventory item.
            }
            else {
                _textOut.text = "failed to read qrcode";
            }
        }
        catch {
            _textOut.text = "failed in try";
        }
    }
}
