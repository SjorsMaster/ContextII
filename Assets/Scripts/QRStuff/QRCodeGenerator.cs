using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZXing;
using ZXing.QrCode;

public class QRCodeGenerator : MonoBehaviour
{
    [SerializeField]
    //used to display
    private RawImage _rawImageReceiver;

    private Texture2D _storeEncodedTexture;

    // Start is called before the first frame update void Start()
    void Start(){
        _storeEncodedTexture = new Texture2D(256, 256);

        string text = PlayerPrefs.GetInt("Department") + ":" + PlayerPrefs.GetInt("Personality") ;
        EncodeTextToQRode(text);
    }

    //Encoding
    private Color32 [] Encode(string textForEncoding, int width, int height){
        BarcodeWriter writer = new BarcodeWriter{
            Format = BarcodeFormat.QR_CODE, 
            Options = new QrCodeEncodingOptions{
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    //Throw a json in here, we can use that on the other end to show what it's been receiving
    public void EncodeTextToQRode(string IN){
        string textWrite = IN;
        Color32[] _convertPixelTotexture = Encode(textWrite, _storeEncodedTexture.width, _storeEncodedTexture.height); 
        _storeEncodedTexture.SetPixels32(_convertPixelTotexture);
        _storeEncodedTexture.Apply();

        _rawImageReceiver.texture = _storeEncodedTexture;
    }
}
