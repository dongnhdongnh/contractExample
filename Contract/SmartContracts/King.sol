pragma solidity ^0.4.18;

contract Owned {
    address owner;
    
    constructor() public payable {
        owner = msg.sender;
    }
    
    modifier onlyOwner {
        require(msg.sender == owner);
        _;
    }
}

contract CrowdSale is Owned{
    address[] public ListShareHolders;                                  //Danh sách cổ đông
    mapping (address => uint256) percents;                              //Quyền hạn của cổ đông
    mapping (address => bool) accepts;                                  //Biểu quyết

    //"0x5e98ff12d889945488ddd53ffb71f6580cae1571","0x9bb7f17d1d53774e0fdfa4b76ba68c48e8a4daba","0xee2d522d8f10769a5f150ed55f40f305c8d0595f"

    function addListShareHolders(address _address) onlyOwner payable public {
        ListShareHolders.push(_address);
    }

    function showListShareHolders() view public returns (address[])  {
        return ListShareHolders;
    }

    // function getPercentAccept() public {
    //     uint length = bids[msg.sender].length;
    //     if (_values.length != length || _fake.length != length ||
    //                 _secret.length != length)
    //         throw;
    //     uint refund;
    //     for (uint i = 0; i < length; i++)
    //     {
    //         var bid = bids[msg.sender][i];
    //         var (value, fake, secret) =
    //                 (_values[i], _fake[i], _secret[i]);
    //         if (bid.blindedBid != sha3(value, fake, secret))
    //             // Bid was not actually revealed.
    //             // Do not refund deposit.
    //             continue;
    //         refund += bid.deposit;
    //         if (!fake && bid.deposit >= value)
    //             if (placeBid(msg.sender, value))
    //                 refund -= value;
    //         // Make it impossible for the sender to re-claim
    //         // the same deposit.
    //         bid.blindedBid = 0;
    //     }
    //     msg.sender.send(refund);
    //     return 1;
    // }

    modifier agree() {

        _;
    }

}

contract King is Owned, CrowdSale{
    int _multiplier;

    constructor(int multiplier) public payable {
        _multiplier = multiplier;
    }

    event Multiplied(int indexed a, address indexed sender, int result );

    function multiply(int a) public returns (int r) {
        r = a * _multiplier;
        emit Multiplied(a, msg.sender, r);
        return r;
    }

    function getOwner() view public returns (address) {
        return owner;
    }
}