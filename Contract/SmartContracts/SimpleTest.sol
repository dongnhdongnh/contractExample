pragma solidity ^0.4.0;

contract SimpleTest {
    uint storedData;

	constructor() public payable {}

    function set(uint x) public {
        storedData = x;
    }

    function get() public view returns (uint) {
        return storedData;
    }
}