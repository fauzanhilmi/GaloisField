# GaloisField
A implementation of Galois Field / Finite Field GF(2<sup>8</sup>) in C#

## Environment
This console-based project is developed with Visual Studio 2015

## Information
These are the operators that are implemented.
* Addition (+)
* Substraction (-)
* Multiplication (*)
* Division (/)
* Power
* Equality Check (==)
* Inequality Check (!=)

I used x<sup>8</sup> + x<sup>4</sup> + x<sup>3</sup> + x<sup>2</sup> + 1 (0x11D) as the irreducible polynomial.  
Multiplication and division are implemented with log table look up. See [this](https://en.wikipedia.org/wiki/Finite_field_arithmetic#Implementation_tricks) and [this](http://www.cs.utsa.edu/~wagner/laws/FFM.html) for more information.

## License
This project is licensed under the MIT License
