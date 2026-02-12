// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using HashidsNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTShang.Core.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class HashidsHelper
    {
        private Hashids _hashids;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashids"></param>
        public HashidsHelper(Hashids hashids)
        {
            _hashids = hashids;
        }

        /// <summary>
        /// 创建Hashids
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="minHashLength"></param>
        /// <param name="alphabet"></param>
        /// <param name="seps"></param>
        /// <returns></returns>
        private static Hashids Create(string salt, int minHashLength, string alphabet, string seps)
        {
            var Hashids = new Hashids(salt, minHashLength, alphabet, seps);
            return Hashids;
        }
        /// <summary>
        /// 创建HashidsHelper
        /// </summary>
        /// <param name="salt"></param>
        /// <param name="minHashLength"></param>
        /// <param name="alphabet"></param>
        /// <param name="seps"></param>
        /// <returns></returns>
        public static HashidsHelper CreateHelper(string salt = "Gardener", int minHashLength = 0, string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", string seps = "cfhistuCFHISTU")
        {
            return new HashidsHelper(Create(salt,minHashLength,alphabet,seps));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string Encode(int number)
        {
            return _hashids.Encode(number);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string EncodeLong(long number)
        {
            return _hashids.EncodeLong(number);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public int[] Encode(string hash)
        {
            return _hashids.Decode(hash);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public long[] EncodeLong(string hash)
        {
            return _hashids.DecodeLong(hash);
        }
    }
}
