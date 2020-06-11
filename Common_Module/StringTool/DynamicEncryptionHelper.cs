
using System;
using System.Text;
using System.Security.Cryptography;

namespace Common_Module.StringTool
{
    /// <summary>
    /// A class that provides easy Blowfish encryption.   动态加密工具类
    /// <p>
    /// </summary>
    ///
    public class DynamicEncryptionHelper
    {

        private BlowfishCBC m_bfish;
        private static Random m_rndGen = new Random();

        /// <summary>
        /// Creates a new Blowfish object using the specified key (oversized password
        /// will be cut).
        /// </summary>
        ///
        /// <param name="password">the password (treated as a real unicode array)</param>
        public DynamicEncryptionHelper(String key)
        {
            // hash down the password to a 160bit key
            SHA1 digest = null;
            try
            {
                digest = System.Security.Cryptography.SHA1.Create();
                digest.ComputeHash(System.Text.Encoding.ASCII.GetBytes(key));
            }
            catch(Exception ex)
            {
                throw ex;
            }

            // setup the encryptor (use a dummy IV)
            m_bfish = new BlowfishCBC(digest.Hash, 0);
            digest.Initialize();
        }

        /// <summary>
        /// Encrypts a string (treated in UNICODE) using the standard Java random
        /// generator, which isn't that great for creating IVs
        /// </summary>
        ///
        /// <param name="sPlainText">string to encrypt</param>
        /// <returns>encrypted string in binhex format</returns>
        public String EncryptString(String sPlainText)
        {
            // get the IV
            long lCBCIV;
            lock (m_rndGen)
            {
                byte[] bb = new byte[256];
                m_rndGen.NextBytes(bb);
                lCBCIV = DynamicEncryptionHelper.ByteArrayToLong(bb, 0);
            }

            // map the call;
            return EncStr(sPlainText, lCBCIV);
        }

        // Internal routine for string encryption

        private String EncStr(String sPlainText, long lNewCBCIV)
        {
            // allocate the buffer (align to the next 8 byte border plus padding)
            int nStrLen = sPlainText.Length;
            byte[] buf = new byte[((nStrLen << 1) & -8) + 8];

            // copy all bytes of the string into the buffer (use network byte order)
            int nI;
            int nPos = 0;
            for (nI = 0; nI < nStrLen; nI++)
            {
                char cActChar = sPlainText[nI];
                buf[nPos++] = (byte)((cActChar >> 8) & 0x0ff);
                buf[nPos++] = (byte)(cActChar & 0x0ff);
            }

            // pad the rest with the PKCS5 scheme
            byte bPadVal = (byte)(buf.Length - (nStrLen << 1));
            while (nPos < buf.Length)
            {
                buf[nPos++] = bPadVal;
            }

            lock (m_bfish)
            {
                // create the encryptor
                m_bfish.SetCBCIV(lNewCBCIV);

                // encrypt the buffer
                m_bfish.Encrypt(buf);
            }

            // return the binhex string
            byte[] newCBCIV = new byte[DynamicEncryptionHelper.BlowfishECB.BLOCKSIZE];
            LongToByteArray(lNewCBCIV, newCBCIV, 0);

            return BytesToBinHex(newCBCIV, 0, DynamicEncryptionHelper.BlowfishECB.BLOCKSIZE)
                    + BytesToBinHex(buf, 0, buf.Length);
        }

        /// <summary>
        /// decrypts a hexbin string (handling is case sensitive)
        /// </summary>
        ///
        /// <param name="sCipherText">hexbin string to decrypt</param>
        /// <returns>decrypted string (null equals an error)</returns>
        public String DecryptString(String sCipherText)
        {
            string value = "";
            try
            {
                // get the number of estimated bytes in the string (cut off broken
                // blocks)
                int nLen = (sCipherText.Length >> 1) & ~7;

                // does the given stuff make sense (at least the CBC IV)?
                if (nLen < DynamicEncryptionHelper.BlowfishECB.BLOCKSIZE)
                    return null;

                // get the CBC IV
                byte[] cbciv = new byte[DynamicEncryptionHelper.BlowfishECB.BLOCKSIZE];
                int nNumOfBytes = BinHexToBytes(sCipherText, cbciv, 0, 0,
                        DynamicEncryptionHelper.BlowfishECB.BLOCKSIZE);
                if (nNumOfBytes < DynamicEncryptionHelper.BlowfishECB.BLOCKSIZE)
                    return null;

                // something left to decrypt?
                nLen -= DynamicEncryptionHelper.BlowfishECB.BLOCKSIZE;
                if (nLen == 0)
                {
                    return "";
                }

                // get all data bytes now
                byte[] buf = new byte[nLen];

                nNumOfBytes = BinHexToBytes(sCipherText, buf,
                        DynamicEncryptionHelper.BlowfishECB.BLOCKSIZE * 2, 0, nLen);

                // we cannot accept broken binhex sequences due to padding
                // and decryption
                if (nNumOfBytes < nLen)
                {
                    return null;
                }

                lock (m_bfish)
                {
                    // (got it)
                    m_bfish.SetCBCIV(cbciv);

                    // decrypt the buffer
                    m_bfish.Decrypt(buf);
                }

                // get the last padding byte
                int nPadByte = (int)buf[buf.Length - 1] & 0x0ff;

                // ( try to get all information if the padding doesn't seem to be
                // correct)
                if ((nPadByte > 8) || (nPadByte < 0))
                {
                    nPadByte = 0;
                }

                // calculate the real size of this message
                nNumOfBytes -= nPadByte;
                if (nNumOfBytes < 0)
                {
                    return "";
                }

                // success
                value = ByteArrayToUNCString(buf, 0, nNumOfBytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        /// <summary>
        /// destroys (clears) the encryption engine, after that the instance is not
        /// valid anymore
        /// </summary>
        ///
        public void Destroy()
        {
            m_bfish.CleanUp();
        }

        /// <summary>
        /// implementation of the Blowfish encryption algorithm in ECB mode
        /// </summary>
        ///
        private class BlowfishECB
        {
            /// <summary>
            /// maximum possible key length 
            /// </summary>
            ///
            public const int MAXKEYLENGTH = 56;

            /// <summary>
            /// block size of this cipher (in bytes) 
            /// </summary>
            ///
            public const int BLOCKSIZE = 8;

            // size of the single boxes
            internal const int PBOX_ENTRIES = 18;
            internal const int SBOX_ENTRIES = 256;

            // the boxes
            internal int[] m_pbox;
            internal int[] m_sbox1;
            internal int[] m_sbox2;
            internal int[] m_sbox3;
            internal int[] m_sbox4;

            /// <summary>
            /// default constructor
            /// </summary>
            ///
            /// <param name="bfkey">key material, up to MAXKEYLENGTH bytes</param>
            public BlowfishECB(byte[] bfkey)
            {
                // create the boxes
                int nI;

                m_pbox = new int[PBOX_ENTRIES];

                for (nI = 0; nI < PBOX_ENTRIES; nI++)
                {
                    m_pbox[nI] = pbox_init[nI];
                }

                m_sbox1 = new int[SBOX_ENTRIES];
                m_sbox2 = new int[SBOX_ENTRIES];
                m_sbox3 = new int[SBOX_ENTRIES];
                m_sbox4 = new int[SBOX_ENTRIES];

                for (nI = 0; nI < SBOX_ENTRIES; nI++)
                {
                    m_sbox1[nI] = sbox_init_1[nI];
                    m_sbox2[nI] = sbox_init_2[nI];
                    m_sbox3[nI] = sbox_init_3[nI];
                    m_sbox4[nI] = sbox_init_4[nI];
                }

                // xor the key over the p-boxes

                int nLen = bfkey.Length;
                if (nLen == 0)
                    return; // such a setup is also valid (zero key "encryption" is
                // possible)
                int nKeyPos = 0;
                int nBuild = 0;
                int nJ;

                for (nI = 0; nI < PBOX_ENTRIES; nI++)
                {
                    for (nJ = 0; nJ < 4; nJ++)
                    {
                        nBuild = (nBuild << 8) | (((int)bfkey[nKeyPos]) & 0x0ff);

                        if (++nKeyPos == nLen)
                        {
                            nKeyPos = 0;
                        }
                    }
                    m_pbox[nI] ^= nBuild;
                }

                // encrypt all boxes with the all zero string
                long lZero = 0;

                // (same as above)
                for (nI = 0; nI < PBOX_ENTRIES; nI += 2)
                {
                    lZero = EncryptBlock(lZero);
                    m_pbox[nI] = (int)((long)(((ulong)lZero) >> 32));
                    m_pbox[nI + 1] = (int)(lZero & 0x0ffffffffL);
                }
                for (nI = 0; nI < SBOX_ENTRIES; nI += 2)
                {
                    lZero = EncryptBlock(lZero);
                    m_sbox1[nI] = (int)((long)(((ulong)lZero) >> 32));
                    m_sbox1[nI + 1] = (int)(lZero & 0x0ffffffffL);
                }
                for (nI = 0; nI < SBOX_ENTRIES; nI += 2)
                {
                    lZero = EncryptBlock(lZero);
                    m_sbox2[nI] = (int)((long)(((ulong)lZero) >> 32));
                    m_sbox2[nI + 1] = (int)(lZero & 0x0ffffffffL);
                }
                for (nI = 0; nI < SBOX_ENTRIES; nI += 2)
                {
                    lZero = EncryptBlock(lZero);
                    m_sbox3[nI] = (int)((long)(((ulong)lZero) >> 32));
                    m_sbox3[nI + 1] = (int)(lZero & 0x0ffffffffL);
                }
                for (nI = 0; nI < SBOX_ENTRIES; nI += 2)
                {
                    lZero = EncryptBlock(lZero);
                    m_sbox4[nI] = (int)((long)(((ulong)lZero) >> 32));
                    m_sbox4[nI + 1] = (int)(lZero & 0x0ffffffffL);
                }
            }

            /// <summary>
            /// to clear data in the boxes before an instance is freed
            /// </summary>
            ///
            public virtual void CleanUp()
            {
                int nI;

                for (nI = 0; nI < PBOX_ENTRIES; nI++)
                {
                    m_pbox[nI] = 0;
                }

                for (nI = 0; nI < SBOX_ENTRIES; nI++)
                {
                    m_sbox1[nI] = m_sbox2[nI] = m_sbox3[nI] = m_sbox4[nI] = 0;
                }
            }

            /// <summary>
            /// selftest routine, to check e.g. for a valid class file transmission
            /// </summary>
            ///
            /// <returns>true: selftest passed / false: selftest failed</returns>
            public static bool SelfTest()
            {
                // test vector #1 (checking for the "signed bug")
                byte[] testKey1 = { (byte) 0x1c, (byte) 0x58, (byte) 0x7f,
                    (byte) 0x1c, (byte) 0x13, (byte) 0x92, (byte) 0x4f,
                    (byte) 0xef };
                int[] tv_p1 = { 0x30553228, 0x6d6f295a };
                int[] tv_c1 = { 0x55cb3774, -784403967 };
                int[] tv_t1 = new int[2];

                // test vector #2 (offical vector by Bruce Schneier)
                String sTestKey2 = "Who is John Galt?";
                byte[] testKey2 = System.Text.Encoding.ASCII.GetBytes(sTestKey2);

                int[] tv_p2 = { -19088744, 0x76543210 };
                int[] tv_c2 = { -862883029, -2145192316 };
                int[] tv_t2 = new int[2];

                // start the tests, check for a proper decryption, too

                BlowfishECB testbf1 = new BlowfishECB(testKey1);

                testbf1.Encrypt(tv_p1, tv_t1);

                if ((tv_t1[0] != tv_c1[0]) || (tv_t1[1] != tv_c1[1]))
                {
                    return false;
                }

                testbf1.Decrypt(tv_t1);

                if ((tv_t1[0] != tv_p1[0]) || (tv_t1[1] != tv_p1[1]))
                {
                    return false;
                }

                BlowfishECB testbf2 = new BlowfishECB(testKey2);

                testbf2.Encrypt(tv_p2, tv_t2);

                if ((tv_t2[0] != tv_c2[0]) || (tv_t2[1] != tv_c2[1]))
                {
                    return false;
                }

                testbf2.Decrypt(tv_t2);

                if ((tv_t2[0] != tv_p2[0]) || (tv_t2[1] != tv_p2[1]))
                {
                    return false;
                }

                // all tests passed
                return true;
            }

            // internal routine to encrypt a 64bit block
            protected internal long EncryptBlock(long lPlainBlock)
            {
                // split the block in two 32 bit halves

                int nHi = DynamicEncryptionHelper.LongHi32(lPlainBlock);
                int nLo = DynamicEncryptionHelper.LongLo32(lPlainBlock);

                // encrypt the block, gain more speed by unrooling the loop
                // (we avoid swapping by using nHi and nLo alternating at
                // odd an even loop nubers) and using local references

                int[] sbox1 = m_sbox1;
                int[] sbox2 = m_sbox2;
                int[] sbox3 = m_sbox3;
                int[] sbox4 = m_sbox4;

                int[] pbox = m_pbox;

                nHi ^= pbox[0];
                nLo ^= (((sbox1[(int)(((uint)nHi) >> 24)] + sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + sbox4[nHi & 0x0ff])
                        ^ pbox[1];
                nHi ^= (((sbox1[(int)(((uint)nLo) >> 24)] + sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + sbox4[nLo & 0x0ff])
                        ^ pbox[2];
                nLo ^= (((sbox1[(int)(((uint)nHi) >> 24)] + sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + sbox4[nHi & 0x0ff])
                        ^ pbox[3];
                nHi ^= (((sbox1[(int)(((uint)nLo) >> 24)] + sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + sbox4[nLo & 0x0ff])
                        ^ pbox[4];
                nLo ^= (((sbox1[(int)(((uint)nHi) >> 24)] + sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + sbox4[nHi & 0x0ff])
                        ^ pbox[5];
                nHi ^= (((sbox1[(int)(((uint)nLo) >> 24)] + sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + sbox4[nLo & 0x0ff])
                        ^ pbox[6];
                nLo ^= (((sbox1[(int)(((uint)nHi) >> 24)] + sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + sbox4[nHi & 0x0ff])
                        ^ pbox[7];
                nHi ^= (((sbox1[(int)(((uint)nLo) >> 24)] + sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + sbox4[nLo & 0x0ff])
                        ^ pbox[8];
                nLo ^= (((sbox1[(int)(((uint)nHi) >> 24)] + sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + sbox4[nHi & 0x0ff])
                        ^ pbox[9];
                nHi ^= (((sbox1[(int)(((uint)nLo) >> 24)] + sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + sbox4[nLo & 0x0ff])
                        ^ pbox[10];
                nLo ^= (((sbox1[(int)(((uint)nHi) >> 24)] + sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + sbox4[nHi & 0x0ff])
                        ^ pbox[11];
                nHi ^= (((sbox1[(int)(((uint)nLo) >> 24)] + sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + sbox4[nLo & 0x0ff])
                        ^ pbox[12];
                nLo ^= (((sbox1[(int)(((uint)nHi) >> 24)] + sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + sbox4[nHi & 0x0ff])
                        ^ pbox[13];
                nHi ^= (((sbox1[(int)(((uint)nLo) >> 24)] + sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + sbox4[nLo & 0x0ff])
                        ^ pbox[14];
                nLo ^= (((sbox1[(int)(((uint)nHi) >> 24)] + sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + sbox4[nHi & 0x0ff])
                        ^ pbox[15];
                nHi ^= (((sbox1[(int)(((uint)nLo) >> 24)] + sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + sbox4[nLo & 0x0ff])
                        ^ pbox[16];

                // finalize, cross and return the reassembled block

                return DynamicEncryptionHelper.MakeLong(nHi, nLo ^ pbox[17]);
            }

            // internal routine to decrypt a 64bit block
            protected internal long DecryptBlock(long lCipherBlock)
            {
                // (same as above)

                int nHi = DynamicEncryptionHelper.LongHi32(lCipherBlock);
                int nLo = DynamicEncryptionHelper.LongLo32(lCipherBlock);

                nHi ^= m_pbox[17];
                nLo ^= (((m_sbox1[(int)(((uint)nHi) >> 24)] + m_sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + m_sbox4[nHi & 0x0ff])
                        ^ m_pbox[16];
                nHi ^= (((m_sbox1[(int)(((uint)nLo) >> 24)] + m_sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + m_sbox4[nLo & 0x0ff])
                        ^ m_pbox[15];
                nLo ^= (((m_sbox1[(int)(((uint)nHi) >> 24)] + m_sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + m_sbox4[nHi & 0x0ff])
                        ^ m_pbox[14];
                nHi ^= (((m_sbox1[(int)(((uint)nLo) >> 24)] + m_sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + m_sbox4[nLo & 0x0ff])
                        ^ m_pbox[13];
                nLo ^= (((m_sbox1[(int)(((uint)nHi) >> 24)] + m_sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + m_sbox4[nHi & 0x0ff])
                        ^ m_pbox[12];
                nHi ^= (((m_sbox1[(int)(((uint)nLo) >> 24)] + m_sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + m_sbox4[nLo & 0x0ff])
                        ^ m_pbox[11];
                nLo ^= (((m_sbox1[(int)(((uint)nHi) >> 24)] + m_sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + m_sbox4[nHi & 0x0ff])
                        ^ m_pbox[10];
                nHi ^= (((m_sbox1[(int)(((uint)nLo) >> 24)] + m_sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + m_sbox4[nLo & 0x0ff])
                        ^ m_pbox[9];
                nLo ^= (((m_sbox1[(int)(((uint)nHi) >> 24)] + m_sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + m_sbox4[nHi & 0x0ff])
                        ^ m_pbox[8];
                nHi ^= (((m_sbox1[(int)(((uint)nLo) >> 24)] + m_sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + m_sbox4[nLo & 0x0ff])
                        ^ m_pbox[7];
                nLo ^= (((m_sbox1[(int)(((uint)nHi) >> 24)] + m_sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + m_sbox4[nHi & 0x0ff])
                        ^ m_pbox[6];
                nHi ^= (((m_sbox1[(int)(((uint)nLo) >> 24)] + m_sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + m_sbox4[nLo & 0x0ff])
                        ^ m_pbox[5];
                nLo ^= (((m_sbox1[(int)(((uint)nHi) >> 24)] + m_sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + m_sbox4[nHi & 0x0ff])
                        ^ m_pbox[4];
                nHi ^= (((m_sbox1[(int)(((uint)nLo) >> 24)] + m_sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + m_sbox4[nLo & 0x0ff])
                        ^ m_pbox[3];
                nLo ^= (((m_sbox1[(int)(((uint)nHi) >> 24)] + m_sbox2[((int)(((uint)nHi) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nHi) >> 8)) & 0x0ff]) + m_sbox4[nHi & 0x0ff])
                        ^ m_pbox[2];
                nHi ^= (((m_sbox1[(int)(((uint)nLo) >> 24)] + m_sbox2[((int)(((uint)nLo) >> 16)) & 0x0ff]) ^ m_sbox3[((int)(((uint)nLo) >> 8)) & 0x0ff]) + m_sbox4[nLo & 0x0ff])
                        ^ m_pbox[1];

                return DynamicEncryptionHelper.MakeLong(nHi, nLo ^ m_pbox[0]);
            }

            /// <summary>
            /// Encrypts a byte buffer (should be aligned to an 8 byte border) to
            /// another buffer (of the same size or bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with plaintext data</param>
            /// <param name="outbuffer">buffer to get the ciphertext data</param>
            public virtual void Encrypt(byte[] inbuffer, byte[] outbuffer)
            {
                int nLen = inbuffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 8)
                {
                    // encrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.ByteArrayToLong(inbuffer, nI);
                    lTemp = EncryptBlock(lTemp);
                    DynamicEncryptionHelper.LongToByteArray(lTemp, outbuffer, nI);
                }
            }

            /// <summary>
            /// encrypts a byte buffer (should be aligned to an 8 byte border) to
            /// itself
            /// </summary>
            ///
            /// <param name="buffer">buffer to encrypt</param>
            public virtual void Encrypt(byte[] buffer)
            {
                int nLen = buffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 8)
                {
                    // encrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.ByteArrayToLong(buffer, nI);
                    lTemp = EncryptBlock(lTemp);
                    DynamicEncryptionHelper.LongToByteArray(lTemp, buffer, nI);
                }
            }

            /// <summary>
            /// encrypts an integer buffer (should be aligned to an two integer
            /// border) to another int buffer (of the same size or bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with plaintext data</param>
            /// <param name="outbuffer">buffer to get the ciphertext data</param>
            public virtual void Encrypt(int[] inbuffer, int[] outbuffer)
            {
                int nLen = inbuffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 2)
                {
                    // encrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.IntArrayToLong(inbuffer, nI);
                    lTemp = EncryptBlock(lTemp);
                    DynamicEncryptionHelper.LongToIntArray(lTemp, outbuffer, nI);
                }
            }

            /// <summary>
            /// encrypts an int buffer (should be aligned to a two integer border)
            /// </summary>
            ///
            /// <param name="buffer">buffer to encrypt</param>
            public virtual void Encrypt(int[] buffer)
            {
                int nLen = buffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 2)
                {
                    // encrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.IntArrayToLong(buffer, nI);
                    lTemp = EncryptBlock(lTemp);
                    DynamicEncryptionHelper.LongToIntArray(lTemp, buffer, nI);
                }
            }

            /// <summary>
            /// encrypts a long buffer to another long buffer (of the same size or
            /// bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with plaintext data</param>
            /// <param name="outbuffer">buffer to get the ciphertext data</param>
            public virtual void Encrypt(long[] inbuffer, long[] outbuffer)
            {
                int nLen = inbuffer.Length;
                for (int nI = 0; nI < nLen; nI++)
                {
                    outbuffer[nI] = EncryptBlock(inbuffer[nI]);
                }
            }

            /// <summary>
            /// encrypts a long buffer to itself
            /// </summary>
            ///
            /// <param name="buffer">buffer to encrypt</param>
            public virtual void Encrypt(long[] buffer)
            {
                int nLen = buffer.Length;
                for (int nI = 0; nI < nLen; nI++)
                {
                    buffer[nI] = EncryptBlock(buffer[nI]);
                }
            }

            /// <summary>
            /// decrypts a byte buffer (should be aligned to an 8 byte border) to
            /// another byte buffer (of the same size or bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with ciphertext data</param>
            /// <param name="outbuffer">buffer to get the plaintext data</param>
            public virtual void Decrypt(byte[] inbuffer, byte[] outbuffer)
            {
                int nLen = inbuffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 8)
                {
                    // decrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.ByteArrayToLong(inbuffer, nI);
                    lTemp = DecryptBlock(lTemp);
                    DynamicEncryptionHelper.LongToByteArray(lTemp, outbuffer, nI);
                }
            }

            /// <summary>
            /// decrypts a byte buffer (should be aligned to an 8 byte border) to
            /// itself
            /// </summary>
            ///
            /// <param name="buffer">buffer to decrypt</param>
            public virtual void Decrypt(byte[] buffer)
            {
                int nLen = buffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 8)
                {
                    // decrypt over a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.ByteArrayToLong(buffer, nI);
                    lTemp = DecryptBlock(lTemp);
                    DynamicEncryptionHelper.LongToByteArray(lTemp, buffer, nI);
                }
            }

            /// <summary>
            /// decrypts an integer buffer (should be aligned to an two integer
            /// border) to another int buffer (of the same size or bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with ciphertext data</param>
            /// <param name="outbuffer">buffer to get the plaintext data</param>
            public virtual void Decrypt(int[] inbuffer, int[] outbuffer)
            {
                int nLen = inbuffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 2)
                {
                    // decrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.IntArrayToLong(inbuffer, nI);
                    lTemp = DecryptBlock(lTemp);
                    DynamicEncryptionHelper.LongToIntArray(lTemp, outbuffer, nI);
                }
            }

            /// <summary>
            /// decrypts an int buffer (should be aligned to an two integer border)
            /// </summary>
            ///
            /// <param name="buffer">buffer to decrypt</param>
            public virtual void Decrypt(int[] buffer)
            {
                int nLen = buffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 2)
                {
                    // decrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.IntArrayToLong(buffer, nI);
                    lTemp = DecryptBlock(lTemp);
                    DynamicEncryptionHelper.LongToIntArray(lTemp, buffer, nI);
                }
            }

            /// <summary>
            /// decrypts a long buffer to another long buffer (of the same size or
            /// bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with ciphertext data</param>
            /// <param name="outbuffer">buffer to get the plaintext data</param>
            public virtual void Decrypt(long[] inbuffer, long[] outbuffer)
            {
                int nLen = inbuffer.Length;
                for (int nI = 0; nI < nLen; nI++)
                {
                    outbuffer[nI] = DecryptBlock(inbuffer[nI]);
                }
            }

            /// <summary>
            /// decrypts a long buffer to itself
            /// </summary>
            ///
            /// <param name="buffer">buffer to decrypt</param>
            public virtual void Decrypt(long[] buffer)
            {

                int nLen = buffer.Length;
                for (int nI = 0; nI < nLen; nI++)
                {
                    buffer[nI] = DecryptBlock(buffer[nI]);
                }
            }

            // the boxes init. data,
            // FIXME: it might be better to create them at runtime to make the class
            // file smaller, e.g. by calculating the hexdigits of pi (default)
            // or just a fixed random sequence (out of the standard)

            static internal readonly int[] pbox_init = { 0x243f6a88, -2052912941, 0x13198a2e,
                0x03707344, -1542899678, 0x299f31d0, 0x082efa98, -330404727,
                0x452821e6, 0x38d01377, -1101764913, 0x34e90c6c, -1062458953,
                -914599715, 0x3f84d5b5, -1253635817, -1843997223, -1988494565 };

            static internal readonly int[] sbox_init_1 = { -785314906, -1730169428, 0x2ffd72db,
                -803545161, -1193168915, 0x6a267e96, -1166241723, -248741991,
                0x24a19947, -1282315017, 0x0801f2e2, -2054226922, 0x636920d8,
                0x71574e69, -1537671517, -191677058, 0x0d95748f, 0x728eb658,
                0x718bcd58, -2112533778, 0x7b54a41d, -1034266187, -1674521287,
                0x2af26013, -976113629, 0x286085f0, -901678824, -1193592593,
                -1904616272, 0x603a180e, 0x6c9e0e8b, -1340175810, -686458943,
                -1120842969, 0x78af2fda, 0x55605c60, -430627341, -1437226092,
                0x57489862, 0x63e81440, 0x55ca396a, 0x2aab10b6, -1261675468,
                0x1141e8ce, -1588296017, 0x7c72e993, -1276242927, 0x636fbc2a,
                0x2ba9c55d, 0x741831f6, -832815594, -1685613794, -1344882125,
                0x6c24cf5c, 0x7a325381, 0x28958677, 0x3b8f4898, 0x6b4bb9af,
                -994056165, 0x66282193, 0x61d809cc, -81679983, 0x487cac60,
                0x5dec8032, -276538019, -377129551, -601480446, -345695352,
                0x23893e81, -745100091, 0x0f6d6ff3, -2081144263, 0x2e0b4482,
                -1534844924, 0x69c8f04a, -1642095778, 0x21c66842, -152474470,
                0x670c9c61, -1412200208, 0x6a51a0d2, -665571480, -1777359064,
                -1420741725, 0x6eef0b6c, 0x137a3be4, -1170476976, 0x7efb2a98,
                -1578015459, 0x39af0176, 0x66ca593e, -2109534584, -1930525159,
                0x456f9fb4, 0x7d84a5c3, 0x3b8b5ebe, -529566248, -2050940813,
                0x401a449f, 0x56c16aa6, 0x4ed3aa62, 0x363f7706, 0x1bfedf72,
                0x429b023d, 0x37d0d724, -804646328, -619713837, 0x49f1c09b,
                0x075372c9, -2137449605, 0x25d479d8, -152510729, -469872614,
                -1233564613, -1754472259, 0x04c006ba, -1045868618, 0x409f60c4,
                0x5e5c9ec2, 0x196a2463, 0x68fb6faf, 0x3e6c53b5, 0x1339b2eb,
                0x3b52ec6f, 0x6dfc511f, -1691314900, -863943356, -1352745719,
                -1092366332, -567063811, 0x660f2807, 0x192e4bb3, -1060394921,
                0x45c8740f, -771006663, -1177289765, 0x5579c0bd, 0x1a60320a,
                -694091578, 0x402c7279, 0x679f25fe, -81812532, -1901729288,
                -617471240, 0x3c7516df, -43947243, 0x2f501ec8, -1392160085,
                0x323db5fa, -48003232, 0x53317b48, 0x3e00df82, -1638115397,
                -898659168, 0x1a87562e, -552113701, -717051658, 0x287effc3,
                -1402522938, -1940957837, 0x695b27b0, -1144366904, -503340195,
                -1192226400, 0x10fa3d98, -48135240, 0x4afcb56c, 0x2dd1d35b,
                -1705778055, -1225243291, -762426948, 0x4bfb9790, -505548070,
                -1530167757, 0x62fb1341, -823867672, -283063590, 0x36774c01,
                -797008130, 0x2bf11fb4, -1780753843, -1366257256, -357724559,
                0x6b93d5a0, -795946544, -1345903136, -1908647121, -1904896841,
                -1879645445, -233690268, -2004305902, -1878134756, 0x4fad5ea0,
                0x688fc31c, -774901359, -1280786003, 0x2f2f2218, -1106372745,
                -361419266, -1962795103, -442446833, -1250986776, 0x18acf3d6,
                -829824359, -1264037920, -49028937, 0x7cc43b81, -760370983,
                0x165fa266, -2137688315, -1815317740, 0x211a1477, -424861595,
                0x77b5fa86, -950779147, -73583153, -338841844, 0x7b3e89a0,
                -700376109, -1373733303, 0x00250e2d, 0x2071b35e, 0x226800bb,
                0x57b8e0af, 0x2464369b, -267798242, 0x5563911d, 0x59dfa6aa,
                0x78c14389, -648391809, 0x207d5ba2, 0x02e5b9c5, -2094660746,
                0x6295cfa9, 0x11c81968, 0x4e734a41, -1287180854, 0x7b14a94a,
                0x1b510052, -1705826027, -703637697, -1130641692, 0x2b60a476,
                -2115603456, 0x08ba6fb5, 0x571be91f, -224990101, 0x2a0dd915,
                -1235000031, -407242314, -13368018, -981117340, 0x53b02d5d,
                -1449160799, 0x08ba4799, 0x6e85076a };

            static internal readonly int[] sbox_init_2 = { 0x4b7a70e9, -1246549692, -613086930,
                -1004984797, -1385257296, 0x49a7df7d, -1662099272, -1880247706,
                -324367247, 0x699a17ff, 0x5664526c, -1028546847, 0x193602a5,
                0x75094c29, -1604775104, -468174274, 0x3f54989a, 0x5b429d65,
                0x6b8fe4d6, -1711849514, -1580033017, -269995787, 0x4d2d38e6,
                -265986623, 0x4cdd2086, -2072974554, 0x6382e9c6, 0x021ecc5e,
                0x09686b3f, 0x3ebaefc9, 0x3c971814, 0x6b6a70a1, 0x687f3584,
                0x52a0e286, -1214491899, -1437595849, 0x3e07841c, 0x7fdeae5c,
                -1904392980, 0x5716f2b8, -1338320329, -263189491, -266592508,
                0x0200b3ff, -1374882534, 0x3cb574b2, 0x25837a58, -603381315,
                -779021319, 0x7ca92ff6, -1808644237, 0x22f54701, 0x3ae5e581,
                0x37c2dadc, -927631820, -1695294041, -1455136442, 0x0fd0030e,
                -322386114, -1535828415, -499593831, 0x3bea0e2f, 0x3280bba1,
                0x183eb331, 0x4e548b38, 0x4f6db908, 0x6f420d03, -167115585,
                0x2cb81290, 0x24977c79, 0x5679b072, -1129346641, -560302305,
                -644675568, -1282691566, -590397650, 0x5512721f, 0x2e6b7124,
                0x501adde6, -1618686585, 0x7a584718, 0x7408da17, -1130390852,
                -380928628, -327488454, -612033030, 0x63094366, -1000029230,
                -283371449, 0x3215d908, -582796489, 0x24c2ba16, 0x12a14d43,
                0x2a65c451, 0x50940002, 0x133ae4dd, 0x71dff89e, 0x10314e55,
                -2119403562, 0x5f11199b, 0x043556f1, -677132437, 0x3c11183b,
                0x5924a509, -225450259, -1745748998, -1631928532, 0x1e153c6e,
                -2031925904, -353800271, -2045878774, 0x5a3e2ab3, 0x771fe71c,
                0x4e3d06fa, 0x2965dcb9, -1712906993, -2143385130, 0x5266c825,
                0x2e4cc978, -1676627094, -971698502, -1797068168, -1510196141,
                0x1e0a2df4, -218673497, 0x361d2b3d, 0x1939260f, 0x19c27960,
                0x5223a708, -149744970, -340918674, -356311194, -474200683,
                -1501837181, -1317062703, 0x018cff28, -1020076561, -1100195163,
                0x65582185, 0x68ab9802, -288447217, -617638597, 0x2aef7dad,
                0x5b6e2f84, 0x1521b628, 0x29076170, -321042571, 0x619f1510,
                0x13cca830, -345916010, 0x0334fe1e, -1442618417, -1250730864,
                0x4c70a239, -711025141, -877994476, -288586052, 0x60622ca7,
                -1666491221, -1292663698, 0x648b1eaf, 0x19bdf0ca, -1608291911,
                0x655abb50, 0x40685a32, 0x3c2ab4b3, 0x319ee9d5, -1071531785,
                -1688990951, -2023776103, -1778935426, 0x623d7da8, -130578278,
                -1746719369, 0x11ed935f, 0x16681281, 0x0e358829, -941219882,
                -1763778655, 0x7858ba99, 0x57f584a5, 0x1b227263, -1685863425,
                0x1ac24696, -843904277, 0x532e3054, -1881585436, 0x6dbc3128,
                0x58ebf2ef, 0x34c6ffea, -30872223, -293847949, 0x5d4a14d9,
                -396052509, 0x42105d14, 0x203e13e0, 0x45eee2b6, -1549095958,
                -613658859, -87339056, -951913406, -278217803, 0x654f3b1d,
                0x41cd2105, -669091426, -2038084153, -464828566, 0x3d816250,
                -815619598, 0x5b8d2646, -58162272, -1043876189, 0x7f1524c3,
                0x69cb7492, 0x47848a0b, 0x5692b285, 0x095bbf00, -1390851939,
                0x1462b174, 0x23820e00, 0x58428d2a, 0x0c55f5ea, 0x1dadf43e,
                0x233f7061, 0x3372f092, -1919713727, -698356495, 0x6c223bdb,
                0x7cde3759, -873565088, 0x4085f2a7, -831049106, -1509457788,
                0x19f8509e, -386934699, 0x61d99735, -1452693590, -989067582,
                0x5a04abfc, -2146710820, -1639679442, -1018874748, -36346107,
                0x0e1e9ec9, -613164077, 0x105588cd, 0x675fda79, -479771840,
                -976997275, 0x713e38d8, 0x3d28f89e, -244449504, 0x153e21e7,
                -1884275382, -421290197, -612127241 };

            static internal readonly int[] sbox_init_3 = { -381855128, -1803468553, -162781668,
                -1805047500, 0x411520f7, 0x7602d4f7, -1124832466, -727580568,
                -737663887, 0x3320f46a, 0x43b7d4b7, 0x500061af, 0x1e39f62e,
                -1759230650, 0x14214f74, -1081374656, 0x4d95fc1d, -1766485585,
                0x70f4ddd3, 0x66a02f45, -1078195732, 0x03bd9785, 0x7fac6dd0,
                0x31cb8504, -1762973773, 0x55fd3941, -635090970, -1412822374,
                0x28507825, 0x530429f4, 0x0a2c86da, -373920261, 0x68dc1462,
                -683120384, 0x680ec0a4, 0x27a18dee, 0x4f3ffea2, -393761396,
                -1249058810, 0x7af4d6b6, -1429332356, -751345684, -830954599,
                0x406b2a42, 0x20fe9e35, -638351943, -298199125, 0x3b124e8b,
                0x1dc9faf7, 0x4b6d1856, 0x26a36631, -354183246, 0x3a6efa74,
                -581221582, 0x6841e7f7, -898096901, -83167922, -654396521,
                0x454056ac, -1169648345, 0x55533a3a, 0x20838d87, -26498633,
                -795437749, 0x55a867bc, -1592419752, -861329053, -1713251533,
                -1507177898, 0x3f3125f9, 0x5ef47e1c, -1876348548, -34019326,
                0x04272f70, -2135222948, 0x05282ce3, -1782508216, -456757982,
                0x48c1133f, -955283748, 0x07f9c9ee, 0x41041f0f, 0x404779a4,
                0x5d886e17, 0x325f51eb, -711212847, -222510705, 0x41113564,
                0x257b7834, 0x602a9c60, -537335645, 0x1f636c1b, 0x0e12b4c2,
                0x02e1329e, -1352249391, -892239595, 0x6b2395e0, 0x333e92e1,
                0x3b240b62, -289490654, -2051890674, -424014439, -562951028,
                0x2da2f728, -804095931, -1783130883, 0x647d0862, -405998096,
                0x5449a36f, -2021832454, -1013056217, -214004450, 0x0a476341,
                -1724973196, 0x3a6f6eab, -185008841, -1475158944, -1578377736,
                -1726226100, -613520627, -964995824, 0x6d672c37, 0x2765d43b,
                -590288892, -248967737, -872349789, -1254551662, 0x690fed0b,
                0x667b9ffb, -824476260, -1601057013, -652910941, -1156370552,
                0x515bad24, 0x7b9479bf, 0x763bd6eb, 0x37392eb3, -871278215,
                -2144935273, -198299347, 0x6842ada7, -966120645, 0x12754ccc,
                0x782ef11c, 0x6a124237, -1215147545, 0x06a1bbe6, 0x4bfb6350,
                0x1a6b1018, 0x11caedfa, 0x3d25bdd8, -488520759, 0x44421659,
                0x0a121386, -653464466, -710153686, 0x64af674e, -628709281,
                -1094719096, 0x64e4c3fe, -1648590761, -252198778, 0x60787bf8,
                0x6003604d, -771914938, -164094032, 0x7745ae04, -684262196,
                -2092799181, -266425487, -1333771897, 0x3c005e5f, 0x77a057be,
                -1108824540, 0x55464299, -1084739999, 0x4e58f48f, -220332638,
                -193663176, -2021016126, 0x5366f9c3, -927756684, -1267338667,
                0x46fcd9b9, 0x7aeb2661, -1960976508, -2073424263, -1856006686,
                0x466e598e, 0x20b45770, -1932175983, -922558900, -1190417183,
                -1149106736, 0x11a86248, 0x7574a99e, -1216407114, -525738999,
                0x662d09a1, -1003338189, -396747006, 0x09f0be8c, 0x4a99a025,
                0x1d6efe10, 0x1ab93d1d, 0x0ba5a4df, -1584991729, 0x2868f169,
                -591930749, 0x573906fe, -1578971493, 0x4fcd7f52, 0x50115e01,
                -1492745222, -1610435132, 0x0de6d027, -1694987225, 0x773f8641,
                -1017099258, 0x61a806b5, -266896856, -1057650976, 0x006058aa,
                0x30dc7d62, 0x11e69ed7, 0x2338ea63, 0x53c2dd94, -1027467724,
                -1144263082, -1866680610, -335774303, -833020554, 0x6f05e409,
                0x4b7c0188, 0x39720a3d, 0x7c927c24, -2031914401, 0x724d9db9,
                0x1ac15bb4, -744572676, -313240200, 0x08fca5b5, -667058989,
                0x4dad0fc4, 0x1e50ef5e, -1318983944, -1568336679, 0x6c51133c,
                0x6fd5c7e7, 0x56e14ec4, 0x362abfce, -574175177, -677760460,
                -1838972398, 0x670efa8e, 0x406000e0 };

            static internal readonly int[] sbox_init_4 = { 0x3a39ce37, -738527793, -1413318857,
                0x5ac52d1b, 0x5cb0679e, 0x4fa33742, -746444992, -1715692610,
                -720269667, -1089506539, -701686658, -956251013, -1215554709,
                0x21a19045, -1301368386, 0x6a366eb4, 0x5748ab2f, -1131123079,
                -962365742, 0x6549c2c8, 0x530ff8ee, 0x468dde7d, -713881059,
                0x4cd04dc6, 0x2939bbdb, -1447410096, -1399511320, -1101077756,
                -1577396752, 0x6a2d519a, 0x63ef8ce2, -1702433246, -1064713544,
                0x43242ef6, -1524759638, -1661808476, -2084544070, -1679201715,
                -1880812208, -1167828010, 0x2826a2f9, -1489356063, 0x4ba99586,
                -279616791, -953159725, -145557542, 0x3f046f69, 0x77fa0a59,
                -2132498155, -2018474495, -1693849939, 0x3b3ee593, -376373926,
                -1640704105, 0x2cf0b7d9, 0x022b8b51, -1764381638, 0x017da67d,
                -774947114, 0x7c7d2d28, 0x1f9f25cf, -1376601957, 0x5ad6b472,
                0x5a88f54c, -534139791, -535190042, 0x47b0acfd, -309069157,
                -388774771, 0x283b57cc, -120232407, 0x79132e28, 0x785f0191,
                -311074731, -141160892, -472686964, 0x15056dd4, -1997247046,
                0x03a16125, 0x0564f0bd, -1007968747, 0x3c9057a2, -1759044884,
                -1455814870, 0x1b3f6d9b, 0x1e6321f5, -174299397, 0x26dcf319,
                0x7533d928, -1319764491, 0x03563482, -1967506245, 0x28517711,
                -1039476232, -1412673177, -861040033, 0x4de81751, 0x3830dc8e,
                0x379d5862, -1826555503, -361066302, -79791154, 0x5121ce64,
                0x774fbe32, -1464409218, -1020707514, 0x48de5369, 0x6413e680,
                -1565652976, -580013532, 0x69852dfd, 0x09072166, -1281735158,
                0x6445c0dd, 0x586cdecf, 0x1c20c8ae, 0x5bbef7dd, 0x1b588d40,
                -858652289, 0x6bb4e3bb, -576558466, 0x3a59ff45, 0x3e350a44,
                -1129001515, 0x72eacea8, -94075717, -1922690386, -1086558393,
                -761535389, 0x542f5d9e, -1362987237, -162634896, 0x740e0d8d,
                -413461673, -126740879, -1353482915, 0x4040cb08, 0x4eb4e2cc,
                0x34d2466a, 0x0115af84, -508558296, -1785185763, 0x06b89fb4,
                -831610808, 0x6f3f3b82, 0x3520ab82, 0x011a1d4b, 0x277227f8,
                0x611560b1, -409780260, -1153795797, 0x344525bd, -1601685023,
                0x51ce794b, 0x2f32c9b7, -1608533303, -534984578, -1127755274,
                -822013501, -1578587449, 0x1a908749, -732971622, -790962485,
                -720709064, 0x0339c32a, -963561881, -1913048708, -525259953,
                -140617289, 0x43f5bb3a, -220915201, 0x27d9459c, -1080614356,
                0x15e6fc2a, 0x0f91fc71, -1684794075, -85617823, -826893077,
                -1029151655, 0x12baa8d1, -1228863650, -486184436, 0x10d25065,
                -888953790, -521376242, 0x1698db3b, 0x4c98a0be, 0x3278e964,
                -1625320142, -523005217, -744475605, -1989021154, 0x1b0a7441,
                0x4ba3348c, -977374944, -1015663912, -550133875, -1684459730,
                -435458233, 0x0fe3f11d, -447948204, 0x1edad891, -832407089,
                -851542417, 0x1618b166, -47440635, -2070949179, -151313767,
                -182193321, -1506642397, -1817692879, 0x56cccd02, -1393524382,
                0x5a75ebb5, 0x6e163697, -1999473716, -560569710, -2118563376,
                0x4c50901b, 0x71c65614, -423180355, 0x327a140a, 0x45e1d006,
                -1007518822, -911584259, 0x62a80f00, -1155153950, 0x35bdd2f6,
                0x71126905, -1308360158, -1228157060, -847864789, 0x53113ec0,
                0x1640e3d3, 0x38abbd60, 0x2547adf0, -1170726756, -146354570,
                0x77afa1c5, 0x20756060, -2050228658, -1964470824, 0x7aaaf9b0,
                0x4cf9aa7e, 0x1948c25c, 0x02fb8a8c, 0x01c36ae4, -689184263,
                -1865090967, -1503863136, 0x3f09252d, -1039604065, -1219600078,
                -831004069, 0x578fdfe3, 0x3ac372e6 };
        }

        private class BlowfishCBC : BlowfishECB
        {

            // here we hold the CBC IV
            internal long m_lCBCIV;

            /// <summary>
            /// get the current CBC IV (for cipher resets)
            /// </summary>
            ///
            /// <returns>current CBC IV</returns>
            public long GetCBCIV()
            {
                return m_lCBCIV;
            }

            /// <summary>
            /// get the current CBC IV (for cipher resets)
            /// </summary>
            ///
            /// <param name="dest">wher eto put current CBC IV in network byte ordered array</param>
            public void GetCBCIV(byte[] dest)
            {
                DynamicEncryptionHelper.LongToByteArray(m_lCBCIV, dest, 0);
            }

            /// <summary>
            /// set the current CBC IV (for cipher resets)
            /// </summary>
            ///
            /// <param name="lNewCBCIV">the new CBC IV</param>
            public void SetCBCIV(long lNewCBCIV)
            {
                m_lCBCIV = lNewCBCIV;
            }

            /// <summary>
            /// set the current CBC IV (for cipher resets)
            /// </summary>
            ///
            /// <param name="newCBCIV">the new CBC IV in network byte ordered array</param>
            public void SetCBCIV(byte[] newCBCIV)
            {
                m_lCBCIV = DynamicEncryptionHelper.ByteArrayToLong(newCBCIV, 0);
            }

            /// <summary>
            /// constructor, stores a zero CBC IV
            /// </summary>
            ///
            /// <param name="bfkey">key material, up to MAXKEYLENGTH bytes</param>
            public BlowfishCBC(byte[] bfkey)
                : base(bfkey)
            {
                //base(bfkey);

                // store zero CBCB IV
                SetCBCIV(0);
            }

            /// <summary>
            /// constructor
            /// </summary>
            ///
            /// <param name="bfkey">key material, up to MAXKEYLENGTH bytes</param>
            /// <param name="lInitCBCIV">the CBC IV</param>
            public BlowfishCBC(byte[] bfkey, long lInitCBCIV)
                : base(bfkey)
            {
                //super(bfkey);

                // store the CBCB IV
                SetCBCIV(lInitCBCIV);
            }

            /// <summary>
            /// constructor
            /// </summary>
            ///
            /// <param name="bfkey">key material, up to MAXKEYLENGTH bytes</param>
            /// <param name="initCBCIV">the CBC IV (array with min. BLOCKSIZE bytes)</param>
            public BlowfishCBC(byte[] bfkey, byte[] initCBCIV)
                : base(bfkey)
            {
                //super(bfkey);

                // store the CBCB IV
                SetCBCIV(initCBCIV);
            }

            /// <summary>
            /// cleans up all critical internals, call this if you don't need an
            /// instance anymore
            /// </summary>
            ///
            public override void CleanUp()
            {
                m_lCBCIV = 0;
                base.CleanUp();
            }

            // internal routine to encrypt a block in CBC mode
            public long EncryptBlockCBC(long lPlainblock)
            {
                // chain with the CBC IV
                lPlainblock ^= m_lCBCIV;

                // encrypt the block
                lPlainblock = base.EncryptBlock(lPlainblock);

                // the encrypted block is the new CBC IV
                return (m_lCBCIV = lPlainblock);
            }

            // internal routine to decrypt a block in CBC mode
            public long DecryptBlockCBC(long lCipherblock)
            {
                // save the current block
                long lTemp = lCipherblock;

                // decrypt the block
                lCipherblock = base.DecryptBlock(lCipherblock);

                // dechain the block
                lCipherblock ^= m_lCBCIV;

                // set the new CBC IV
                m_lCBCIV = lTemp;

                // return the decrypted block
                return lCipherblock;
            }

            /// <summary>
            /// encrypts a byte buffer (should be aligned to an 8 byte border) to
            /// another buffer (of the same size or bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with plaintext data</param>
            /// <param name="outbuffer">buffer to get the ciphertext data</param>
            public override void Encrypt(byte[] inbuffer, byte[] outbuffer)
            {
                int nLen = inbuffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 8)
                {
                    // encrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.ByteArrayToLong(inbuffer, nI);
                    lTemp = EncryptBlockCBC(lTemp);
                    DynamicEncryptionHelper.LongToByteArray(lTemp, outbuffer, nI);
                }
            }

            /// <summary>
            /// encrypts a byte buffer (should be aligned to an 8 byte border) to
            /// itself
            /// </summary>
            ///
            /// <param name="buffer">buffer to encrypt</param>
            public override void Encrypt(byte[] buffer)
            {

                int nLen = buffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 8)
                {
                    // encrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.ByteArrayToLong(buffer, nI);
                    lTemp = EncryptBlockCBC(lTemp);
                    DynamicEncryptionHelper.LongToByteArray(lTemp, buffer, nI);
                }
            }

            /// <summary>
            /// encrypts an int buffer (should be aligned to an two integer border)
            /// to another int buffer (of the same size or bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with plaintext data</param>
            /// <param name="outbuffer">buffer to get the ciphertext data</param>
            public override void Encrypt(int[] inbuffer, int[] outbuffer)
            {
                int nLen = inbuffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 2)
                {
                    // encrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.IntArrayToLong(inbuffer, nI);
                    lTemp = EncryptBlockCBC(lTemp);
                    DynamicEncryptionHelper.LongToIntArray(lTemp, outbuffer, nI);
                }
            }

            /// <summary>
            /// encrypts an integer buffer (should be aligned to an
            /// </summary>
            ///
            /// <param name="buffer">buffer to encrypt</param>
            public override void Encrypt(int[] buffer)
            {
                int nLen = buffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 2)
                {
                    // encrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.IntArrayToLong(buffer, nI);
                    lTemp = EncryptBlockCBC(lTemp);
                    DynamicEncryptionHelper.LongToIntArray(lTemp, buffer, nI);
                }
            }

            /// <summary>
            /// encrypts a long buffer to another long buffer (of the same size or
            /// bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with plaintext data</param>
            /// <param name="outbuffer">buffer to get the ciphertext data</param>
            public override void Encrypt(long[] inbuffer, long[] outbuffer)
            {
                int nLen = inbuffer.Length;
                for (int nI = 0; nI < nLen; nI++)
                {
                    outbuffer[nI] = EncryptBlockCBC(inbuffer[nI]);
                }
            }

            /// <summary>
            /// encrypts a long buffer to itself
            /// </summary>
            ///
            /// <param name="buffer">buffer to encrypt</param>
            public override void Encrypt(long[] buffer)
            {
                int nLen = buffer.Length;
                for (int nI = 0; nI < nLen; nI++)
                {
                    buffer[nI] = EncryptBlockCBC(buffer[nI]);
                }
            }

            /// <summary>
            /// decrypts a byte buffer (should be aligned to an 8 byte border) to
            /// another buffer (of the same size or bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with ciphertext data</param>
            /// <param name="outbuffer">buffer to get the plaintext data</param>
            public override void Decrypt(byte[] inbuffer, byte[] outbuffer)
            {
                int nLen = inbuffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 8)
                {
                    // decrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.ByteArrayToLong(inbuffer, nI);
                    lTemp = DecryptBlockCBC(lTemp);
                    DynamicEncryptionHelper.LongToByteArray(lTemp, outbuffer, nI);
                }
            }

            /// <summary>
            /// decrypts a byte buffer (should be aligned to an 8 byte border) to
            /// itself
            /// </summary>
            ///
            /// <param name="buffer">buffer to decrypt</param>
            public override void Decrypt(byte[] buffer)
            {
                int nLen = buffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 8)
                {
                    // decrypt over a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.ByteArrayToLong(buffer, nI);
                    lTemp = DecryptBlockCBC(lTemp);
                    DynamicEncryptionHelper.LongToByteArray(lTemp, buffer, nI);
                }
            }

            /// <summary>
            /// decrypts an integer buffer (should be aligned to an two integer
            /// border) to another int buffer (of the same size or bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with ciphertext data</param>
            /// <param name="outbuffer">buffer to get the plaintext data</param>
            public override void Decrypt(int[] inbuffer, int[] outbuffer)
            {

                int nLen = inbuffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 2)
                {
                    // decrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.IntArrayToLong(inbuffer, nI);
                    lTemp = DecryptBlockCBC(lTemp);
                    DynamicEncryptionHelper.LongToIntArray(lTemp, outbuffer, nI);
                }
            }

            /// <summary>
            /// decrypts an int buffer (should be aligned to a two integer border)
            /// </summary>
            ///
            /// <param name="buffer">buffer to decrypt</param>
            public override void Decrypt(int[] buffer)
            {
                int nLen = buffer.Length;
                long lTemp;
                for (int nI = 0; nI < nLen; nI += 2)
                {
                    // decrypt a temporary 64bit block
                    lTemp = DynamicEncryptionHelper.IntArrayToLong(buffer, nI);
                    lTemp = DecryptBlockCBC(lTemp);
                    DynamicEncryptionHelper.LongToIntArray(lTemp, buffer, nI);
                }
            }

            /// <summary>
            /// decrypts a long buffer to another long buffer (of the same size or
            /// bigger)
            /// </summary>
            ///
            /// <param name="inbuffer">buffer with ciphertext data</param>
            /// <param name="outbuffer">buffer to get the plaintext data</param>
            public override void Decrypt(long[] inbuffer, long[] outbuffer)
            {
                int nLen = inbuffer.Length;
                for (int nI = 0; nI < nLen; nI++)
                {
                    outbuffer[nI] = DecryptBlockCBC(inbuffer[nI]);
                }
            }

            /// <summary>
            /// decrypts a long buffer to itself
            /// </summary>
            ///
            /// <param name="buffer">buffer to decrypt</param>
            public override void Decrypt(long[] buffer)
            {
                int nLen = buffer.Length;
                for (int nI = 0; nI < nLen; nI++)
                {
                    buffer[nI] = DecryptBlockCBC(buffer[nI]);
                }
            }

        }

        /// <summary>
        /// gets bytes from an array into a long
        /// </summary>
        ///
        /// <param name="buffer">where to get the bytes</param>
        /// <param name="nStartIndex">index from where to read the data</param>
        /// <returns>the 64bit integer</returns>
        private static long ByteArrayToLong(byte[] buffer, int nStartIndex)
        {
            return (((long)buffer[nStartIndex]) << 56)
                    | (((long)buffer[nStartIndex + 1] & 0x0ffL) << 48)
                    | (((long)buffer[nStartIndex + 2] & 0x0ffL) << 40)
                    | (((long)buffer[nStartIndex + 3] & 0x0ffL) << 32)
                    | (((long)buffer[nStartIndex + 4] & 0x0ffL) << 24)
                    | (((long)buffer[nStartIndex + 5] & 0x0ffL) << 16)
                    | (((long)buffer[nStartIndex + 6] & 0x0ffL) << 8)
                    | ((long)buffer[nStartIndex + 7] & 0x0ff);
        }

        /// <summary>
        /// converts a long o bytes which are put into a given array
        /// </summary>
        ///
        /// <param name="lValue">the 64bit integer to convert</param>
        /// <param name="buffer">the target buffer</param>
        /// <param name="nStartIndex">where to place the bytes in the buffer</param>
        private static void LongToByteArray(long lValue, byte[] buffer,
                int nStartIndex)
        {
            buffer[nStartIndex] = (byte)((long)(((ulong)lValue) >> 56));
            buffer[nStartIndex + 1] = (byte)(((long)(((ulong)lValue) >> 48)) & 0x0ff);
            buffer[nStartIndex + 2] = (byte)(((long)(((ulong)lValue) >> 40)) & 0x0ff);
            buffer[nStartIndex + 3] = (byte)(((long)(((ulong)lValue) >> 32)) & 0x0ff);
            buffer[nStartIndex + 4] = (byte)(((long)(((ulong)lValue) >> 24)) & 0x0ff);
            buffer[nStartIndex + 5] = (byte)(((long)(((ulong)lValue) >> 16)) & 0x0ff);
            buffer[nStartIndex + 6] = (byte)(((long)(((ulong)lValue) >> 8)) & 0x0ff);
            buffer[nStartIndex + 7] = (byte)lValue;
        }

        /// <summary>
        /// converts values from an integer array to a long
        /// </summary>
        ///
        /// <param name="buffer">where to get the bytes</param>
        /// <param name="nStartIndex">index from where to read the data</param>
        /// <returns>the 64bit integer</returns>
        private static long IntArrayToLong(int[] buffer, int nStartIndex)
        {
            return (((long)buffer[nStartIndex]) << 32)
                    | (((long)buffer[nStartIndex + 1]) & 0x0ffffffffL);
        }

        /// <summary>
        /// converts a long to integers which are put into a given array
        /// </summary>
        ///
        /// <param name="lValue">the 64bit integer to convert</param>
        /// <param name="buffer">the target buffer</param>
        /// <param name="nStartIndex">where to place the bytes in the buffer</param>
        private static void LongToIntArray(long lValue, int[] buffer,
                int nStartIndex)
        {
            buffer[nStartIndex] = (int)((long)(((ulong)lValue) >> 32));
            buffer[nStartIndex + 1] = (int)lValue;
        }

        /// <summary>
        /// makes a long from two integers (treated unsigned)
        /// </summary>
        ///
        /// <param name="nLo">lower 32bits</param>
        /// <param name="nHi">higher 32bits</param>
        /// <returns>the built long</returns>
        private static long MakeLong(int nLo, int nHi)
        {
            return (((long)nHi << 32) | ((long)nLo & 0x00000000ffffffffL));
        }

        /// <summary>
        /// gets the lower 32 bits of a long
        /// </summary>
        ///
        /// <param name="lVal">the long integer</param>
        /// <returns>lower 32 bits</returns>
        private static int LongLo32(long lVal)
        {
            return (int)lVal;
        }

        /// <summary>
        /// gets the higher 32 bits of a long
        /// </summary>
        ///
        /// <param name="lVal">the long integer</param>
        /// <returns>higher 32 bits</returns>
        private static int LongHi32(long lVal)
        {
            return (int)(((long)(((ulong)lVal) >> 32)));
        }

        // our table for binhex conversion
        static internal readonly char[] HEXTAB = { '0', '1', '2', '3', '4', '5', '6', '7', '8',
            '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        /// <summary>
        /// converts a byte array to a binhex string
        /// </summary>
        ///
        /// <param name="data">the byte array</param>
        /// <param name="nStartPos">start index where to get the bytes</param>
        /// <param name="nNumOfBytes">number of bytes to convert</param>
        /// <returns>the binhex string</returns>
        private static String BytesToBinHex(byte[] data, int nStartPos,
                int nNumOfBytes)
        {
            StringBuilder sbuf = new StringBuilder();
            sbuf.Length = nNumOfBytes << 1;

            int nPos = 0;
            for (int nI = 0; nI < nNumOfBytes; nI++)
            {
                sbuf[nPos++] = HEXTAB[(data[nI + nStartPos] >> 4) & 0x0f];
                sbuf[nPos++] = HEXTAB[data[nI + nStartPos] & 0x0f];
            }
            return sbuf.ToString();
        }

        /// <summary>
        /// converts a binhex string back into a byte array (invalid codes will be
        /// skipped)
        /// </summary>
        ///
        /// <param name="sBinHex">binhex string</param>
        /// <param name="data">the target array</param>
        /// <param name="nSrcPos">normally</param>
        /// <param name="nDstPos">to store the bytes from which position in the array</param>
        /// <param name="nNumOfBytes">number of bytes to extract</param>
        /// <returns>number of extracted bytes</returns>
        private static int BinHexToBytes(String sBinHex, byte[] data, int nSrcPos,
                int nDstPos, int nNumOfBytes)
        {
            // check for correct ranges
            int nStrLen = sBinHex.Length;

            int nAvailBytes = (nStrLen - nSrcPos) >> 1;
            if (nAvailBytes < nNumOfBytes)
            {
                nNumOfBytes = nAvailBytes;
            }

            int nOutputCapacity = data.Length - nDstPos;
            if (nNumOfBytes > nOutputCapacity)
            {
                nNumOfBytes = nOutputCapacity;
            }

            // convert now
            int nResult = 0;
            for (int nI = 0; nI < nNumOfBytes; nI++)
            {
                byte bActByte = 0;
                bool blConvertOK = true;
                for (int nJ = 0; nJ < 2; nJ++)
                {
                    bActByte <<= 4;
                    char cActChar = sBinHex[nSrcPos++];

                    if ((cActChar >= 'a') && (cActChar <= 'f'))
                    {
                        bActByte |= (byte)((cActChar - 'a') + 10);
                    }
                    else
                    {
                        if ((cActChar >= '0') && (cActChar <= '9'))
                        {
                            bActByte |= (byte)(cActChar - '0');
                        }
                        else
                        {
                            blConvertOK = false;
                        }
                    }
                }
                if (blConvertOK)
                {
                    data[nDstPos++] = bActByte;
                    nResult++;
                }
            }

            return nResult;
        }

        /// <summary>
        /// converts a byte array into an UNICODE string
        /// </summary>
        ///
        /// <param name="data">the byte array</param>
        /// <param name="nStartPos">where to begin the conversion</param>
        /// <param name="nNumOfBytes">number of bytes to handle</param>
        /// <returns>the string</returns>
        private static String ByteArrayToUNCString(byte[] data, int nStartPos,
                int nNumOfBytes)
        {
            // we need two bytes for every character
            nNumOfBytes &= ~1;

            // enough bytes in the buffer?
            int nAvailCapacity = data.Length - nStartPos;

            if (nAvailCapacity < nNumOfBytes)
            {
                nNumOfBytes = nAvailCapacity;
            }

            StringBuilder sbuf = new StringBuilder();
            sbuf.Length = nNumOfBytes >> 1;

            int nSBufPos = 0;

            while (nNumOfBytes > 0)
            {
                sbuf[nSBufPos++] = (char)(((int)data[nStartPos] << 8) | ((int)data[nStartPos + 1] & 0x0ff));
                nStartPos += 2;
                nNumOfBytes -= 2;
            }

            return sbuf.ToString();
        }
    }
}