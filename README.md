# README #

This README documents the necessary steps and prject structure of the application.

### What is this Prroject is about? ###

* Project Description
  * Recursive-length prefix (RLP) serialization is the primary encoding method used to serialize objects in Ethereum's execution layer.
    * This project includes the implementation of both the RLP encoding and decoding algorithms
    * The current implementation has been made for RLP encoding to support ```byte[], string, int, List``` data types.
    * The current implementation has been made for RLP decoding to decode the encoded byte arrays of ```byte[], string, List``` data types.
* Versions
  * Project Version : ```1```
* References
  * [Etherium Yellow Paper](https://ethereum.github.io/yellowpaper/paper.pdf)
  * [Etherium Specification](https://ethereum.org/en/developers/docs/data-structures-and-encoding/rlp/)
  * [A Medium Article](https://medium.com/coinmonks/data-structure-in-ethereum-episode-1-recursive-length-prefix-rlp-encoding-decoding-d1016832f919)

### How do I get set up? ###

* Summary of set up
  * Prerequisites
    * .NET Core 6
* Project Structure
  * RLPLibrary directory includes all the core functionalities.
  * RLPLibraryTest directory includes all the unit tests for the above functionalities.
  * RLPConsoleApplication directory includes the console application to run the application.
* To build the application run the following command from the root folder.
    * ```dotnet build```
* To run the application run the following command from the root folder.
    * ```dotnet run --project RLPConsoleApplication/RLPConsoleApplication.csproj```
* Dependencies
  * We used the ```MSTest``` framework to write unit tests.

### Who do I talk to? ###

* Repo owner
  * Hashan Maduwantha

### Additional things to know !!! ###

```While the algorithm is effective for encoding and decoding data, it is important to note that this implementation may not be the most optimal or memory-efficient solution. Also there are some use cases that I could not properly test about. As a beginner in C#, I acknowledge that there is room for improvement in the code, and I am committed to exploring more efficient ways to implement the RLP algorithm in the future.```

```Thank you for taking the time to review this project, and I welcome any feedback or suggestions for improvement.```