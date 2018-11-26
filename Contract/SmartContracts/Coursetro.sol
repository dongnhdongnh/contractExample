pragma solidity ^0.4.18;

contract Owned {
    address owner;
    
    function Owned() public payable {
        owner = msg.sender;
    }
    
    modifier onlyOwner {
        require(msg.sender == owner);
        _;
    }
}

contract Coursetro is Owned {
    
    struct Instructor {
        uint age;
        bytes16 fName;
        bytes16 lName;
    }


    event instructorInfo(
       bytes16 fName,
       bytes16 lName,
       uint age
    );
    
    mapping (address => Instructor) instructors;
    address[] public instructorAccts;

    function Coursetro() public payable {
    }

    function kill (address _address) onlyOwner public payable {
        //suicide(_address);
        _address.transfer(address(this).balance);
    }
    
    function setInstructor(address _address, uint _age, bytes16 _fName, bytes16 _lName) onlyOwner public {
        Instructor storage instructor = instructors[_address];
        
        instructor.age = _age;
        instructor.fName = _fName;
        instructor.lName = _lName;
        
        instructorAccts.push(_address) - 1;
        emit instructorInfo(_fName, _lName, _age);
    }
    
    function getInstructors() view public returns(address[]) {
        return instructorAccts;
    }
    
    function getInstructor(address _address) view public returns (uint, bytes16, bytes16) {
        return (instructors[_address].age, instructors[_address].fName, instructors[_address].lName);
    }
    
    function countInstructors() view public returns (uint) {
        return instructorAccts.length;
    }
    
}