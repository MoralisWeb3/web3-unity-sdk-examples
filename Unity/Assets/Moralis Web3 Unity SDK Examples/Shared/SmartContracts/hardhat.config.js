require("@nomiclabs/hardhat-waffle");
require("@nomiclabs/hardhat-etherscan");

///////////////////////////////////////////////////////////
// CONFIGURATION
///////////////////////////////////////////////////////////

//TODO: Security Best Practice: Set these values to "" before committing to git
const PRIVATE_KEY = "2f6009cddf4c79754af198995fd9db86f0c4ced09e5e33b8f0d701362f8231d5";                     // Populate from MetaMask, after sign-in
const MUMBAI_NETWORK_URL = "https://speedy-nodes-nyc.moralis.io/b7793ecdae1b69241aa47057/polygon/mumbai";   // Populate from admin.moralis.io, after sign-in
const POLYGONSCAN_API_KEY = "6BR5JYMPZIMCKERGSTTBHJPBUCXETJUT22";                                           // Populate from polygonscan.com, after sign-in

///////////////////////////////////////////////////////////
// EXPORTS
///////////////////////////////////////////////////////////
/**
 * @type import('hardhat/config').HardhatUserConfig
 */
 module.exports = {
  solidity: "0.8.7",
  networks: {
    mumbai: {
      url: MUMBAI_NETWORK_URL,
      accounts: [PRIVATE_KEY]
    }
  },
  etherscan: {
    apiKey: POLYGONSCAN_API_KEY 
  }
};
