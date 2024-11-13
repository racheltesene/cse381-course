/* CSE 381 - RSA 
*  (c) BYU-Idaho - It is an honor code violation to post this
*  file completed in a public file sharing site. F4.
*
*  Instructions: Implement the Euclid, ModularExponentiation, GeneratePrivateKey,
*  Encrypt, and Decrypt functions per the instructions in the comments.  
*  Run all tests in RSATest.cs to verify your code.
*/

using System.Numerics;

namespace AlgorithmLib
{
    public class RSA
    {
        /* Extended Euclidean Algorithm to calculate the GCD (Greatest Common Divisor) 
         * of two numbers `a` and `b`, as well as coefficients `i` and `j` such that:
         * gcd = i * a + j * b.
         *
         * Inputs:
         *     a - First number
         *     b - Second number
         * Outputs:
         *     A tuple (gcd, i, j) where:
         *     - gcd is the greatest common divisor of a and b
         *     - i and j are the coefficients for a and b respectively in the linear combination
         */
        public static (BigInteger, BigInteger, BigInteger) Euclid(BigInteger a, BigInteger b)
        {
            // Base case: If b is zero, gcd is a. Coefficients i=1, j=0 represent gcd = 1*a + 0*b.
            if (b == 0)
            {
                return (a, 1, 0);
            }

            // Recursive case: Compute gcd and coefficients for (b, a % b)
            var (gcd, i, j) = Euclid(b, a % b);

            // Update coefficients: `i` becomes `j`, and `j` becomes `i - (a / b) * j`
            // This ensures gcd = i*b + j*(a % b) is equivalent to gcd = i*b + j*(a - (a / b) * b)
            return (gcd, j, i - (a / b) * j);
        }

        /* Modular Exponentiation function to compute x^y mod n efficiently.
         * This uses the "exponentiation by squaring" method, which reduces the number of 
         * multiplications, especially useful for large numbers.
         *
         * Inputs:
         *     x - The base number
         *     y - The exponent
         *     n - The modulus
         * Output:
         *     Result of (x^y) mod n
         */
        public static BigInteger ModularExponentiation(BigInteger x, BigInteger y, BigInteger n)
        {
            // Base case: any number raised to 0 is 1, so return 1 for y == 0
            if (y == 0)
            {
                return 1;
            }

            // Recursive case: if `y` is even, split into (x^(y/2))^2 mod n
            if (y % 2 == 0)
            {
                // Calculate (x^(y/2)) mod n, then square the result
                BigInteger halfPower = ModularExponentiation(x, y / 2, n);
                return (halfPower * halfPower) % n;
            }
            else // If `y` is odd, use the formula x * (x^(y-1)) mod n
            {
                return (x * ModularExponentiation(x, y - 1, n)) % n;
            }
        }

        /* RSA Private Key Generation
         * Generates the RSA private key `d` given two prime numbers `p` and `q`, and
         * the public exponent `e`.
         *
         * Inputs:
         *     p - First prime number
         *     q - Second prime number
         *     e - Public exponent, which is co-prime with φ(n) = (p-1)*(q-1)
         * Output:
         *     Private key `d`, a positive integer satisfying (e * d) % φ(n) = 1
         */
        public static BigInteger GeneratePrivateKey(BigInteger p, BigInteger q, BigInteger e)
        {
            // Calculate φ(n) = (p-1) * (q-1), Euler's totient function for modulus `n`
            BigInteger r = (p - 1) * (q - 1);

            // Use the Euclidean algorithm to find `d` such that (e * d) % r = 1.
            var (gcd, d, _) = Euclid(e, r);

            // Adjust `d` to be positive, as modular inverses are typically required to be non-negative
            if (d < 0)
            {
                d += r;
            }

            return d; // Return the computed private key
        }

        /* RSA Encryption
         * Encrypts a value using the RSA public key components `e` (exponent) and `n` (modulus).
         *
         * Inputs:
         *     value - The plaintext integer value to be encrypted
         *     e - Public exponent
         *     n - Modulus (product of primes p and q)
         * Output:
         *     Encrypted integer value: (value^e) mod n
         */
        public static BigInteger Encrypt(BigInteger value, BigInteger e, BigInteger n)
        {
            // Perform encryption by calculating (value^e) mod n using modular exponentiation
            return ModularExponentiation(value, e, n);
        }

        /* RSA Decryption
         * Decrypts an encrypted value using the RSA private key `d` and modulus `n`.
         *
         * Inputs:
         *     value - The encrypted integer value
         *     d - Private key exponent
         *     n - Modulus (product of primes p and q)
         * Output:
         *     Decrypted integer value: (value^d) mod n
         */
        public static BigInteger Decrypt(BigInteger value, BigInteger d, BigInteger n)
        {
            // Perform decryption by calculating (value^d) mod n using modular exponentiation
            return ModularExponentiation(value, d, n);
        }
    }
}
