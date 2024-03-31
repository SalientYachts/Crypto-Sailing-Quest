using UnityEngine;
using System.Collections.Generic;

namespace Thirdweb.Examples
{
    public class MintNFTs : MonoBehaviour
    {
        private const string TOKEN_ERC20_CONTRACT = "";
        private const string DROP_ERC20_CONTRACT = "";
        private const string TOKEN_ERC721_CONTRACT = "";
        private const string DROP_ERC721_CONTRACT = "";
        private const string TOKEN_ERC1155_CONTRACT = "";
        private const string DROP_ERC1155_CONTRACT = "0x7fcEfC5dF8e1Fb3aaB589f30062691c30D46e10c";
        private const string MARKETPLACE_CONTRACT = "";
        private const string PACK_CONTRACT = "";

        public async void MintERC20()
        {
            try
            {
                // Traditional Minting (Requires Minter Role)
                // Contract contract = ThirdwebManager.Instance.SDK.GetContract(TOKEN_ERC20_CONTRACT);

                // Minting
                // var transactionResult = await contract.ERC20.Mint("1.2");
                // Debugger.Instance.Log("[Mint ERC20] Successful", transactionResult.ToString());

                // Signature Minting
                // var receiverAddress = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
                // var payload = new ERC20MintPayload(receiverAddress, "3.2");
                // var signedPayload = await contract.ERC20.signature.Generate(payload);
                // bool isValid = await contract.ERC20.signature.Verify(signedPayload);
                // if (isValid)
                // {
                //     Debugger.Instance.Log("Sign minting ERC20...", $"Signature: {signedPayload.signature}");
                //     var result = await contract.ERC20.signature.Mint(signedPayload);
                //     Debugger.Instance.Log("[Mint (Signature) ERC20] Successful", result.ToString());
                // }
                // else
                // {
                //     Debugger.Instance.Log("Signature Invalid", $"Signature: {signedPayload.signature} is invalid!");
                // }

                // Claiming
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC20_CONTRACT);
                var result = await contract.ERC20.Claim("0.3");
                Debugger.Instance.Log("[Claim ERC20] Successful", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Mint ERC20] Error", e.Message);
            }
        }

        public async void MintERC721()
        {
            try
            {
                // NFT Collection Signature Minting (Requires Mint Permission)
                // Contract contract = ThirdwebManager.Instance.SDK.GetContract(TOKEN_ERC721_CONTRACT);

                // NFTMetadata meta = new NFTMetadata()
                // {
                //     name = "Unity NFT",
                //     description = "Minted From Unity",
                //     image = "ipfs://QmbpciV7R5SSPb6aT9kEBAxoYoXBUsStJkMpxzymV4ZcVc",
                // };

                // Minting
                // var result = await contract.ERC721.Mint(meta);
                // Debugger.Instance.Log("[Mint ERC721] Successful", result.ToString());

                // Signature Minting
                // var receiverAddress = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
                // var payload = new ERC721MintPayload(receiverAddress, meta);
                // var signedPayload = await contract.ERC721.signature.Generate(payload);
                // bool isValid = await contract.ERC721.signature.Verify(signedPayload);
                // if (isValid)
                // {
                //     Debugger.Instance.Log("Sign minting ERC721...", $"Signature: {signedPayload.signature}");
                //     var result = await contract.ERC721.signature.Mint(signedPayload);
                //     Debugger.Instance.Log("[Mint (Signature) ERC721] Successful", result.ToString());
                // }
                // else
                // {
                //     Debugger.Instance.Log("Signature Invalid", $"Signature: {signedPayload.signature} is invalid!");
                // }

                // NFT Drop Claiming
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC721_CONTRACT);
                var result = await contract.ERC721.Claim(1);
                Debugger.Instance.Log("[Claim ERC721] Successful", result[0].ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Mint ERC721] Error", e.Message);
            }
        }

        public async void MintERC1155_0()
        {
            try
            {
                // Contract contract = ThirdwebManager.Instance.SDK.GetContract(TOKEN_ERC1155_CONTRACT);

                // NFTMetadata meta = new NFTMetadata()
                // {
                //     name = "Unity NFT",
                //     description = "Minted From Unity",
                //     image = "ipfs://QmbpciV7R5SSPb6aT9kEBAxoYoXBUsStJkMpxzymV4ZcVc",
                // };

                // Minting
                // var result = await contract.ERC1155.Mint(new NFTMetadataWithSupply() { supply = 10, metadata = meta });
                // Debugger.Instance.Log("[Mint ERC1155] Successful", result.ToString());
                // You can use an existing token ID to mint additional supply
                // var result = await contract.ERC1155.MintAdditionalSupply("0", 10);
                // Debugger.Instance.Log("[Mint Additional Supply ERC1155] Successful", result.ToString());

                // Signature Minting
                // var receiverAddress = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
                // var payload = new ERC1155MintPayload(receiverAddress, meta, 1000);
                // var signedPayload = await contract.ERC1155.signature.Generate(payload);
                // You can use an existing token ID to signature mint additional supply
                // var payloadWithSupply = new ERC1155MintAdditionalPayload(receiverAddress, "0", 1000);
                // var signedPayload = await contract.ERC1155.signature.GenerateFromTokenId(payloadWithSupply);
                // bool isValid = await contract.ERC1155.signature.Verify(signedPayload);
                // if (isValid)
                // {
                //     Debugger.Instance.Log("Sign minting ERC1155...", $"Signature: {signedPayload.signature}");
                //     var result = await contract.ERC1155.signature.Mint(signedPayload);
                //     Debugger.Instance.Log("[Mint (Signature) ERC1155] Successful", result.ToString());
                // }
                // else
                // {
                //     Debugger.Instance.Log("Signature Invalid", $"Signature: {signedPayload.signature} is invalid!");
                // }

                //edition drop -claiming
                //bool canclaim = await contract.erc1155.claimconditions.canclaim("0", 1);
                // if (!canclaim)
                // {
                //     debugger.instance.log("[mint erc1155] cannot claim", "connected wallet not eligible to claim.");
                ///    return;
                //}

                // Edition Drop Claiming
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC1155_CONTRACT);
                TransactionResult transactionResult = await contract.ERC1155.Claim("0", 1);
                Debugger.Instance.Log("[Claim ERC1155] Successful", transactionResult.ToString());

                // Edition Drop - Signature minting additional supply
                // var payload = new ERC1155MintAdditionalPayload("0xE79ee09bD47F4F5381dbbACaCff2040f2FbC5803", "1");
                // payload.quantity = 3;
                // var p = await contract.ERC1155.signature.GenerateFromTokenId(payload);
                // var result = await contract.ERC1155.signature.Mint(p);
                // Debug.Log("sigminted tokenId: " + result.id);
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Mint ERC1155] Error", e.Message);
            }
        }

        public async void MintERC1155_1()
        {
            try
            {
                // Edition Drop Claiming
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC1155_CONTRACT);
                TransactionResult transactionResult = await contract.ERC1155.Claim("1", 1);
                Debugger.Instance.Log("[Claim ERC1155] Successful", transactionResult.ToString());

            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Mint ERC1155] Error", e.Message);
            }
        }

        public async void MintERC1155_2()
        {
            try
            {
                // Edition Drop Claiming
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC1155_CONTRACT);
                TransactionResult transactionResult = await contract.ERC1155.Claim("2", 1);
                Debugger.Instance.Log("[Claim ERC1155] Successful", transactionResult.ToString());

            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Mint ERC1155] Error", e.Message);
            }
        }

        public async void MintERC1155_3()
        {
            try
            {
                // Edition Drop Claiming
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC1155_CONTRACT);
                TransactionResult transactionResult = await contract.ERC1155.Claim("3", 1);
                Debugger.Instance.Log("[Claim ERC1155] Successful", transactionResult.ToString());

            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Mint ERC1155] Error", e.Message);
            }
        }

        public async void MintERC1155_4()
        {
            try
            {
                // Edition Drop Claiming
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC1155_CONTRACT);
                TransactionResult transactionResult = await contract.ERC1155.Claim("4", 1);
                Debugger.Instance.Log("[Claim ERC1155] Successful", transactionResult.ToString());

            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Mint ERC1155] Error", e.Message);
            }
        }
        
    }
}
