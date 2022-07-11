require("@nomiclabs/hardhat-waffle");
require("@nomiclabs/hardhat-etherscan");

///////////////////////////////////////////////////////////
// CONFIGURATION
///////////////////////////////////////////////////////////

//TODO: Security Best Practice: Set these values to "" before committing to git
const PRIVATE_KEY = "";          // Populate from MetaMask, after sign-in
const MUMBAI_NETWORK_URL = "";   // Populate from admin.moralis.io, after sign-in
const POLYGONSCAN_API_KEY = "";  // Populate from polygonscan.com, after sign-in

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
