// SPDX-License-Identifier: MIT
pragma solidity ^0.8.7;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////
// CLASS
//      *   Description         :   Each contract instance 
//                                  manages CRUD for its 
//                                  greeting text message
//      *   Deployment Address  :   
///////////////////////////////////////////////////////////
contract Greeter
{

    ///////////////////////////////////////////////////////////
    // FIELDS
    //      *   Values stored on contract
    ///////////////////////////////////////////////////////////


    // User address who owns this contract instance
    address _owner;


    // Greeting text message
    string _greeting;


    ///////////////////////////////////////////////////////////
    // CONSTRUCTOR
    //      *   Runs when contract is executed
    ///////////////////////////////////////////////////////////
    constructor() 
    {
        _owner = msg.sender;
        _greeting = "Default Greeting";
    }


    ///////////////////////////////////////////////////////////
    // FUNCTION: SETTER
    //      *   Set greeting 
    //      *   Changes contract state, so requires calling via
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function setGreeting(string memory greeting) public 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == _owner);

        _greeting = greeting;
    }


    ///////////////////////////////////////////////////////////
    // FUNCTION: GETTER
    // *    Get greeting 
    // *    Changes no contract state, so requires calling via  
    //      either ExecuteContractFunction or RunContractFunction
    ///////////////////////////////////////////////////////////
    function getGreeting() public view returns (string memory) 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == _owner);

        return _greeting;
    }
}