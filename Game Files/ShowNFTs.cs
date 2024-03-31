using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using Unity.VisualScripting;
using System;
using Thirdweb.Examples;





public class ShowNFTs : MonoBehaviour
{

    //public const string ContractAddress = "0x7fcEfC5dF8e1Fb3aaB589f30062691c30D46e10c";



    public string contractAddress;
    
    [Header("UI ELEMENTS (DO NOT EDIT)")]
    public Transform contentParent;
    public Prefab_NFT nftPrefab;
    public GameObject loadingPanel;

    private void Start()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // FindObjectOfType<Prefab_ConnectWallet>()?.OnConnectedCallback.AddListener(() => LoadNFTs());
        // FindObjectOfType<Prefab_ConnectWallet>()?.OnConnectedCallback.AddListener(() => LoadNFTs());

        LoadNFTs();
    }


    public async void LoadNFTs()
    {
        loadingPanel.SetActive(true);
        Contract contract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);
        List<NFT> nfts = await contract.ERC1155.GetOwned();

        foreach (NFT nft in nfts)
        {
            if (!Application.isPlaying)
                return;

            Prefab_NFT nftPrefabScript = Instantiate(nftPrefab, contentParent);
            nftPrefabScript.LoadNFT(nft);
        }

        loadingPanel.SetActive(false);

    }
}