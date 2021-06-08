using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BazaarPlugin;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class PurchaseHandler : MonoBehaviour
{
    [SerializeField] private Button initButton;
    [SerializeField] private Button queryInventoryButton;
    [SerializeField] private Button querySkuDetailButton;
    [SerializeField] private Button queryPurchasesButton;
    [SerializeField] private Button purchaseProductButton;
    [SerializeField] private Button consumeProductButton;

    [SerializeField] private InputField itemIdField;

    [SerializeField] private Text logText;
    [SerializeField] private ScrollRect scroll;

    [SerializeField] private string publicKey;
    [SerializeField] private string[] skus;

#if UNITY_ANDROID

    private void Start()
    {
        initButton.onClick.AddListener(() =>
        {
            MyDebug.Log("Waiting for initialize...");
            BazaarIAB.init(publicKey);
        });
        queryInventoryButton.onClick.AddListener(() =>
        {
            MyDebug.Log("Waiting for query inventory...");
            BazaarIAB.queryInventory(skus);
        });
        querySkuDetailButton.onClick.AddListener(() =>
        {
            MyDebug.Log("Waiting for query sku detail...");
            BazaarIAB.querySkuDetails(skus);
        });
        queryPurchasesButton.onClick.AddListener(() =>
        {
            MyDebug.Log("Waiting for query purchases...");
            BazaarIAB.queryPurchases();
        });
        purchaseProductButton.onClick.AddListener(() =>
        {
            MyDebug.Log($"Waiting for purchase {itemIdField.text}...");
            BazaarIAB.purchaseProduct(itemIdField.text);
        });
        consumeProductButton.onClick.AddListener(() =>
        {
            MyDebug.Log($"Waiting for consume {itemIdField.text}...");
            BazaarIAB.consumeProduct(itemIdField.text);
        });

        MyDebug._logTex = logText;
        MyDebug._scroll = scroll;
    }

    void OnEnable()
    {
        // Listen to all events for illustration purposes
        IABEventManager.billingSupportedEvent += billingSupportedEvent;
        IABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
        IABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
        IABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
        IABEventManager.querySkuDetailsSucceededEvent += querySkuDetailsSucceededEvent;
        IABEventManager.querySkuDetailsFailedEvent += querySkuDetailsFailedEvent;
        IABEventManager.queryPurchasesSucceededEvent += queryPurchasesSucceededEvent;
        IABEventManager.queryPurchasesFailedEvent += queryPurchasesFailedEvent;
        IABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
        IABEventManager.purchaseFailedEvent += purchaseFailedEvent;
        IABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
        IABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
    }


    void OnDisable()
    {
        // Remove all event handlers
        IABEventManager.billingSupportedEvent -= billingSupportedEvent;
        IABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
        IABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
        IABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
        IABEventManager.querySkuDetailsSucceededEvent -= querySkuDetailsSucceededEvent;
        IABEventManager.querySkuDetailsFailedEvent -= querySkuDetailsFailedEvent;
        IABEventManager.queryPurchasesSucceededEvent -= queryPurchasesSucceededEvent;
        IABEventManager.queryPurchasesFailedEvent -= queryPurchasesFailedEvent;
        IABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
        IABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
        IABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
        IABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
    }


    void billingSupportedEvent()
    {
        MyDebug.Log("billingSupportedEvent", MyDebug.Color.green);
        MyDebug.Log("-----------------------------");
    }

    void billingNotSupportedEvent(string error)
    {
        MyDebug.Log("billingNotSupportedEvent:\n" + error, MyDebug.Color.red);
        MyDebug.Log("-----------------------------");
    }

    void queryInventorySucceededEvent(List<BazaarPurchase> purchases, List<BazaarSkuInfo> skus)
    {
        MyDebug.Log($"queryInventorySucceededEvent.\ntotal purchases: {purchases.Count} ", MyDebug.Color.green);

        for (int i = 0; i < purchases.Count; ++i)
        {
            MyDebug.Log(purchases[i].ToString(), MyDebug.Color.blue);
        }

        MyDebug.Log($"total skus: {skus.Count}", MyDebug.Color.green);
        for (int i = 0; i < skus.Count; ++i)
        {
            MyDebug.Log(skus[i].ToString(), MyDebug.Color.blue);
        }

        if (skus.Count > 0)
        {
            itemIdField.text = this.skus[0];
        }

        MyDebug.Log("-----------------------------");
    }

    void queryInventoryFailedEvent(string error)
    {
        MyDebug.Log("queryInventoryFailedEvent: " + error, MyDebug.Color.red);
        MyDebug.Log("-----------------------------");
    }

    private void querySkuDetailsSucceededEvent(List<BazaarSkuInfo> skus)
    {
        MyDebug.Log($"querySkuDetailsSucceededEvent.\ntotal skus: {skus.Count}", MyDebug.Color.green);

        for (int i = 0; i < skus.Count; ++i)
        {
            MyDebug.Log(skus[i].ToString(), MyDebug.Color.blue);
        }

        MyDebug.Log("-----------------------------");
    }

    private void querySkuDetailsFailedEvent(string error)
    {
        MyDebug.Log("querySkuDetailsFailedEvent: " + error, MyDebug.Color.red);
        MyDebug.Log("-----------------------------");
    }

    private void queryPurchasesSucceededEvent(List<BazaarPurchase> purchases)
    {
        MyDebug.Log($"queryPurchasesSucceededEvent.\ntotal purchases: {purchases.Count}", MyDebug.Color.green);

        for (int i = 0; i < purchases.Count; ++i)
        {
            MyDebug.Log(purchases[i].ToString(), MyDebug.Color.blue);
        }

        MyDebug.Log("-----------------------------");
    }

    private void queryPurchasesFailedEvent(string error)
    {
        MyDebug.Log("queryPurchasesFailedEvent:\n" + error, MyDebug.Color.red);
        MyDebug.Log("-----------------------------");
    }

    void purchaseSucceededEvent(BazaarPurchase purchase)
    {
        MyDebug.Log("purchaseSucceededEvent:\n" + purchase, MyDebug.Color.green);
        MyDebug.Log("-----------------------------");
    }

    void purchaseFailedEvent(string error)
    {
        MyDebug.Log("purchaseFailedEvent:\n" + error, MyDebug.Color.red);
        MyDebug.Log("-----------------------------");
    }

    void consumePurchaseSucceededEvent(BazaarPurchase purchase)
    {
        MyDebug.Log("consumePurchaseSucceededEvent:\n" + purchase, MyDebug.Color.green);
        MyDebug.Log("-----------------------------");
    }

    void consumePurchaseFailedEvent(string error)
    {
        MyDebug.Log("consumePurchaseFailedEvent:\n" + error, MyDebug.Color.red);
        MyDebug.Log("-----------------------------");
    }

#endif

    public static class MyDebug
    {
        public static Text _logTex;
        public static ScrollRect _scroll;

        public enum Color
        {
            red,
            blue,
            black,
            white,
            green
        }

        public static async void Log(string log, Color color = Color.black)
        {
            _logTex.text += $"<color={color}>" + log + "</color>" + "\n";
            await Task.Delay(100);
            while (_scroll.verticalNormalizedPosition > 0)
            {
                _scroll.verticalNormalizedPosition -= 0.005f;
                await Task.Delay(1);
            }
        }
    }
}